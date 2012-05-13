///By Niyan, Checked and Modified by Chii

using SagaMap;
using SagaDB.Actors;

public class prontDockWarper2 : Npc
{
    public override void  OnInit()
    {
        MapName = "Prt_f02";
        Type = 1016;
        Name = "To Cognito Harbour";
        StartX = 30614F;
        StartY = -32363F;
        StartZ = -4781F;
	Startyaw = 33597;
        SetScript(3);
        AddButton(Functions.EnterShip,new func(OnButton));
    }

    public void OnButton(ActorPC pc)
    {
          Warp(pc, 13, 14190f, -6451f, -5474f);
    } 
}