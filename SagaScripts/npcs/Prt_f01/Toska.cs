using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Toska : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1073;
            Name = "Toska Alden";
            StartX = 14043F;
            StartY = 13242F;
            StartZ = 13282;
            Startyaw = 81804;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}