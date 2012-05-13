using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Oscar : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1055;
            Name = "Oscar Eatone";
            StartX = 9512F;
            StartY = 86979F;
            StartZ = 4193;
            Startyaw = 32768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.Regenbogen);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}
