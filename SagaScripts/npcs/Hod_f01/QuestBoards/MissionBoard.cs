using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public abstract class MissionBoardHod01 : MapItem
{
    private static bool initialized = false;
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1125;
        AddQuest(1);
        AddQuest(2);
        AddQuest(9);
        AddQuest(24);
        AddQuest(25);
        AddQuest(29);
	AddQuest(156);
        AddQuest(323);
        AddQuest(407);
        
        if (!MissionBoardHod01.initialized)//makes sure these code only be executed once
        {
            AddQuestItem(1, 101, 2630, 6);
            AddMobLoot(10026, 1, 101, 2630, 8000);
            AddMobLoot(10027, 1, 101, 2630, 8000);

            AddQuestItem(2, 201, 1, 2643, 2);
            AddQuestItem(2, 201, 2, 2610, 1);
            AddMobLoot(10015, 2, 201, 2643, 8000);
            AddMobLoot(10016, 2, 201, 2643, 8000);
            AddMobLoot(10017, 2, 201, 2610, 8000);
            AddMobLoot(10018, 2, 201, 2610, 8000);

            AddQuestItem(9, 902, 1, 2621, 1);
            AddQuestItem(9, 902, 2, 2622, 1);
            AddQuestItem(9, 902, 3, 2623, 1);
            
            List<uint> Mobs = new List<uint>();
            Mobs.Add(10017);
            Mobs.Add(10018);
            AddEnemyInfo(24, 2401, Mobs, 4);
            
            AddQuestItem(29, 2901, 1, 2603, 3);
            AddQuestItem(29, 2901, 2, 2667, 2);
            AddMobLoot(10002, 29, 2901, 2603, 8000);
            AddMobLoot(10003, 29, 2901, 2603, 8000);
            AddMobLoot(10004, 29, 2901, 2603, 8000);
            AddMobLoot(10046, 29, 2901, 2667, 8000);
            AddMobLoot(10047, 29, 2901, 2667, 8000);
            
            AddQuestItem(407, 40701, 2667, 4); 
            AddMobLoot(10046, 407, 40701, 2667, 8000);
            AddMobLoot(10047, 407, 40701, 2667, 8000);
        }
        MissionBoardHod01.initialized = true;
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
            case 407:
                AddStep(407, 40701);
                AddStep(407, 40702);
                QuestStart(pc);
                break;
            case 1:
                AddStep(1,101);
                AddStep(1,102);
                QuestStart(pc);
                break;
            case 2:
                AddStep(2, 201);
                AddStep(2, 202);
                QuestStart(pc);
                break;
            case 24:
                AddStep(24, 2401);
                AddStep(24, 2402);
                QuestStart(pc);
                break;
            case 25:
                AddStep(25, 2501);
                AddStep(25, 2502);
                QuestStart(pc);
                SendNavPoint(pc, 25, 1002, 1460f, -13664f, -6472f);            
                break;
            case 29:
                AddStep(29, 2901);
                AddStep(29, 2902);
                QuestStart(pc);
                break; 
	    case 156:
		AddStep(156, 15601);
		AddStep(156, 15602);
		SendNavPoint(pc, 156, 1003, 12484f, -15132f, -4779f);
		QuestStart(pc);
		break;           
            case 323:
                AddStep(323, 32301);
                AddStep(323, 32302);
                AddStep(323, 32303);
                QuestStart(pc);
                SendNavPoint(pc, 323, 1000, -12092f, -6490f, -8284f);
                break;
            case 9:
                AddStep(9, 901);
                AddStep(9, 902);
                AddStep(9, 903);
                QuestStart(pc);
                SendNavPoint(pc, 9, 1005, -1216f, 3328f, -10144f);
                break;
            
        }
        SendQuestList(pc);
    }
}

public class MissionBoardHod01_1 : MissionBoardHod01
{
    public override void OnSub()
    {
        Name = "MissionBoardHod01_1";
        StartX = 3540.303F;
        StartY = -13958.85F;
        StartZ = -6481F;
        Startyaw = 40000;
    }
}

public class MissionBoardHod01_2 : MissionBoardHod01
{
    public override void OnSub()
    {
        Name = "MissionBoardHod01_2";
        StartX = 2962.561F;
        StartY = -4105.907F;
        StartZ = -8458F;
        Startyaw = 30000;
    }
}

public class MissionBoardHod01_3 : MissionBoardHod01
{
    public override void OnSub()
    {
        Name = "MissionBoardHod01_3";
        StartX = 2677.451F;
        StartY = 6946.643F;
        StartZ = -10081F;
        Startyaw = 0;
    }
}

public class MissionBoardHod01_4 : MissionBoardHod01
{
    public override void OnSub()
    {
        Name = "MissionBoardHod01_4";
        StartX = -9284.975F;
        StartY = -1955.267F;
        StartZ = -9321F;
        Startyaw = 40000;
    }
}

