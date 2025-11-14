using ClientModule;
using DBAgentProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerModule
{
    public sealed class DBAgentClientReceiver : IClientPacketReceiver
    {
        private void OnResInfo(ClientBaseModule client, DBResInfo info)
        {
            //client.ProcessPacket(info.sequence, info);
        }
    }
}
