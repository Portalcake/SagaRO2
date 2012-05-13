using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class PartyMemberInfo : Packet
    {
        public PartyMemberInfo()
        {
            this.data = new byte[24];
            this.ID = 0x0D06;
        }

        public void SetIndex(byte index)
        {
            this.PutByte(index, 4);
        }

        public void SetActorID(uint id)
        {
            this.PutUInt(id, 5);
        }

        public void SetActorInfo(SagaDB.Actors.ActorPC pc)
        {
            this.PutByte((byte)(pc.mapID + 0x65), 9);//Unknown
            this.PutByte((byte)(pc.Race), 10);
            this.PutByte(pc.mapID, 11);
            this.PutUShort(pc.maxHP, 12);
            this.PutUShort(pc.HP, 14);
            this.PutUShort(pc.maxSP, 16);
            this.PutUShort(pc.SP, 18);
            this.PutByte(pc.LP, 20);//Unknown
            this.PutByte((byte)pc.cLevel, 21);
            this.PutByte((byte)pc.job, 22);
            this.PutByte((byte)pc.jLevel, 23);
        }
    }
}
