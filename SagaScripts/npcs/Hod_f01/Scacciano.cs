  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Scacciano : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1003;
        Name = "Scacciano Morrigan";
        StartX = 12484F;
        StartY = -15132F;
        StartZ = -4779F;
        Startyaw = 48000;
        SetScript(506);
        AddPersonalQuest(5, 0, 0, 162);
        AddPersonalQuest(6, 0, 5, 45);
        AddPersonalQuestStep(5, 503, StepStatus.Active);
        AddPersonalQuestStep(6, 603, StepStatus.Active);
	    AddQuestStep(156, 15601, StepStatus.Active);
        AddQuestStep(327, 32702, StepStatus.Active);
        AddQuestStep(406, 40602, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
        AddButton(Functions.PersonalQuest, new func(OnQuest), true);
        AddButton(Functions.AcceptPersonalRequest, new  personalfunc(OnAcceptPersonalQuest), true);
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 881);
    }

    public void OnAcceptPersonalQuest(ActorPC pc, uint QID, byte button)
    {
        switch(QID)
        {
            case 5:
            switch (button)
            {
                case 2:
                    AddStep(5, 502);
                    AddStep(5, 503);
                    PersonalQuestStart(pc);
                    NPCSpeech(pc, 1);
                    SendNavPoint(pc, 5, 1005, -1216f, 3328f, -10144f);
                    break;
                case 3:
                    NPCSpeech(pc, 2);
                    break;
            }
            break;
            case 6:
                switch (button)
                {
                    case 2:
                        AddStep(6, 602);
                        AddStep(6, 603);
                        PersonalQuestStart(pc);
                        NPCSpeech(pc, 1368);
                        SendNavPoint(pc, 6, 1002, 1460f, -13664f, -6472f);
                        break;
                    case 3:
                        NPCSpeech(pc, 1369);
                        break;
                }
                break;
        }
    }

    public void OnQuest(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 406, 40602) == StepStatus.Active)
        {
            if (CountItem(pc, 4245) >= 1)
            {
                UpdateQuest(pc, 406, 40602, StepStatus.Completed);
                QuestCompleted(pc, 406);
                TakeItem(pc, 4245, 1);
                UpdateIcon(pc);
                NPCSpeech(pc, 4560);
                NPCChat(pc, 0);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
        if (GetQuestStepStatus(pc, 5, 503) == StepStatus.Active)
        {
            if (CountItem(pc, 2618) >= 1)
            {
                UpdateQuest(pc, 5, 503, StepStatus.Completed);
                QuestCompleted(pc, 5);
                TakeItem(pc, 2618, 1);
                UpdateIcon(pc);
                NPCSpeech(pc, 42);
                NPCChat(pc, 0);
                RemoveNavPoint(pc, 5);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
        if (GetQuestStepStatus(pc, 6, 603) == StepStatus.Active)
        {
            if (CountItem(pc, 2619) >= 1)
            {
                UpdateQuest(pc, 6, 603, StepStatus.Completed);
                QuestCompleted(pc, 6);
                TakeItem(pc, 2619, 1);
                AddRewardChoice(pc, 50350001);
                AddRewardChoice(pc, 50300002);            
                UpdateIcon(pc);
                NPCSpeech(pc, 51);
                NPCChat(pc, 0);
                RemoveNavPoint(pc, 6);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
	if (GetQuestStepStatus(pc, 156, 15601) == StepStatus.Active)
	{
	     UpdateQuest(pc, 156, 15601, StepStatus.Completed);
	     RemoveNavPoint(pc, 156);
	     UpdateIcon(pc);
	     NPCSpeech(pc, 823);
	     NPCChat(pc, 0);
	}
        if (GetQuestStepStatus(pc, 327, 32702) == StepStatus.Active)
        {
            if (CountItem(pc, 2624) >= 1)
            {
                UpdateQuest(pc, 327, 32702, StepStatus.Completed);
                QuestCompleted(pc, 327);
                TakeItem(pc, 2624, 1);
                UpdateIcon(pc);
                NPCSpeech(pc, 93);
                NPCChat(pc, 0);
                RemoveNavPoint(pc, 327);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
        
    }

    public void OnReward(ActorPC pc, uint QID)
    {
        switch (QID)
        {
            case 406:
                GiveExp(pc, 140, 50);
                GiveZeny(pc, 5);
                RemoveQuest(pc, 406); 
                break;
            case 5:
                GiveExp(pc, 48, 0);
                GiveZeny(pc, 18);
                RemoveQuest(pc, 5);
                break;
            case 6:
                GiveExp(pc, 52, 0);
                GiveZeny(pc, 54);
                RemoveQuest(pc, 6);
                break;
            case 327:
                GiveExp(pc, 52, 5);
                GiveZeny(pc, 70);
                RemoveQuest(pc, 327);
                break;
        }       

    }
}