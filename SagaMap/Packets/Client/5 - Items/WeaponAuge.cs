using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB;
using SagaDB.Items;

namespace SagaMap.Packets.Client
{

    public class WeaponAuge : Packet
    {
        public WeaponAuge()
        {
            this.size = 7;
            this.offset = 4;
        }

        public byte GetSlot()
        {
           return this.GetByte(4);
        }

        public byte GetWeaponSlot()
        {
            return this.GetByte(6);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.WeaponAuge();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnWeaponAuge(this);
        }

    }
}
