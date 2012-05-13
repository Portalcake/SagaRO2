using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class SamRamon : Npc
{
    public override void OnInit()
    {
        MapName = "Prt_f02";
        Type = 1285;
        Name = "Sam Ramon";
        StartX = -3716F;
        StartY = 2222F;
        StartZ = -404F;
        Startyaw = 21312;
        SetScript(823);
        AddButton(Functions.EnterShip,new func(OnButton));
    }

    public void OnButton(ActorPC pc)
    {
          Warp(pc, 20, -14231.8f, -18221.53f, 9549.206f);
    } 
}