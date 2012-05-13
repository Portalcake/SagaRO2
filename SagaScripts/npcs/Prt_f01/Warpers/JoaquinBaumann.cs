using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class JoaquinBaumann : Npc
{
    public override void OnInit()
    {
        MapName = "Prt_f01";
        Type = 1280;
        Name = "Joaquin Baumann";
        StartX = 13804F;
        StartY = 76914F;
        StartZ = 5068F;
        Startyaw = 49104;
        SetScript(823);
        AddButton(Functions.EnterShip,new func(OnButton));
    }

    public void OnButton(ActorPC pc)
    {
          Warp(pc, 20, -14231.8f, -18221.53f, 9549.206f);
    } 
}