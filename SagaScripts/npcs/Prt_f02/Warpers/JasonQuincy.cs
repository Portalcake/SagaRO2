using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class JasonQuincy : Npc
{
    public override void OnInit()
    {
        MapName = "Prt_f02";
        Type = 1285;
        Name = "Jason Quincy";
        StartX = 32244F;
        StartY = 42204F;
        StartZ = -5002F;
        Startyaw = -23520;
        SetScript(823);
        AddButton(Functions.EnterShip,new func(OnButton));
    }

    public void OnButton(ActorPC pc)
    {
          Warp(pc, 20, -14231.8f, -18221.53f, 9549.206f);
    } 
}