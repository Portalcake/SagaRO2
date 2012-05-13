using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f03
{
    public class Glasshood : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f03";
            Type = 1155;
            Name = "Glasshood";
            StartX = 26432F;
            StartY = -24240F;
            StartZ = 7157;
            Startyaw = 23048;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}