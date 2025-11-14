using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerModule.PacketInvoker
{
    public sealed class CppPacketInvoker : IPacketInvoker
    {
        public CppPacketInvoker()
        {

        }

        public bool Init()
        {
            return true;
        }

        public bool Deserialize(int id, MemoryStream dms, User user)
        {
            throw new NotImplementedException();
        }
    }
}
