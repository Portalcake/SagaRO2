   //////////////////////////////////
  ///        Chii 19/11/07       ///
 ///     Zipu Prontera Guard    ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Zipu : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1096;
            Name = "Zipu Hanse";
            StartX = 2054F;
            StartY = 85383F;
            StartZ = 4283;
            Startyaw = 379;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}