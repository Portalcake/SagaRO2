using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class LattiYobb : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1264;
            Name = "Latti Yobb";
            StartX = 16818F;
            StartY = -44283F;
            StartZ = -23339;
            Startyaw = 19784;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}