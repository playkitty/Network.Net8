using NOBAgentClient;
using Protocol;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.OleDb;
using System.ServiceProcess;
using System.Management;

namespace CSharpGUITest
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        /// 
        /// 
        [STAThread]
        static void Main()
        {
            //ManagementClass
            //var scc = new ServiceController("NateOnBiz Admin", "SKDEV-NOBSTAGE");
            //var scService = ServiceController.GetServices();

            //OleDbEnumerator oe = new OleDbEnumerator();
            //var dt = oe.GetElements();
            //for(int i = 0; i < dt.Rows.Count; ++i)
            //{
            //    Console.WriteLine("Provider: {0}", dt.Rows[i][0].ToString());
            //}

            //ConnectionOptions option = new ConnectionOptions();
            //option.Username = "";
            //option.Password = "";
            try
            {
                ConnectionOptions option = new ConnectionOptions();
                //option.
                option.Username = "Administrator";
                option.Password = "";
                //var str = String.Format("Win32_Service", "SKCC16D00567");
                ManagementPath path = new ManagementPath("Win32_Process");
                //ManagementScope scope = new ManagementScope(path, option);
                //scope.Connect();
                //ServiceController sc = new ServiceController("NateOnBiz Admin", "SKDEV-NOBSTAGE.skdev.com");
                var scService = ServiceController.GetServices();

                //String
                //String compare = "graf";
                //char[] compareWord = "graf";
                //var compareWord = "Graf".ToCharArray();
                //var list = scService.Where(e => e.ServiceName.IndexOfAny(compareWord) != -1).Select(k => k.ServiceName).ToList();
                var list2 = scService.Where(e => e.ServiceName.IndexOf("Graf") != -1).Select(k => k.ServiceName).ToList();
                
                //var list = scService.ToList();
                ServiceController sc = new ServiceController("Grafana", "SKCC16D00567");
                var status = sc.Status;
                //sc.Stop();
                //sc.Start();

            }
            catch(System.TimeoutException exptimeout)
            {

            }
            catch(Exception e)
            {

            }
            

          

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainController.Inst.Init();

            Application.Run(MainController.Inst.GUIController.form);
        }
    }
}
