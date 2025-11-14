// Implements the connection logic for the socket server.
// After accepting a connection, all data read from the client
// is sent back to the client. The read and echo back to the client pattern
// is continued until the client disconnects.
//using Google.Protobuf;
using Protocol;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Common;
using System.Collections.Concurrent;

namespace ServerModule
{
    public class ServerBaseModule
    {
        private int connectionPoolSize;   // the maximum number of connections the sample is designed to handle simultaneously
        //private int m_receiveBufferSize;// buffer size to use for each socket I/O operation
        //BufferManager m_bufferManager;  // represents a large reusable set of buffers for all socket operations
        //const int opsToPreAlloc = 2;    // read, write (don't alloc buffer space for accepts)
        private Socket listenSocket;            // the socket used to listen for incoming connection requests
                                        // pool of reusable SocketAsyncEventArgs objects for write, read and accept socket operations
        //SocketAsyncEventArgsPool m_readWritePool;
        Stack<User> m_Pool = new Stack<User>();
        int m_totalBytesRead;           // counter of the total # bytes received by the server
        int m_numConnectedSockets;      // the total number of clients connected to the server
        //Semaphore m_maxNumberAcceptedClients;

        //private IPacketReceiver Receiver;
        //private Dictionary<Type, MethodInfo> PacketMethods;
        //private Dictionary<int, Type> PacketTypes;
        private IPacketInvoker PacketInvoker;
        private readonly int packetHeaderSize = 0;

        //private List<User> Users = new List<User>();
        private ConcurrentDictionary<int, User> Users = new ConcurrentDictionary<int, User>();
        protected int receiveBufferSize = 0;
        private int userSeed = 0;

        private IConfig config;
        //private ServerBaseModule me = null;

        //public ServerBaseModule Me { get => this.me; }
        public IConfig Config { get => this.config; }

        public User Get(int index)
        {
            if (this.Users.TryGetValue(index, out var user) == false)
                return null;

            return user;
        }
        // Create an uninitialized server instance.
        // To start the server listening for connection requests
        // call the Init method followed by Start method
        //
        // <param name="numConnections">the maximum number of connections the sample is designed to handle simultaneously</param>
        // <param name="receiveBufferSize">buffer size to use for each socket I/O operation</param>
        public ServerBaseModule()
        {
            this.packetHeaderSize = PacketHeader.GetHeaderSize();
            //this.me = this;
        }

        // Initializes the server by preallocating reusable buffers and
        // context objects.  These objects do not need to be preallocated
        // or reused, but it is done this way to illustrate how the API can
        // easily be used to create reusable objects to increase server performance.
        //
        //public bool Init(int connectionPoolSize, int receiveBufferSize, IPacketReceiver receiver, Dictionary<Type, MethodInfo> packetMethods, Dictionary<int, Type> packetTypes
            //, Type userType, IConfig config)
        public bool Init(int connectionPoolSize, int receiveBufferSize, IPacketInvoker packetInvoker, Type userType, IConfig config)
        {
            m_totalBytesRead = 0;
            m_numConnectedSockets = 0;
            this.connectionPoolSize = connectionPoolSize;
            //this.packetHeaderSize = receiveBufferSize;
            // allocate buffers such that the maximum number of sockets can have one outstanding read and
            //write posted to the socket simultaneously
            //m_bufferManager = new BufferManager(receiveBufferSize * numConnections * opsToPreAlloc,
                //receiveBufferSize);

            //m_readWritePool = new SocketAsyncEventArgsPool(numConnections);
            //m_maxNumberAcceptedClients = new Semaphore(numConnections, numConnections);
            //this.Receiver = receiver;
            //this.PacketMethods = packetMethods;
            //this.PacketTypes = packetTypes;
            this.PacketInvoker = packetInvoker;
            this.receiveBufferSize = receiveBufferSize;
            this.config = config;
            // Allocates one large byte buffer which all I/O operations use a piece of.  This gaurds
            // against memory fragmentation
            //m_bufferManager.InitBuffer();

            for (int i = 0; i < this.connectionPoolSize; i++)
            {
                var user = (User)Activator.CreateInstance(userType);
                user.Init(this);
                //user.ReceiveBufferSize = receiveBufferSize;
                user.eventComplated = new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                user.recvEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                user.recvEventArgs.UserToken = user;

                user.disconnectEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                user.disconnectEventArgs.UserToken = user;

                user.SetBuffer();
                m_Pool.Push(user);
            }

            return true;
        }

