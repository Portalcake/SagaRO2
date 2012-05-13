using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Misha : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1000;
        Name = "Misha Berardini";
        StartX = -12092F;
        StartY = -6490F;
        StartZ = -8284F;
        Startyaw = -9232;
        SetScript(497);        
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
        AddButton(Functions.Shop);
		AddButton(Functions.Supply);
		SupplyMenuID = 11;
	//Exchange
		//Create  Beholder Eyes + 4
		AddSupplyProduct(57,2010003,1); //The Alter stone
		AddSupplyMatrial(57,1700058,10); // Solid Oxygen 10
		AddSupplyMatrial(57,85,1); // Aquamarine
		AddSupplyMatrial(57,9434,10); // Rusted Brooch 10
		//Add 800Rufi
		
		
		//Create  BoneCrush Rain+
		AddSupplyProduct(61,2010012,1); //The Alter stone
		AddSupplyMatrial(61,1700058,10); // Solid Oxygen 10
		AddSupplyMatrial(61,86,1); // Topaz
		AddSupplyMatrial(61,9458,10); // Rusted hook 10
		//Add 800Rufi
		
		
		//Create  Burning Anger 
		AddSupplyProduct(65,2010021,1); //The Alter stone
		AddSupplyMatrial(65,1700058,10); // Solid Oxygen 10
		AddSupplyMatrial(65,84,1); // Ruby
		AddSupplyMatrial(65,1700005,10); // Extra Chunk
		//Add 800Rufi
		
		
		//Create  Dragon Blade Crusher
		AddSupplyProduct(69,2010030,1); //The Alter stone
		AddSupplyMatrial(69,1700058,10); // Solid Oxygen 10
		AddSupplyMatrial(69,88,1); // Onyx
		AddSupplyMatrial(69,9461,10); // Marine Shell 10
		//Add 800Rufi		
		
		
		//Create  Coldheart +4
		AddSupplyProduct(73,2010036,1); //The Alter stone
		AddSupplyMatrial(73,1700058,10); // Solid Oxygen 10
		AddSupplyMatrial(73,87,1); // Moonstone
		AddSupplyMatrial(73,9456,10); // Sea Grass 10
		//Add 800Rufi
		
		
		//Create  Magic Charm +4
		AddSupplyProduct(77,2010045,1); //The Alter stone
		AddSupplyMatrial(77,1700058,10); // Solid Oxygen 10
		AddSupplyMatrial(77,85,1); // Aquamarine
		AddSupplyMatrial(77,9493,10); // Coral Piece 10
		//Add 800Rufi		
//Goods
AddGoods(100089); AddGoods(100091); AddGoods(100093); AddGoods(100094); AddGoods(400079); AddGoods(400081); AddGoods(400083); AddGoods(400084); AddGoods(300128); AddGoods(300130); AddGoods(300132); AddGoods(300134); AddGoods(500109); AddGoods(500111); AddGoods(500113); AddGoods(500114); AddGoods(570272); AddGoods(570274); AddGoods(570276); AddGoods(570277); AddGoods(2010000); AddGoods(2010009); AddGoods(2010018); AddGoods(2010033);

//Quest Steps
        AddQuestStep(1, 102, StepStatus.Active);
        AddQuestStep(2, 202, StepStatus.Active);
        AddQuestStep(323, 32301, StepStatus.Active);
        AddQuestStep(323, 32303, StepStatus.Active);
        AddQuestStep(334, 33402, StepStatus.Active);
    }
   
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 896);
    }

    public void OnQuest(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 1, 102) == StepStatus.Active && CountItem(pc, 2630) >= 6)
        {
            UpdateQuest(pc, 1, 102, StepStatus.Completed);
            QuestCompleted(pc, 1);
            TakeItem(pc, 2630, 6);
            UpdateIcon(pc);
            NPCChat(pc, 0);
            SetReward(pc, new rewardfunc(OnReward));
        }
        if (GetQuestStepStatus(pc, 2, 202) == StepStatus.Active && CountItem(pc, 2643) >= 2 && CountItem(pc, 2610) >= 1)
        {
            UpdateQuest(pc, 2, 202, StepStatus.Completed);
            QuestCompleted(pc, 2);
            TakeItem(pc, 2643, 2);
            TakeItem(pc, 2610, 1);
            AddRewardChoice(pc, 50700000);
            AddRewardChoice(pc, 50700006);
            UpdateIcon(pc);
            NPCChat(pc, 0);
            SetReward(pc, new rewardfunc(OnReward));
        }
        if (GetQuestStepStatus(pc, 323, 32301) == StepStatus.Active)
        {
            UpdateQuest(pc, 323, 32301, StepStatus.Completed);
            UpdateIcon(pc);
            NPCSpeech(pc, 2222);
            NPCChat(pc, 0);
            RemoveNavPoint(pc, 323);
            SendNavPoint(pc, 323, 1004, 4672f, 9792f, -9472f); 
        }
        if (GetQuestStepStatus(pc, 323, 32303) == StepStatus.Active && CountItem(pc, 2631) >= 1)
        {
            UpdateQuest(pc, 323, 32303, StepStatus.Completed);
            QuestCompleted(pc, 323);
            TakeItem(pc, 2631, 1);
            UpdateIcon(pc);
            NPCSpeech(pc, 2228);
            NPCChat(pc, 0);
            RemoveNavPoint(pc, 323);
            SetReward(pc, new rewardfunc(OnReward));
        }
        if (GetQuestStepStatus(pc, 334, 33402) == StepStatus.Active)
        {
            if (CountItem(pc, 2652) == 0)
            {
                UpdateIcon(pc);
                GiveItem(pc, 2652, 1);
                NPCSpeech(pc, 235);
                NPCChat(pc, 0);
            }
        }
    }

    public void OnReward(ActorPC pc, uint QID)
    {
        if (QID == 1)
        {
            GiveExp(pc, 140, 50);
            GiveZeny(pc, 40);
            RemoveQuest(pc, 1);
        }
        if (QID == 2)
        {
            GiveExp(pc, 140, 50);
            GiveZeny(pc, 40);
            RemoveQuest(pc, 1);             
        }
        if (QID == 323)
        {
            GiveExp(pc, 63, 10);
            GiveZeny(pc, 38);
            GiveItem(pc, 50100001, 1);
            RemoveQuest(pc, 323);
        }

    }
}