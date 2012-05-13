using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Mayer : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1223;
            Name = "Mayer";
            StartX = 15475F;
            StartY = 67344F;
            StartZ = 5064;
            Startyaw = 85736;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}