using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Login.Get
{
    /// <summary>
    /// Client sending a GUID to the server.
    /// </summary>
    public class SendToMap : Packet
    {
        /// <summary>
        /// Create a SendGUID packet.
        /// </summary>
        public SendToMap()
        {
            this.size = 18;
            this.offset = 4;
        }

        public string GetIPAddress()
        {
            byte[] tmp;
            string ip;
            tmp = this.GetBytes(4, 4);
            ip = tmp[3].ToString() + ".";
            ip = ip + tmp[2].ToString() + ".";
            ip = ip + tmp[1].ToString() + ".";
            ip = ip + tmp[0].ToString();
            return ip;
        }

        public ushort GetPort()
        {
            return this.GetUShort(8);
        }

        public uint GetCharID()
        {
            return this.GetUInt(10);
        }

        public uint GetValidationKey()
        {
            return this.GetUInt(14);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaGateway.Packets.Login.Get.SendToMap();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginSession)(client)).OnSendToMap(this);
        }


    }
}