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
    public sealed class LLMPacketReceiver : IPacketReceiver
    {
        //private void OnReqRegistLLM(LLMUser user, ReqRegistLLM req)
        private void OnReqRegistLLM(LLMUser user, ReqRegistLLM req, byte[] bytes)
        {
            //user.AgentController.ReqLoginInfo();
            var utf8Str = Encoding.UTF8.GetString(bytes);

            Console.WriteLine("LLM Response: {0}", utf8Str);

            user.Active = true;

            ObjectController.Instance().Enqueue(user);


            return;
            String question = "이름이 뭐야?";
            var quesBytes = Encoding.UTF8.GetBytes(question);

            BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
            binaryWriter.Write(Convert.ToInt32(101));
            binaryWriter.Write(4 + 4 + quesBytes.Count());
            binaryWriter.Write(quesBytes);

            BinaryReader reader = new BinaryReader(binaryWriter.BaseStream);
            reader.BaseStream.Position = 0;
            var buff = reader.ReadBytes((int)reader.BaseStream.Length);

            //var quesBytes = Encoding.UTF8.GetBytes(buff);
            user.Send(buff);

            
        }


        private void OnResAnswer(LLMUser user, ResAnswer res, byte[] bytes)
        {
            if (user.Active == false)
            {
                // off 중
                return;
            }


            using (var ms = new MemoryStream(bytes))
            {
                BinaryReader binaryReader = new BinaryReader(ms);
                var index = binaryReader.ReadInt32();
                int remainSize = (int)binaryReader.BaseStream.Length- 4;
                var strBytes = binaryReader.ReadBytes(remainSize);
                var chatBotAnswer = Encoding.UTF8.GetString(strBytes);

                var agentUser = (AgentUser)ObjectController.Instance().AgentServer.Get(index);

                using (MemoryStream ms2 = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize<ResChatBot>(ms2, new ResChatBot
                    {
                        result = ErrorCode.Success,
                        answer = chatBotAnswer
                    });

                    agentUser.Send(new PacketHeader(ePacketID.ResChatBot, ms2.ToArray()));
                }

                //users.Send()

                Console.WriteLine("LLM Response: {0}", chatBotAnswer);
            }
            //user.AgentController.ReqLoginInfo();
            //var binaryReader = new BinaryReader(dms, Encoding.UTF8, true);
            //BitConverter.ToInt64(bytes);
            //bytes
            //var utf8Str = Encoding.UTF8.GetString(bytes);

            //Console.WriteLine("LLM Response: {0}", utf8Str);

            ObjectController.Instance().Enqueue(user);
            //return;
        }

    }
}
