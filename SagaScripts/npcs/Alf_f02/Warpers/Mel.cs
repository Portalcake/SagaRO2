using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f02
{
    public class Mel : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f02";
            Type = 1310;
            Name = "Mel";
            StartX = 31079F;
            StartY = -773F;
            StartZ = -1956;
            Startyaw = 24296;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}