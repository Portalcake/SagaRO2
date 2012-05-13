using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class Ercert : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1267;
            Name = "Ercert";
            StartX = 15812F;
            StartY = -40126F;
            StartZ = -23352;
            Startyaw = -24648;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
		 AddButton(Functions.Shop);

//Goods
AddGoods(51500002); AddGoods(51500003); AddGoods(51500004); AddGoods(51500005); AddGoods(51500006); AddGoods(51500007); AddGoods(51500008); AddGoods(51500009); AddGoods(51500010); AddGoods(51500011); AddGoods(51500012);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}