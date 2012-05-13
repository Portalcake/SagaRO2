using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f02
{
    public class Orak : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 1099;
            Name = "Orak Edith";
            StartX = 18432F;
            StartY = -35936F;
            StartZ = -4015;
            Startyaw = 23064;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}