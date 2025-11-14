using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServerModule
{
    public sealed class ProtoNetPacketInvoker : IPacketInvoker
    {
        private Dictionary<Type, MethodInfo> packetMethods = null;
        private Dictionary<int, Type> packetTypes = null;
        private IPacketReceiver receiver = null;
        public ProtoNetPacketInvoker()
        {

        }

        public bool Init(Dictionary<Type, MethodInfo> packetMethods, Dictionary<int, Type> packetTypes, IPacketReceiver receiver)
        {
            this.packetMethods = packetMethods;
            this.packetTypes = packetTypes;
            this.receiver = receiver;
            return true;
        }

        public bool Deserialize(int id, MemoryStream dms, User user)//, out MethodInfo method)
        {
            //method
            if (this.packetTypes.TryGetValue(id, out var type) == false)
            {
                return false;
            }

            var info = ProtoBuf.Serializer.Deserialize(type, dms);
            if (this.packetMethods.TryGetValue(info.GetType(), out var method) == false)
            {
                return false;
            }

            method.Invoke(this.receiver, new object[] { user, info });
            return true;
        }
    }
}
