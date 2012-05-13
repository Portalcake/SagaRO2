using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaMap.Scripting;

namespace Hod_f01.QuestHandlers
{
    public class _6:QuestElement
    {
        public _6()
        {
            AddStepHandler(602, Step602);
        }
        public void Step602(ActorPC pc)
        {
            UpdateQuest(pc, 6, 602, StepStatus.Completed);
            GiveItem(pc, 2619, 1);
            UpdateIcon(pc);
            NPCSpeech(pc, 48);
            NPCChat(pc, 0);
            RemoveNavPoint(pc, 6);
            SendNavPoint(pc, 6, 10023, 12484f, -15132f, -4779f);
        }
    }
}
