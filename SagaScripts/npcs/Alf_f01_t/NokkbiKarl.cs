using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class NokkbiKarl : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1241;
            Name = "NokkbiKarl";
            StartX = 5364F;
            StartY = 4827F;
            StartZ = 7615;
            Startyaw = -65104;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}