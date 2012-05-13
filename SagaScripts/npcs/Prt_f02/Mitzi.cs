using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f02
{
    public class Mitzi : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 1081;
            Name = "Mitzi";
            StartX = -10799F;
            StartY = 22436F;
            StartZ = 1702;
            Startyaw = -21624;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}