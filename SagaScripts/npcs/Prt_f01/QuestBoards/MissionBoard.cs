using System; 
using System.Collections.Generic; 

using SagaMap; 
  
using SagaDB.Actors; 
using SagaDB.Items; 


public abstract class MissionBoardPrt01 : MapItem 
{ 
    private static bool initialized = false;
    public override void OnInit() 
    { 
        MapName = "Prt_f01"; 
        Type = 1127;  
		AddQuest(157, 12, 0);
		AddQuest(158, 12, 157);
		AddQuest(164, 13, 0);
		AddQuest(165, 12, 0);
		AddQuest(166, 12, 165);
		AddQuest(167, 12, 166);
		AddQuest(173, 16, 0);
		AddQuest(174, 16, 173);
		AddQuest(175, 16, 174);
		AddQuest(176, 17, 175);
		AddQuest(177, 17, 176);
		AddQuest(178, 17, 177);
        AddQuest(180, 12, 0);
        AddQuest(181, 12, 180);
        AddQuest(182, 13, 0);
        AddQuest(183, 13, 182);
		AddQuest(195, 14, 0);
		AddQuest(196, 14, 195);
        AddQuest(197, 16, 0);
        AddQuest(198, 16, 197);
        AddQuest(199, 16, 198);
        AddQuest(200, 16, 199);
        AddQuest(201, 16, 200);
        AddQuest(202, 16, 201);
        AddQuest(203, 17, 202);
        AddQuest(204, 17, 203);
        AddQuest(205, 17, 204);
        AddQuest(206, 18, 205);
        AddQuest(207, 18, 206);
        AddQuest(208, 18, 207);
		AddQuest(227, 13, 0); 
		AddQuest(228, 13, 227); 
		AddQuest(229, 13, 228); 
		AddQuest(230, 15, 229); 
		AddQuest(231, 15, 230); 
		AddQuest(232, 15, 231); 
		AddQuest(233, 15, 232); 
		AddQuest(234, 15, 234); 
		AddQuest(274, 18, 0); //Meant to have another quest before...
		AddQuest(276, 19, 0); 
		AddQuest(277, 19, 276); 
		AddQuest(278, 20, 277); 
		AddQuest(302, 21, 278); 
		AddQuest(303, 21, 302);
		AddQuest(304, 21, 303); 
		AddQuest(305, 21, 304); 
		AddQuest(306, 21, 305); 
		AddQuest(279, 17, 0);  
        if (!MissionBoardPrt01.initialized)
        {
            AddQuestItem(164, 16401, 3976, 5);
            AddMobLoot(10069, 164, 16401, 3976, 2500);
            AddMobLoot(10070, 164, 16401, 3976, 2500);

            AddQuestItem(195, 19501, 3990, 7);
            AddMobLoot(10153, 195, 19501, 3990, 1500);
            AddMobLoot(10154, 195, 19501, 3990, 1500);
            AddMobLoot(10155, 195, 19501, 3990, 1500);

            AddQuestItem(196, 19601, 3991, 5);
            AddMobLoot(10076, 196, 19601, 3991, 1500);
            AddMobLoot(10077, 196, 19601, 3991, 1500);
            AddMobLoot(10078, 196, 19601, 3991, 1500);

            AddQuestItem(279, 27901, 4045, 7);
            AddMobLoot(10159, 279, 27901, 4045, 1500);
            AddMobLoot(10160, 279, 27901, 4045, 1500);
            AddMobLoot(10161, 279, 27901, 4045, 1500);
        }
        MissionBoardPrt01.initialized = true;
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
		case 157:
			AddStep(157, 15701);
			AddStep(157, 15702);
			AddStep(157, 15703);
			AddNavPoint(157, 15703, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika	
			QuestStart(pc);
			break;

		case 158:
			AddStep(158, 15801);
			AddStep(158, 15802);
			AddStep(158, 15803);
			AddStep(158, 15804);
			AddStep(158, 15805);
			AddNavPoint(158, 15801, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika	
			QuestStart(pc);
			break;


		case 164: 
			AddStep(164, 16401); 
			AddStep(164, 16402); 
			QuestStart(pc); 
			break;

		case 165: 
			AddStep(165, 16501); 
			AddStep(165, 16502); 
			AddNavPoint(165, 16501, 5, 1010, 9752f, 75223f, 5108f); //Regina			
			QuestStart(pc); 
			break;

		case 166: 
			AddStep(166, 16601); 
			AddStep(166, 16602); 
			AddStep(166, 16603); 
			AddNavPoint(166, 16601, 6, 1152, -47520f, -49440f, 3094f); //Helena
			QuestStart(pc); 
			break;

		case 167: 
			AddStep(167, 16701); 
			AddStep(167, 16702); 
			AddNavPoint(167, 16701, 6, 1152, -47520f, -49440f, 3094f); //Helena
			QuestStart(pc); 
			break;

		case 173: 
			AddStep(173, 17301); 
			AddStep(173, 17302); 
	    	AddNavPoint(173, 17301, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika	
			QuestStart(pc); 
			break;

		case 174: 
			AddStep(174, 17401); 
			AddStep(174, 17402); 
			AddStep(174, 17403); 
	    	AddNavPoint(174, 17401, 5, 1009, 40375f, 82998f, 3853f); //Volker		
			QuestStart(pc); 
			break;

		case 175: 
			AddStep(175, 17501); 
			AddStep(175, 17502);  
	    	AddNavPoint(175, 17501, 5, 1009, 40375f, 82998f, 3853f); //Volker	
			QuestStart(pc); 
			break;

		case 176: 
			AddStep(176, 17601); 
			AddStep(176, 17602); 
			AddStep(176, 17603); 
	    	AddNavPoint(176, 17601, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika		
			QuestStart(pc); 
			break;

		case 177: 
			AddStep(177, 17701); 
			AddStep(177, 17702); 
			AddStep(177, 17703); 
	    	AddNavPoint(177, 17701, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika	
			QuestStart(pc); 
			break;

		case 178: 
			AddStep(178, 17801); 
			AddStep(178, 17802); 
			AddStep(178, 17803); 
			AddStep(178, 17804);
	    	AddNavPoint(178, 17801, 6, 1089, -29860f, 4500f, 1082f); //Hanne	
			QuestStart(pc); 
			break;

		case 180:
			AddStep(180, 18001);
			AddStep(180, 18002);
			AddStep(180, 18003);
	    	AddNavPoint(180, 18001, 5, 1006, 11616f, 69760f, 5194f); //Meinhard		
			QuestStart(pc);
			break;

		case 181:
			AddStep(181, 18101);
			AddStep(181, 18102);
	    	AddNavPoint(181, 18101, 5, 1006, 11616f, 69760f, 5194f); //Meinhard	
			QuestStart(pc);
			break;

		case 182:
			AddStep(182, 18201);
			AddStep(182, 18202);
			AddStep(182, 18203);
	    	AddNavPoint(182, 18201, 6, 1153, 46020f, -51421f, 2698f); //Reymond
	    	AddNavPoint(182, 18203, 6, 1153, 46020f, -51421f, 2698f); //Reymond		
			QuestStart(pc);
			break;

		case 183:
			AddStep(183, 18301);
			AddStep(183, 18302);
	    	AddNavPoint(183, 18301, 6, 1153, 46020f, -51421f, 2698f); //Reymond	
			QuestStart(pc);
			break;

		case 195: 
			AddStep(195, 19501); 
			AddStep(195, 19502); 
			QuestStart(pc); 
			break;

		case 196: 
			AddStep(196, 19601); 
			AddStep(196, 19602); 
			QuestStart(pc); 
			break;

		case 197: 
			AddStep(197, 19701); 
			AddStep(197, 19702); 
	    	AddNavPoint(197, 19701, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika	
			QuestStart(pc); 
			break;

		case 198: 
			AddStep(198, 19801); 
			AddStep(198, 19802); 
			AddStep(198, 19803);
	    	AddNavPoint(198, 19801, 8, 1098, 49415.03f, 7792.097f, -6106.818f); //Rufus	
			QuestStart(pc); 
			break;

		case 199: 
			AddStep(199, 19901); 
			AddStep(199, 19902); 
	    	AddNavPoint(199, 19901, 8, 1098, 49415.03f, 7792.097f, -6106.818f); //Rufus	
			QuestStart(pc); 
			break;

		case 200: 
			AddStep(200, 20001); 
			AddStep(200, 20002); 
	    	AddNavPoint(200, 20001, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika	
			QuestStart(pc); 
			break;

		case 201: 
			AddStep(201, 20101); 
			AddStep(201, 20102); 
			AddStep(201, 20103);
	    	AddNavPoint(201, 20101, 8, 1092, 8231.702f, 46475.5f, -7303.248f); //Shantos	
			QuestStart(pc); 
			break;

		case 202: 
			AddStep(202, 20201); 
			AddStep(202, 20202); 
	    	AddNavPoint(202, 20201, 8, 1092, 8231.702f, 46475.5f, -7303.248f); //Shantos		
			QuestStart(pc); 
			break;

		case 203: 
			AddStep(203, 20301); 
			AddStep(203, 20302); 
	    	AddNavPoint(203, 20301, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika		
			QuestStart(pc); 
			break;

		case 204: 
			AddStep(204, 20401); 
			AddStep(204, 20402); 
			AddStep(204, 20403);
	    	AddNavPoint(204, 20401, 8, 1097, -14230.25f, 471.966f, -7404.452f); //Arno		
			QuestStart(pc); 
			break;

		case 205: 
			AddStep(205, 20501); 
			AddStep(205, 20502); 
	    	AddNavPoint(205, 20501, 8, 1097, -14230.25f, 471.966f, -7404.452f); //Arno	    		
			QuestStart(pc); 
			break;

		case 206: 
			AddStep(206, 20601); 
			AddStep(206, 20602); 
	    	AddNavPoint(206, 20601, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika		
			QuestStart(pc); 
			break;

		case 207: 
			AddStep(207, 20701); 
			AddStep(207, 20702); 
			AddStep(207, 20703);
	    	AddNavPoint(207, 20701, 8, 1095, -41102.66f, 29516.62f, -7938.911f); //Kuno		
			QuestStart(pc); 
			break;

		case 208: 
			AddStep(208, 20801); 
			AddStep(208, 20802); 
	    	AddNavPoint(208, 20801, 8, 1095, -41102.66f, 29516.62f, -7938.911f); //Kuno	    		
			QuestStart(pc); 
			break;

		case 227: 
			AddStep(227, 22701); 
			AddStep(227, 22702); 
	    	AddNavPoint(227, 22701, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika
			QuestStart(pc); 
			break;

		case 228: 
			AddStep(228, 22801); 
			AddStep(228, 22802); 
			AddStep(228, 22803); 
			AddStep(228, 22804); 
	    	AddNavPoint(228, 22801, 5, 1151, 21668f, 7200f, 13318f); //Sophie
	    	AddNavPoint(228, 22802, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika	
			QuestStart(pc); 
			break;

		case 229: 
			AddStep(229, 22901); 
			AddStep(229, 22902); 
			AddStep(229, 22903); 
	    	AddNavPoint(229, 22901, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika	
			QuestStart(pc); 
			break;

		case 230: 
			AddStep(230, 23001); 
			AddStep(230, 23002); 
			AddStep(230, 23003); 
	    	AddNavPoint(230, 23001, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika	
			QuestStart(pc); 
			break;

		case 231: 
			AddStep(231, 23101); 
			AddStep(231, 23102); 
			AddStep(231, 23103); 
	    	AddNavPoint(231, 23101, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika		
			QuestStart(pc); 
			break;

		case 232: 
			AddStep(232, 23201); 
			AddStep(232, 23202); 
			AddStep(232, 23203); 
	    	AddNavPoint(232, 23201, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika
			QuestStart(pc); 
			break;

		case 233: 
			AddStep(233, 23301); 
			AddStep(233, 23302); 
			AddStep(233, 23303); 
	    	AddNavPoint(233, 23301, 6, 1080, -7360f, -3904f, 180f); //Achim	
			QuestStart(pc); 
			break;

		case 234: 
			AddStep(234, 23401); 
			AddStep(234, 23402); 
			AddStep(234, 23403); 
	    	AddNavPoint(234, 23401, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika	
			QuestStart(pc); 
			break;

		case 274:
			AddStep(274, 27401);
			AddStep(274, 27402);
			AddStep(274, 27403);
	    	AddNavPoint(274, 27401, 5, 1009, 40375f, 82998f, 3853f); //Volker		
			QuestStart(pc);
			break;

		case 276:
			AddStep(276, 27601);
			AddStep(276, 27602);
			AddStep(276, 27603);
	    	AddNavPoint(276, 27601, 5, 1007, 22528f, 88672f, 5120f); //Magnet	
			QuestStart(pc);
			break;

		case 277:
			AddStep(277, 27701);
			AddStep(277, 27702);
			AddStep(277, 27703);
	    	AddNavPoint(277, 27701, 5, 1007, 22528f, 88672f, 5120f); //Magnet	
			QuestStart(pc);
			break;

		case 278:
			AddStep(278, 27801);
			AddStep(278, 27802);
			AddStep(278, 27803);
	    	AddNavPoint(278, 27801, 5, 1007, 22528f, 88672f, 5120f); //Magnet	    		
			QuestStart(pc);
			break;

		case 302:
			AddStep(302, 30201);
			AddStep(302, 30202);
			AddStep(302, 30203);
	    	AddNavPoint(302, 30201, 5, 1007, 22528f, 88672f, 5120f); //Magnet		
			QuestStart(pc);
			break;

		case 303:
			AddStep(303, 30301);
			AddStep(303, 30302);
	    	AddNavPoint(303, 30301, 5, 1007, 22528f, 88672f, 5120f); //Magnet	
			QuestStart(pc);
			break;

		case 304:
			AddStep(304, 30401);
			AddStep(304, 30402);
	    	AddNavPoint(304, 30401, 5, 1008, 16287f, 94272f, 4192f); //Mr Pitt	
			QuestStart(pc);
			break;

		case 305:
			AddStep(305, 30501);
			AddStep(305, 30502);
	    	AddNavPoint(305, 30501, 5, 1051, 14783f, 78975f, 5088f); //Nikki	
			QuestStart(pc);
			break;

		case 306:
			AddStep(306, 30601);
			AddStep(306, 30602);
	    	AddNavPoint(306, 30601, 5, 1007, 22528f, 88672f, 5120f); //Magnet
			QuestStart(pc);
			break;

		case 279: 
			AddStep(279, 27901); 
			AddStep(279, 27902); 
			QuestStart(pc); 
			break;
        } 
        SendQuestList(pc); 
		SendNavPoint(pc);
    } 
} 

public class MissionBoardPrt01_1 : MissionBoardPrt01 
{ 
    public override void OnSub() 
    { 
        Name = "MissionBoardPrt01_1"; 
        StartX = 16783.22F;
        StartY = 55635.7F;
        StartZ = 9143.083F;
        Startyaw = -65508;
    } 
} 

public class MissionBoardPrt01_2 : MissionBoardPrt01 
{ 
    public override void OnSub() 
    { 
        Name = "MissionBoardPrt01_2"; 
        StartX = 11177.54F;
        StartY = 77093.04F;
        StartZ = 5004F;
        Startyaw = -49152;
    } 
}

public class MissionBoardPrt01_3 : MissionBoardPrt01 
{ 
    public override void OnSub() 
    { 
        Name = "MissionBoardPrt01_3"; 
        StartX = 18128.39F;
        StartY = 93318.58F;
        StartZ = 4164F;
        Startyaw = -39504;
    } 
}

public class MissionBoardPrt01_4 : MissionBoardPrt01 
{ 
    public override void OnSub() 
    { 
        Name = "MissionBoardPrt01_4"; 
        StartX = 14096.47F;
        StartY = 98420.09F;
        StartZ = 4168F;
        Startyaw = -32768;
    } 
}

public class MissionBoardPrt01_5 : MissionBoardPrt01 
{ 
    public override void OnSub() 
    { 
        Name = "MissionBoardPrt01_5"; 
        StartX = 4575.972F;
        StartY = 86896.4F;
        StartZ = 4164F;
        Startyaw = -65484;
    } 
}

public class MissionBoardPrt01_6 : MissionBoardPrt01 
{ 
    public override void OnSub() 
    { 
        Name = "MissionBoardPrt01_6";
        StartX = 22710.7F;
        StartY = 81868.73F;
        StartZ = 5004F;
        Startyaw = -25700;
    } 
}

public class MissionBoardPrt01_7 : MissionBoardPrt01 
{ 
    public override void OnSub() 
    { 
        Name = "MissionBoardPrt01_7";
        StartX = -16349.6F;
        StartY = -31370.86F;
        StartZ = -848.037F;
        Startyaw = 0;
    } 
}

public class MissionBoardPrt01_8 : MissionBoardPrt01 
{ 
    public override void OnSub() 
    { 
        Name = "MissionBoardPrt01_8";
        StartX = 36958.82F;
        StartY = -16677.64F;
        StartZ = -4000.709F;
        Startyaw = -26000;
    } 
}
