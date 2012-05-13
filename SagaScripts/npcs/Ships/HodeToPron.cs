using System;
using System.Collections.Generic;

using SagaMap;

using SagaDB.Actors;
using SagaDB.Items;

namespace Hod02
{
    public class HodeToPron : Ship
    {
        public override void OnInit()
        {
            MapName = "Hod_f02";
            Type = 50001;
            Name = "Ship to Prontera";
            StartX = -25368.5F;
            StartY = -58447.53F;
            StartZ = 850;
            Startyaw = 16835;
           //Must Add waypoint after init
            Init(2, 27810.89f, -32505.42f, -4942.582f, 6);
            AddApprochWaypoint(-25368.5f, -58447.53f, 850f, 16835);
            AddApprochWaypoint(-17803.86f, -46746.76f, 850f, 17000);
            AddApprochWaypoint(-11053f, -16460f, 850f, 18000);
            AddDepartureWaypoint(-11053f, -16460f, 850f, 18000);
            AddDepartureWaypoint(-22008.44f, 2806.83f, 850f, 31724);
            AddDepartureWaypoint(-54599.51f, -4439.462f, 850f, 43453);
            Start();
        }

    }
}