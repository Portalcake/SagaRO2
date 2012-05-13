using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;


namespace SagaLib
{
    public class Client
    {
        public NetIO netIO;
        public uint SessionID;

        public Client()
        {

        }

        public Client(Socket mSock, Dictionary<ushort, Packet> mCommandTable)
        {
            this.netIO = new NetIO(mSock, mCommandTable, this, null);
        }

        virtual public void OnConnect()
        {
            byte[] tempServerKey = Encryption.GenerateKey();
            byte[] expandedServerKey = Encryption.GenerateDecExpKey(tempServerKey);

            Packets.Server.SendKey sendPacket = new Packets.Server.SendKey();
            sendPacket.SetKey(expandedServerKey);
            sendPacket.SetCollumns(4);
            sendPacket.SetRounds(10);
            sendPacket.SetDirection(2);
            this.netIO.SendPacket(sendPacket, this.SessionID);

            this.netIO.ServerKey = tempServerKey;
        }

        public virtual void OnDisconnect() { }

    }
}