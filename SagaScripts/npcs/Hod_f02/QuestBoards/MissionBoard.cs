using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Hod02
{
    public class MissionBoardHod02_1 : MissionBoardHod01
    {
        public override void OnSub()
        {
            MapName = "Hod_f02";
            Type = 1125;
            Name = "Item_0";
            StartX = 39356F;
            StartY = -30848F;
            StartZ = 3440F;
            Startyaw = 10000;
        }
    }

    public class MissionBoardHod02_2 : MissionBoardHod01
    {
        public override void OnSub()
        {
            MapName = "Hod_f02";
            Type = 1125;
            Name = "Item_24";
            StartX = 9680F;
            StartY = -14520F;
            StartZ = 1212.375F;
            Startyaw = 33000;
        }
    }
}