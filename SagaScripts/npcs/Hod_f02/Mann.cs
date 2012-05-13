using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Hod02
{
    public class Mann : Npc
    {
        public override void OnInit()
        {
            MapName = "Hod_f02";
            Type = 1060;
            Name = "Sigiswald Mann";
            StartX = -4768F;
            StartY = -15392F;
            StartZ = 344;
            Startyaw = 1000;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}