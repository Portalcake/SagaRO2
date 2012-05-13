using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class KafraMailBoxHod00_1 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "Hod_f00";        
        Name = "KafraMailBoxHod00_1";
        StartX = 3733.418F;
        StartY = -3643.577F;
        StartZ = 1533F;
        Startyaw = -1116;
    }
}

public class KafraMailBoxHod00_2 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "Hod_f00";
        Name = "KafraMailBoxHod00_2";
        StartX = 17668.19F;
        StartY = 7889.467F;
        StartZ = 1869.765F;
        Startyaw = -33984;
    }
}

public class KafraMailBoxHod00_3 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "Hod_f00";
        Name = "KafraMailBoxHod00_3";
        StartX = 7241.447F;
        StartY = 16860.13F;
        StartZ = 2838.833F;
        Startyaw = -8268;
    }
}
