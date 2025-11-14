using Protocol;
using ServerModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentServerModule
{
    public class AgentUser : User
    {
        private AgentController agentController = new AgentController();

        public AgentUser()
        {
            this.agentController.BindController(this);
        }

        public AgentController AgentController { get => this.agentController; }

        public void Send(PacketHeader packetHeader)
        {
            BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
            binaryWriter.Write(Convert.ToInt32(packetHeader.id));
            binaryWriter.Write(packetHeader.size);
            binaryWriter.Write(packetHeader.body);

            BinaryReader binaryReader = new BinaryReader(binaryWriter.BaseStream);
            binaryReader.BaseStream.Position = 0;
            var buff = binaryReader.ReadBytes((int)binaryWriter.BaseStream.Length);
            this.Send(buff);

            //Console.WriteLine("Send: {0}, {1}, {2}", packetHeader.id, packetHeader.size, packetHeader.body);
        }


        public override void OnClosed()
        {
            this.agentController.UnBindController();
            //UnBindController();
        }
    }
}
