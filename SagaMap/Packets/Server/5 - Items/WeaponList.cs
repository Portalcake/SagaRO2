using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{
    public class WeaponList : Packet
    {
        public WeaponList()
        {
            this.data = new byte[379];
            this.ID = 0x0510;
            this.offset = 4;
        }

        public void SetWeapon(List<Weapon> weapons)
        {
            for (int i = 0; i < weapons.Count && i < 5; i++)
            {
                this.PutString(Global.SetStringLength(weapons[i].name, 24), (ushort)((i*75)+4));
                this.PutByte(weapons[i].level, (ushort)((i*75)+28));
                this.PutUInt(weapons[i].exp,(ushort)((i*75)+ 29));
                this.PutByte((byte)weapons[i].type, (ushort)((i*75)+33));//Unknown
                this.PutUShort(weapons[i].durability,(ushort)((i*75)+ 34));
                this.PutUShort(weapons[i].U1, (ushort)((i*75)+36));//Unknown
                this.PutUInt(weapons[i].augeSkillID,(ushort)((i*75)+ 38));
                this.PutUInt(weapons[i].stones[0], (ushort)((i*75)+42));//Unkown
                this.PutUInt(weapons[i].stones[1], (ushort)((i * 75) + 46));//Unkown
                this.PutUInt(weapons[i].stones[2], (ushort)((i * 75) + 50));//Unkown
                this.PutUInt(weapons[i].stones[3], (ushort)((i * 75) + 54));//Unkown
                this.PutUInt(weapons[i].stones[4], (ushort)((i * 75) + 58));//Unkown
                this.PutUInt(weapons[i].stones[5], (ushort)((i * 75) + 62));//Unkown
                this.PutUInt(0,(ushort)((i*75)+ 66));//Unkown
                this.PutUInt(0, (ushort)((i*75)+70));//Unkown
                this.PutUInt(0, (ushort)((i*75)+74));//Unkown
                this.PutByte(weapons[i].active, (ushort)((i*75)+78));//Unkown
            }
        }
    }

}

