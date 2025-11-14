using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using Protocol;
using ServerModule;

namespace AgentServerModule
{
    public sealed class LLMServer : ServerBaseModule
    {
        LLMServerConfig config = null;
        public LLMServer()
            : base()
        {
        }


        public bool Init(LLMServerConfig config)
        {
            this.config = config;

            
            //packetInvoker.ini
            var methods = typeof(LLMPacketReceiver).GetRuntimeMethods().Where(e => e.GetParameters().Length == 3
            && e.GetParameters()[0].ParameterType == typeof(LLMUser)
            && e.GetParameters()[1].ParameterType.IsDefined(typeof(PacketAttribute))).ToDictionary(e => e.GetParameters()[1].ParameterType);

            var packets = Assembly.GetAssembly(typeof(PacketHeader)).GetTypes().Where(e => e.IsDefined(typeof(PacketAttribute)));

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

                if (packetMode == ePacketMode.ReceiveLLMServer)
                {
                    packetTypes.Add(Convert.ToInt32(packetId), packet);
                }
            }

            var packetInvoker = new BytesPacketInvoker();
            packetInvoker.Init(methods, packetTypes, new LLMPacketReceiver());

            return base.Init(this.config.connectionPoolSize, this.config.receiveBandWidth,
                packetInvoker, typeof(LLMUser), config);
            //return base.Init(numConnections, receiveBufferSize, null, methods, packetTypes, typeof(AgentUser));
        }

        public void Start()
        {
            IPEndPoint ep2 = new IPEndPoint(IPAddress.Parse(this.config.ip), this.config.port);
            //IPEndPoint ep2 = new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(port));
            this.Start(ep2);
        }
    }
}
