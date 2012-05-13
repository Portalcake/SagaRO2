using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f03
{
public class BenjaminMeier : Npc
{
    public override void OnInit()
    {
        MapName = "Prt_f03";
        Type = 1290;
        Name = "Benjamin Meier";
        StartX = -11109F;
        StartY = 24359F;
        StartZ = 1598F;
        Startyaw = 6416;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}