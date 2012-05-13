using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Pelshia : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1013;
            Name = "Pelshia Hiltrud";
            StartX = 20823F;
            StartY = 68260F;
            StartZ = 5210F;
            Startyaw = 26817;
            SetScript(3);
			
			AddQuestStep(158, 15804, StepStatus.Active);
			
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.OfficialQuest, new func(OnQuest), true);
        }

        public void OnButton(ActorPC pc)
		{
			 NPCChat(pc, 823);
		}

        public void OnQuest(ActorPC pc)
        {
            if (GetQuestStepStatus(pc, 158, 15804) == StepStatus.Active)
            {
                UpdateQuest(pc, 158, 15804, StepStatus.Completed);
				UpdateIcon(pc);
                RemoveNavPoint(pc, 158);
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
			}
        }
    }
}