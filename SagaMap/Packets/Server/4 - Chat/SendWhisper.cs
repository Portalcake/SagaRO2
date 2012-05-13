using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendWhisper : Packet
    {
        private ushort textLength;
        static private ushort maxTextLength = 127;

        public SendWhisper(int textLength)
        {
            if ((ushort)textLength > SendWhisper.maxTextLength) textLength = maxTextLength;
            this.textLength = (ushort)textLength;
            this.data = new byte[40 + textLength*2]; //Length 2, ID 2, name 34, unknown 1, message length 1.
            this.offset = 4;
            this.ID = 0x0402;   
            //this.data[38] = 2; // Static ?

            byte length = (byte)(textLength * 2);
            this.PutByte(length, 39);
        }

        public void SetUnknown(byte u)
        {
            this.PutByte(u, 38);
        }

        public void SetName(string name)
        {
            name = Global.SetStringLength(name, 34); 
            this.PutString(name, 4); 
        }

        public void SetMessage(string text)
        {
            if (this.textLength == text.Length)
            {
                text = Global.SetStringLength(text, SendWhisper.maxTextLength);
                this.PutString(text, 40);
            }
        }

    }
}
