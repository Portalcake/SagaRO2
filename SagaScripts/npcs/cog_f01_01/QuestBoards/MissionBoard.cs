using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;


public abstract class MissionBoardcog_01_01 : MapItem
{
    private static bool initialized = false;
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1131;
		AddQuest(353, 25, 0);
		AddQuest(368, 25, 0);
		AddQuest(369, 25, 368);
		AddQuest(355, 26, 0);
		AddQuest(420, 27, 0);
		AddQuest(356, 27, 0);
		AddQuest(357, 27, 356);
		AddQuest(365, 27, 0);
		AddQuest(426, 31, 0);
		AddQuest(363, 33, 0);
		AddQuest(436, 34, 0);
        if (!MissionBoardcog_01_01.initialized)
        {
		List<uint> Mobs_436 = new List<uint>();
        	Mobs_436.Add(10363);
        	AddEnemyInfo(436, 43601, Mobs_436, 1);

		List<uint> Mobs_426 = new List<uint>();
        	Mobs_426.Add(10351);
        	Mobs_426.Add(10352);
        	AddEnemyInfo(426, 42601, Mobs_426, 10);
        }
        MissionBoardcog_01_01.initialized = true;
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
		case 353:
		AddStep(353, 35301);
		AddStep(353, 35302);
		AddStep(353, 35303);
		AddNavPoint(353, 35301, 12, 1024, -8408f, -34514f, 6913f); //Pretan
		QuestStart(pc);
		break;
		
		case 355:
		AddStep(355, 35501);
		AddStep(355, 35502);
		AddStep(355, 35503);
		AddNavPoint(355, 35501, 13, 1026, -5284f, -1663f, -3931f); // Moritz
		QuestStart(pc);
		break;

		case 356:
		AddStep(356, 35601);
		AddStep(356, 35602);
		AddStep(356, 35603);
		AddStep(356, 35604);
		AddNavPoint(356, 35601, 13, 1026, -5284f, -1663f, -3931f); // Moritz
		QuestStart(pc);
		break;
		
		case 357:
		AddStep(357, 35701);
		AddNavPoint(357, 35701, 13, 1021, -9408f, -6118f, -3949f); // Derek
		QuestStart(pc);
		break;

		case 363:
		AddStep(363, 36301);
		AddStep(363, 36302);
		AddStep(363, 36303);
		AddNavPoint(363, 36301, 13, 1023, -1096f, -145f, -3799f); // Ireyneal
		QuestStart(pc);
		break;

		case 365:
		AddStep(365, 36501);
		AddStep(365, 36502);
		AddStep(365, 36503);
		AddNavPoint(365, 36501, 13, 1023, -1096f, -145f, -3799f); // Ireyneal
		QuestStart(pc);
		break;

		case 368:
		AddStep(368, 36801);
		AddStep(368, 36802);
		AddStep(368, 36803);
		AddStep(368, 36804);
		AddStep(368, 36805);
		AddNavPoint(368, 36801, 12, 1022, 1174f, -17256f, 5797f); // Cheyenne
		QuestStart(pc);
		break;
		
		case 369:
		AddStep(369, 36901);
		AddStep(369, 36902);
		AddNavPoint(369, 36901, 12, 1022, 1174f, -17256f, 5797f); // Cheyenne
		QuestStart(pc);
		break;

		case 420:
		AddStep(420, 42001);
		AddStep(420, 42002);
		AddStep(420, 42003);
		AddNavPoint(420, 42001, 12, 1054, -7713f, -14312f, 6294f); // Alina
		QuestStart(pc);
		break;

		case 426:
		AddStep(426, 42601);
		AddStep(426, 42602);
		QuestStart(pc);
		break;

		case 436:
		AddStep(436, 43601);
		AddStep(436, 43602);
		QuestStart(pc);
		break;
        } 
        SendQuestList(pc); 
		SendNavPoint(pc);
    } 
}

public class MissionBoardcog_01_01_01 : MissionBoardcog_01_01
{
    public override void OnSub()
    {
        Name = "MissionBoardcog_01_01_01";
        StartX = 6571.222F;
        StartY = -15753.83F;
        StartZ = 5040F;
        Startyaw = -103412;
    }
}

public class MissionBoardcog_01_01_02 : MissionBoardcog_01_01
{
    public override void OnSub()
    {
        Name = "MissionBoardcog_01_01_02";
        StartX = -8086.29F;
        StartY = -21152.53F;
        StartZ = 6080F;
        Startyaw = -94640;
    }
}

public class MissionBoardcog_01_01_03 : MissionBoardcog_01_01
{
    public override void OnSub()
    {
        Name = "MissionBoardcog_01_01_03";
        StartX = 436.556F;
        StartY = -27275.4F;
        StartZ = 6096F;
        Startyaw = -30660;
    }
}

public class MissionBoardcog_01_01_04 : MissionBoardcog_01_01
{
    public override void OnSub()
    {
        Name = "MissionBoardcog_01_01_04";
        StartX = 5336.799F;
        StartY = -30123.7F;
        StartZ = 6088F;
        Startyaw = -54284;
    }
}