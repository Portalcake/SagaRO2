using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

/*
            byte[] data = { 
                0x01, 0x04,
                0x01, 0x0, 0x0, 0x7F,
                0x40, 0x1F, 
                0xDE, 0xF8, 0x07, 0x00,
                0x31, 0xE0, 0x53, 0x04 };

 */

namespace SagaLogin.Packets.Server
{
    public class SendToMapServer : Packet
    {
        public SendToMapServer()
        {
            this.data = new byte[18];
            this.offset = 4;
            this.ID = 0x0106;
        }

        public void SetServer(string serverIP,ushort serverPort)
        {
            System.Net.IPAddress sIP = System.Net.IPAddress.Parse(serverIP);
            byte[] ipAdr = sIP.GetAddressBytes();
            this.PutByte(ipAdr[3], 4);
            this.PutByte(ipAdr[2], 5);
            this.PutByte(ipAdr[1], 6);
            this.PutByte(ipAdr[0], 7);
            this.PutUShort(serverPort, 8);

        }

        public void SetValidation(uint validation1, uint validation2)
        {
            this.PutUInt(validation1, 10);
            this.PutUInt(validation2, 14);
        }

    }

}