using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Pikas : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1090;
            Name = "Pikas Wibert";
            StartX = 40352F;
            StartY = 83039F;
            StartZ = 6358;
            Startyaw = 35168;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}