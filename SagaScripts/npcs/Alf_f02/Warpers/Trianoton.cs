using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f02
{
    public class EliasRasch : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f02";
            Type = 1309;
            Name = "Trianoton";
            StartX = -30080F;
            StartY = -22027F;
            StartZ = 1026;
            Startyaw = 6280;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}