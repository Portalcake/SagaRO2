using System;
using System.Collections.Generic;

using SagaMap;
  
using SagaDB.Actors;
using SagaDB.Items;

public class startzone02 : MapItem
{
    public override void OnInit()
    {
        MapName = "rag2startzone_01";
        Type = 1143;
        Name = "Startzone02";
        StartX = 1693.506F;
        StartY = 7638.542F;
        StartZ = 50F;
        Startyaw = 8360;
        AddQuest(396);
        AddQuestItem(396, 39602, 9944, 1);
    }

    public override void OnClicked(ActorPC pc)
    {
        SendQuestList(pc);
    }

    public override void OnQuestConfirmed(ActorPC pc, uint QuestID)
    {
        if (QuestID == 396 && !IfGotQuest(pc, 396))
        {
            AddStep(396, 39601);
            AddStep(396, 39602);
            AddStep(396, 39603);
            QuestStart(pc);
            SendQuestList(pc);
            SendNavPoint(pc, 396, 1174, 9184f, 7584f, 32f);
        }
    }
}

public class startzonebox01 : MapItem
{
    public override void OnInit()
    {
        MapName = "rag2startzone_01";
        Type = 47;
        Name = "StartzoneBox01";
        StartX = 9092F;
        StartY = 1148F;
        StartZ = -1F;
        Startyaw = 0;
    }

    public override void OnOpen(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 396, 39602) == StepStatus.Active)
        {
            ClearNPCItem();
            AddNPCItem(9944);
            SendLootList(pc);
            SendNavPoint(pc, 396, 1174, 9248f, -6144f, 96f);
        }
        SetAnimation(pc, 2);
    }
}

public class startzonebox02 : MapItem
{
    public override void OnInit()
    {
        MapName = "rag2startzone_01";
        Type = 47;
        Name = "StartzoneBox02";
        StartX = 8641F;
        StartY = 772F;
        StartZ = -1F;
        Startyaw = 0;
    }

    public override void OnOpen(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 396, 39602) == StepStatus.Active)
        {
            ClearNPCItem();
            AddNPCItem(9944);
            SendLootList(pc);
            SendNavPoint(pc, 396, 1174, 9248f, -6144f, 96f);
        }
        SetAnimation(pc, 2);
    }
}

public class startzonebox03 : MapItem
{
    public override void OnInit()
    {
        MapName = "rag2startzone_01";
        Type = 47;
        Name = "StartzoneBox03";
        StartX = 8959F;
        StartY = 255F;
        StartZ = -1F;
        Startyaw = 0;
    }

    public override void OnOpen(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 396, 39602) == StepStatus.Active)
        {
            ClearNPCItem();
            AddNPCItem(9944);
            SendLootList(pc);
            SendNavPoint(pc, 396, 1174, 9248f, -6144f, 96f);
        }
        SetAnimation(pc, 2);
    }
}