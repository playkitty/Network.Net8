using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBAgentModule
{
    public abstract class ClientBaseModule
    {
        public Socket Socket;
        public SocketAsyncEventArgs SocketAsyncEvent = new SocketAsyncEventArgs();
        public Queue<byte[]> BufferQueue = new Queue<byte[]>();
        private Dictionary<Type, MethodInfo> PacketMethods;
        private Dictionary<int, Type> PacketTypes = new Dictionary<int, Type>();
        private IClientPacketReceiver Receiver;// = new PacketReceiver();
        private int packetHeaderSize = 0;

        private byte[] receiveBuffer = null;
        private int receiveBufferSize = 0;

        public ClientBaseModule()
        {
            //this.packetHeaderSize = PacketHeader.GetHeaderSize();
        }

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
            //this.PacketMethods = typeof(PacketReceiver).GetRuntimeMethods()
            //    .Where(e => e.GetParameters().Length == 1 && e.GetParameters()[0].ParameterType.IsDefined(typeof(ProtoContractAttribute)))
            //    .ToDictionary(e => e.GetParameters()[0].ParameterType);


            //var packets = Assembly.GetAssembly(typeof(PacketHeader)).GetTypes().Where(e => e.IsDefined(typeof(ProtoContractAttribute)) &&
            //e.IsDefined(typeof(PacketAttribute)));

            //this.PacketTypes.Clear();
            //foreach (var packet in packets)
            //{
            //    var customAttributeTypedArguments = packet.CustomAttributes.Where(e => e.AttributeType == typeof(PacketAttribute)).Select(j => j.ConstructorArguments.Select(k => k));
            //    if (customAttributeTypedArguments.Count() < 1)
            //        continue;

            //    var arguments = customAttributeTypedArguments.ElementAt(0);
            //    if (arguments.Count() != 2)
            //        continue;

            //    var packetMode = (ePacketMode)arguments.ElementAt(0).Value;
            //    var packetId = (ePacketID)arguments.ElementAt(1).Value;

            //    if (packetMode == ePacketMode.ReceiveAgentClient)
            //    {
            //        PacketTypes.Add(packetId, packet);
            //    }
            //}
        }

        public void Connect(String ip, int port)
        {
            var socketAsyncEventArgs = new SocketAsyncEventArgs();
            socketAsyncEventArgs.RemoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            socketAsyncEventArgs.UserToken = this.Socket;
            socketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Connected);

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
                this.BufferQueue.Enqueue(szData);

                // 분해자한테 보난다음에 분해 tasksyncker로 분해해서 유저에게 나르자 (X)
                var bytes = this.BufferQueue.Dequeue();
                var bytesSize = bytes.Count();
                if (bytesSize < this.packetHeaderSize)
                    return;

                // 사이즈 작으면 잘라야함
                // 8 is headersize
                while (bytesSize > this.packetHeaderSize)
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
                        if (this.PacketTypes.TryGetValue(id, out var type) == true)
                        {
                            var info = ProtoBuf.Serializer.Deserialize(type, ms);
                            if (this.PacketMethods.TryGetValue(info.GetType(), out var method))
                            {
                                method.Invoke(this.Receiver, new object[] { this, info });
                            }
                        }
                    }
                }

                this.SocketAsyncEvent.SetBuffer(0, this.receiveBufferSize);
                this.Socket.ReceiveAsync(this.SocketAsyncEvent);
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

        //overide
        protected abstract void ConnectSuccess();
        public abstract void ProcessPacket(int sequence, object obj);
    }

}
