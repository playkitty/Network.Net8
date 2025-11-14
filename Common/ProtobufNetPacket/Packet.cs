using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOBAgentClient.Packet
{
    [ProtoContract]
    public class NOBNotifyServerInfo
    {
        [ProtoMember(1)]
        public byte[] ServerKey { get; set; }
    }
}
