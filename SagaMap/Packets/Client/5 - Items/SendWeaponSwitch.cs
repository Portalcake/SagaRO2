using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class SendWeaponSwitch : Packet
    {
        public SendWeaponSwitch()
        {
            this.size = 5;
            this.offset = 4;
        }

        public byte GetID()
        {
            return this.GetByte(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SendWeaponSwitch();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnWeaponSwitch(this);
        }
    }
}
