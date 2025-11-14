using ClientModule;
using Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NOBAgentClient
{
    public sealed class PacketReceiver : IClientPacketReceiver
    {
        private void OnResLoginInfo(ClientBaseModule client, ResLoginInfo info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            MainController.Inst.SetController(info);
        }
        
        private void OnResFolderInfo(ClientBaseModule client, ResFolderInfoBegin info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            MainController.Inst.GUIController.form.ClearFolder();
        }

        private void OnNotifyFolderInfo(ClientBaseModule client, NotifyFolderInfo info)
        {
            MainController.Inst.GUIController.form.DrawFolder(info.folders, null);
        }

        private void OnNotifyFileInfo(ClientBaseModule client, NotifyFileInfo info)
        {
            MainController.Inst.GUIController.form.DrawFolder(null, info.files);
        }

        private void OnResFolderInfoEnd(ClientBaseModule client, ResFolderInfoEnd info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            // 서비스 테스트
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    ProtoBuf.Serializer.Serialize<ReqServiceList>(ms, new ReqServiceList { keyword = "Grafana" });

            //    MainController.Inst.NetworkController.Send(new PacketHeader(ePacketID.ReqServiceList, ms.ToArray()));
            //}
            
            Console.WriteLine("OnResFolderInfoEnd");
        }

        private void OnResDuplicateCheckBegin(ClientBaseModule client, ResDuplicateCheckBegin info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            Console.WriteLine("ResDuplicateCheckBegin");
        }

        private void OnResDuplicateCheck(ClientBaseModule client, ResDuplicateCheck info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            Console.WriteLine("ResDuplicateCheck");
        }

        private void OnResDuplicateCheckEnd(ClientBaseModule client, ResDuplicateCheckEnd info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            MainController.Inst.ReqFileSendBegin();
            Console.WriteLine("ResDuplicateCheckEnd");
        }

        private void OnResFileSendBegin(ClientBaseModule client, ResFileSendBegin info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            if (MainController.Inst.CurFileName != info.filename)
            {
                // error
                return;
            }

            if (info.overwriteFilename == string.Empty)
            {
                // file send start
                MainController.Inst.FileTransferStart();
            }
            else
            {
                // 모두 덮어쓰기
                if (MainController.Inst.Command == 0)
                {
                    ++MainController.Inst.SendIndex;

                    // 바로 진행
                    MainController.Inst.FileTransferStart();
                    return;
                }

                if (MainController.Inst.AllApply == false)
                {                     // 메시지 박스 선택 전체 덮어쓰기, 덮어쓰기, 취소
                    var check = new OverwriteConfirm(info.overwriteFilename, info.overwriteFilecount);
                    check.ShowDialog();
                    MainController.Inst.AllApply = check.AllSkip;
                    MainController.Inst.Command = check.Command;
                }

                
                var netController = client as NetworkController;
                using (MemoryStream ms = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize<ReqFileOverwriteCheck>(ms, new ReqFileOverwriteCheck { command = MainController.Inst.Command });
                    netController.Send(new PacketHeader(ePacketID.ReqFileOverwriteCheck, ms.ToArray()));
                }
            }

            Console.WriteLine("ResFileSendBegin");
        }

        private void OnResFileOverwriteCheck(ClientBaseModule client, ResFileOverwriteCheck info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            MainController.Inst.Command = info.command;
            if (info.command == -1)
            {
                // 끝
                MainController.Inst.FileTransferEnd();
                return;
            }
            else if (info.command == 2)
            {
                if (MainController.Inst.AllApply)
                {
                    ++MainController.Inst.SendIndex;
                }

                if (MainController.Inst.IsFileContentsEnd)
                {
                    // 끝
                    MainController.Inst.FileTransferEnd();
                }
                else
                {
                    MainController.Inst.ReqFileSendBegin();
                }
            }

            // file send start
            MainController.Inst.FileTransferStart();

            Console.WriteLine("ResFileOverwriteCheck");
        }

        private void OnResFileContentsSend(ClientBaseModule client, ResFileContentsSend info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }
            try
            {
                MainController.Inst.FileContentsSend();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception");
            }
        }

        private void OnResFileSendEnd(ClientBaseModule client, ResFileSendEnd info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            ++MainController.Inst.SendIndex;
            if (MainController.Inst.IsFileContentsEnd)
            {
                // 끝
                MainController.Inst.FileTransferEnd();
            }
            else
            {
                // next
                MainController.Inst.ReqFileSendBegin();
            }

            Console.WriteLine("ResFileSendEnd completed file {0}", info.filename);
        }

        private void OnResFileNameChange(ClientBaseModule client, ResFileNameChange info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            MainController.Inst.ResFileNameChange(info);
        }

        private void OnResFileDelete(ClientBaseModule client, ResFileDelete info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            MainController.Inst.ResFileDelete(info);
        }

        private void OnResServiceList(ClientBaseModule client, ResServiceList info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            MainController.Inst.ResServiceList(info);
        }

        private void ResServiceStart(ClientBaseModule client, ResServiceStart info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            MainController.Inst.ResServiceStart(info);
        }
        private void ResServiceStop(ClientBaseModule client, ResServiceStop info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            MainController.Inst.ResServiceStop(info);
        }

        private void ResFileReceiveBegin(ClientBaseModule client, ResFileReceiveBegin info)
        {
            if (info.result != ErrorCode.Success)
            {
                // error
                return;
            }

            MainController.Inst.ResFileReceiveBegin(info);
        }

        private void ResFileReceiveConents(ClientBaseModule client, ResFileReceiveConents info)
        {
            MainController.Inst.ResFileReceiveConents(info);
        }

        private void NotifyFileEnd(ClientBaseModule client, NotifyFileReceiveEnd info)
        {
            MainController.Inst.NotifyFileReceiveEnd(info);
        }
        private void ResChatBot(ClientBaseModule client, ResChatBot info)
        {
            MainController.Inst.ResChatBot(info);
        }
    }
}
