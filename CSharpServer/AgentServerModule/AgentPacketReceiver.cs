using Protocol;
using ServerModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AgentServerModule
{
    public sealed class AgentPacketReceiver : IPacketReceiver
    {
        private void OnReqFolderInfo(AgentUser user, ReqLoginInfo info)
        {
            user.AgentController.ReqLoginInfo();
        }
        
        //public NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private void OnReqFolderInfo(AgentUser user, ReqFolderInfo info)
        {
            if (info.directory.Count() == 0)
            {
                //DriveInfo[] alldrive = DriveInfo.GetDrives();
                //alldrive.isready == true
            }

            user.AgentController.ReqFolderInfo(info);
            //NLog.LogManager.Configuration
            NLog.Logger logger = NLog.LogManager.GetLogger("name");
            logger.Debug("OnReqFolderInfo");
        }

        private void OnReqDuplicateCheckBegin(AgentUser user, ReqDuplicateCheckBegin info)
        {
            user.AgentController.ReqDuplicateCheckBegin();
        }

        private void OnReqDuplicateCheck(AgentUser user, ReqDuplicateCheck info)
        {
            user.AgentController.ReqDuplicateCheck(info);
        }

        private void OnReqDuplicateCheckEnd(AgentUser user, ReqDuplicateCheckEnd info)
        {
            user.AgentController.ReqDuplicateCheckEnd();
        }

        private void OnReqFileSendBegin(AgentUser user, ReqFileSendBegin info)
        {
            user.AgentController.ReqFileSendBegin(info);
            //user.AgentController.ReqDuplicateCheckBegin();
        }

        private void OnReqFileOverwriteCheck(AgentUser user, ReqFileOverwriteCheck info)
        {
            user.AgentController.ReqFileOverwriteCheck(info);
            //user.AgentController.ReqDuplicateCheck(info);
        }

        //private int count = 0;
        private void OnReqFileSend(AgentUser user, ReqFileContentsSend info)
        {
            user.AgentController.ReqFileContentsSend(info);
            //Console.WriteLine("OnReqFileSend {0}", ++count);
            //user.AgentController.ReqDuplicateCheckEnd();
        }

        private void OnReqFileSendEnd(AgentUser user, ReqFileSendEnd info)
        {
            user.AgentController.ReqFileSendEnd(info);
            //user.AgentController.ReqDuplicateCheckEnd();
        }

        private void OnReqFileNameChange(AgentUser user, ReqFileNameChange info)
        {
            user.AgentController.ReqFileNameChange(info);
        }

        private void OnReqFileDelete(AgentUser user, ReqFileDelete info)
        {
            user.AgentController.ReqFileDelete(info);
        }

        private void OnReqServiceList(AgentUser user, ReqServiceList info)
        {
            user.AgentController.ReqServiceList(info);
        }

        private void OnReqServiceStart(AgentUser user, ReqServiceStart info)
        {
            user.AgentController.ReqServiceStart(info);
        }

        private void OnReqServiceStop(AgentUser user, ReqServiceStop info)
        {
            user.AgentController.ReqServiceStop(info);
        }

        private void OnReqFileReceiveBegin(AgentUser user, ReqFileReceiveBegin info)
        {
            user.AgentController.ReqFileReceiveBegin(info);
        }

        private void OnReqFileConents(AgentUser user, ReqFileReceiveConents info)
        {
            user.AgentController.ReqFileConents(info);
        }

        private void OnReqChatBot(AgentUser user, ReqChatBot info)
        {
            user.AgentController.ReqChatBot(info);
        }
        
    }
}
