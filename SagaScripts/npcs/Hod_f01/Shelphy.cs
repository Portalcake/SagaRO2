  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
using SagaMap.Scripting;
 
using SagaDB.Actors;
using SagaDB.Items;
namespace Hod_f01
{    
    public class Shelphy : Npc
    {
        public class QuestGroupSh : QuestGroup
        {
            public QuestGroupSh()
            {
                AddHandler(6, new QuestHandlers._6());
                AddHandler(25, new QuestHandlers._25());
                AddHandler(333, new QuestHandlers._333());
                AddHandler(334, new QuestHandlers._334());
            }
        }
        //Kafra
        public override void OnInit()
        {
            MapName = "Hod_f01";
            Type = 1002;
            Name = "Shelphy Adriana";
            StartX = 1460F;
            StartY = -13664F;
            StartZ = -6472F;
            Startyaw = 1000;
            SetScript(1810);
            SetSavePoint(1, 1655f, -13364f, -6403);
            AddPersonalQuestStep(6, 602, StepStatus.Active);
            AddQuestStep(25, 2501, StepStatus.Active);
            AddQuestStep(333, 33302, StepStatus.Active);
            AddQuestStep(334, 33402, StepStatus.Active);
            SetQuestGroup(new QuestGroupSh());
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.Kafra);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 906);
        }
        

    }
}