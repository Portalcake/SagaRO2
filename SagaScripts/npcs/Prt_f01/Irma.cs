using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Irman : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1071;
            Name = "Irma Swane";
            StartX = 16064F;
            StartY = 73503F;
            StartZ = 5014;
            Startyaw = 32768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}