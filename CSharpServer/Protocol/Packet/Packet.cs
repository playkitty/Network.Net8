using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ServiceProcess;

namespace Protocol
{
    public enum ePacketMode : int
    {
        NONE = 0,
        ReceiveAgentServer,     // AgentServer Receive
        ReceiveAgentClient,     // AgentClient Receive
        ReceiveLLMServer,       // LLMServer Receive
        ReceiveLLMClient,       // LLMClient Receive
        MAX
    }

    public enum ePacketID : int
    {
        NONE = 0,
        ReqLoginInfo,
        ResLoginInfo,
        ReqFolderInfo,
        ResFolderInfoBegin,
        NotifyFolderInfo,
        NotifyFileInfo,
        ResFolderInfoEnd,
        ReqDuplicateCheckBegin,
        ResDuplicateCheckBegin,
        ReqDuplicateCheck,
        ResDuplicateCheck,
        ReqDuplicateCheckEnd,
        ResDuplicateCheckEnd,
        ReqFileSendBegin,
        ResFileSendBegin,
        ReqFileOverwriteCheck,
        ResFileOverwriteCheck,
        ReqFileContentsSend,
        ResFileContentsSend,
        ReqFileSendEnd,
        ResFileSendEnd,
        ReqFileNameChange,
        ResFileNameChange,
        ReqFileDelete,
        ResFileDelete,
        ReqServiceList,
        ResServiceList,
        ReqServiceStart,
        ResServiceStart,
        ReqServiceStop,
        ResServiceStop,
        ReqFileReceiveBegin,
        ResFileReceiveBegin,
        ReqFileReceiveConents,
        ResFileReceiveConents,
        NotifyFileReceiveEnd,
        ReqChatBot,
        ResChatBot,
        MAX
    }

    public class PacketHeader
    {
        public ePacketID id { get; set; }
        public int size { get; set; }
        public byte[] body { get; set; }

        public PacketHeader(ePacketID id, byte[] body)
        {
            this.id = id;
            this.body = body;
            this.ToBuild();
        }
        private void ToBuild()
        {
            size = PacketHeader.GetHeaderSize() + body.Length;
        }

