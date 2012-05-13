using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class KafraMailBoxPrt04_1 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "Prt_f04";        
        Name = "KafraMailBoxPrt04_1";
        StartX = 44296.03F;
        StartY = 38473.31F;
        StartZ = -12068F;
        Startyaw = -24896;
    }
}

public class KafraMailBoxPrt04_2 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "Prt_f04";
        Name = "KafraMailBoxPrt04_2";
        StartX = -17878.46F;
        StartY = -12231.86F;
        StartZ = -11354.78F;
        Startyaw = -59860;
    }
}

public class KafraMailBoxPrt04_3 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "Prt_f04";
        Name = "KafraMailBoxPrt04_3";
        StartX = 33042.48F;
        StartY = -35935.73F;
        StartZ = -11234.47F;
        Startyaw = -73476;
    }
}