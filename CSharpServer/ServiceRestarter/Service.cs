using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceRestarter
{
    public partial class Service : ServiceBase
    {
        TaskSyncer timer = new TaskSyncer();
        TaskSyncer schduler = new TaskSyncer();
        Config config = null;
        public Service(Config config)
        {
            InitializeComponent();
            this.config = config;
        }
        public void OnStart()
        {
            this.TimerLoop();
            this.SchedulerTimerLoop();
        }

        protected override void OnStart(string[] args)
        {
            this.TimerLoop();
            this.SchedulerTimerLoop();
        }

        protected override void OnStop()
        {
        }

        public void SchedulerTimerLoop()
        {
            schduler.Post(new TaskJob(() =>
            {
                foreach (var exe in this.config.ExeSchedule.executions)
                {
                    if (File.Exists(exe))
                    {
                        var psi = new ProcessStartInfo
                        {
                            FileName = exe,
                            //FileName = @"cmd.exe",
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            Verb = "runas",
                            //Arguments = "/D" + exe,
                            //WindowStyle = ProcessWindowStyle.Hidden,
                            ErrorDialog = false
                        };
                        NLog.LogManager.GetLogger("name").Debug(Directory.GetCurrentDirectory() + " " + exe + " run");
                        //NLog.LogManager.GetLogger("name").Debug(System.Environment.CurrentDirectory + exe + " run");
                        //NLog.LogManager.GetLogger("name").Debug(System.Windows.Forms.Application.StartupPath + exe + "run");

                        var poc = Process.Start(psi);
                        //Process.Start(exe);
                    }
                }
                
                Thread.Sleep(this.config.ExeSchedule.time);
                this.SchedulerTimerLoop();
            }));

        }

        public void TimerLoop()
        {
            timer.Post(new TaskJob(() =>
            {
                foreach (var name in config.ServiceNames)
                {
                    var scService = ServiceController.GetServices();
                    var list = scService.Where(e => e.ServiceName == name);
                    if (list.Count() > 1 || list.Count() <= 0)
                        continue;

                    var service = list.ElementAt(0);
                    if (service.Status == ServiceControllerStatus.Stopped)
                    {
                        service.Start();
                    }
                }

                Thread.Sleep(this.config.ServiceCheckTime);
                this.TimerLoop();
            }));
        }
    }
}
