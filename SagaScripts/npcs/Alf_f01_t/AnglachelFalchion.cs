using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class AnglachelFalchion : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1106;
            Name = "AnglachelFalchion";
            StartX = -11108F;
            StartY = -9897F;
            StartZ = 9320;
            Startyaw = -37488;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}