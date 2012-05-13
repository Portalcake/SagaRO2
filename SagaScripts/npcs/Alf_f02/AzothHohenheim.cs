using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f02
{
    public class AzothHohenheim : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f02";
            Type = 1107;
            Name = "Azoth Hohenheim";
            StartX = -50408F;
            StartY = -2393F;
            StartZ = 3756;
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