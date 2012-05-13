using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class Harot_Sabirine : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1019;
            Name = "Harot_Sabirine";
            StartX = 10938F;
            StartY = 3455F;
            StartZ = 7623;
            Startyaw = -81576;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}