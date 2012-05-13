  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f04
{
    public class Thara : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 1216;
            Name = "Thara";
            StartX = 20475F;
            StartY = 28727F;
            StartZ = -11835;
            Startyaw = 25723;
            SetScript(3976);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.Shop);

//Goods
AddGoods(51500002); AddGoods(51500003); AddGoods(51500004); AddGoods(51500005); AddGoods(51500006); AddGoods(51500007); AddGoods(51500008); AddGoods(51500009); AddGoods(51500010); AddGoods(51500011); AddGoods(51500012); 
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 3977);
        }

    }
}