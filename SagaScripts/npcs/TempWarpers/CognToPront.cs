///By Niyan, Checked and Modified by Chii

using SagaMap;
using SagaDB.Actors;

public class cogDockWarper : Npc
{
    public override void  OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1016;
        Name = "To Prontera Harbour";
        StartX = 14326F;
        StartY = -6239F;
        StartZ = -5325F;
	Startyaw = 43221;
        SetScript(3);
        AddButton(Functions.EnterShip,new func(OnButton));
    }

    public void OnButton(ActorPC pc)
    {
          Warp(pc, 6, 29752f, -32378f, -4942f);
    } 
}