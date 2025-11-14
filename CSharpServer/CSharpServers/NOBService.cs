using AgentServerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CSharpServer
{
    public sealed class NOBService : ServiceBase
    {
        NLog.Logger logger = NLog.LogManager.GetLogger("name");
        private System.ComponentModel.IContainer components = new System.ComponentModel.Container();
        private Config config = null;
        public NOBService(Config config)
        {
            InitializeComponent();
            this.config = config;
            logger.Debug("service initialize");
        }

        protected override void OnStart(string[] args)
        {
            logger.Debug("service start");
            AgentServer server = new AgentServer();//(100, 4096, reciever, melist, packetTypes);
            server.Init(this.config.AgentServer);
            server.Start();

            LLMServer llm = new LLMServer();//(100, 4096, reciever, melist, packetTypes);
            llm.Init(this.config.LLMServer);
            llm.Start();
        }

        protected override void OnStop()
        {
            logger.Debug("service stop");
        }

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            //components = new System.ComponentModel.Container();
            this.ServiceName = "NateOnBiz AgentService";
        }
        #endregion
    }
}
