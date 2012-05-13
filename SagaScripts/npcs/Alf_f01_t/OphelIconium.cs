using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class OphelIconium : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1102;
            Name = "OphelIconium";
            StartX = -11545F;
            StartY = -20103F;
            StartZ = 9570;
            Startyaw = -36064;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}