using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class Pablo : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1258;
            Name = "Pablo";
            StartX = 17093F;
            StartY = -41365F;
            StartZ = -23357;
            Startyaw = -20416;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}