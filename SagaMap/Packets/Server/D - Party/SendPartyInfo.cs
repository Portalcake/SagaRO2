using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendPartyInfo : Packet
    {
        public SendPartyInfo()
        {
            this.data = new byte[286];
            this.ID = 0x0D04;
        }

        public void SetLeaderIndex(byte u)
        {
            this.PutByte(u, 4);
        }

        public void SetLeader(uint id)
        {
            this.PutUInt(id, 5);
        }

        public void SetSetting1(byte s1)
        {
            this.PutByte(s1, 9);
        }

        public void SetSetting2(byte s2)
        {
            this.PutByte(s2, 10);
        }

        public void SetSetting3(byte s3)
        {
            this.PutByte(s3, 11);
        }

        public void SetSetting4(uint s4)
        {
            this.PutUInt(s4, 12);
        }

        public void SetMemberInfo(List<MapClient> Clients)
        {
            int i = 0;
            foreach (MapClient client in Clients)
            {
                //this.PutByte((byte)(i + 1), (ushort)(16 + i * 54));//index
                this.PutByte(1, (ushort)(16 + i * 54));//index
                this.PutUInt(client.Char.id, (ushort)(17 + i * 54));
                string name = client.Char.name;
                Global.SetStringLength(name, 16);
                this.PutString(name, (ushort)(21 + i * 54));
                this.PutByte((byte)(client.Char.mapID + 0x65), (ushort)(55 + i * 54));//unknown
                this.PutByte((byte)(client.Char.Race), (ushort)(56 + i * 54));
                this.PutByte(client.Char.mapID, (ushort)(57 + i * 54));//map
                this.PutUShort(client.Char.maxHP, (ushort)(58 + i * 54));
                this.PutUShort(client.Char.HP, (ushort)(60 + i * 54));
                this.PutUShort(client.Char.maxSP, (ushort)(62 + i * 54));
                this.PutUShort(client.Char.SP, (ushort)(64 + i * 54));
                this.PutByte(client.Char.LP, (ushort)(66 + i * 54));
                this.PutByte((byte)client.Char.cLevel, (ushort)(67 + i * 54));
                this.PutByte((byte)client.Char.job, (ushort)(68 + i * 54));
                this.PutByte((byte)client.Char.jLevel, (ushort)(69 + i * 54));
                i++;
            }
        }
    }
}
