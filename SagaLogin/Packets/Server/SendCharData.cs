using System;
using System.Collections.Generic;
using System.Text;

/*byte[] data = 
            {  
                0x01, 0x07, 
                0xCA, 0x08, 0x00, 0x00, // JExp
                0x03, // race
                0x03, // Eye
                0x16, // Eye color
                0x02, // Eyebrow
                0x0C, // Hair
                0x17, // Hair color
                0x01, // Pattern?
                0x01, // Ear type?
                0x01, // Horn type?
                0x01, // Horn color?
                0x01, // Wing type?
                0x01, // Wing color?
                0x9E, 0x0D, 0x00, 0x00, // Top head ItemId
                0xD1, 0x0D, 0x00, 0x00, // Middle head ItemId
                0x00, 0x00, 0x00, 0x00, // Bottom head ItemId
                0x9D, 0x0C, 0x00, 0x00, // Body ItemId
                0x00, 0x00, 0x00, 0x00, // Pants ItemId
                0x00, 0x00, 0x00, 0x00, // Hands ItemId
                0x00, 0x00, 0x00, 0x00, // Legs ItemId
                0x00, 0x00, 0x00, 0x00, // Belt ItemId
                0x00, 0x00, 0x00, 0x00, // Back ItemId
                0x00, 0x00, 0x00, 0x00, // Right ring ItemId
                0x00, 0x00, 0x00, 0x00, // Left ring ItemId
                0x00, 0x00, 0x00, 0x00, // Necklet ItemId
                0x00, 0x00, 0x00, 0x00, // Earring ItemId
                0x00, 0x00, 0x00, 0x00, // Ammo ItemId
                0x00, 0x00, 0x00, 0x00, // Left shield1 ItemId
                0x00, 0x00, 0x00, 0x00, // Left shield2 ItemId
                0xF1, 0x49, 0x02, 0x00, // AugeSkill Id(weapon)
                0x00, 0x00, 0x00, 0x00  // ??
            };
*/

using SagaLib;
using SagaDB.Actors;

namespace SagaLogin.Packets.Server
{

    public class SendCharData : Packet
    {
        public SendCharData()
        {
            this.data = new byte[109];
            this.offset = 4;
            this.ID = 0x0105;

            this.SetUnknown1();
        }

        public void SetJobExp(uint jobExp)
        {
            this.PutUInt(jobExp, 4);
        }

        public void SetFace(byte eye, byte eyeColor, byte eyebrow, byte hair, byte hairColor)
        {
            this.PutByte(eye,8);
            this.PutByte(eyeColor,9);
            this.PutByte(eyebrow,10);
            this.PutByte(hair,11);
            this.PutByte(hairColor,12);
        }


        public void SetUnknown1()
        {
            for (int i = 0; i < 6; i++) this.PutByte(1, (ushort)(13 + i));
        }

        public void SetEquipment(int[] equip) // int[18]
        {
            for (int i = 0; i < equip.Length && i < 16; i++)
            {
                this.PutInt(equip[i], (ushort)(19 + (i * 5)) );//cb3 changed this to Uint + 1 byte(Unknown)
            }
        }

        public void SetShield(uint id)
        {
            this.PutUInt(id, 99);
        }

        public void SetAugeSkill(uint auge)
        {
            this.PutUInt(auge, 104);
        }

        public void SetSaveMap(byte map)
        {
            this.PutByte(map, 108);
        }

    }

}

