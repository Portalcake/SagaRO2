  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Klaret : Npc//Shop
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1001;
        Name = "Klaret Natali se Pruysenaere";
        StartX = -3680F;
        StartY = -5008F;
        StartZ = -8045F;
        Startyaw = 38000;
        SetScript(500);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
        AddButton(Functions.Shop);
        AddButton(Functions.Supply);
        SupplyMenuID = 10;
		
//Exchange
// Create Red Potion I
            AddSupplyProduct(46,51500003,1);
            AddSupplyMatrial(46,9406,1);
            AddSupplyMatrial(46,9500,1);
// Create Red Potion II
            AddSupplyProduct(47,51500004,1);
            AddSupplyMatrial(47,9406,1);
            AddSupplyMatrial(47,9500,1);
            AddSupplyMatrial(47,10490,1);
// Create Red Potion III
            AddSupplyProduct(48,51500005,1);
            AddSupplyMatrial(48,9406,1);
            AddSupplyMatrial(48,1700051,1);
            AddSupplyMatrial(48,9338,1);
// Create Red Potion IV
            AddSupplyProduct(49,51500006,1);
            AddSupplyMatrial(49,1700009,1);
            AddSupplyMatrial(49,1700051,1);
            AddSupplyMatrial(49,9338,1);
// Create Red Potion V
            AddSupplyProduct(50,51500007,1);
            AddSupplyMatrial(50,1700009,1);
            AddSupplyMatrial(50,1700051,1);
            AddSupplyMatrial(50,1700075,1);
// Create Red Potion VI
            AddSupplyProduct(51,51500008,1);
            AddSupplyMatrial(51,1700009,1);
            AddSupplyMatrial(51,1700097,1);
            AddSupplyMatrial(51,1700078,1);
// Create Red Potion VII
            AddSupplyProduct(52,51500009,1);
            AddSupplyMatrial(52,1700009,1);
            AddSupplyMatrial(52,1700097,1);
            AddSupplyMatrial(52,1700079,1);
// Create Red Potion VIII
            AddSupplyProduct(53,51500010,1);
            AddSupplyMatrial(53,1700009,1);
            AddSupplyMatrial(53,1700097,1);
            AddSupplyMatrial(53,1700088,1);
// Create Blue Potion I
            AddSupplyProduct(54,51500013,1);
            AddSupplyMatrial(54,1700009,1);
            AddSupplyMatrial(54,9453,1);
            AddSupplyMatrial(54,10446,1);
            AddSupplyMatrial(54,1700013,1);
// Create Blue Potion II
            AddSupplyProduct(55,51500014,1);
            AddSupplyMatrial(55,1700009,1);
            AddSupplyMatrial(55,2686,1);
            AddSupplyMatrial(55,1700077,1);
            AddSupplyMatrial(55,1700078,1);
			

//Goods
AddGoods(4101); AddGoods(2575); AddGoods(51500002); AddGoods(51500003); AddGoods(51500004); AddGoods(51500005); AddGoods(51500006); AddGoods(51500007); AddGoods(51500008); AddGoods(51500009); AddGoods(51500010); AddGoods(51500011); AddGoods(51500012); 

//Quest Steps
AddQuestStep(334, 33402, StepStatus.Active);        
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 944);
    }

    public void OnQuest(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 334, 33402) == StepStatus.Active)
        {
            if (CountItem(pc, 2653) == 0)
            {
                UpdateIcon(pc);
                GiveItem(pc, 2653, 1);
                NPCSpeech(pc, 238);
                NPCChat(pc, 0);
            }
        }
    }
}