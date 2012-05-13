using System;
using System.Collections.Generic;

using SagaMap;

using SagaDB.Actors;
using SagaDB.Items;

namespace cog_f01_02
{
    public class CogniToPron : Ship
    {
        public override void OnInit()
        {
            MapName = "cog_f01_02";
            Type = 50004;
            Name = "Ship to Prontera";
            StartX = 16304F;
            StartY = -7749F;
            StartZ = -5000;
            Startyaw = 9975;
            Init(13, 29618f, -32418f, -4942f, 6);
        }

    }
}