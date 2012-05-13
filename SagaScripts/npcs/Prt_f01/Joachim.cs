using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Joachim : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1014;
            Name = "Joachim Tristan";
            StartX = 14032F;
            StartY = -347F;
            StartZ = 13233;
            Startyaw = 16500;
            SetScript(3);
			
			AddQuestStep(158, 15802, StepStatus.Active);
			
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.OfficialQuest, new func(OnQuest), true);
        }

        public void OnButton(ActorPC pc)
            {
                NPCChat(pc, 823);
            }

        public void OnQuest(ActorPC pc)
        {
            if (GetQuestStepStatus(pc, 158, 15802) == StepStatus.Active)
            {
				GiveItem(pc, 3972, 1);
                UpdateQuest(pc, 158, 15802, StepStatus.Completed);
				UpdateIcon(pc);
                RemoveNavPoint(pc, 158);
				SendNavPoint(pc, 158, 1151, 21668f, 7200f, 13318f);
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
			}
        }
    }
}