using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaMap.Scripting;


namespace Hod_f01.QuestHandlers
{
    public class _25 : QuestElement
    {
        public _25()
        {
            AddStepHandler(2501, Step2501);
        }
        
        public void Step2501(ActorPC pc)
        {
            UpdateQuest(pc, 25, 2501, StepStatus.Completed);
            UpdateIcon(pc);
            NPCSpeech(pc, 229);
            NPCChat(pc, 0);
            RemoveNavPoint(pc, 25);
            SendNavPoint(pc, 25, 1005, -1216f, 3328f, -10144f);
        }
    }
}
