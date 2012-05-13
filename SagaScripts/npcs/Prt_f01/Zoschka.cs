using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Zoschka : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1076;
            Name = "Zoschka Whiting";
            StartX = 12448F;
            StartY = 79680F;
            StartZ = 5056;
            Startyaw = 36768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}