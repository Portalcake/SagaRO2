using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class KafraMailBoxHod01_1 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "Hod_f01";        
        Name = "KafraMailBoxHod01_1";
        StartX = 3157.256F;
        StartY = -14286.34F;
        StartZ = -6479F;
        Startyaw = 40000;
    }
}

public class KafraMailBoxHod01_2 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "Hod_f01";
        Name = "KafraMailBoxHod01_2";
        StartX = 2434.902F;
        StartY = -3981.677F;
        StartZ = -8476F;
        Startyaw = 30000;
    }
}

public class KafraMailBoxHod01_3 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "Hod_f01";
        Name = "KafraMailBoxHod01_3";
        StartX = -8821.073F;
        StartY = -1603.447F;
        StartZ = -9344F;
        Startyaw = 40000;
    }
}

public class KafraMailBoxHod01_4 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "Hod_f01";
        Name = "KafraMailBoxHod01_4";
        StartX = 2168.297F;
        StartY = 6960.776F;
        StartZ = -10107F;
        Startyaw = 0;
    }
}