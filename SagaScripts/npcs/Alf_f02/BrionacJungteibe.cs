using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f02
{
    public class BrionacJungteibe : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f02";
            Type = 1109;
            Name = "Brionac Jungteibe";
            StartX = -49350F;
            StartY = -1399F;
            StartZ = 3728;
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