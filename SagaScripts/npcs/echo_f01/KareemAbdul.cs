using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class KareemAbdul : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1259;
            Name = "Kareem Abdul";
            StartX = 15540F;
            StartY = -45664F;
            StartZ = -23326;
            Startyaw = 14464;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}