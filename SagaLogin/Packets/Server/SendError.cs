using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public enum ERROR_TYPE { NO_ERROR, ERROR1, ERROR2 };

    public class SendError : Packet
    {
        public SendError()
        {
            this.data = new byte[5];
            this.offset = 4;
            this.ID = 0x0103;
        }

        public void SetError(ERROR_TYPE error)
        {
           this.PutByte((byte)error, 4);
        }

    }

}