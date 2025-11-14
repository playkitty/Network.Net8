using Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AgentServerModule
{
    public sealed class AgentController
    {
        private AgentUser user = null;
        private string directory;
        private HashSet<string> PreCopyFolders = new HashSet<string>();
        private HashSet<string> PreCopyFiles = new HashSet<string>();
        private HashSet<string> DuplicationFiles = new HashSet<string>();

        //FileStream FileStream = null;
        BinaryWriter BinaryWriter = null;
        private string Filename = string.Empty;
        private int Filesize = 0;
        private BinaryReader binaryReader = null;
        //private bool AllSkip = false;
        public AgentController()
        {

        }

        //public string Directory { get => this.directory; set { this.directory = value; } }

        public void BindController(AgentUser user)
        {
            this.user = user;
        }

        public void UnBindController()
        {
            //this.user = null;
            if (this.BinaryWriter != null)
                this.BinaryWriter.Close();
        }

        public void ReqLoginInfo()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResLoginInfo>(ms, new ResLoginInfo
                {
                    result = ErrorCode.Success,
                    bandWidth = this.user.ReceiveBufferSize
                });

                user.Send(new PacketHeader(ePacketID.ResLoginInfo, ms.ToArray()));
            }
        }

        public bool ReqFolderInfo(ReqFolderInfo info)
        {
            this.directory = info.directory;

            DirectoryInfo di = new DirectoryInfo(info.directory);
            var fileList = di.GetFiles().Select(e => e.Name).ToList();
            var dirList = di.GetDirectories().Select(e => e.Name).ToList();

            user.AgentController.FolderInfoBegin(info.directory, dirList, fileList);
            user.AgentController.NotifyFolderInfo(dirList);
            user.AgentController.NotifyFileInfo(fileList);
            user.AgentController.FolderInfoEnd();

            return true;
        }

        public bool FolderInfoBegin(string directory, List<string> folders, List<string> files)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResFolderInfoBegin>(ms, new ResFolderInfoBegin
                {
                    result = ErrorCode.Success,
                    directory = directory,
                    folderCount = folders.Count(),
                    fileCount = files.Count()
                });

                user.Send(new PacketHeader(ePacketID.ResFolderInfoBegin, ms.ToArray()));
            }

            return true;
        }

        private void SendNotifyFolderInfo(List<string> list)
        {
            if (list.Count() <= 0)
                return;

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<NotifyFolderInfo>(ms, new NotifyFolderInfo
                {
                    folders = list
                });

                var header = new PacketHeader(ePacketID.NotifyFolderInfo, ms.ToArray());
                user.Send(header);
            }
        }

        private void SendNotifyFileInfo(List<string> list)
        {
            if (list.Count() <= 0)
                return;

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<NotifyFileInfo>(ms, new NotifyFileInfo
                {
                    files = list
                });

                var header = new PacketHeader(ePacketID.NotifyFileInfo, ms.ToArray());
                user.Send(header);
            }
        }

        public bool NotifyFolderInfo(List<string> folders)
        {
            List<string> list = new List<string>();
            int sizeCount = 0;
            foreach (var dir in folders)
            {
                sizeCount += dir.Count();
                if (sizeCount < 800)
                {
                    list.Add(dir);
                    continue;
                }


                this.SendNotifyFolderInfo(list);
                sizeCount = 0;
                list.Clear();
            }

            this.SendNotifyFolderInfo(list);

            return true;
        }

        public bool NotifyFileInfo(List<string> files)
        {
            List<string> list = new List<string>();
            int sizeCount = 0;
            foreach (var file in files)
            {
                sizeCount += file.Count();
                if (sizeCount < 800)
                {
                    list.Add(file);
                    continue;
                }

                this.SendNotifyFileInfo(list);
                sizeCount = 0;
                list.Clear();
            }

            this.SendNotifyFileInfo(list);

            return true;
        }

        public bool FolderInfoEnd()
        {
            // send
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResFolderInfoEnd>(ms, new ResFolderInfoEnd
                {
                    result = ErrorCode.Success
                });

                user.Send(new PacketHeader(ePacketID.ResFolderInfoEnd, ms.ToArray()));
            }

            return true;
        }

        public bool ReqDuplicateCheckBegin()
        {
            this.DuplicationFiles.Clear();
            this.PreCopyFolders.Clear();
            this.PreCopyFiles.Clear();

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResDuplicateCheckBegin>(ms, new ResDuplicateCheckBegin
                {
                    result = ErrorCode.Success
                });

                user.Send(new PacketHeader(ePacketID.ResDuplicateCheckBegin, ms.ToArray()));
            }

            return true;
        }

        public bool ReqDuplicateCheck(ReqDuplicateCheck info)
        {
            this.PreCopyFolders.UnionWith(info.foldernames);
            this.PreCopyFiles.UnionWith(info.filenames);
            //this.PreCopyFolders.AddRange(info.foldernames);
            //this.PreCopyFiles.AddRange(info.filenames);

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResDuplicateCheck>(ms, new ResDuplicateCheck
                {
                    result = ErrorCode.Success
                });

                user.Send(new PacketHeader(ePacketID.ResDuplicateCheck, ms.ToArray()));
            }

            return true;
        }

        public bool ReqDuplicateCheckEnd()
        {
            //var duplicationFiles = new List<string>();
            foreach (var file in this.PreCopyFiles)
            {
                var pathFile = this.directory + file;
                if (File.Exists(pathFile) == true)
                {
                    // 중복 카운팅
                    this.DuplicationFiles.Add(file);
                }
            }

            foreach (var folder in this.PreCopyFolders)
            {
                Directory.CreateDirectory(this.directory + folder);
            }

            //var size = DuplicationFiles.Count();
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResDuplicateCheckEnd>(ms, new ResDuplicateCheckEnd
                {
                    result = ErrorCode.Success
                });

                user.Send(new PacketHeader(ePacketID.ResDuplicateCheckEnd, ms.ToArray()));
            }

            return true;
        }

        public void ReqFileSendBegin(ReqFileSendBegin info)
        {
            if (this.PreCopyFiles.Contains(info.filename) == false)
            {
                // error 
                return;
            }

            this.Filename = info.filename;
            this.Filesize = info.filesize;

            var res = new ResFileSendBegin { result = ErrorCode.Success, filename = this.Filename };
            if (this.DuplicationFiles.Contains(info.filename))
            {
                // duplicate
                res.overwriteFilename = info.filename;
                res.overwriteFilecount = DuplicationFiles.Count();
            }
            else
            {
                //using (new FileStream(this.directory + this.Filename, FileMode.Create, FileAccess.Write)) { }
                var fileStream = new FileStream(this.directory + this.Filename, FileMode.Create, FileAccess.Write);
                this.BinaryWriter = new BinaryWriter(fileStream);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResFileSendBegin>(ms, res);
                user.Send(new PacketHeader(ePacketID.ResFileSendBegin, ms.ToArray()));
            }
        }

        public void ReqFileOverwriteCheck(ReqFileOverwriteCheck info)
        {
            //if (this.AllOverwrite)
            //{
            //    // error
            //    return;
            //}

            //this.AllOverwrite = info.command == 0;

            switch (info.command)
            {
                case -1:
                    {
                        this.DuplicationFiles.Clear();
                    }
                    break;
                case 0:
                    {
                        this.DuplicationFiles.Clear();
                        var fileStream = new FileStream(this.directory + this.Filename, FileMode.Create, FileAccess.Write);
                        this.BinaryWriter = new BinaryWriter(fileStream);
                        //using (new FileStream(this.directory + this.Filename, FileMode.Create, FileAccess.Write)) { }
                        //this.BinaryWriter = new BinaryWriter(this.FileStream);
                    }
                    break;
                case 1:
                    {
                        var fileStream = new FileStream(this.directory + this.Filename, FileMode.Create, FileAccess.Write);
                        this.BinaryWriter = new BinaryWriter(fileStream);
                        //using (new FileStream(this.directory + this.Filename, FileMode.Create, FileAccess.Write)) { }
                        ////this.BinaryWriter = new BinaryWriter(this.FileStream);
                        DuplicationFiles.Remove(this.Filename);
                    }
                    break;
                case 2:
                    {
                        //this.AllSkip = true;
                        this.DuplicationFiles.Remove(this.Filename);
                    }
                    break;
            }

            // 중복 리스트 제거
            //DuplicationFiles.Remove(this.Filename);

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResFileOverwriteCheck>(ms, new ResFileOverwriteCheck
                {
                    result = ErrorCode.Success,
                    command = info.command
                });

                user.Send(new PacketHeader(ePacketID.ResFileOverwriteCheck, ms.ToArray()));
            }
        }

        public void ReqFileContentsSend(ReqFileContentsSend info)
        {
            if (this.BinaryWriter == null)
                return;

            //this.FileStream = new FileStream(this.directory + this.Filename, FileMode.Open, FileAccess.Write);
            //this.BinaryWriter = new BinaryWriter(this.FileStream);

            if (info.bytes.Count() > 0)
            {
                //this.BinaryWriter.BaseStream.Position = this.BinaryWriter.BaseStream.Length;
                this.BinaryWriter.Write(info.bytes, 0, info.bytes.Count());
            }

            ///this.BinaryWriter.Close();

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResFileContentsSend>(ms, new ResFileContentsSend
                {
                    result = ErrorCode.Success,
                });

                user.Send(new PacketHeader(ePacketID.ResFileContentsSend, ms.ToArray()));
            }
        }

        public void ReqFileSendEnd(ReqFileSendEnd info)
        {
            //info.bytes

            this.BinaryWriter.Close();

            this.PreCopyFiles.Remove(this.Filename);
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResFileSendEnd>(ms, new ResFileSendEnd
                {
                    result = ErrorCode.Success,
                    filename = this.Filename,
                    remainFileCount = this.PreCopyFiles.Count()
                });

                user.Send(new PacketHeader(ePacketID.ResFileSendEnd, ms.ToArray()));
            }
        }

        public void ReqFileNameChange(ReqFileNameChange info)
        {
            var errorCode = ErrorCode.Success;
            var pathFile = this.directory + info.filename;
            if (File.Exists(pathFile) == false)
            {
                errorCode = ErrorCode.Failed;
            }
            else
            {
                if (File.Exists(this.directory + info.changeFilename))
                {
                    errorCode = ErrorCode.Failed;
                }
                else
                {
                    var changeFile = this.directory + info.changeFilename;
                    File.Move(pathFile, changeFile);
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResFileNameChange>(ms, new ResFileNameChange
                {
                    result = errorCode,
                    filename = info.filename,
                    changeFilename = info.changeFilename
                });

                user.Send(new PacketHeader(ePacketID.ResFileNameChange, ms.ToArray()));
            }
        }

        public void ReqFileDelete(ReqFileDelete info)
        {
            var errorCode = ErrorCode.Success;
            var pathFile = this.directory + info.filename;
            if (File.Exists(pathFile) == false)
            {
                errorCode = ErrorCode.Failed;
            }
            else
            {
                File.Delete(pathFile);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResFileDelete>(ms, new ResFileDelete
                {
                    result = errorCode,
                    filename = info.filename,
                });

                user.Send(new PacketHeader(ePacketID.ResFileDelete, ms.ToArray()));
            }
        }
        public void ReqServiceList(ReqServiceList info)
        {
            var scService = ServiceController.GetServices();
            var list = scService.Where(e => e.ServiceName.IndexOf(info.keyword) != -1).Select(k => new ResServiceList.ServiceInfo { name = k.ServiceName, status = k.Status }).ToList();
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResServiceList>(ms, new ResServiceList
                {
                    result = ErrorCode.Success,
                    services = list,
                    testint = 3
                });

                user.Send(new PacketHeader(ePacketID.ResServiceList, ms.ToArray()));
            }
        }

        public void ReqServiceStart(ReqServiceStart info)
        {
            var scService = ServiceController.GetServices();
            var list = scService.Where(e => e.ServiceName == info.serviceName);
            if (list.Count() > 1)
                return;

            list.ElementAt(0).Start();

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResServiceStart>(ms, new ResServiceStart
                {
                    result = ErrorCode.Success,
                    serviceName = info.serviceName
                });

                user.Send(new PacketHeader(ePacketID.ResServiceStart, ms.ToArray()));
            }
        }

        public void ReqServiceStop(ReqServiceStop info)
        {
            var scService = ServiceController.GetServices();
            var list = scService.Where(e => e.ServiceName == info.serviceName);
            if (list.Count() > 1)
                return;

            list.ElementAt(0).Stop();

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResServiceStop>(ms, new ResServiceStop
                {
                    result = ErrorCode.Success,
                    serviceName = info.serviceName
                });

                user.Send(new PacketHeader(ePacketID.ResServiceStop, ms.ToArray()));
            }
        }

        public void ReqFileReceiveBegin(ReqFileReceiveBegin info)
        {
            // 검증

            // 파일 체크한번
            // + 현재경로에서
            if (File.Exists(this.directory + "\\" + info.filename) == false)
            {
                // error
                return;
            }

            //using (var fileStream = new FileStream(this.directory + "\\" + info.filename, FileMode.Open, FileAccess.Read))
            //{
            var fileStream = new FileStream(this.directory + "\\" + info.filename, FileMode.Open, FileAccess.Read);
            //this.FileSize = (int)fileSteram.Length;
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResFileReceiveBegin>(ms, new ResFileReceiveBegin
                {
                    result = ErrorCode.Success,
                    filename = info.filename,
                    filesize = (int)fileStream.Length
                });

                this.user.Send(new PacketHeader(ePacketID.ResFileReceiveBegin, ms.ToArray()));
            }
            //}

            this.binaryReader = new BinaryReader(fileStream);
        }

        public void ReqFileConents(ReqFileReceiveConents info)
        {
            var readBytes = (int)(this.user.SendBufferSize * 0.7f);
            var sendBytes = this.binaryReader.ReadBytes(readBytes);

            //while (sendBytes.Count() != 0)
            //{
            // 파일데이터 noti
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<ResFileReceiveConents>(ms, new ResFileReceiveConents
                {
                    bytes = sendBytes
                });

                user.Send(new PacketHeader(ePacketID.ResFileReceiveConents, ms.ToArray()));
            }

            //sendBytes = binaryReader.ReadBytes(readBytes);
            //}


            // 종료 noti
            if (sendBytes.Count() == 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize<NotifyFileReceiveEnd>(ms, new NotifyFileReceiveEnd
                    {
                        result = ErrorCode.Success,
                    });

                    user.Send(new PacketHeader(ePacketID.NotifyFileReceiveEnd, ms.ToArray()));
                }
                binaryReader.Close();
            }

        }

        public void ReqChatBot(ReqChatBot info)
        {
            //String question = "이름이 뭐야?";
            var quesBytes = Encoding.UTF8.GetBytes(info.question);

            BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
            binaryWriter.Write(Convert.ToInt32(101));
            binaryWriter.Write(4 + 4 + + 4 +quesBytes.Count());
            binaryWriter.Write(Convert.ToInt32(this.user.Index));
            binaryWriter.Write(quesBytes);

            BinaryReader reader = new BinaryReader(binaryWriter.BaseStream);
            reader.BaseStream.Position = 0;
            var buff = reader.ReadBytes((int)reader.BaseStream.Length);

            //var quesBytes = Encoding.UTF8.GetBytes(buff);

            //this.user.Index;

            var llm = ObjectController.Instance().Dequeue();
            if (llm != null)
                llm.Send(buff);

//            var llm = LLMController.Instance().Dequeue();
        }
    }
}
