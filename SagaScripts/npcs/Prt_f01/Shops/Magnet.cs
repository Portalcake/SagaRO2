using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Magnet : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1007;
            Name = "Magnet Wihelmina";
            StartX = 22528F;
            StartY = 88672F;
            StartZ = 5120;
            Startyaw = 32768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.OfficialQuest, new func(OnQuest), true);
            AddButton(Functions.Shop);
            AddButton(Functions.Supply);
            SupplyMenuID = 10;

//Goods
AddGoods(4101); AddGoods(2578); AddGoods(2579); AddGoods(2580); AddGoods(2581); AddGoods(2582); AddGoods(51500002); AddGoods(51500003); AddGoods(51500004); AddGoods(51500005); AddGoods(51500006); AddGoods(51500007); AddGoods(51500008); AddGoods(51500009); AddGoods(51500010); AddGoods(51500011); AddGoods(51500012); 

//Exchange
// Create Red Potion I
            AddSupplyProduct(46,51500003,1);
            AddSupplyMatrial(46,9406,1);
            AddSupplyMatrial(46,9500,1);
// Create Red Potion II
            AddSupplyProduct(47,51500004,1);
            AddSupplyMatrial(47,9406,1);
            AddSupplyMatrial(47,9500,1);
            AddSupplyMatrial(47,10490,1);
// Create Red Potion III
            AddSupplyProduct(48,51500005,1);
            AddSupplyMatrial(48,9406,1);
            AddSupplyMatrial(48,1700051,1);
            AddSupplyMatrial(48,9338,1);
// Create Red Potion IV
            AddSupplyProduct(49,51500006,1);
            AddSupplyMatrial(49,1700009,1);
            AddSupplyMatrial(49,1700051,1);
            AddSupplyMatrial(49,9338,1);
// Create Red Potion V
            AddSupplyProduct(50,51500007,1);
            AddSupplyMatrial(50,1700009,1);
            AddSupplyMatrial(50,1700051,1);
            AddSupplyMatrial(50,1700075,1);
// Create Red Potion VI
            AddSupplyProduct(51,51500008,1);
            AddSupplyMatrial(51,1700009,1);
            AddSupplyMatrial(51,1700097,1);
            AddSupplyMatrial(51,1700078,1);
// Create Red Potion VII
            AddSupplyProduct(52,51500009,1);
            AddSupplyMatrial(52,1700009,1);
            AddSupplyMatrial(52,1700097,1);
            AddSupplyMatrial(52,1700079,1);
// Create Red Potion VIII
            AddSupplyProduct(53,51500010,1);
            AddSupplyMatrial(53,1700009,1);
            AddSupplyMatrial(53,1700097,1);
            AddSupplyMatrial(53,1700088,1);
// Create Blue Potion I
            AddSupplyProduct(54,51500013,1);
            AddSupplyMatrial(54,1700009,1);
            AddSupplyMatrial(54,9453,1);
            AddSupplyMatrial(54,10446,1);
            AddSupplyMatrial(54,1700013,1);
// Create Blue Potion II
            AddSupplyProduct(55,51500014,1);
            AddSupplyMatrial(55,1700009,1);
            AddSupplyMatrial(55,2686,1);
            AddSupplyMatrial(55,1700077,1);
            AddSupplyMatrial(55,1700078,1);

//Quest Steps
		    AddQuestStep(276, 27601, StepStatus.Active);
		    AddQuestStep(276, 27603, StepStatus.Active);
		    AddQuestStep(277, 27701, StepStatus.Active);
		    AddQuestStep(277, 27703, StepStatus.Active);
		    AddQuestStep(278, 27801, StepStatus.Active);
		    AddQuestStep(278, 27803, StepStatus.Active);
		    AddQuestStep(302, 30201, StepStatus.Active);
		    AddQuestStep(303, 30301, StepStatus.Active);
		    AddQuestStep(305, 30502, StepStatus.Active);
		    AddQuestStep(306, 30601, StepStatus.Active);

