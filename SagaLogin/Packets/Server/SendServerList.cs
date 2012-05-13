using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SendServerList : Packet
    {
        byte numberOfServers;

        public SendServerList()
        {
            this.data = new byte[5];
            this.offset = 4;
            this.ID = 0x0102;
            this.numberOfServers = 0;
        }

        public void AddServer(byte index,string serverName,short playerCount,CharServer.Status ping)
        {
            this.numberOfServers++;

            byte[] tempdata = new byte[this.data.Length +39];

            this.data.CopyTo(tempdata, 0);
            this.data = tempdata;

            this.PutByte(this.numberOfServers, 4);

            int currentPos = 5 + ((this.numberOfServers-1) * 39);
             
            //server index
            //this.PutByte((byte)(this.numberOfServers-1),(ushort)currentPos);
            this.PutByte(index,(ushort)currentPos);
            //server name
            if(serverName.Length > 16) serverName = Global.SetStringLength(serverName, 16);
            this.PutString(serverName, (ushort)(currentPos + 1));     
           //server ping
            this.PutByte((byte)ping,(ushort)(currentPos + 1 + 34));
            //chars on server
            this.PutByte((byte)playerCount,(ushort)(currentPos + 1 + 34 + 1));

           }



    }
}
