using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class AddShortcut : Packet
    {
        public AddShortcut()
        {
            this.size = 10;
        }

        public byte GetShortcutType()
        {
            return this.GetByte(4);
        }

        public byte GetSlotNumber()
        {
            byte tmp=this.GetByte(5);
            if(tmp==0)tmp=0x0A;
            return tmp;
        }

        public uint GetIDNumber()
        {
            return this.GetUInt(6);
        }
        
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.AddShortcut();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnAddShortcut(this);
        }
    }
}
