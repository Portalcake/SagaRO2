using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class Eglon6 : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1250;
            Name = "Eglon6";
            StartX = 14125F;
            StartY = 3868F;
            StartZ = 9824;
            Startyaw = 46921;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}