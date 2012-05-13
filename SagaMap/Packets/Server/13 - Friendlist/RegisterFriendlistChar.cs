using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{

    public class RegisterFriendlistChar : Packet
    {

        public RegisterFriendlistChar()
        {
            this.data = new byte[45];
            this.ID = 0x1301;
            this.offset = 4;
        }

        public void SetName(string name)
        {
            PutString(name, 4);
        }

        public void SetJob(byte job)
        {
            PutByte(job, 40);
        }

        public void SetClvl(byte clvl)
        {
            PutByte(clvl, 41);
        }

        public void SetJlvl(byte jlvl)
        {
            PutByte(jlvl, 42);
        }

        public void SetMap(byte map)
        {
            PutByte(map, 43);
        }

        public void SetReason(byte reason)
        {
            PutByte(reason, 44);
        }

    }
}

