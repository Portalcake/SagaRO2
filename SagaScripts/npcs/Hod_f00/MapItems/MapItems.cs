using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Hod00_Item0 : MapItem
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 9;
        Name = "Hod00_Item_0";
        StartX = -28208F;
        StartY = 2384F;
        StartZ = 3316F;
        Startyaw = 0;
    }

    public override void OnOpen(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 9, 902) == StepStatus.Active && CountItem(pc, 2621) == 0)
        {
            ClearNPCItem();
            AddNPCItem(2621);
            SendLootList(pc);
        }
        SetAnimation(pc, 2);
    }
}

public class Hod00_Item1 : MapItem
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 10;
        Name = "Hod00_Item_1";
        StartX = -26860F;
        StartY = 3004F;
        StartZ = 3271F;
        Startyaw = 0;
    }
    public override void OnOpen(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 9, 902) == StepStatus.Active && CountItem(pc, 2622) == 0)
        {
            ClearNPCItem();
            AddNPCItem(2622);
            SendLootList(pc);
        }
        SetAnimation(pc, 2);
    }
}

public class Hod00_Item2 : MapItem
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 11;
        Name = "Hod00_Item_2";
        StartX = -27452F;
        StartY = 1960F;
        StartZ = 3225F;
        Startyaw = 0;
    }
    public override void OnOpen(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 9, 902) == StepStatus.Active && CountItem(pc, 2623) == 0)
        {
            ClearNPCItem();
            AddNPCItem(2623);
            SendLootList(pc);
        }
        SetAnimation(pc, 2);
    }
}

public class Hod00_Item4 : MapItem
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 48;
        Name = "Hod00_Item_4";
        StartX = -24082.9F;
        StartY = 1727.788F;
        StartZ = 2946.835F;
        Startyaw = -772;
    }
}

public class Hod00_Item9 : MapItem
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 5000;
        Name = "Hod00_Item_9";
        StartX = 19734.41F;
        StartY = 14858.25F;
        StartZ = 2183.412F;
        Startyaw = 18696;
    }
}

public class Hod00_Item10 : MapItem
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 5000;
        Name = "Hod00_Item_10";
        StartX = 19625.06F;
        StartY = 15065.73F;
        StartZ = 2184.206F;
        Startyaw = 0;
    }
}

public class Hod00_Item13 : MapItem
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 48;
        Name = "Hod00_Item_13";
        StartX = -5148.679F;
        StartY = 16833.43F;
        StartZ = 3946.146F;
        Startyaw = -44924;
    }
}

public class Hod00_Item15 : MapItem
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 1374921898;
        Name = "Hod00_Item_15";
        StartX = -557662.3F;
        StartY = 284082.9F;
        StartZ = 1436009F;
        Startyaw = 477144463;
    }
}

