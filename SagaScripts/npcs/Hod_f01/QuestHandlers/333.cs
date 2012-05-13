using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaMap.Scripting;


namespace Hod_f01.QuestHandlers
{
    public class _333 : QuestElement
    {
        public _333()
        {
            AddStepHandler(33302, Step33302);
        }
        
        public void Step33302(ActorPC pc)
        {
            if (CountItem(pc, 2651) > 0)
            {
                UpdateQuest(pc, 333, 33302, StepStatus.Completed);
                TakeItem(pc, 2651, 1);
                UpdateIcon(pc);
                NPCSpeech(pc, 223);
                NPCChat(pc, 0);
                RemoveNavPoint(pc, 333);
                SendNavPoint(pc, 333, 1005, -1216f, 3328f, -10144f);
            }
        }
    }
}
