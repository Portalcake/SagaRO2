using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f02
{
    public class Lothair : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 1079;
            Name = "Lothair Eaton";
            StartX = -41474F;
            StartY = -27485F;
            StartZ = 1306;
            Startyaw = -8432;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}