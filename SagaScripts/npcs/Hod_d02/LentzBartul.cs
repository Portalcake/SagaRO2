using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Hod_d02
{
    public class LentzBartul : Npc
    {
        public override void OnInit()
        {
            MapName = "Hod_d02";
            Type = 1224;
            Name = "Lentz Bartul";
            StartX = 10686F;
            StartY = 38135F;
            StartZ = -2215;
            Startyaw = 52000;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.Smith);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 2444);
        }

    }
}