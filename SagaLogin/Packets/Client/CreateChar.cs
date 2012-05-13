using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class CreateChar : Packet
    {
        public CreateChar()
        {
            this.size = 76;
            this.offset = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CreateChar();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnCreateChar(this);
        }

        public string GetCharName()
        {
            return this.GetString(4);
        }
        public byte GetEye()
        {
            return this.GetByte(39);
        }
        public byte GetEyeColor()
        {
            return this.GetByte(40);
        }
        public byte GetEyebrows()
        {
            return this.GetByte(41);
        }
        public byte GetHair()
        {
            return this.GetByte(42);
        }
        public byte GetHairColor()
        {
            return this.GetByte(43);
        }
        public string GetWeaponName()
        {
            return this.GetString(50);
        }
        public ushort GetWeaponNameType()
        {
            return this.GetUShort(74);
        }
    }
}

