using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class SendWeaponMove : Packet
    {
        public SendWeaponMove()
        {
            this.size = 7;
            this.offset = 4;
        }

        public byte GetDirection()
        {
            return this.GetByte(4);
        }

        public byte GetWeaponSlot()
        {
            return this.GetByte(5);
        }

        public byte GetPosition()
        {
            return this.GetByte(6);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SendWeaponMove();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMoveWeapon(this);
        }
    }
}
