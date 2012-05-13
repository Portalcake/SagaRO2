using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f04
{
public class RodrigoDiaz : Npc
{
    public override void OnInit()
    {
        MapName = "Prt_f04";
        Type = 1294;
        Name = "Rodrigo Diaz";
        StartX = -3665F;
        StartY = 17796F;
        StartZ = -9047F;
        Startyaw = -8832;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
       }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}