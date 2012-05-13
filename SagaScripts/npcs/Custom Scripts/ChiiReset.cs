   //////////////////////////////////
  ///        Chii 13/01/08       ///
 ///        Stat Resetter       ///
//////////////////////////////////

//Chii Note: This is a custom script and calls corresponding custom npcScripts from NpcScripts.pak

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class ChiiReset : Npc
{
	public override void OnInit()
	{
	MapName = "Prt_f01";
		Type = 1239;
		Name = "ChiiResetter";
		StartX = 16463F;
		StartY = 97017;
		StartZ = 4273;
		Startyaw = 21415;
		SetScript(60000001);
		AddButton(Functions.EverydayConversation, new func(function));
		AddButton(Functions.OfficialQuest,new func(OnButton));
	}

	public void function(ActorPC pc)
	{
		NPCChat(pc, 60000002);
	}
	public void OnButton(ActorPC pc)
	{
		pc.luk = 0;
		pc.str = 5;
		pc.dex = 3;
		pc. intel = 3;
		pc.con = 2;
		pc.stpoints = (byte)((pc.cLevel-1)*2);
		NPCChat(pc, 60000003);
		Warp(pc,6,-14811,-56337,3153);
	}
}