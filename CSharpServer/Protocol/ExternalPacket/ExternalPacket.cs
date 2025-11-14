using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public enum eExternalPacketID : int
    {
        NONE = 0,
        ReqRegistLLM,
        ResRegistLLM,
        ReqQuestion,
        ResAnswer,
        MAX
    }

    //[ProtoContract]
    //[StructLayout(LayoutKind.Sequential, Pack = 1)]
    [PacketAttribute(ePacketMode.ReceiveLLMServer, (int)eExternalPacketID.ReqRegistLLM)]
    public sealed class ReqRegistLLM
    {
        //[ProtoMember(1)]
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        //public string keyword;
        public String keyword { get; set; } = String.Empty;
    }

    //[ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveLLMClient, (int)eExternalPacketID.ResRegistLLM)]
    public sealed class ResRegistLLM
    {
        [ProtoMember(1)]
        public String keyword { get; set; } = String.Empty;
    }


    //[ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveLLMClient, (int)eExternalPacketID.ReqQuestion)]
    public sealed class ReqQuestion
    {
        [ProtoMember(1)]
        public String keyword { get; set; } = String.Empty;
    }

    //[ProtoContract]
    [PacketAttribute(ePacketMode.ReceiveLLMServer, (int)eExternalPacketID.ResAnswer)]
    public sealed class ResAnswer
    {
        [ProtoMember(1)]
        public String keyword { get; set; } = String.Empty;
    }
}
