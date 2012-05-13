using System;
using System.Collections.Generic;

using SagaMap;

using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f02
{
    public class PronToHode : Ship
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 50002;
            Name = "Ship to Hodemimes";
            StartX = 33197F;
            StartY = -31890F;
            StartZ = -4542;
            Startyaw = 50000;
            Init(6, -7502f, -16069f, 436f, 2);
        }

    }
}