        // Starts the server such that it is listening for
        // incoming connection requests.
        //
        // <param name="localEndPoint">The endpoint which the server will listening
        // for connection requests on</param>
        protected void Start(IPEndPoint localEndPoint)
        {
            // create the socket which listens for incoming connections
            listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(localEndPoint);
            // start the server with a listen backlog of 100 connections
            listenSocket.Listen(100);

            // post accepts on the listening socket
            SocketAsyncEventArgs acceptEventArg = new SocketAsyncEventArgs();
            acceptEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptEventArg_Completed);
            StartAccept(acceptEventArg);

            //Console.WriteLine("{0} connected sockets with one outstanding receive posted to each....press any key", m_outstandingReadCount);
            Console.WriteLine("Press any key to terminate the server process....");
            //Console.ReadKey();
        }

        // Begins an operation to accept a connection request from the client
        //
        // <param name="acceptEventArg">The context object to use when issuing
        // the accept operation on the server's listening socket</param>
        private void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            // loop while the method completes synchronously
            bool willRaiseEvent = false;
            while (!willRaiseEvent)
            {
                //m_maxNumberAcceptedClients.WaitOne();

                // socket must be cleared since the context object is being reused
                acceptEventArg.AcceptSocket = null;
                willRaiseEvent = listenSocket.AcceptAsync(acceptEventArg);
                if (!willRaiseEvent)
                {
                    ProcessAccept(acceptEventArg);
                }
            }
        }

        // This method is the callback method associated with Socket.AcceptAsync
        // operations and is invoked when an accept operation is complete
        //
        void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);

            // Accept the next connection request
            StartAccept(e);
        }

        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            Interlocked.Increment(ref m_numConnectedSockets);
            Console.WriteLine("Client connection accepted. There are {0} clients connected to the server",
                m_numConnectedSockets);

            // Get the socket for the accepted client connection and put it into the
            //ReadEventArg object user token

            // 풀사이즈 클때
            var user = m_Pool.Pop();
            user.Socket = e.AcceptSocket;
            user.Closed = 0;

            //user.index = this.Users.Count();
            user.index = userSeed++;
            this.Users.TryAdd(user.index, user);

            // As soon as the client is connected, post a receive to the connection
            bool willRaiseEvent = e.AcceptSocket.ReceiveAsync(user.recvEventArgs);
            if (!willRaiseEvent)
            {
                ProcessReceive(user.recvEventArgs);
            }
        }

        // This method is called whenever a receive or send operation is completed on a socket
        //
        // <param name="e">SocketAsyncEventArg associated with the completed receive operation</param>
        void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            // determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                case SocketAsyncOperation.Disconnect:
                    ProcessClose(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }

        // This method is invoked when an asynchronous receive operation completes.
        // If the remote host closed the connection, then the socket is closed.
        // If data was received then the data is echoed back to the client.
        //
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            // check if the remote host closed the connection
            //AsyncUserToken token = (AsyncUserToken)e.UserToken;
            User user = e.UserToken as User;
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                //increment the count of the total bytes receive by the server
                Interlocked.Add(ref m_totalBytesRead, e.BytesTransferred);

                byte[] szData = new byte[e.BytesTransferred];
                Array.Copy(e.Buffer, e.Offset, szData, 0, e.BytesTransferred);
                user.bytes.Enqueue(szData);

                // 분해자한테 보난다음에 분해 tasksyncker로 분해해서 유저에게 나르자 (X)
                user.Post(new TaskJob(() =>
                {
                    if (user.bytes.TryDequeue(out var bytes) == false)
                    {
                        return;
                    }

                    var bytesSize = bytes.Count();
                    // 사이즈 작으면 잘라야함
                    // 8 is headersize
                    while (bytesSize >= this.packetHeaderSize)
                    {
                        BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
                        binaryWriter.Write(bytes);
                        BinaryReader binaryReader = new BinaryReader(binaryWriter.BaseStream);
                        binaryWriter.BaseStream.Position = 0;
                        ////
                        var id = BitConverter.ToInt32(binaryReader.ReadBytes(4), 0);
                        var size = BitConverter.ToInt32(binaryReader.ReadBytes(4), 0);
                        //var sequence = BitConverter.ToInt32(binaryReader.ReadBytes(8), 0);

                        if (size > bytes.Count())
                        {
                            //user.Socket.Shutdown(SocketShutdown.Both);
                            //user.Socket.Close();
                            //CloseClientSocket(e);
                            //user.Socket.Shutdown(SocketShutdown.Both);
                            //user.Socket.DisconnectAsync(user.disconnectEventArgs);
                            //user.Reset();
                            user.Close();
                            return;
                        }

                        var readSize = size - this.packetHeaderSize;
                        if (readSize < 0)
                        {
                            user.Close();
                            return;
                        }

                        var bodyBytes = binaryReader.ReadBytes(readSize);
                        bytesSize = bytes.Count() - size;
                        if (bytesSize >= this.packetHeaderSize)
                        {
                            bytes = bytes.Skip(size).ToArray();
                        }

                        using (MemoryStream dms = new MemoryStream(bodyBytes))
                        {
                            if (this.PacketInvoker.Deserialize(id, dms, user) == false)
                            {
                                return;
                            }

                            //if (this.PacketTypes.TryGetValue(id, out var type) == false)
                            //{
                            //    return;
                            //}

                            //var info = ProtoBuf.Serializer.Deserialize(type, dms);
                            //if (this.PacketMethods.TryGetValue(info.GetType(), out var method) == false)
                            //{
                            //    return;
                            //}

                            //method.Invoke(this.Receiver, new object[] { user, info });
                        }
                    }
                    //}
                }));

                //e.SetBuffer(e.Offset, m_receiveBufferSize);
                user.SetBuffer();

                if (user.Socket.Connected && user.Closed == 0)
                    user.Socket.ReceiveAsync(e);
            }
            else
            {
                user.Close();
                //CloseClientSocket(e);
            }
        }

        // This method is invoked when an asynchronous send operation completes.
        // The method issues another receive on the socket to read any additional
        // data sent from the client
        //
        // <param name="e"></param>
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // done echoing data back to the client
                User user = e.UserToken as User;
            }
            else
            {
                CloseClientSocket(e);
            }
        }

        private void ProcessClose(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // done echoing data back to the client
                //User user = e.UserToken as User;
                CloseClientSocket(e);
            }
            else
            {
                CloseClientSocket(e);
            }
        }

        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            User token = e.UserToken as User;

            // close the socket associated with the client
            try
            {
                token.Socket.Shutdown(SocketShutdown.Both);
            }
            // throws if client process has already closed
            catch (Exception)
            {
                Console.WriteLine("CloseClientSocket Exception");
            }

            token.Socket.Close();

            token.OnClosed();
            //this.Users.RemoveAt(token.index);
            this.Users.TryRemove(token.index, out var user);
            // decrement the counter keeping track of the total number of clients connected to the server
            Interlocked.Decrement(ref m_numConnectedSockets);

            // Free the SocketAsyncEventArg so they can be reused by another client
            m_Pool.Push(token);
            //m_readWritePool.Push(e);

            //m_maxNumberAcceptedClients.Release();
            Console.WriteLine("A client has been disconnected from the server. There are {0} clients connected to the server", m_numConnectedSockets);
        }
    }
}