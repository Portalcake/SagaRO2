using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class KafraMailBoxcog_f02_01 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "cog_f02";
        Name = "KafraMailBoxcog_f02_01";
        StartX = -37145.93F;
        StartY = -33507.79F;
        StartZ = -23872F;
        Startyaw = 0;
    }
}

public class KafraMailBoxcog_f02_02 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "cog_f02";
        Name = "KafraMailBoxcog_f02_02";
        StartX = -16508.82F;
        StartY = -17648.73F;
        StartZ = -23456F;
        Startyaw = 16386;
    }
}

public class KafraMailBoxcog_f02_03 : KafraMailBox
{
    public override void OnSub()
    {
        MapName = "cog_f02";
        Name = "KafraMailBoxcog_f02_03";
        StartX = 33433.63F;
        StartY = -3827.104F;
        StartZ = -23588F;
        Startyaw = -12378;
    }
}