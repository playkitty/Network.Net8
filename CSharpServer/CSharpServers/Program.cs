using AgentServerModule;
using System.Net;
using System.Threading;
using System.Configuration;
using System;
//using DBAgentServerModule;
using System.ServiceProcess;
using Common;
using System.IO;
using System.Collections.Generic;
using ServerModule;

namespace CSharpServer
{
    public class Config
    {
        public bool ServiceMode { get; set; }

        public AgentServerConfig AgentServer { get; set; }
        public LLMServerConfig LLMServer { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            NLog.Logger logger = NLog.LogManager.GetLogger("name");
            var configFileName = ConfigurationManager.AppSettings.Get("ConfigFileName");
            var path = AppDomain.CurrentDomain.BaseDirectory + configFileName;
            
            logger.Debug("{0}", path);
            var config = JsonConfigLoader.LoadConfig<Config>(path);
            
            if (config == null)
            {
                logger.Debug("config loaded fail");
                return;
            }

            logger.Debug("service mode {0}", config.ServiceMode);
            if (config.ServiceMode)
            {
                logger.Debug("servicemode start");
                ServiceBase[] ServicesToRun = new ServiceBase[] { new NOBService(config) };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                AgentServer server = new AgentServer();//(100, 4096, reciever, melist, packetTypes);
                server.Init(config.AgentServer);
                server.Start();

                LLMServer llm = new LLMServer();
                llm.Init(config.LLMServer);
                llm.Start();

                while (true)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
