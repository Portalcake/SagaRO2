using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class ItemToggle : Packet
    {
        public ItemToggle()
        {
            this.size = 15;
            this.offset = 4;
        }

        public byte GetSkillType()
        {
            return this.GetByte(4);
        }

        public byte GetContainer()
        {
            return this.GetByte(5);
        }

        public byte GetIndex()
        {
            return this.GetByte(6);
        }

        public uint GetSkillID()
        {
            return this.GetUInt(7);
        }

        public uint GetTargetID()
        {
            return this.GetUInt(11);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.ItemToggle();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnItemToggle(this);
        }
    }
}


