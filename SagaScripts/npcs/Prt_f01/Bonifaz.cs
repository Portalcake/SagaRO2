using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Bonifaz : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1072;
            Name = "Bonifaz Knapp";
            StartX = 13120F;
            StartY = 43296F;
            StartZ = 9561;
            Startyaw = 79768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}