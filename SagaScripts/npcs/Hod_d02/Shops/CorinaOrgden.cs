using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Hod_d02
{
    public class CorinaOrgden : Npc
    {
        public override void OnInit()
        {
            MapName = "Hod_d02";
            Type = 1225;
            Name = "Corina Orgden";
            StartX = 6121F;
            StartY = 35771F;
            StartZ = -2494;
            Startyaw = 58000;
            SetScript(824);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.Shop);

//Goods
AddGoods(4101); AddGoods(2575); AddGoods(51500002); AddGoods(51500003); AddGoods(51500004); AddGoods(51500005); AddGoods(51500006); AddGoods(51500007); AddGoods(51500008); AddGoods(51500009); AddGoods(51500010); AddGoods(51500011); AddGoods(51500012);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 2444);
        }

    }
}