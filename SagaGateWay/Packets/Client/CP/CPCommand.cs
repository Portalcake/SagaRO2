using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Client.CP
{
    /// <summary>
    /// Client packet that contains the key user by the client.
    /// </summary>
    public class CPCommand : Packet
    {
        /// <summary>
        /// Create an empty send key packet
        /// </summary>
        public CPCommand()
        {
            this.size = 11;
            this.offset = 4;
        }

        public string GetCommand()
        {
            System.Text.ASCIIEncoding enc = new ASCIIEncoding();
            byte[] buf = new byte[this.data.Length - 8];
            Array.Copy(this.data, 8, buf, 0, buf.Length);
            return enc.GetString(buf);
        }

        public override bool isStaticSize()
        {
            return false;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaGateway.Packets.Client.CP.CPCommand();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((ControlPanelClient)(client)).OnCPCommand(this);
        }

    }
}
