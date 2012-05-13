using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Ludwig : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1070;
            Name = "Ludwig Frisby";
            StartX = 19920F;
            StartY = 90323F;
            StartZ = 5056;
            Startyaw = 65400;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}