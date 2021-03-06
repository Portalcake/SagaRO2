using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Anne : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1162;
            Name = "Anne";
            StartX = 11840F;
            StartY = 84255F;
            StartZ = 5024;
            Startyaw = 66768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}