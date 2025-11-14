using Common;
using Protocol;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NOBAgentClient
{
    public sealed class MainController
    {
        private static MainController _inst = null;

        public String Directory = "D:\\";

        //private int recieveSize = 1024; // 최초 receive용
        private int sendIndex = 0;
        private int curSendCount = 0;
        private BinaryReader binaryReader;
        private BinaryWriter binaryWriter;

        public NetworkController NetworkController = new NetworkController();
        public GUIController GUIController = new GUIController();
        public Dictionary<string, string> FilePaths = new Dictionary<string, string>();
        public List<string> Filenames = new List<string>();
        private TaskSyncer timer = new TaskSyncer();

        private String recvFilename = String.Empty;
        private int recvSize = 0;
        private int progressRecvSize = 0;

        public MainController()
        {
        }

        public bool IsFileContentsEnd { get => this.Filenames.Count() <= this.sendIndex; }

        public String CurFileName { get => this.Filenames[this.sendIndex]; }
        public int SendIndex { get => this.sendIndex; set => this.sendIndex = value; }
        public bool AllApply { get; set; }
        public int Command { get; set; } = -1;
        public int FileSize { get; set; } = 0;
        public int BandWidth { get => this.NetworkController.ReceiveBufferSize; }


        public static MainController Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new MainController();
                }

                return _inst;
            }
        }

        public bool Init()
        {
            this.NetworkController.Init(this.BandWidth);
            var ip = ConfigurationManager.AppSettings.Get("IPAddress");
            var port = ConfigurationManager.AppSettings.Get("Port");
            if (ip == null || ip.Count() == 0)
            {
                ip = "127.0.0.1";
            }

            if (port == null || port.Count() == 0)
            {
                port = "19004";
            }


            // 여기서 기본 UI 설정 폴더관리 이미지 지우기
            //GUIController.form.

            this.NetworkController.Connect(ip, Convert.ToInt32(port));

            return true;
        }

        public void TimerLoop()
        {
            timer.Post(new TaskJob(() =>
            {
                var keyword = ConfigurationManager.AppSettings.Get("ServiceKeyword");
                if (keyword == null)
                {
                    return;
                }

                var delay = ConfigurationManager.AppSettings.Get("ServiceStatusDelay");
                if (delay == null)
                {
                    return;
                }

                Thread.Sleep(Convert.ToInt32(delay));
                this.ReqServiceList("NateOnBiz");
                this.TimerLoop();
            }));
        }

        public void ReqLoginInfo()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ReqLoginInfo>(ms, new ReqLoginInfo { });

                this.NetworkController.Send(new PacketHeader(ePacketID.ReqLoginInfo, ms.ToArray()));
            }
        }

        public void SetController(ResLoginInfo info)
        {
            //this.recieveSize = info.bandWidth;
            this.NetworkController.RecieveBandWidth(info.bandWidth);

            this.NetworkController.SendReqFolderInfo(this.Directory);

            this.TimerLoop();
        }

        public void SendReqDuplicateCheck(List<string> foldernames, List<string> filenames)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ReqDuplicateCheckBegin>(ms, new ReqDuplicateCheckBegin { });

                this.NetworkController.Send(new PacketHeader(ePacketID.ReqDuplicateCheckBegin, ms.ToArray()));
            }

            var sizeCount = 0;
            var size = this.BandWidth * 0.7f;
            var folerlist = new List<string>();
            var filelist = new List<string>();
            foreach (var dir in foldernames)
            {
                sizeCount += dir.Count();
                if (sizeCount < size)
                {
                    folerlist.Add(dir);
                    continue;
                }


                this.SendReqDuplicateChecks(folerlist, filelist);
                sizeCount = 0;
                folerlist.Clear();
            }

            this.SendReqDuplicateChecks(folerlist, filelist);
            folerlist.Clear();

            foreach (var file in filenames)
            {
                sizeCount += file.Count();
                if (sizeCount < size)
                {
                    filelist.Add(file);
                    continue;
                }

                this.SendReqDuplicateChecks(folerlist, filelist);
                sizeCount = 0;
                filelist.Clear();
            }

            this.SendReqDuplicateChecks(folerlist, filelist);
            filelist.Clear();

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ReqDuplicateCheckEnd>(ms, new ReqDuplicateCheckEnd { });

                this.NetworkController.Send(new PacketHeader(ePacketID.ReqDuplicateCheckEnd, ms.ToArray()));
            }
        }

        private void SendReqDuplicateChecks(List<string> folders, List<string> files)
        {
            if (folders.Count() <= 0 && files.Count() <= 0)
                return;

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ReqDuplicateCheck>(ms, new ReqDuplicateCheck
                {
                    foldernames = folders,
                    filenames = files
                });

                this.NetworkController.Send(new PacketHeader(ePacketID.ReqDuplicateCheck, ms.ToArray()));
            }
        }

        public void ReqFileSendBegin()
        {
            var fileName = this.CurFileName;
            if (this.FilePaths.TryGetValue(fileName, out var path) == false)
            {
                // error
                return;
            }

            using (var fileSteram = new FileStream(path + "\\" + fileName, FileMode.Open, FileAccess.Read))
            {
                this.FileSize = (int)fileSteram.Length;
                using (MemoryStream ms = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize<ReqFileSendBegin>(ms, new ReqFileSendBegin
                    {
                        filename = fileName,
                        filesize = this.FileSize
                    });

                    this.NetworkController.Send(new PacketHeader(ePacketID.ReqFileSendBegin, ms.ToArray()));
                }
            }
        }

        public void FileTransferStart()
        {
            var fileName = this.Filenames[this.SendIndex];
            if (this.FilePaths.TryGetValue(fileName, out var path) == false)
            {
                // error
                return;
            }

            var fileStream = new FileStream(path + "\\" + fileName, FileMode.Open, FileAccess.Read);
            this.FileSize = (int)fileStream.Length;
            //this.maxCount = (this.FileSize / 1024) + 1;
            this.curSendCount = 0;


            this.binaryReader = new BinaryReader(fileStream);
            this.FileContentsSend();
        }

        public void FileContentsSend()
        {
            var readBytes = (int)(this.BandWidth * 0.7f);
            this.GUIController.SetProgressBar(this.CurFileName, readBytes, this.curSendCount, this.FileSize, this.SendIndex, this.Filenames.Count());
            var sendBytes = this.binaryReader.ReadBytes(readBytes);
            if (this.curSendCount != 0 && sendBytes.Count() == 0)
            {
                // 끝
                using (MemoryStream ms = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize<ReqFileSendEnd>(ms, new ReqFileSendEnd { });

                    this.NetworkController.Send(new PacketHeader(ePacketID.ReqFileSendEnd, ms.ToArray()));
                }

                this.binaryReader.Close();
                return;
            }
            ++this.curSendCount;

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ReqFileContentsSend>(ms, new ReqFileContentsSend
                {
                    bytes = sendBytes
                });

                this.NetworkController.Send(new PacketHeader(ePacketID.ReqFileContentsSend, ms.ToArray()));
            }
        }

        public void FileTransferEnd()
        {
            this.AllApply = false;
            this.Command = -1;

            this.SendIndex = 0;
            this.curSendCount = 0;
            this.FilePaths.Clear();
            if (this.binaryReader != null)
            {
                this.binaryReader.Close();
                this.binaryReader = null;
            }
            this.NetworkController.SendReqFolderInfo(this.Directory);
            this.Filenames.Clear();

            this.GUIController.form.SetProgressVar(100, "파일 전송 완료");
        }

        public void ReqFileNameChange(String fileName, String changeFileName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ReqFileNameChange>(ms, new ReqFileNameChange
                {
                    filename = fileName,
                    changeFilename = changeFileName
                });

                this.NetworkController.Send(new PacketHeader(ePacketID.ReqFileNameChange, ms.ToArray()));
            }
        }

        public void ReqFileDelete(String fileName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ReqFileDelete>(ms, new ReqFileDelete
                {
                    filename = fileName,
                });

                this.NetworkController.Send(new PacketHeader(ePacketID.ReqFileDelete, ms.ToArray()));
            }
        }

        public void ResFileNameChange(ResFileNameChange info)
        {
            this.GUIController.form.ChangeName(info.changeFilename);
        }

        public void ResFileDelete(ResFileDelete info)
        {
            this.GUIController.form.DeleteName(info.filename);
        }

        public void ReqServiceList(String keyword)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ReqServiceList>(ms, new ReqServiceList { keyword = keyword });

                this.NetworkController.Send(new PacketHeader(ePacketID.ReqServiceList, ms.ToArray()));
            }
        }

        public void ResServiceList(ResServiceList info)
        {
            this.GUIController.form.DrawServiceList(info.services);
            //this.GUIController.form.DeleteName(info.filename);
        }

        public void ReqServiceStart(String serviceName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ReqServiceStart>(ms, new ReqServiceStart { serviceName = serviceName });

                this.NetworkController.Send(new PacketHeader(ePacketID.ReqServiceStart, ms.ToArray()));
            }
        }

        public void ResServiceStart(ResServiceStart info)
        {
            //this.GUIController.form.DrawServiceList(info.services);
            //this.GUIController.form.DeleteName(info.filename);
        }

        public void ReqServiceStop(String serviceName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ReqServiceStop>(ms, new ReqServiceStop { serviceName = serviceName });

                this.NetworkController.Send(new PacketHeader(ePacketID.ReqServiceStop, ms.ToArray()));
            }
        }

        public void ResServiceStop(ResServiceStop info)
        {
            //this.GUIController.form.DrawServiceList(info.services);
            //this.GUIController.form.DeleteName(info.filename);
        }

        public void ReqFileReceiveBegin(String fileName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ReqFileReceiveBegin>(ms, new ReqFileReceiveBegin { filename = fileName });

                this.NetworkController.Send(new PacketHeader(ePacketID.ReqFileReceiveBegin, ms.ToArray()));
            }
        }

        public void ResFileReceiveBegin(ResFileReceiveBegin info)
        {
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Download");
            var fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Download" + "\\" + info.filename, FileMode.Create, FileAccess.Write);
            this.binaryWriter = new BinaryWriter(fileStream);


            this.recvSize = info.filesize;
            this.recvFilename = info.filename;

            String text = this.recvFilename + " 파일 내려받는 중 " + 0 + "%";
            Console.WriteLine("ProgressBar Percent ({0}/100)", 0);
            GUIController.form.SetProgressVar(0, text);

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ReqFileReceiveConents>(ms, new ReqFileReceiveConents { });

                this.NetworkController.Send(new PacketHeader(ePacketID.ReqFileReceiveConents, ms.ToArray()));
            }
        }

        public void ResFileReceiveConents(ResFileReceiveConents info)
        {
            if (info.bytes.Count() > 0)
            {
                //this.BinaryWriter.BaseStream.Position = this.BinaryWriter.BaseStream.Length;
                this.binaryWriter.Write(info.bytes, 0, info.bytes.Count());

                progressRecvSize += info.bytes.Count();
                var curProgressRate = (int)(progressRecvSize / (this.recvSize * 0.01));
                if (curProgressRate > 100)
                    curProgressRate = 100;


                String text = this.recvFilename + " 파일 내려받는 중 " + curProgressRate + "%";
                Console.WriteLine("ProgressBar Percent ({0}/100)", curProgressRate);
                GUIController.form.SetProgressVar(curProgressRate, text);

                using (MemoryStream ms = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize<ReqFileReceiveConents>(ms, new ReqFileReceiveConents { });

                    this.NetworkController.Send(new PacketHeader(ePacketID.ReqFileReceiveConents, ms.ToArray()));
                }
            }
        }

        public void NotifyFileReceiveEnd(NotifyFileReceiveEnd info)
        {
            this.binaryWriter.Close();
            this.progressRecvSize = 0;
            this.recvSize = 0;
            GUIController.form.SetProgressVar(100, "파일 내려받기 완료");
        }

        public void ReqChatBot(String text)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ReqChatBot>(ms, new ReqChatBot { question = text });

                //MainController.Inst.ReqChatBot(text);
                this.NetworkController.Send(new PacketHeader(ePacketID.ReqChatBot, ms.ToArray()));
            }
        }

        public void ResChatBot(ResChatBot res)
        {
            GUIController.form.AddTextLine(res.answer);
        }
    }
}
