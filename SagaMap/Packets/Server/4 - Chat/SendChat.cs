using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SagaLib;


namespace SagaMap.Packets.Server
{
    
    public class SendChat : Packet
    {
        public enum MESSAGE_TYPE { NORMAL, PARTY, YELL, SYSTEM_MESSAGE, CHANEL, SYSTEM_MESSAGE_RED };

        private ushort textLength;

        static private ushort maxTextLength = 127;

        public SendChat(int textLength)
        {
            if ((ushort)textLength > SendChat.maxTextLength) textLength = maxTextLength;
            this.textLength = (ushort)textLength;
            this.data = new byte[40 + textLength*2];
            this.offset = 4;
            this.ID = 0x0401;

            byte len = (byte)(textLength * 2);
            this.PutByte(len, 39);
        }

        public void SetMessageType(MESSAGE_TYPE type)
        {
            this.PutByte((byte)type, 4);
        }

        public void SetName(string name)
        {
            name = Global.SetStringLength(name, 16);
            this.PutString(name, 5);
        }

        public void SetMessage(string text)
        {
            if (text.Length == this.textLength)
            {
                text = Global.SetStringLength(text, SendChat.maxTextLength);
                this.PutString(text, 40);
            }
        }
    }
}
