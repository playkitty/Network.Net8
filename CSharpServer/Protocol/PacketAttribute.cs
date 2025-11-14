using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public sealed class PacketAttribute : Attribute
    {
        //private readonly ePacketMode mode = ePacketMode.NONE;
        //private readonly ePacketID id = ePacketID.NONE;

        //public PacketAttribute(ePacketMode mode, ePacketID id)
        public PacketAttribute(ePacketMode mode, int id)
        {
            //this.mode = mode;
            //this.id = id;
        }

        //public ePacketID ID { get => this.id; }
    }
}
