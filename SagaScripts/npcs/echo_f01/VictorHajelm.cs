using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class VictorHajelm : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1266;
            Name = "Victor Hajelm";
            StartX = 13265F;
            StartY = -38406F;
            StartZ = -23758;
            Startyaw = 23528;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}