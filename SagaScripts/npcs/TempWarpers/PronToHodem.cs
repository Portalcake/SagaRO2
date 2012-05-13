///By Niyan, Checked and Modified by Chii

using SagaMap;
using SagaDB.Actors;

public class prontDockWarper : Npc
{
    public override void  OnInit()
    {
        MapName = "Prt_f02";
        Type = 1016;
        Name = "To Hodemimes Harbour";
        StartX = 30742F;
        StartY = -35474F;
        StartZ = -4860F;
	Startyaw = 33497;
        SetScript(3);
        AddButton(Functions.EnterShip,new func(OnButton));
    }

    public void OnButton(ActorPC pc)
    {
          Warp(pc, 2, -7660f, -15972f, 435f);
    } 
}