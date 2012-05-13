using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;

/*
                0x01, 
                0x06, 
                0xAC, 0xB9, 0x04, 0xD5, 0xB8, 0xD2, 0x7C, 0xB7, //|
                0xE4, 0xC2, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //|
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //| Server name (wchar[17])
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //|
                0x00, 0x00,                                     //|
                0x02,                   // Supposed to be characters count on all servers (you can't 
                                        // create new character if value >= 3)
                0x02,                   // Characters count for current server

                // 1st char
                0x72, 0x02, 0x00, 0x00, // CharID?
                0x53, 0x00, 0x61, 0x00, 0x67, 0x00, 0x61, 0x00, //|
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //|
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //| Character name (wchar[17]) 
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //| 
                0x00, 0x00,                                     //|
                0x00,                   // Race (0 - Norman)
                0xCA, 0x08, 0x00, 0x00, // CExp
                0x01,                   // Job (1 - Novice)
                0x00,                   // 0 - active, 1 - pending deletion

                // 2nd char
                0x58, 0x08, 0x00, 0x00,  
                0x53, 0x00, 0x6F, 0x00, 0x67, 0x00, 0x61, 0x00, 
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                0x00, 0x00, 
                0x00,
                0x00, 0x00, 0x00, 0x00,
                0x01, 
                0x00, 

*/
using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SendCharList : Packet
    {
        byte numberOfChars;

        public SendCharList()
        {
            this.data = new byte[40];
            this.offset = 4;
            this.ID = 0x0104;
            this.numberOfChars = 0;
            this.data[4] = 0;
        }

        public void SetServerName(string serverName)
        {
            serverName = Global.SetStringLength(serverName,16);
            this.PutString(serverName, 5);
        }

        public void SetCharCountAllServer(byte count)
        {
            this.PutByte(count, 39);
        }


        public void AddChar(uint charID, string charName, RaceType charRace, uint cExp, JobType job, byte pendingDeletion)
        {
            this.numberOfChars++;

            byte[] tempdata = new byte[this.data.Length + 47];

            this.data.CopyTo(tempdata, 0);
            this.data = tempdata;
            int currentPos = 40;

            this.PutByte(this.numberOfChars, (ushort)currentPos);

            currentPos = 41 + ((this.numberOfChars - 1) * 46);

            this.PutUInt(charID, (ushort)currentPos);

            if (charName.Length > 16) charName = Global.SetStringLength(charName, 16);
            this.PutString(charName, (ushort)(currentPos + 4));

            this.PutByte((byte)charRace, (ushort)(currentPos + 4 + 34));

            this.PutUInt(cExp, (ushort)(currentPos + 4 + 34 + 1));

            this.PutByte((byte)job, (ushort)(currentPos + 4 + 34 + 1 + 4));

            this.PutByte(pendingDeletion, (ushort)(currentPos + 4 + 34 + 1 + 4 + 1));

        }



    }
}
