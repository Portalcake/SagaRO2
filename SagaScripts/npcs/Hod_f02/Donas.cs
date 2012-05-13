using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Hod02
{
    public class Donas : Npc
    {
        public override void OnInit()
        {
            MapName = "Hod_f02";
            Type = 1144;
            Name = "Donas";
            StartX = 38240F;
            StartY = 12608F;
            StartZ = 3543;
            Startyaw = 0;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}