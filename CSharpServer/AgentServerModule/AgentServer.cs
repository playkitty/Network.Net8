using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Protocol;
using ProtoBuf;
using ServerModule;
using System.IO;
using DBAgentProtocol;
using Common;
using System.Net;

namespace AgentServerModule
{
    public sealed class AgentServer : ServerBaseModule
    {
        DBAgentClient dbAgentClient;// = new DBAgentClient(new Action(() => {  }));
        AgentServerConfig config = null;
        //private static AgentServer instance = null;
        public AgentServer()
            : base()
        {
             this.dbAgentClient = new DBAgentClient();

            ObjectController.Instance().AgentServer = this;
        }

        //public static AgentServer Instance()
        //{
        //    if (instance == null)
        //        instance = new AgentServer();

        //    return instance;
        //}

        //public int ReceiveBufferSize { get => this.receiveBufferSize; }
        //public int SendBufferSize { get => this.config.sendBandWidth; }

        public bool Init(AgentServerConfig config)
        {
            this.config = config;

            var methods = typeof(AgentPacketReceiver).GetRuntimeMethods().Where(e => e.GetParameters().Length == 2
            && e.GetParameters()[0].ParameterType == typeof(AgentUser)
            && e.GetParameters()[1].ParameterType.IsDefined(typeof(ProtoContractAttribute))).ToDictionary(e => e.GetParameters()[1].ParameterType);

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
                var packetId = (ePacketID)arguments.ElementAt(1).Value;

                if (packetMode == ePacketMode.ReceiveAgentServer)
                {
                    packetTypes.Add(Convert.ToInt32(packetId), packet);
                }
            }

            var packetInvoker = new ProtoNetPacketInvoker();
            packetInvoker.Init(methods, packetTypes, new AgentPacketReceiver());

            return base.Init(this.config.connectionPoolSize, this.config.receiveBandWidth,
                packetInvoker, typeof(AgentUser), config);
            //return base.Init(this.config.connectionPoolSize, this.config.receiveBandWidth,
            //    new AgentPacketReceiver(), methods, packetTypes, typeof(AgentUser), config);
        }

        public void Start()
        {
            IPEndPoint ep2 = new IPEndPoint(IPAddress.Parse(this.config.ip), this.config.port);
            //IPEndPoint ep2 = new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(port));
            this.Start(ep2);
        }

        public void DBClientStart(string ip, int port)
        {
            dbAgentClient.Init();

            dbAgentClient.Connect(ip, port);
        }

        //public void FirstAction()
        //{
            
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        var sequence = this.dbAgentClient.AddSequence();
        //        ProtoBuf.Serializer.Serialize<DBReqInfo>(ms, new DBReqInfo
        //        {
        //            directory = "test",
        //            sequence = sequence
        //        });

        //        this.dbAgentClient.Send(new DBPacketHeader(eDBPacketID.ReqInfo, ms.ToArray()));
        //        //this.dbAgentClient.Send<DBReqInfo>(new DBPacketHeader(eDBPacketID.ReqInfo, ms.ToArray()), sequence, e =>
        //        //{
        //        //    var resInfo = (DBResInfo)e;

        //        //    Console.WriteLine("info");
        //        //});
        //    }
        //}
    }
}
