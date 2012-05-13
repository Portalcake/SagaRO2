using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace rag2startzone_01
{
    public class Rein : Npc
    {
        public override void OnInit()
        {
        MapName = "rag2startzone_01";
        Type = 1173;
        Name = "Rein";
        StartX = 9248.0F;
        StartY = -6144.0F;
        StartZ = 96F;
        Startyaw = 12000;
        SetScript(4003);
        AddQuestStep(396, 39603, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
        }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 4006);
    }

    public void OnQuest(ActorPC pc)
    {
        if (CountItem(pc, 9944) >= 1)
        {
            TakeItem(pc, 9944, 1);
            UpdateQuest(pc, 396, 39603, StepStatus.Completed);
            QuestCompleted(pc, 396);
            UpdateIcon(pc);
            RemoveNavPoint(pc, 396);
            NPCChat(pc, 3922);
            SetReward(pc, new rewardfunc(OnReward));
        }
    }

    public void OnReward(ActorPC pc, uint QID)
    {
        if (QID == 396)
        {
            GiveItem(pc, 50150000, 1);
            GiveExp(pc, 140, 50);
            GiveZeny(pc, 7);
            RemoveQuest(pc, 396);
            AddStep(397, 39701);
            AddStep(397, 39702);
            QuestStart(pc);
           }
       }
   }
}