///By Niyan, Checked and Modified by Chii

using SagaMap;
using SagaDB.Actors;

public class hodeDockWarper : Npc
{
    public override void  OnInit()
    {
        MapName = "Hod_f02";
        Type = 1016;
        Name = "To Prontera Harbour";
        StartX = -8369F;
        StartY = -16062F;
        StartZ = 568F;
	Startyaw = 1475;
        SetScript(3);
        AddButton(Functions.EnterShip,new func(OnButton));
    }

    public void OnButton(ActorPC pc)
    {
          Warp(pc, 6, 30109f, -35516f, -4785f);
    } 
}