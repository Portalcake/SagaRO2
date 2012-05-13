using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f04
{
public class CymonLucy : Npc
{
    public override void OnInit()
    {
        MapName = "prt_f04";
        Type = 1293;
        Name = "Cymon Lucy";
        StartX = 26850F;
        StartY = -29473F;
        StartZ = -11080F;
        Startyaw = -15496;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
       }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}