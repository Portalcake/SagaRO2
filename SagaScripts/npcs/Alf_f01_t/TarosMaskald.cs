using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class TarosMaskald : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1247;
            Name = "TarosMaskald";
            StartX = -1951F;
            StartY = -19636F;
            StartZ = 9473;
            Startyaw = -32040;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}