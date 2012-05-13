using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f02
{
    public class Ivadel : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f02";
            Type = 1312;
            Name = "Ivadel";
            StartX = 1934F;
            StartY = 19509F;
            StartZ = -1731;
            Startyaw = -8;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}