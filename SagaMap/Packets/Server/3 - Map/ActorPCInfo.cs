using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB;
using SagaDB.Actors;
/*
0xE4 0x00, 0x00, 0x00, // objectID
0x01,                  // gender

0xA3, 0x39, 0x88, 0xFE, // x (int, real_coord = x / 1000)
0x85, 0x8D, 0xF9, 0xFF, // y (int, real_coord = x / 1000)
0xF9, 0x56, 0x34, 0x00, // z (int, real_coord = x / 1000)
0x91, 0xA2, 0xFF, 0xFF, // yaw (int)

//off_25:
0x34, 0xD7, 0xE0, 0xAC, 0xA0, 0xBC, 0x74, 0xB9, 0xCC, 0xB9,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, // name (wchar 17)
0x00, //77h Race
 * 
0x05, //79h Eyes
0x19, //7Ah Eye color
0x01, //7Bh Eyebrows
0x08, //7Ch Hair
0x20, //7Dh Hair color
 * 
0x01, //7Eh Pattern
0x01, //7Fh Ear type
0x08, //80h Horn type
0x01, //81h Horn color? (not used)
0x01, //82h Wing type
0x01, //83h Wing color? (not used)
 * 
0x9E, 0x0D, 0x00, 0x00, //84h Top head ItemId
0x00, 0x00, 0x00, 0x00, //88h Middle head ItemId
0x00, 0x00, 0x00, 0x00, //8Ch Bottom head ItemId
0x00, 0x00, 0x00, 0x00, //90h Left shield ItemId
0xF1, 0x49, 0x02, 0x00, //a8h AugeSkill Id (weapon)
0x88, 0x0C, 0x00, 0x00, //94h Body ItemId
0x00, 0x00, 0x00, 0x00, //98h Pants ItemId
0x00, 0x00, 0x00, 0x00, //9ch Hands ItemId
0x00, 0x00, 0x00, 0x00, //a0h Legs ItemId
0x00, 0x00, 0x00, 0x00, //a4h Back ItemId
 * 
//off_111
0x00, // stance
0x01, //78h Job
0x00, 
0x00
 */

namespace SagaMap.Packets.Server
{

    public class ActorPCInfo : Packet
    {
        public ActorPCInfo()
        {
            this.data = new byte[128];//13bytes extra
            this.offset = 4;
            this.ID = 0x0302;
        }

        public void SetActorID(uint pID)
        {
            this.PutUInt(pID, 4);
        }

        public void SetGender(GenderType gender)
        {
            this.PutByte((byte)gender, 8);
        }

        public void SetLocation(float x, float y, float z)
        {
            this.PutFloat(x, 9);
            this.PutFloat(y, 13);
            this.PutFloat(z, 17);
        }

        public void SetYaw(int yaw)
        {
            this.PutInt(yaw, 21);
        }

        public void SetName(string name)
        {
            name = Global.SetStringLength(name, 16);
            this.PutString(name, 25);
        }

        public void SetRace(RaceType race)
        {
            this.PutByte((byte)race, 59);
        }

        public void SetFace(byte[] face)
        {
            for (int i = 0; i < 5; i++) this.PutByte(face[i], (ushort)(60 + i));
        }

        public void SetDetails(byte[] details)
        {
            for (int i = 0; i < 6; i++) this.PutByte(details[i], (ushort)(65 + i));
        }

        public void SetEquip(int[] equip, byte[] dyes, SagaDB.Items.Weapon activeweapon)
        {
            /*Send equip order: 
             *head top, middle, bottom, left hand, auge, body, pants, hands, legs, back
             *TOP_HEAD = 0, MIDDLE_HEAD = 1, BOTTOM_HEAD = 2, LEFT_HAND = 14
             *Auge Skill ID
             *BODY = 3, PANTS = 4, HANDS = 5, LEGS = 6, BACK = 8
             *4 bytes per equip and 1 byte dye is sent.
            */
            int i;
            //Top, middle, bottom head.
            for (i = 0; i < 3; i++)
            {
                this.PutInt(equip[i], (ushort)(71 + (i * 5)));
                this.PutByte(dyes[i], (ushort)(75 + (i * 5)));
            }
            //Left Hand
            this.PutInt(equip[14], 86);
            this.PutInt(dyes[14], 90);
            //Augeskill ID
            if(activeweapon!=null)this.PutUInt(activeweapon.augeSkillID,91);
            //Body, pants, hands, legs
            for (i = 3; i < 7; i++)
            {
                this.PutInt(equip[i], (ushort)(95 + ((i - 3) * 5)));
                this.PutByte(dyes[i], (ushort)(99 + ((i - 3) * 5)));
            }
            //Back
            this.PutInt(equip[8], 115);
            this.PutByte(dyes[8], 119);
        }

        public void SetJob(JobType job)
        {
            this.PutByte((byte)job, 121);
        }

    }
}
