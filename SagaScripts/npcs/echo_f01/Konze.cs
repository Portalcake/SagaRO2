using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class Konze : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1252;
            Name = "Konze";
            StartX = 12437F;
            StartY = -42214F;
            StartZ = -23408;
            Startyaw = 1984;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}