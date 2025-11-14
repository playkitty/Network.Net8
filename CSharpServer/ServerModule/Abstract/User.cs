
using Common;
using Protocol;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ServerModule
{
    public abstract class User : TaskSyncer
    {
        public int Closed = 0;
        public Socket Socket;
        public int index = 0;
        //public SocketAsyncEventArgs sendEventArgs = new SocketAsyncEventArgs();
        public SocketAsyncEventArgs recvEventArgs = new SocketAsyncEventArgs();
        public SocketAsyncEventArgs disconnectEventArgs = new SocketAsyncEventArgs();
        public EventHandler<SocketAsyncEventArgs> eventComplated = null;
        //public int ReceiveBufferSize = 0;
        byte[] m_Buffer;                // the underlying byte array maintained by the Buffer Manager

        public ConcurrentQueue<byte[]> bytes = new ConcurrentQueue<byte[]>();

        private PacketSender Sender = new PacketSender();
        protected ServerBaseModule server = null;

        public int ReceiveBufferSize { get => this.server.Config.receiveBandWidth; }
        public int SendBufferSize { get => this.server.Config.sendBandWidth; }
        public int Index { get => this.index; }

        public User()
        {
            recvEventArgs.UserToken = this;
            //m_Buffer = new byte[this.ReceiveBufferSize];
        }

        public void Init(ServerBaseModule server)
        {
            //this.ReceiveBufferSize = receiveSize;
            this.server = server;
            //this.server
            //this.server.Config.receiveBandWidth;
            //m_Buffer = new byte[this.ReceiveBufferSize];
            m_Buffer = new byte[this.server.Config.receiveBandWidth];
        }

        public void Close()
        {
            if (Interlocked.CompareExchange(ref this.Closed, 1, 0) == 0)
            {
                this.Socket.Shutdown(SocketShutdown.Both);
                this.Socket.DisconnectAsync(this.disconnectEventArgs);
                //this.Socket.Close();
                this.Reset();
            }
            //Interlocked.Exchange(ref this.Closed, 1);
            //this.Socket.Shutdown(SocketShutdown.Both);
            //this.Socket.DisconnectAsync(this.disconnectEventArgs);
            //this.Reset();
        }

        public void Reset()
        {
            while (this.bytes.TryDequeue(out var x)) ;

            //this.bytes.re
        }

        public void SetBuffer()
        {
            this.recvEventArgs.SetBuffer(m_Buffer, 0, this.server.Config.receiveBandWidth);
        }

        public void Send(byte[] bytes)
        {
            this.Sender.Send(this, bytes);
        }

        public abstract void OnClosed();
        //protected abstract void OnClose();
    }
}