using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Hod2
{
    public class KafraMailBoxHod02_1 : KafraMailBox
    {
        public override void OnSub()
        {
            MapName = "Hod_f02";
            Type = 1123;
            Name = "Item_1";
            StartX = 39552F;
            StartY = -30560F;
            StartZ = 3476F;
            Startyaw = 10000;
        }
    }

    public class KafraMailBoxHod02_2 : KafraMailBox
    {
        public override void OnSub()
        {
            MapName = "Hod_f02";
            Type = 1123;
            Name = "Item_23";
            StartX = 10080F;
            StartY = -14496F;
            StartZ = 1292F;
            Startyaw = 33000;
        }
    }   
}