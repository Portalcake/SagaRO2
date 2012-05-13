using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Map.Send
{
    public enum IdentError
    {
        NO_ERROR, ERROR, MAP_ALREADY_HOSTED
    }

    public class LoginAnswer : Packet
    {
        public LoginAnswer()
        {
            this.data = new byte[5];
            this.offset = 4;
            this.ID = 0xFF01;
        }

        public void SetError(IdentError error)
        {
            this.PutByte((byte)error,4);
        }


    }

}
