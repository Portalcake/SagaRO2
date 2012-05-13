using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public abstract class KafraMailBox : MapItem
{
    public override void OnInit()
    {
        Type = 1123;
        this.OnSub();
    }

    public virtual void OnSub()
    {
    }

    public override void OnClicked(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 274, 27403) == StepStatus.Active)
        {
            if (CountItem(pc, 4040) >= 8)
            {
                TakeItem(pc, 4040, 8);
                UpdateQuest(pc, 274, 27403, StepStatus.Completed);
                QuestCompleted(pc, 274);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
		
        if (GetQuestStepStatus(pc, 164, 16402) == StepStatus.Active)
        {
            if (CountItem(pc, 3976) >= 5)
            {
                TakeItem(pc, 3976, 5);
                UpdateQuest(pc, 164, 16402, StepStatus.Completed);
                QuestCompleted(pc, 164);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
		
        if (GetQuestStepStatus(pc, 174, 17403) == StepStatus.Active)
        {
            UpdateQuest(pc, 174, 17403, StepStatus.Completed);
            QuestCompleted(pc, 174);
            SetReward(pc, new rewardfunc(OnReward));
        }
		
        if (GetQuestStepStatus(pc, 176, 17603) == StepStatus.Active)
        {
            UpdateQuest(pc, 176, 17603, StepStatus.Completed);
            QuestCompleted(pc, 176);
            SetReward(pc, new rewardfunc(OnReward));
        }
		
        if (GetQuestStepStatus(pc, 180, 18003) == StepStatus.Active)
        {
            if (CountItem(pc, 3985) >= 5)
            {
                TakeItem(pc, 3985, 5);
                UpdateQuest(pc, 180, 18003, StepStatus.Completed);
                QuestCompleted(pc, 180);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
		
        if (GetQuestStepStatus(pc, 195, 19502) == StepStatus.Active)
        {
            if (CountItem(pc, 3990) >= 7)
            {
                TakeItem(pc, 3990, 7);
                UpdateQuest(pc, 195, 19502, StepStatus.Completed);
                QuestCompleted(pc, 195);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
		
        if (GetQuestStepStatus(pc, 196, 19602) == StepStatus.Active)
        {
            if (CountItem(pc, 3991) >= 5)
            {
                TakeItem(pc, 3991, 5);		
                UpdateQuest(pc, 196, 19602, StepStatus.Completed);
                QuestCompleted(pc, 196);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
		
        if (GetQuestStepStatus(pc, 407, 40702) == StepStatus.Active)
        {
            if (CountItem(pc, 2667) >= 4)
            {
                TakeItem(pc, 2667, 4);
                UpdateQuest(pc, 407, 40702, StepStatus.Completed);
                QuestCompleted(pc, 407);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
		
        if (GetQuestStepStatus(pc, 29, 2902) == StepStatus.Active)
        {
            if (CountItem(pc, 2667) >= 2 && CountItem(pc, 2603) >= 3)
            {
                TakeItem(pc, 2667, 2);
                TakeItem(pc, 2603, 3);
                UpdateQuest(pc, 29, 2902, StepStatus.Completed);
                QuestCompleted(pc, 29);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
		
        if (GetQuestStepStatus(pc, 279, 27902) == StepStatus.Active)
        {
            if (CountItem(pc, 4045) >= 7)
            {
                TakeItem(pc, 4045, 7);
                UpdateQuest(pc, 279, 27902, StepStatus.Completed);
                QuestCompleted(pc, 279);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }

        if (GetQuestStepStatus(pc, 231, 23103) == StepStatus.Active)
        {
            if (CountItem(pc, 4019) > 0)
            {
                TakeItem(pc, 4019, 1);
                UpdateQuest(pc, 231, 23103, StepStatus.Completed);
                QuestCompleted(pc, 231);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }

        if (GetQuestStepStatus(pc, 302, 30203) == StepStatus.Active)
        {
            if (CountItem(pc, 4051) > 0)
            {
             	UpdateQuest(pc, 302, 30203, StepStatus.Completed);
				TakeItem(pc, 4051, 1);
				QuestCompleted(pc, 302);
				SetReward(pc, new rewardfunc(OnReward));
			}
		}

        if (GetQuestStepStatus(pc, 353, 35303) == StepStatus.Active)
        {
            UpdateQuest(pc, 353, 35303, StepStatus.Completed);
            QuestCompleted(pc, 353);
            SetReward(pc, new rewardfunc(OnReward));
        }
		
		if (GetQuestStepStatus(pc, 355, 35503) == StepStatus.Active)
        {
            if (CountItem(pc, 4183) > 4 && CountItem(pc, 4184) > 4)
            {
             	UpdateQuest(pc, 355, 35503, StepStatus.Completed);
				TakeItem(pc, 4183, 5);
				TakeItem(pc, 4184, 5);
				QuestCompleted(pc, 355);
				SetReward(pc, new rewardfunc(OnReward));
			}
		}

        if (GetQuestStepStatus(pc, 363, 36303) == StepStatus.Active)
        {
            UpdateQuest(pc, 363, 36303, StepStatus.Completed);
            QuestCompleted(pc, 363);
            SetReward(pc, new rewardfunc(OnReward));
        }
        if (GetQuestStepStatus(pc, 403, 40303) == StepStatus.Active)
        {
            UpdateQuest(pc, 403, 40303, StepStatus.Completed);
            QuestCompleted(pc, 403);
            SetReward(pc, new rewardfunc(OnReward));
        }
        if (GetQuestStepStatus(pc, 436, 43602) == StepStatus.Active)
        {
            UpdateQuest(pc, 436, 43602, StepStatus.Completed);
            QuestCompleted(pc, 436);
            SetReward(pc, new rewardfunc(OnReward));
        }
    }

    public void OnReward(ActorPC pc, uint QID)
    {
        switch (QID)
        {
            case 29:
                GiveExp(pc, 34, 0);
                GiveZeny(pc, 26);
                GiveItem(pc, 1700113, 2);
                RemoveQuest(pc, 29);
                break;

            case 164:
				GiveExp(pc, 800, 0);
            	GiveZeny(pc, 232);
            	GiveItem(pc, 1700113, 3);
            	RemoveQuest(pc, 164);
				break;

            case 174:
            	GiveExp(pc, 1240, 0);
            	GiveZeny(pc, 300);
				GiveItem(pc, 1700113, 3);
            	RemoveQuest(pc, 174);
            	AddStep(175, 17501);
            	AddStep(175, 17502);
				AddNavPoint(175, 17501, 5, 1009, 40375f, 82998f, 3853f); //Volker
				SendNavPoint(pc);
            	QuestStart(pc); 
				break;

            case 176:
            	GiveExp(pc, 1400, 0);
            	GiveZeny(pc, 324);
            	RemoveQuest(pc, 176);
            	AddStep(177, 17701);
            	AddStep(177, 17702);
            	AddStep(177, 17703);
				AddNavPoint(177, 17701, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika
				SendNavPoint(pc);
            	QuestStart(pc); 
				break;

            case 180:
            	GiveExp(pc, 680, 0);
            	GiveZeny(pc, 212);
            	GiveItem(pc, 1700113, 2);
            	RemoveQuest(pc, 180);
            	AddStep(181, 18101);
            	AddStep(181, 18102);
				AddNavPoint(181, 18101, 5, 1006, 11616f, 69760f, 5194f); //Meinhard
				SendNavPoint(pc);
            	QuestStart(pc); 
				break;

            case 195:
				GiveExp(pc, 0, 608);
            	GiveZeny(pc, 252);
            	GiveItem(pc, 1700113, 3);
            	RemoveQuest(pc, 195);
				break;

            case 196:
	           	GiveExp(pc, 0, 608);
            	GiveZeny(pc, 252);
            	GiveItem(pc, 1700113, 3);
            	RemoveQuest(pc, 196);
				break;

            case 231:
            	GiveExp(pc, 2295, 0);
            	GiveZeny(pc, 587);
				GiveItem(pc, 1700113, 14);
            	RemoveQuest(pc, 231);
            	AddStep(232, 23201);
            	AddStep(232, 23202);
            	AddStep(232, 23203);
				AddNavPoint(232, 23201, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika
				SendNavPoint(pc);
            	QuestStart(pc); 
				break;

            case 274:
				GiveExp(pc, 3315, 0);
            	GiveZeny(pc, 710);
            	RemoveQuest(pc, 274);
				break;

            case 279:
				GiveExp(pc, 1925, 0);
            	GiveZeny(pc, 446);
            	RemoveQuest(pc, 279);
				break;

            case 302:
            	GiveExp(pc, 0, 1788);
            	GiveZeny(pc, 600);
				GiveItem(pc, 1700114, 3);
            	RemoveQuest(pc, 302);
            	AddStep(303, 30301);
            	AddStep(303, 30302);
				AddNavPoint(303, 30301, 5, 1007, 22528f, 88672f, 5120f); //Magnet
				SendNavPoint(pc);
            	QuestStart(pc); 
				break;

            case 353:
                GiveExp(pc, 4180, 0);
                GiveZeny(pc, 792);
                GiveItem(pc, 1700114, 5);
                RemoveQuest(pc, 353);
                break;
				
				case 355:
                GiveExp(pc, 4551, 0);
                GiveZeny(pc, 853);
                GiveItem(pc, 1700114, 5);
                RemoveQuest(pc, 355);
                break;

            case 363:
				GiveExp(pc, 7535, 0);
            	GiveZeny(pc, 1364);
			    GiveItem(pc, 1700115, 3);
            	RemoveQuest(pc, 363);
				break;
            
			case 403:
                GiveExp(pc, 9, 0);
                GiveZeny(pc, 9);
				GiveItem(pc, 400000, 3);
                RemoveQuest(pc, 403);
                break;

				case 407:
                GiveExp(pc, 44, 0);
                GiveZeny(pc, 39);
                GiveItem(pc, 1700113, 1);
                RemoveQuest(pc, 407);
                break;

            case 436:
                GiveExp(pc, 8003, 0);
                GiveZeny(pc, 1447);
                GiveItem(pc, 1700115, 4);
                RemoveQuest(pc, 436);
                break;
        }
    }
}