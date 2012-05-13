using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Mainhared : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1006;
            Name = "Mainhared Anselm";
            StartX = 11616F;
            StartY = 69760F;
            StartZ = 5194;
            Startyaw = 74768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.OfficialQuest, new func(OnQuest), true);			
            AddButton(Functions.Shop);

// Goods
AddGoods(100094); AddGoods(100095); AddGoods(100096); AddGoods(400084); AddGoods(400085); AddGoods(400086); AddGoods(300134); AddGoods(300135); AddGoods(300136); AddGoods(500114); AddGoods(500115); AddGoods(500116); AddGoods(570277); AddGoods(570278); AddGoods(570279); AddGoods(700127); AddGoods(700128); AddGoods(800111); AddGoods(800112); AddGoods(2010000); AddGoods(2010001); AddGoods(2010009); AddGoods(2010010); AddGoods(2010018); AddGoods(2010019); AddGoods(2010033); AddGoods(2010034); AddGoods(2010027); AddGoods(2010042); AddGoods(2010043);

//Quest Steps
AddQuestStep(180, 18001, StepStatus.Active);
AddQuestStep(181, 18101, StepStatus.Active);
AddQuestStep(232, 23202, StepStatus.Active);
AddQuestStep(233, 23302, StepStatus.Active);

//Quest Items
AddQuestItem(180, 18002, 3985, 5);
AddMobLoot(10098, 180, 18002, 3985, 2000);
AddMobLoot(10099, 180, 18002, 3985, 2000);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

        public void OnQuest(ActorPC pc)
        {
            if (GetQuestStepStatus(pc, 180, 18001) == StepStatus.Active)
            {
                UpdateQuest(pc, 180, 18001, StepStatus.Completed);
                UpdateIcon(pc);
                RemoveNavPoint(pc, 180);
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
            }

            if (GetQuestStepStatus(pc, 181, 18101) == StepStatus.Active)
            {
				UpdateQuest(pc, 181, 18101, StepStatus.Completed);
				UpdateIcon(pc);
				GiveItem(pc, 3986, 1);
				SendNavPoint(pc, 181, 1009, 40375f, 82998f, 3853f);
				NPCSpeech(pc, 232);
				NPCChat(pc, 0);
            }

            if (GetQuestStepStatus(pc, 232, 23202) == StepStatus.Active)
            {
                UpdateQuest(pc, 232, 23202, StepStatus.Completed);
                UpdateIcon(pc);
                RemoveNavPoint(pc, 232);
				AddNavPoint(232, 23203, 6, 1080, -7360f, -3904f, 180f); //Achim	
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
            }

            if (GetQuestStepStatus(pc, 233, 23302) == StepStatus.Active && CountItem(pc, 4020) > 0)
            {
                UpdateQuest(pc, 233, 23302, StepStatus.Completed);
				TakeItem(pc, 4020, 1);
				GiveItem(pc, 4021, 1);
		        UpdateIcon(pc);
				RemoveNavPoint(pc, 233);
		        SendNavPoint(pc, 233, 1012, 13931.36f, 74893.79f, 5049.054f);
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
			}
        }
    }
}