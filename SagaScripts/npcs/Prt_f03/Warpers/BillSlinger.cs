using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f03
{
public class BillSlinger : Npc
{
    public override void OnInit()
    {
        MapName = "Prt_f03";
        Type = 1288;
        Name = "Bill Slinger";
        StartX = -23593F;
        StartY = -39876F;
        StartZ = 246F;
        Startyaw = 56200;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}