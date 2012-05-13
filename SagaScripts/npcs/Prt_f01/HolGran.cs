using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class HolGran : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1011;
            Name = "HolGran";
            StartX = 17664F;
            StartY = 74272F;
            StartZ = 5044;
            Startyaw = 49768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.Smith);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}