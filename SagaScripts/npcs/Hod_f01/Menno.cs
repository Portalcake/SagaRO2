  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Menno : Npc
{
	public override void OnInit()
	{
		MapName = "Hod_f01";
		Type = 1228;
		Name = "Menno Buckstone";
		StartX = 836F;
		StartY = -15182F;
		StartZ = -6394F;
		Startyaw = 12520;
		SetScript(4775);
		AddQuestStep(397, 39702, StepStatus.Active);
		AddButton(Functions.EverydayConversation, new func(OnButton));
		
		AddButton(Functions.Supply);
		SupplyMenuID = 2;

		//Exchange

		// Create Big Candy
		AddSupplyProduct(7,3604,1); // ID 60 since Elisa.cs stopped at 59
		AddSupplyMatrial(7,9407,20); // Fruity Smelling Soap x20
		AddSupplyMatrial(7,9456,20); // Fish Oil x20
		AddSupplyMatrial(7,9447,20); // Fish Food x20
		//Add 800Rufi

		// Create Stars Headgear
		AddSupplyProduct(8,3503,1);
		AddSupplyMatrial(8,9507,20); // Lucky Star x20
		AddSupplyMatrial(8,9493,20); // Coral Piece x20
		AddSupplyMatrial(8,9444,10); // Mermaid Hairpin x10
		//Add 800 Rufi

		// Create Beardoll Hat
		AddSupplyProduct(9,3437,1);
		AddSupplyMatrial(9,9400,20); // Autumn Leave x20
		AddSupplyMatrial(9,9437,15); // Red Crystal x15
		AddSupplyMatrial(9,9438,15); // Blue Crystal x15
		//Add 800 Ruffi

		// Create Crab Pin
		AddSupplyProduct(10,3506,1);
		AddSupplyMatrial(10,9504,20); // Claws x20
		AddSupplyMatrial(10,9505,10); // Broken Cute doll x10
		AddSupplyMatrial(10,9506,1); // VadonZ's Clock Wheel x1
		//Add 800 Ruffi

		// Create Angel Ring
		AddSupplyProduct(11,3514,1);
		AddSupplyMatrial(11,9406,20); // Fruit Shell x20
		AddSupplyMatrial(11,9402,20); // Seed x20
		AddSupplyMatrial(11,9453,10); // Hydra Tears x10
		AddSupplyMatrial(11,9503,1); // Broken Soul Ring x1
		//Add 800 Ruffi
	}
	
	public void OnButton(ActorPC pc)
	{
		NPCChat(pc, 4778);
	}
}
