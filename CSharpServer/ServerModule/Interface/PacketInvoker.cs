using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServerModule
{
    public interface IPacketInvoker
    {
        bool Deserialize(int id, MemoryStream dms, User user);
    }
}