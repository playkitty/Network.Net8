using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAgentProtocol
{
    public enum eDBPacketMode : int
    {
        NONE = 0,
        ReceiveDB,    // AgentServer Receive
        ReceiveServer,    // AgentClient Receive
        MAX
    }

    public enum eDBPacketID : int
    {
        NONE = 0,
        ReqInfo,
        ResInfo,
        MAX
    }

    public class DBPacketHeader
    {
        public eDBPacketID id { get; set; }
        public int size { get; set; }
        public byte[] body { get; set; }

        public DBPacketHeader(eDBPacketID id, byte[] body)
        {
            this.id = id;
            this.body = body;
            this.ToBuild();
        }
        public void ToBuild()
        {
            size = GetHeaderSize() + body.Length;
        }

        public static int GetHeaderSize()
        {
            return sizeof(int) + sizeof(int);// + sizeof(int);
        }

        public int GetBodySize()
        {
            return size - GetHeaderSize();
        }
    }

    [ProtoContract]
    [DBPacketAttribute(eDBPacketMode.ReceiveDB, eDBPacketID.ReqInfo)]
    public sealed class DBReqInfo
    {
        [ProtoMember(1)]
        public int sequence { get; set; }
        [ProtoMember(2)]
        public string directory { get; set; }
    }

    [ProtoContract]
    [DBPacketAttribute(eDBPacketMode.ReceiveServer, eDBPacketID.ResInfo)]
    public sealed class DBResInfo
    {
        [ProtoMember(1)]
        public int sequence { get; set; }
        [ProtoMember(2)]
        public string directory { get; set; }
    }
}
