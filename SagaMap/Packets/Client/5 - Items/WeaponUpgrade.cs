using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB;
using SagaDB.Items;

namespace SagaMap.Packets.Client
{
    public class WeaponUpgrade : Packet
    {
        public WeaponUpgrade()
        {
            this.size = 15;
            this.offset = 4;

            //data unknown
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0 only observed</returns>
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.WeaponUpgrade();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnWeaponUpgrade(this);
        }

    }
}