        public static int GetHeaderSize()
        {
            return sizeof(int) + sizeof(int);
        }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqLoginInfo)]
    public sealed class ReqLoginInfo
    {
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResLoginInfo)]
    public sealed class ResLoginInfo
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }

        [ProtoMember(2)]
        public int bandWidth { get; set; }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqFolderInfo)]
    public sealed class ReqFolderInfo
    {
        [ProtoMember(1)]
        public string directory { get; set; }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResFolderInfoBegin)]
    public sealed class ResFolderInfoBegin
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }

        [ProtoMember(2)]
        public string directory { get; set; }

        [ProtoMember(3)]
        public int folderCount { get; set; }

        [ProtoMember(4)]
        public int fileCount { get; set; }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.NotifyFolderInfo)]
    public sealed class NotifyFolderInfo
    {
        [ProtoMember(1)]
        public List<string> folders { get; set; } = new List<string>();
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.NotifyFileInfo)]
    public sealed class NotifyFileInfo
    {
        [ProtoMember(1)]
        public List<string> files { get; set; } = new List<string>();
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResFolderInfoEnd)]
    public sealed class ResFolderInfoEnd
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqDuplicateCheckBegin)]
    public sealed class ReqDuplicateCheckBegin
    {
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResDuplicateCheckBegin)]
    public sealed class ResDuplicateCheckBegin
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqDuplicateCheck)]
    public sealed class ReqDuplicateCheck
    {
        [ProtoMember(1)]
        public List<string> foldernames { get; set; } = new List<string>();

        [ProtoMember(2)]
        public List<string> filenames { get; set; } = new List<string>();
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResDuplicateCheck)]
    public sealed class ResDuplicateCheck
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqDuplicateCheckEnd)]
    public sealed class ReqDuplicateCheckEnd
    {
       
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResDuplicateCheckEnd)]
    public sealed class ResDuplicateCheckEnd
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }

        //[ProtoMember(2)]
        //public List<string> duplicationFiles { get; set; } = new List<string>();
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqFileSendBegin)]
    public sealed class ReqFileSendBegin
    {
        [ProtoMember(1)]
        public string filename { get; set; } = string.Empty;
        [ProtoMember(2)]
        public int filesize { get; set; } = 0;
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResFileSendBegin)]
    public sealed class ResFileSendBegin
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }

        [ProtoMember(2)]
        public string filename { get; set; } = string.Empty;

        [ProtoMember(3)]
        public string overwriteFilename { get; set; } = string.Empty;

        [ProtoMember(4)]
        public int overwriteFilecount { get; set; } = 0;
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqFileOverwriteCheck)]
    public sealed class ReqFileOverwriteCheck
    {
        [ProtoMember(1)]
        public int command { get; set; } // -1: cancel, 0: all overwite, 1: overwrite, 2: skip
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResFileOverwriteCheck)]
    public sealed class ResFileOverwriteCheck
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }

        [ProtoMember(2)]
        public int command { get; set; } // 0: all overwite, 1: overwrite, 2: skip
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqFileContentsSend)]
    public sealed class ReqFileContentsSend
    {
        [ProtoMember(1)]
        public byte[] bytes { get; set; }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResFileContentsSend)]
    public sealed class ResFileContentsSend
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqFileSendEnd)]
    public sealed class ReqFileSendEnd
    {
        //[ProtoMember(1)]
        //public string filename { get; set; }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResFileSendEnd)]
    public sealed class ResFileSendEnd
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }
        [ProtoMember(2)]
        public string filename { get; set; }
        [ProtoMember(3)]
        public int remainFileCount { get; set; }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqFileNameChange)]
    public sealed class ReqFileNameChange
    {
        [ProtoMember(1)]
        public String filename { get; set; } = String.Empty;
        [ProtoMember(2)]
        public String changeFilename { get; set; } = String.Empty;
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResFileNameChange)]
    public sealed class ResFileNameChange
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }
        [ProtoMember(2)]
        public String filename { get; set; } = String.Empty;
        [ProtoMember(3)]
        public String changeFilename { get; set; } = String.Empty;
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqFileDelete)]
    public sealed class ReqFileDelete
    {
        [ProtoMember(1)]
        public String filename { get; set; } = String.Empty;
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResFileDelete)]
    public sealed class ResFileDelete
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }
        [ProtoMember(2)]
        public String filename { get; set; } = String.Empty;
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqServiceList)]
    public sealed class ReqServiceList
    {
        [ProtoMember(1)]
        public String keyword { get; set; } = String.Empty;
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResServiceList)]
    public sealed class ResServiceList
    {
        [ProtoContract]
        public sealed class ServiceInfo
        {
            [ProtoMember(1)]
            public String name { get; set; } = String.Empty;
            [ProtoMember(2)]
            public ServiceControllerStatus status { get; set; } = ServiceControllerStatus.Stopped;
        }

        [ProtoMember(1)]
        public ErrorCode result { get; set; }
        [ProtoMember(2)]
        public List<ServiceInfo> services { get; set; } = new List<ServiceInfo>();
        [ProtoMember(3)]
        public int testint { get; set; }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqServiceStart)]
    public sealed class ReqServiceStart
    {
        [ProtoMember(1)]
        public String serviceName { get; set; } = String.Empty;
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResServiceStart)]
    public sealed class ResServiceStart
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }
        [ProtoMember(2)]
        public String serviceName { get; set; } = String.Empty;
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqServiceStop)]
    public sealed class ReqServiceStop
    {
        [ProtoMember(1)]
        public String serviceName { get; set; } = String.Empty;
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResServiceStop)]
    public sealed class ResServiceStop
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }
        [ProtoMember(2)]
        public String serviceName { get; set; } = String.Empty;
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqFileReceiveBegin)]
    public sealed class ReqFileReceiveBegin
    {
        [ProtoMember(1)]
        public string filename { get; set; } = string.Empty;
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResFileReceiveBegin)]
    public sealed class ResFileReceiveBegin
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }

        [ProtoMember(2)]
        public string filename { get; set; } = string.Empty;

        [ProtoMember(3)]
        public int filesize { get; set; } = 0;
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqFileReceiveConents)]
    public sealed class ReqFileReceiveConents
    {
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResFileReceiveConents)]
    public sealed class ResFileReceiveConents
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }

        [ProtoMember(2)]
        public byte[] bytes { get; set; }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.NotifyFileReceiveEnd)]
    public sealed class NotifyFileReceiveEnd
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentServer, (int)ePacketID.ReqChatBot)]
    public sealed class ReqChatBot
    {
        [ProtoMember(1)]
        public String question { get; set; }
    }

    [ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveAgentClient, (int)ePacketID.ResChatBot)]
    public sealed class ResChatBot
    {
        [ProtoMember(1)]
        public ErrorCode result { get; set; }

        [ProtoMember(2)]
        public String answer { get; set; }
    }
}
