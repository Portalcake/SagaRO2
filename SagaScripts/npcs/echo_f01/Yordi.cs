using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class Yordi : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1268;
            Name = "Yordi";
            StartX = 15707F;
            StartY = -45652F;
            StartZ = -23345;
            Startyaw = 19840;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}