using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f02
{
    public class Hanne : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 1089;
            Name = "Hanne William";
            StartX = -29860F;
            StartY = 4500F;
            StartZ = 1082;
            Startyaw = 0;
            SetScript(3);
			
			List<uint> Mobs = new List<uint>();
            Mobs.Add(10265);
            AddEnemyInfo(178, 17802, Mobs, 1);
			
		    AddQuestStep(177, 17703, StepStatus.Active);
		    AddQuestStep(178, 17801, StepStatus.Active);
		    AddQuestStep(178, 17803, StepStatus.Active);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.OfficialQuest, new func(OnQuest), true);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }
    	public void OnQuest(ActorPC pc)
    	{
			if (GetQuestStepStatus(pc, 177, 17703) == StepStatus.Active)
			{
				UpdateQuest(pc, 177, 17703, StepStatus.Completed);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 177);
				QuestCompleted(pc, 177);
				NPCSpeech(pc, 823);
				NPCChat(pc, 0);
				SetReward(pc, new rewardfunc(OnReward));
			}
			
			if (GetQuestStepStatus(pc, 178, 17801) == StepStatus.Active)
			{
				UpdateQuest(pc, 178, 17801, StepStatus.Completed);
			    UpdateIcon(pc);
				NPCSpeech(pc, 823);
				NPCChat(pc, 0);
			}
			
			if (GetQuestStepStatus(pc, 178, 17803) == StepStatus.Active)
			{
				UpdateQuest(pc, 178, 17803, StepStatus.Completed);
			    UpdateIcon(pc);
				RemoveNavPoint(pc, 178);
				AddNavPoint(178, 17804, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika
				SendNavPoint(pc);
				NPCSpeech(pc, 823);
				NPCChat(pc, 0);
			}
		}	
		
    	public void OnReward(ActorPC pc, uint QID)
    	{
			if (QID == 177)
			{
				GiveExp(pc, 1400, 0);
				GiveZeny(pc, 324);
				RemoveQuest(pc, 177);
				AddStep(178, 17801);
				AddStep(178, 17802);
				AddStep(178, 17803);
				AddStep(178, 17804);
				AddNavPoint(178, 17801, 6, 1089, -29860f, 4500f, 1082f); //Hanne	
				QuestStart(pc);  
				UpdateIcon(pc); 
				SendNavPoint(pc);
			}
		}
    }
}