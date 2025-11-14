using System;
using ServerModule;


namespace DBAgentServerModule
{
    public sealed class DBAgentServerConfig : IConfig
    {
        public String ip { get; set; }
        public int port { get; set; }
        public int receiveBandWidth { get; set; }
        public int sendBandWidth { get; set; }
        public int connectionPoolSize { get; set; }
    }

    //public class AgentServerConfig
    //{
    //    public Config AgentServer { get; set; }
    //}
}
