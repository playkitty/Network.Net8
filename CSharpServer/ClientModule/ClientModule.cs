using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientModule
{
    public abstract class ClientBaseModule : TaskSyncer
    {
        public Socket Socket;
        public SocketAsyncEventArgs SocketAsyncEvent = new SocketAsyncEventArgs();
        public ConcurrentQueue<byte[]> BufferQueue = new ConcurrentQueue<byte[]>();
        private Dictionary<Type, MethodInfo> PacketMethods;
        private Dictionary<int, Type> PacketTypes = new Dictionary<int, Type>();
        private IClientPacketReceiver Receiver;// = new PacketReceiver();
        private int packetHeaderSize = 0;

        private byte[] receiveBuffer = null;
        private int receiveBufferSize = 1024;

        //int count = 0;
        //int callCount = 0;

        public ClientBaseModule()
        {
            //this.packetHeaderSize = PacketHeader.GetHeaderSize();
        }

        public int ReceiveBufferSize { get => this.receiveBufferSize; }

        public bool Init(int packeHeaderSize, int receiveBufferSize, IClientPacketReceiver receiver, 
            Dictionary<Type, MethodInfo> packetMethods, Dictionary<int, Type> packetTypes)
        {
            this.packetHeaderSize = packeHeaderSize;
            this.Receiver = receiver;
            this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.receiveBufferSize = receiveBufferSize;
            receiveBuffer = new byte[this.receiveBufferSize];

            this.PacketMethods = packetMethods;
            this.PacketTypes = packetTypes;

            return true;
        }

        public void RecieveBandWidth(int receiveBufferSize)
        {
            this.receiveBufferSize = receiveBufferSize;
            receiveBuffer = new byte[this.receiveBufferSize];
        }

        public void Connect(String ip, int port)
        {
            var socketAsyncEventArgs = new SocketAsyncEventArgs();
            socketAsyncEventArgs.RemoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            socketAsyncEventArgs.UserToken = this.Socket;
            socketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Connected);
            //socketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Connected);

            this.Socket.ConnectAsync(socketAsyncEventArgs);
        }

        public void IO_Received(object sender, SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                //increment the count of the total bytes receive by the server
                //Interlocked.Add(ref m_totalBytesRead, e.BytesTransferred);
                //Console.WriteLine("The server has read a total of {0} bytes", m_totalBytesRead);
                byte[] szData = new byte[e.BytesTransferred];
                Array.Copy(e.Buffer, e.Offset, szData, 0, e.BytesTransferred);

                //string sData = Encoding.UTF8.GetString(szData);
                //Console.WriteLine("received {0}", sData);
                //Console.WriteLine("count {0}", ++count);
                this.BufferQueue.Enqueue(szData);

                //Console.WriteLine("count {0}", ++count);
                // 분해자한테 보난다음에 분해 tasksyncker로 분해해서 유저에게 나르자 (X)
                //Thread.Sleep(1);
                Post(new TaskJob(() =>
                {
                    //Console.WriteLine("callCount {0}", ++callCount);
                    if (this.BufferQueue.TryDequeue(out var bytes) == false)
                        return;
                    //var bytes = this.BufferQueue.Dequeue();
                    //var bytes = szData;
                    var bytesSize = bytes.Count();
                    if (bytesSize < this.packetHeaderSize)
                        return;

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

                        if (size > bytes.Count())
                            break;

                        var bodyBytes = binaryReader.ReadBytes(size - this.packetHeaderSize);
                        bytesSize = bytes.Count() - size;
                        if (bytesSize > this.packetHeaderSize)
                        {
                            bytes = bytes.Skip(size).ToArray();
                        }

                        using (MemoryStream ms = new MemoryStream(bodyBytes))
                        {
                            if (this.PacketTypes.TryGetValue(id, out var type) == false)
                            {
                                return;
                            }

                            var info = ProtoBuf.Serializer.Deserialize(type, ms);
                            if (this.PacketMethods.TryGetValue(info.GetType(), out var method) == false)
                            {
                                return;
                            }

                            method.Invoke(this.Receiver, new object[] { this, info });
                        }
                    }
                }));
                

                this.SocketAsyncEvent.SetBuffer(receiveBuffer, 0, this.receiveBufferSize);
                this.Socket.ReceiveAsync(this.SocketAsyncEvent);
            }
            else
            {
                // disconnect
            }
        }

        public void IO_Connected(object sender, SocketAsyncEventArgs e)
        {
            if (this.Socket.Connected && e.SocketError == SocketError.Success)
            {
                // 연결 성공
                Console.WriteLine("connected success");
                this.SocketAsyncEvent.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Received);
                this.SocketAsyncEvent.SetBuffer(this.receiveBuffer, 0, this.receiveBufferSize);
                this.SocketAsyncEvent.UserToken = this.Socket;
                this.Socket.ReceiveAsync(this.SocketAsyncEvent);

                this.ConnectSuccess();
            }
        }

        //override
        protected abstract void ConnectSuccess();
        public abstract void ProcessPacket(int sequence, object obj);
    }

}
