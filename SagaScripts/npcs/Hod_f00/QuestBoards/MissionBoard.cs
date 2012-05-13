using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public abstract class MissionBoardHod00 : MapItem
{
    private static bool initialized = false;
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 1142;
        AddQuest(398);
        AddQuest(402);
        if (!MissionBoardHod00.initialized)
        {
            AddQuestItem(398, 39802, 2666, 1);
            AddQuestItem(402, 40201, 2630, 3);
            //Add Temporary loots for quests, must be done at Initialization!
            AddMobLoot(10000, 398, 39802, 2666, 8000);
            AddMobLoot(10001, 398, 39802, 2666, 8000);
            AddMobLoot(10026, 402, 40201, 2630, 8000);
            AddMobLoot(10027, 402, 40201, 2630, 8000);
        }
        MissionBoardHod00.initialized = true;
        this.OnSub();
        
    }

    public virtual void OnSub()
    {
    }

    public override void OnClicked(ActorPC pc)
    {
        SendQuestList(pc);
    }

    public override void OnQuestConfirmed(ActorPC pc, uint QuestID)
    {
        if (IfGotQuest(pc, QuestID)) return;
        switch (QuestID)
        {
            case 398:
                AddStep(398, 39801);
                AddStep(398, 39802);
                AddStep(398, 39803);
                QuestStart(pc);
                SendNavPoint(pc, 398, 1146, 7392, 18985, 2690);
                break; 
            case 402:
                AddStep(402, 40201);
                AddStep(402, 40202);
                QuestStart(pc);
                break;
        }
        SendQuestList(pc);

    }
}

public class MissionBoardHod00_1 : MissionBoardHod00
{
    public override void OnSub()
    {
        Name = "MissionBoardHod00_1";
        StartX = 18205.25F;
        StartY = 7872.492F;
        StartZ = 1775.861F;
        Startyaw = 31840;
    }
}

public class MissionBoardHod00_2 : MissionBoardHod00
{
    public override void OnSub()
    {
        Name = "MissionBoardHod00_2";
        StartX = 7084.355F;
        StartY = 17266.62F;
        StartZ = 2785.766F;
        Startyaw = 41560;
    }
}

public class MissionBoardHod00_3 : MissionBoardHod00
{
    public override void OnSub()
    {
        Name = "MissionBoardHod00_3";
        StartX = 3204.811F;
        StartY = -3609.328F;
        StartZ = 1527.333F;
        Startyaw = 64472;
    }
}