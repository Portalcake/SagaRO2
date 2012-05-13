using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    /// <summary>
    /// Take chat input from client.
    /// </summary>
    public class GetChat : Packet
    {
        public enum MESSAGE_TYPE { NORMAL, PARTY, YELL, SYSTEM_MESSAGE, CHANEL, SYSTEM_MESSAGE_RED };
        /// <summary>
        /// Constructor
        /// </summary>
        public GetChat()
        {
            this.offset = 4;
            this.size = 8; // minimum size
        }

        public override bool isStaticSize() { return false; }


        public bool isValid()
        {
            ushort packetSize = this.GetUShort(0);
            byte textSize = this.GetByte(5);
 
            if ((textSize + 10) == packetSize) return true;
            else return false;
        }


        public bool isAtCommand()
        {
            if (this.data[10] == '/' ||  this.data[10] == '!' || this.data[10] == '~')
                return true;
            else
                return false;
        }


        public MESSAGE_TYPE GetMessageType()
        {
            return (MESSAGE_TYPE)this.GetByte(4);
        }

        /// <summary>
        /// Get the Message 
        /// </summary>
        /// <returns>the Message as string</returns>
        public string GetMessage()
        {
            if (this.isValid())
            {
                return this.GetStringFixedSize(6, this.GetByte(5) );
            }
            else return "NOT_VALID";

        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetChat();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSendChat(this);
        }
    }
}
