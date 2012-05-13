using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class SendIdentity : Packet
    {
        public SendIdentity()
        {
            this.size = 16;
            this.offset = 4;
        }

        public uint GetCharID()
        {
            return this.GetUInt(4);
        }

        public uint GetValidationKey()
        {
            return this.GetUInt(8);
        }

        public uint GetSessionID()
        {
            return this.GetUInt(12);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SendIdentity();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSendIdentity(this);
        }
    }
}
