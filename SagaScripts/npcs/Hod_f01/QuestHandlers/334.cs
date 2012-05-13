using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaMap.Scripting;


namespace Hod_f01.QuestHandlers
{
    public class _334 : QuestElement
    {
        public _334()
        {
            AddStepHandler(33402, Step33402);
        }
        
        public void Step33402(ActorPC pc)
        {
            if (CountItem(pc, 2654) == 0)
            {
                UpdateIcon(pc);
                GiveItem(pc, 2654, 1);
                TakeItem(pc, 2654, 1);
                NPCSpeech(pc, 241);
                NPCChat(pc, 0);
            }
        }
    }
}
