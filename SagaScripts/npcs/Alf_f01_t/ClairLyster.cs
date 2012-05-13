using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class ClairLyster : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1316;
            Name = "ClairLyster";
            StartX = -14128F;
            StartY = -19805F;
            StartZ = 9427;
            Startyaw = -48264;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}