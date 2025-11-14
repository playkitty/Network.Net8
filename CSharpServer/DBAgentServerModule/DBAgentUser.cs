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
    public class DBAgentUser : User
    {
        public DBAgentUser()
        {
        }

        public override void OnClosed()
        {
            throw new NotImplementedException();
        }

        public void Send(DBPacketHeader packetHeader)
        {
            BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
            binaryWriter.Write(Convert.ToInt32(packetHeader.id));
            binaryWriter.Write(packetHeader.size);
            binaryWriter.Write(packetHeader.body);

            BinaryReader binaryReader = new BinaryReader(binaryWriter.BaseStream);
            binaryReader.BaseStream.Position = 0;
            var buff = binaryReader.ReadBytes((int)binaryWriter.BaseStream.Length);
            this.Send(buff);

            //this.Sender.Send(this, bytes);
        }
    }
}
