using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SendAck : Packet
    {
        public SendAck()
        {
            this.data = new byte[5];
            this.offset = 4;
            this.ID = 0x0101;
        }

        public void SetAck(bool noerror)
        {
            if (noerror) this.PutByte(0, 4); //no error
            else this.PutByte(1, 4); // error
        }

    }

}