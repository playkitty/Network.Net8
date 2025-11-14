using DBAgentProtocol;
using ServerModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAgentServerModule
{
    public sealed class DBAgentPacketReceiver : IPacketReceiver
    {
        private void OnReqInfo(DBAgentUser user, DBReqInfo info)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<DBResInfo>(ms, new DBResInfo
                {
                    directory = "test",
                    sequence = info.sequence
                });

                user.Send(new DBPacketHeader(eDBPacketID.ResInfo, ms.ToArray()));
            }
        }
    }
}
