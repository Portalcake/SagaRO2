using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f02
{
    public class LimeSasha : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f02";
            Type = 1235;
            Name = "Lime Sasha";
            StartX = -47965F;
            StartY = -1431F;
            StartZ = 3486;
            Startyaw = 0;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}