using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceRestarter
{
    public class Config
    {
        public class ExecutionSchedule
        {
            public int time { get; set; }
            public List<String> executions { get; set; }
        }
        public bool ServiceMode { get; set; }
        public List<String> ServiceNames { get; set; }
        public int ServiceCheckTime { get; set; }
        public ExecutionSchedule ExeSchedule { get; set; }


    }
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        static void Main()
        {
            var configFileName = ConfigurationManager.AppSettings.Get("ConfigFileName");
            var path = AppDomain.CurrentDomain.BaseDirectory + configFileName;

            NLog.LogManager.GetLogger("name").Debug("{0}", path);
            var config = JsonConfigLoader.LoadConfig<Config>(path);
            if (config == null)
            {
                NLog.LogManager.GetLogger("name").Debug("config loaded fail");
                return;
            }

            NLog.LogManager.GetLogger("name").Debug("service mode {0}", config.ServiceMode);

            //if (config.ServiceMode == false)
            //{
            //    var ser = new Service(config);
            //    ser.OnStart();
            //    while (true)
            //    {
            //        Thread.Sleep(1000);
            //    }
            //}
            //else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new Service(config)
                };
                ServiceBase.Run(ServicesToRun);
            }


            
        }
    }
}
