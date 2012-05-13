using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Hod02
{
    public class Sill : Npc
    {
        public override void OnInit()
        {
            MapName = "Hod_f02";
            Type = 1143;
            Name = "Adria Sill";
            StartX = -3392F;
            StartY = -1344F;
            StartZ = 138;
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