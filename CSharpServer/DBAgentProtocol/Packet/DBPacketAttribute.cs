using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAgentProtocol
{
    public sealed class DBPacketAttribute : Attribute
    {
        //private readonly ePacketMode mode = ePacketMode.NONE;
        //private readonly ePacketID id = ePacketID.NONE;

        public DBPacketAttribute(eDBPacketMode mode, eDBPacketID id)
        {
            //this.mode = mode;
            //this.id = id;
        }

        //public ePacketID ID { get => this.id; }
    }
}
