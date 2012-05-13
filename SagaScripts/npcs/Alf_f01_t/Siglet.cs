using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class Siglet : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1016;
            Name = "Siglet Jungteibe";
            StartX = -3617F;
            StartY = 10479F;
            StartZ = 7847;
            Startyaw = -27488;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}