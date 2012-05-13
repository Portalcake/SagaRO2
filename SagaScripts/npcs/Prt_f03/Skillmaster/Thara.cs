using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f03
{
    public class Thara2 : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f03";
            Type = 1216;
            Name = "Thara2";
            StartX = 11432F;
            StartY = 24523F;
            StartZ = 5201;
            Startyaw = -18000;
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