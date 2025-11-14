using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerModule
{
    public class PacketSender : TaskSyncer
    {
        ConcurrentQueue<byte[]> bytes = new ConcurrentQueue<byte[]>();

        public PacketSender()
        {
        }

        public void Send(User user, byte[] sendBytes)
        {
            this.bytes.Enqueue(sendBytes);

            this.Post(new TaskJob(() =>
            {
                BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
                while (this.bytes.Count() != 0)
                {
                    if (this.bytes.TryDequeue(out var bytes) == true)
                    {
                        binaryWriter.Write(bytes);
                    }
                }

                if (binaryWriter.BaseStream.Length == 0)
                    return;

                BinaryReader binaryReader = new BinaryReader(binaryWriter.BaseStream);
                binaryWriter.BaseStream.Position = 0;
                var totalBytes = binaryReader.ReadBytes((int)binaryWriter.BaseStream.Length);

                var sendEventArgs = new SocketAsyncEventArgs();
                sendEventArgs.Completed += user.eventComplated;
                sendEventArgs.UserToken = user;
                sendEventArgs.SetBuffer(totalBytes, 0, totalBytes.Count());

                if (user.Socket.Connected && user.Closed == 0)
                    user.Socket.SendAsync(sendEventArgs);
            }));
        }
    }
}
