using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actors;

/*
0x4D, 0x00, 
0x03, 0x04, 
0x40, 0x01, 0x00, 0x00, // CharId
0x4B, 0x00, 0x6F, 0x00, 0x4B, 0x00, 0x6F, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, // CharName (wchar[17])
0x91, 0x1E, 0xB2, 0xFE, // x (int, real_coord = x / 1000)
0xEB, 0x86, 0xD0, 0xFF, // y (int, real_coord = x / 1000)
0x73, 0x7F, 0x2D, 0x00, // z (int, real_coord = x / 1000)
0x00, 0x00, 0x00, 0x00, // yaw (int)
 * 
0x00, // Race     
 * 
0x03, // Eyes
0x16, // Eye color
0x02, // Eyebrows
0x0C, // Hair
0x17, // Hair color
 * 
0x01, // Pattern
0x01, // Ear type
0x0C, // Horn type
0x01, // Horn color? (not used)
0x01, // Wing type
0x01, // Wing color? (not used)
 * 
0x00, // Weapon Set Slot 0 (Not set = 0xFF)
0xFF, // Weapon Set Slot 1
0x00, // Current slot
0x32, // Invent slot?
0x32, // ??
0x01, // Weapon slot?
0x00  // ??
 */


namespace SagaMap.Packets.Server
{
    /// <summary>
    /// A packet thats being sent to the client when he has stopped moving. Re-setting it's exact
    /// position and other variables.
    /// </summary>
    public class ActorPlayerInfo : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ActorPlayerInfo()
        {
            this.data = new byte[77];
            this.ID = 0x0303;
            this.offset = 4;

        }

        public void SetActorID(uint pID)
        {
            this.PutUInt(pID, 4);
        }

        public void SetName(string name)
        {
            name = Global.SetStringLength(name, 16);
            this.PutString(name, 8);
        }

        public void SetLocation(float x, float y, float z)
        {
            this.PutFloat(x, 42);
            this.PutFloat(y, 46);
            this.PutFloat(z, 50);
        }

        public void SetYaw(int yaw)
        {
            this.PutInt(yaw, 54);
        }

        public void SetRace(RaceType race)
        {
            this.PutByte((byte)race, 58);
        }

        public void SetFace(byte[] face)
        {
            for (int i = 0; i < 5; i++) this.PutByte(face[i], (ushort)(59 + i));
        }

        public void SetDetails(byte[] details)
        {
            for (int i = 0; i < 6; i++) this.PutByte(details[i], (ushort)(64 + i));
        }

        public void SetSlots(byte[] slots)
        {
            for (int i = 0; i < 7; i++) this.PutByte(slots[i], (ushort)(70 + i));
        }

    }
}