//Quest Items
            AddQuestItem(276, 27602, 4042, 5);
            AddMobLoot(10104, 276, 27602, 4042, 2000);
            AddMobLoot(10105, 276, 27602, 4042, 2000);

            AddQuestItem(277, 27702, 4043, 4);
            AddMobLoot(10083, 277, 27702, 4043, 10000);
            AddMobLoot(10084, 277, 27702, 4043, 10000);
            AddMobLoot(10085, 277, 27702, 4043, 10000);

            AddQuestItem(278, 27802, 4044, 8);
            AddMobLoot(10086, 278, 27802, 4044, 10000);
            AddMobLoot(10087, 278, 27802, 4044, 10000);

            AddQuestItem(302, 30202, 4051, 1);
            AddMobLoot(10302, 302, 30202, 4051, 10000);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

        public void OnQuest(ActorPC pc)
        {
            if (GetQuestStepStatus(pc, 276, 27601) == StepStatus.Active)
            {
                UpdateQuest(pc, 276, 27601, StepStatus.Completed);
				UpdateIcon(pc);
                RemoveNavPoint(pc, 276);
				AddNavPoint(276, 27603, 5, 1007, 22528f, 88672f, 5120f); //Magnet	
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
			}

            if (GetQuestStepStatus(pc, 276, 27603) == StepStatus.Active && CountItem(pc, 4042) > 4)
            {
				UpdateQuest(pc, 276, 27603, StepStatus.Completed);
				TakeItem(pc, 4042, 5);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 276);
				QuestCompleted(pc, 276);
				NPCSpeech(pc, 823);
				NPCChat(pc, 0);
				SetReward(pc, new rewardfunc(OnReward));
		    }

            if (GetQuestStepStatus(pc, 277, 27701) == StepStatus.Active)
            {
				UpdateQuest(pc, 277, 27701, StepStatus.Completed);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 277);
				AddNavPoint(277, 27703, 5, 1007, 22528f, 88672f, 5120f); //Magnet	
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
			}

            if (GetQuestStepStatus(pc, 277, 27703) == StepStatus.Active && CountItem(pc, 4043) > 3)
            {
                UpdateQuest(pc, 277, 27703, StepStatus.Completed);
				TakeItem(pc, 4043, 4);
		        UpdateIcon(pc);
				RemoveNavPoint(pc, 277);
				QuestCompleted(pc, 277);
				NPCSpeech(pc, 823);
				NPCChat(pc, 0);
				SetReward(pc, new rewardfunc(OnReward));
			}

            if (GetQuestStepStatus(pc, 278, 27801) == StepStatus.Active)
            {
                UpdateQuest(pc, 278, 27801, StepStatus.Completed);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 278);
				AddNavPoint(278, 27803, 5, 1007, 22528f, 88672f, 5120f); //Magnet	
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
			}

            if (GetQuestStepStatus(pc, 278, 27803) == StepStatus.Active && CountItem(pc, 4044) > 7)
            {
                UpdateQuest(pc, 278, 27803, StepStatus.Completed);
				TakeItem(pc, 4044, 8);
		        UpdateIcon(pc);
				RemoveNavPoint(pc, 278);
				QuestCompleted(pc, 278);
				NPCSpeech(pc, 823);
				NPCChat(pc, 0);
				SetReward(pc, new rewardfunc(OnReward));
		    }

            if (GetQuestStepStatus(pc, 302, 30201) == StepStatus.Active)
            {
                UpdateQuest(pc, 302, 30201, StepStatus.Completed);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 302);
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
			}

            if (GetQuestStepStatus(pc, 303, 30301) == StepStatus.Active)
            {
                UpdateQuest(pc, 303, 30301, StepStatus.Completed);
				GiveItem(pc, 4052, 1);
				RemoveNavPoint(pc, 303);
				SendNavPoint(pc, 303, 1008, 16287f, 94272f, 4192f);
				UpdateIcon(pc);
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
			}

            if (GetQuestStepStatus(pc, 305, 30502) == StepStatus.Active)
            {
                UpdateQuest(pc, 305, 30502, StepStatus.Completed);
				UpdateIcon(pc);
                RemoveNavPoint(pc, 305);
				QuestCompleted(pc, 305);
				NPCSpeech(pc, 823);
				NPCChat(pc, 0);
				SetReward(pc, new rewardfunc(OnReward));
			}

            if (GetQuestStepStatus(pc, 306, 30601) == StepStatus.Active)
            {
                UpdateQuest(pc, 306, 30601, StepStatus.Completed);
				GiveItem(pc, 4054, 1);
				SendNavPoint(pc, 306, 1008, 16287f, 94272f, 4192f);
				UpdateIcon(pc);
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
			}
        }

    	public void OnReward(ActorPC pc, uint QID)
    	{
		    if (QID == 276)
		    {
				GiveExp(pc, 0, 2270);
				GiveZeny(pc, 791);
				GiveItem(pc, 1700114, 1);
				RemoveQuest(pc, 276);
            	AddStep(277, 27701);
            	AddStep(277, 27702);
            	AddStep(277, 27703);
				AddNavPoint(277, 27701, 5, 1007, 22528f, 88672f, 5120f); //Magnet
            	QuestStart(pc);
				UpdateIcon(pc);
				SendNavPoint(pc);
			}

		    if (QID == 277)
		    {
				GiveExp(pc, 0, 1469);
				GiveZeny(pc, 512);
				RemoveQuest(pc, 277);
            	AddStep(278, 27801);
            	AddStep(278, 27802);
            	AddStep(278, 27803);
				AddNavPoint(278, 27801, 5, 1007, 22528f, 88672f, 5120f); //Magnet
            	QuestStart(pc);
				UpdateIcon(pc);
				SendNavPoint(pc);
			}

		    if (QID == 278)
		    {
				GiveExp(pc, 0, 2072);
				GiveZeny(pc, 707);
				RemoveQuest(pc, 278);
            	AddStep(302, 30201);
            	AddStep(302, 30202);
				AddStep(302, 30203);
				AddNavPoint(302, 30201, 5, 1007, 22528f, 88672f, 5120f); //Magnet	
            	QuestStart(pc);
				UpdateIcon(pc);
				SendNavPoint(pc);
		    }

		    if (QID == 305)
		    {
				GiveExp(pc, 2943, 0);
				GiveZeny(pc, 600);
				RemoveQuest(pc, 305);
            	AddStep(306, 30601);
            	AddStep(306, 30602);
				AddNavPoint(305, 30501, 5, 1051, 14783f, 78975f, 5088f); //Nikki
            	QuestStart(pc);
				UpdateIcon(pc);
				SendNavPoint(pc);
			}
		}
    }
}