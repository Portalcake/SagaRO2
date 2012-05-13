using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class Gogonyak : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1253;
            Name = "Gogonyak";
            StartX = 17842F;
            StartY = -41731F;
            StartZ = -23370;
            Startyaw = 34608;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}