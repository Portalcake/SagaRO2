using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Zilbert : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1074;
            Name = "Zilbert Huntington";
            StartX = 13985F;
            StartY = 27142F;
            StartZ = 13274F;
            Startyaw = 81768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}