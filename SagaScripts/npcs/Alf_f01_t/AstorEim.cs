using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class AstorEim : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1101;
            Name = "AstorEim";
            StartX = 10300F;
            StartY = 2336F;
            StartZ = 7591;
            Startyaw = -124152;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}