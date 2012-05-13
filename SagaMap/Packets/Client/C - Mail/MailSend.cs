using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class MailSend : Packet
    {
        public MailSend() 
        {
            this.size = 492;
        }

        public string GetName()
        {
            return Global.SetStringLength(this.GetString(4), 16);
        }

        public byte Unknown()
        {
            return this.GetByte(38);
        }

        public byte GetSlot()
        {
            return this.GetByte(39);
        }

        public byte GetAmount()
        {
            return this.GetByte(40);
        }

        public uint GetZeny()
        {
            return this.GetUInt(44);
        }

        public string GetTopic()
        {
            return Global.SetStringLength(this.GetString(48), 19);
        }

        public string GetContent()
        {
            return Global.SetStringLength(this.GetString(90), 200);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.MailSend();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMailSend(this);
        }
    }
}
