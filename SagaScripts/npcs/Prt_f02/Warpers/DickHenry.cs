using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class DickHenry : Npc
{
    public override void OnInit()
    {
        MapName = "Prt_f02";
        Type = 1287;
        Name = "Dick Henry";
        StartX = 21057F;
        StartY = -33607F;
        StartZ = -4222F;
        Startyaw = 33784;
        SetScript(823);
        AddButton(Functions.EnterShip,new func(OnButton));
    }

    public void OnButton(ActorPC pc)
    {
          Warp(pc, 20, -14231.8f, -18221.53f, 9549.206f);
    } 
}