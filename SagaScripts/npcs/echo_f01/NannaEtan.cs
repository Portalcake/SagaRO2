using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class NannaEtan : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1260;
            Name = "Nanna Etan";
            StartX = 16351F;
            StartY = -44780F;
            StartZ = -23330;
            Startyaw = 29136;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}