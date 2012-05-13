using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f04
{
public class NicholasSid : Npc
{
    public override void OnInit()
    {
        MapName = "Prt_f04";
        Type = 1295;
        Name = "Nicholas Sid";
        StartX = 42042F;
        StartY = 44069F;
        StartZ = -12733F;
        Startyaw = 432;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
       }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}