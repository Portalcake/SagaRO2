using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class Eglon5 : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1250;
            Name = "Eglon5";
            StartX = 882F;
            StartY = -27217F;
            StartZ = 10230;
            Startyaw = 31499;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}