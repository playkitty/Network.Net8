using ClientModule;
using ProtoBuf;
using Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NOBAgentClient
{
    public class NetworkController : ClientBaseModule
    {
        public bool Init(int receiveBufferSize)
        {
            var packetMethods = typeof(PacketReceiver).GetRuntimeMethods()
                .Where(e => e.GetParameters().Length == 2 
                && e.GetParameters()[0].ParameterType == typeof(ClientBaseModule)
                && e.GetParameters()[1].ParameterType.IsDefined(typeof(ProtoContractAttribute)))
                .ToDictionary(e => e.GetParameters()[1].ParameterType);

            var packets = Assembly.GetAssembly(typeof(PacketHeader)).GetTypes().Where(e => e.IsDefined(typeof(ProtoContractAttribute)) &&
            e.IsDefined(typeof(PacketAttribute)));

            var packetTypes = new Dictionary<int, Type>();
            foreach (var packet in packets)
            {
                var customAttributeTypedArguments = packet.CustomAttributes.Where(e => e.AttributeType == typeof(PacketAttribute)).Select(j => j.ConstructorArguments.Select(k => k));
                if (customAttributeTypedArguments.Count() < 1)
                    continue;

                var arguments = customAttributeTypedArguments.ElementAt(0);
                if (arguments.Count() != 2)
                    continue;

                var packetMode = (ePacketMode)arguments.ElementAt(0).Value;
                var packetId = (int)arguments.ElementAt(1).Value;
                if (packetMode == ePacketMode.ReceiveAgentClient)
                {
                    packetTypes.Add(packetId, packet);
                }
            }

            return base.Init(PacketHeader.GetHeaderSize(), receiveBufferSize, new PacketReceiver(), packetMethods, packetTypes);
        }

        public void SendReqFolderInfo(String directory)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ReqFolderInfo>(ms, new ReqFolderInfo { directory = directory });

                this.Send(new PacketHeader(ePacketID.ReqFolderInfo, ms.ToArray()));
            }
        }

        public void Send(PacketHeader packetHeader)
        {
            BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
            binaryWriter.Write(Convert.ToInt32(packetHeader.id));
            binaryWriter.Write(packetHeader.size);
            binaryWriter.Write(packetHeader.body);

            BinaryReader binaryReader = new BinaryReader(binaryWriter.BaseStream);
            binaryReader.BaseStream.Position = 0;
            this.Send(binaryReader.ReadBytes((int)binaryWriter.BaseStream.Length));
            
            //Console.WriteLine("Send: {0}, {1}, {2}", packetHeader.id, packetHeader.size, packetHeader.body);
            //this.Socket.Send(bytes, bytes.Count(), SocketFlags.None);
        }

        private void Send(byte[] bytes)
        {   
            this.Socket.Send(bytes, bytes.Count(), SocketFlags.None);
        }

        protected override void ConnectSuccess()
        {
            MainController.Inst.ReqLoginInfo();

            //SendReqFolderInfo(MainController.Inst.Directory);
        }

        public override void ProcessPacket(int sequence, object obj)
        {
        }
    }
}
