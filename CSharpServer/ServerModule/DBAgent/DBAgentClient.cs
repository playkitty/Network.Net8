//using DBAgentModule;
using ClientModule;
using DBAgentProtocol;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerModule
{
    public sealed class DBAgentClient : ClientBaseModule
    {
        //Action action;
        int sequence = 0;
        Dictionary<int, Action<object>> resAction = new Dictionary<int, Action<object>>();
        public DBAgentClient()
        {
            //action = firstAction;
        }

        protected override void ConnectSuccess()
        {
            SendDBReqInfo();
            //if (this.action != null)
            //    this.action();
        }

        public int AddSequence()
        {
            return Interlocked.Increment(ref this.sequence);
        }

        public override void ProcessPacket(int sequence, object obj)
        {
            if (resAction.TryGetValue(sequence, out var action) == false)
                return;

            action(obj);
        }

        public bool Init()
        {
            var packetMethods = typeof(DBAgentClientReceiver).GetRuntimeMethods()
                .Where(e => e.GetParameters().Length == 2 
                && e.GetParameters()[0].ParameterType == typeof(ClientBaseModule)
                && e.GetParameters()[1].ParameterType.IsDefined(typeof(ProtoContractAttribute)))
                .ToDictionary(e => e.GetParameters()[1].ParameterType);

            var packets = Assembly.GetAssembly(typeof(DBPacketHeader)).GetTypes().Where(e => e.IsDefined(typeof(ProtoContractAttribute)) &&
            e.IsDefined(typeof(DBPacketAttribute)));

            var packetTypes = new Dictionary<int, Type>();
            //this.PacketTypes.Clear();
            foreach (var packet in packets)
            {
                var customAttributeTypedArguments = packet.CustomAttributes.Where(e => e.AttributeType == typeof(DBPacketAttribute)).Select(j => j.ConstructorArguments.Select(k => k));
                if (customAttributeTypedArguments.Count() < 1)
                    continue;

                var arguments = customAttributeTypedArguments.ElementAt(0);
                if (arguments.Count() != 2)
                    continue;

                var packetMode = (eDBPacketMode)arguments.ElementAt(0).Value;
                var packetId = (int)arguments.ElementAt(1).Value;

                if (packetMode == eDBPacketMode.ReceiveServer)
                {
                    packetTypes.Add(packetId, packet);
                }
            }

            return base.Init(DBPacketHeader.GetHeaderSize(), 4096, new DBAgentClientReceiver(), packetMethods, packetTypes);
            //return base.Init(DBPacketHeader.GetHeaderSize(), 4096, null, packetMethods, packetTypes);
        }

        public void Send(DBPacketHeader packetHeader)
        {
            BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
            binaryWriter.Write(Convert.ToInt32(packetHeader.id));
            binaryWriter.Write(packetHeader.size);
            binaryWriter.Write(packetHeader.body);

            BinaryReader binaryReader = new BinaryReader(binaryWriter.BaseStream);
            binaryReader.BaseStream.Position = 0;
            var buff = binaryReader.ReadBytes((int)binaryWriter.BaseStream.Length);
            this.Send(buff);
        }

        public void Send<T>(DBPacketHeader packetHeader, int sequence, Action<object> action)
        {
            BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
            binaryWriter.Write(Convert.ToInt32(packetHeader.id));
            binaryWriter.Write(packetHeader.size);
            binaryWriter.Write(packetHeader.body);

            BinaryReader binaryReader = new BinaryReader(binaryWriter.BaseStream);
            binaryReader.BaseStream.Position = 0;
            var buff = binaryReader.ReadBytes((int)binaryWriter.BaseStream.Length);
            this.Send(buff);

            resAction.Add(sequence, action);
        }

        private void Send(byte[] bytes)
        {
            this.Socket.Send(bytes, bytes.Count(), SocketFlags.None);
        }

        public void SendDBReqInfo()
        {

            using (MemoryStream ms = new MemoryStream())
            {
                var sequence = this.AddSequence();
                ProtoBuf.Serializer.Serialize<DBReqInfo>(ms, new DBReqInfo
                {
                    directory = "test",
                    sequence = sequence
                });

                this.Send(new DBPacketHeader(eDBPacketID.ReqInfo, ms.ToArray()));
                //this.dbAgentClient.Send<DBReqInfo>(new DBPacketHeader(eDBPacketID.ReqInfo, ms.ToArray()), sequence, e =>
                //{
                //    var resInfo = (DBResInfo)e;

                //    Console.WriteLine("info");
                //});
            }
        }
    }
}
