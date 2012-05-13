using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class MissionBoardcog_02_01 : MissionBoardcog_01_01
{
    public override void OnSub()
    {
        MapName = "cog_f02";
        Type = 1131;
        Name = "MissionBoardcog_02_01";
        StartX = -36576F;
        StartY = -33520F;
        StartZ = -23960F;
        Startyaw = 0;
    }
}

public class MissionBoardcog_02_02 : MissionBoardcog_01_01
{
    public override void OnSub()
    {
        MapName = "cog_f02";
        Type = 1131;
        Name = "MissionBoardcog_02_02";
        StartX = -16492.66F;
        StartY = -18178.39F;
        StartZ = -23408F;
        Startyaw = 16386;
    }
}

public class MissionBoardcog_02_03 : MissionBoardcog_01_01
{
    public override void OnSub()
    {
        MapName = "cog_f02";
        Type = 1131;
        Name = "MissionBoardcog_02_03";
        StartX = 33231.98F;
        StartY = -3327.492F;
        StartZ = -23696F;
        Startyaw = -12378;
    }
}