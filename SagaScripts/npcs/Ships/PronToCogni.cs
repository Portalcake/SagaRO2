using System;
using System.Collections.Generic;

using SagaMap;

using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f02
{
    public class PronToCogni : Ship
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 50003;
            Name = "Ship to Cognito";
            StartX = 35894F;
            StartY = -36471F;
            StartZ = -4402;
            Startyaw = 50000;
            Init(6, 13994f, -6728f, -5475f, 13);
        }

    }
}