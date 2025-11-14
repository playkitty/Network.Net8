using Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServerModule
{
    public sealed class BytesPacketInvoker : IPacketInvoker
    {
        private Dictionary<Type, MethodInfo> packetMethods = null;
        private Dictionary<int, Type> packetTypes = null;
        private IPacketReceiver receiver = null;
        public BytesPacketInvoker()
        {

        }

        public bool Init(Dictionary<Type, MethodInfo> packetMethods, Dictionary<int, Type> packetTypes, IPacketReceiver receiver)
        {
            this.packetMethods = packetMethods;
            this.packetTypes = packetTypes;
            this.receiver = receiver;

            return true;
        }

        public bool Deserialize(int id, MemoryStream dms, User user)
        {
            dms.Position = 0;
            var binaryReader = new BinaryReader(dms, Encoding.UTF8, true);
            var bytes = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
            //var utf8Str = Encoding.UTF8.GetString(bytes);

            if (this.packetTypes.TryGetValue(id, out var type) == false)
            {
                return false;
            }

            if (this.packetMethods.TryGetValue(type, out var method) == false)
            {
                return false;
            }

            //var info = ProtoBuf.Serializer.Deserialize(type, dms);

            //int size = Marshal.SizeOf(type);
            //IntPtr ptr = Marshal.AllocHGlobal(size);
            //Marshal.Copy(bytes, 0, ptr, size);
            //var structure = Marshal.PtrToStructure(ptr, type);
            //Marshal.FreeHGlobal(ptr);

            method.Invoke(this.receiver, new object[] { user, null, bytes });

            return true;

            String question = "이름이 뭐야?";
            var quesBytes = Encoding.UTF8.GetBytes(question);

            BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
            binaryWriter.Write(Convert.ToInt32(101));
            binaryWriter.Write(4+4+quesBytes.Count());
            binaryWriter.Write(quesBytes);

            BinaryReader reader = new BinaryReader(binaryWriter.BaseStream);
            reader.BaseStream.Position = 0;
            var buff = reader.ReadBytes((int)reader.BaseStream.Length);

            //var quesBytes = Encoding.UTF8.GetBytes(buff);
            user.Send(buff);
            //user.Send()
            //dms.
            //throw new NotImplementedException();

            return true;
        }
    }
}
