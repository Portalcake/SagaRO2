using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f04
{
public class OliverGraf : Npc
{
    public override void OnInit()
    {
        MapName = "Prt_f04";
        Type = 1292;
        Name = "Oliver Graf";
        StartX = -19607F;
        StartY = -19247F;
        StartZ = -11557F;
        Startyaw = -41416;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
       }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}