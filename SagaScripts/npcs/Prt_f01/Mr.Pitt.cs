using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Mr_Pitt : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1008;
            Name = "Mr. Pitt";
            StartX = 16287F;
            StartY = 94272F;
            StartZ = 4192;
            Startyaw = 73768;
            SetScript(3);
            SetSavePoint(5, 13725f, 75364f, 5094);

            // ---- Quest Steps -----------------------------------------

            AddQuestStep(303, 30302, StepStatus.Active);
            AddQuestStep(304, 30401, StepStatus.Active);
            AddQuestStep(306, 30602, StepStatus.Active);

            AddButton(Functions.OfficialQuest, new func(OnQuest), true);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.Kafra);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

        public void OnQuest(ActorPC pc)
        {
            if (GetQuestStepStatus(pc, 303, 30302) == StepStatus.Active && CountItem(pc, 4052) > 0)
            {
                UpdateQuest(pc, 303, 30302, StepStatus.Completed);
                TakeItem(pc, 4052, 1);
                RemoveNavPoint(pc, 303);
                UpdateIcon(pc);
                QuestCompleted(pc, 303);
                NPCSpeech(pc, 823);
                NPCChat(pc, 0);
                SetReward(pc, new rewardfunc(OnReward));
            }

            if (GetQuestStepStatus(pc, 304, 30401) == StepStatus.Active)
            {
                UpdateQuest(pc, 304, 30401, StepStatus.Completed);
                GiveItem(pc, 4053, 1);
				RemoveNavPoint(pc, 302);
                SendNavPoint(pc, 304, 1051, 14783f, 78975f, 5088f);
                UpdateIcon(pc);
                NPCSpeech(pc, 823);
                NPCChat(pc, 0);
            }

            if (GetQuestStepStatus(pc, 306, 30602) == StepStatus.Active && CountItem(pc, 4054) > 0)
            {
                UpdateQuest(pc, 306, 30602, StepStatus.Completed);
                TakeItem(pc, 4054, 1);
                RemoveNavPoint(pc, 306);
                UpdateIcon(pc);
                QuestCompleted(pc, 306);
                NPCSpeech(pc, 823);
                NPCChat(pc, 0);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }

        public void OnReward(ActorPC pc, uint QID)
        {
            if (QID == 303)
            {
                GiveExp(pc, 0, 1788);
                GiveZeny(pc, 600);
                RemoveQuest(pc, 303);
                AddStep(304, 30401);
                AddStep(304, 30402);
				AddNavPoint(304, 30401, 5, 1008, 16287f, 94272f, 4192f); //Mr Pitt
                QuestStart(pc);
				SendNavPoint(pc);
				UpdateIcon(pc);
            }

            if (QID == 306)
            {
                GiveExp(pc, 0, 1788);
                GiveZeny(pc, 600);
				GiveItem(pc, 1700114, 3);
                RemoveQuest(pc, 306);
                QuestStart(pc);
            }
        }
    }
}