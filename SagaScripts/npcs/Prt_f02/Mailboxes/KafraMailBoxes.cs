using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class KafraMailBoxPrt02_1 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "Prt_f02";        
        Name = "KafraMailBoxPrt02_1";
        StartX = -16822.93F;
        StartY = -31360.58F;
        StartZ = -816F;
        Startyaw = 0;
    }
}

public class KafraMailBoxPrt02_2 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "Prt_f02";
        Name = "KafraMailBoxPrt02_2";
        StartX = 37373.95F;
        StartY = -16372.13F;
        StartZ = -3915.265F;
        Startyaw = -26000;
    }
}

public class KafraMailBoxPrt02_3 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "Prt_f02";
        Name = "KafraMailBoxPrt02_3";
        StartX = -12242.26F;
        StartY = 3244.763F;
        StartZ = -587.709F;
        Startyaw = -38536;
    }
}