using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB;

namespace SagaLogin.Packets.Server
{
    public enum LoginError
    {
        NO_ERROR = 0x0, WRONG_PASS = 0x3, ALREADY_CONNECTED = 0x7, WRONG_USER = 0xC,
        NOT_TEST_ACCOUNT = 0xD, SERVER_IN_TESTMODE = 0xE, LOGIN_SERVER_OFFLINE = 0xF,
        GAME_SERVER_OFFLINE = 0x10, ACCOUNT_NOT_ACTIVATED = 0x11
    }

    public class LoginAnswer : Packet
    {
        public LoginAnswer()
        {
            this.data = new byte[41];
            this.offset = 4;
            //cb2
            //this.ID = 0x0102;
            //cb3
            this.ID = 0x0101;
        }

        public void SetGender(GenderType gender)
        {
            this.PutByte((byte)gender,4);
        }

        public void SetMaxCharsAllowed(byte maxChars)
        {
            this.PutByte(maxChars,5);
        }

        public void SetLastLogin(string lastLogin)
        {
            lastLogin = Global.SetStringLength(lastLogin, 16);
            this.PutString(lastLogin,6);
        }


        public void SetLoginError(LoginError error)
        {
            this.PutByte((byte)error,40);
        }

    }

}
