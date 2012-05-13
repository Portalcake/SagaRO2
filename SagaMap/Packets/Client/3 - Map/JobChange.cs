using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class JobChange : Packet
    {
        public JobChange()
        {
            this.size = 9;
            this.offset = 4;
        }

        public byte GetJob()
        {
            return this.GetByte(4);
        }

        public byte GetChangeWeapon()
        {
            return this.GetByte(5);
        }

        public ushort GetPostFix()
        {
            return this.GetUShort(7);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.JobChange();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnJobChange(this);
        }
    }
}
