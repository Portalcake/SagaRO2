using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Adrian : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1248;
            Name = "Adrian";
            StartX = 34424F;
            StartY = -8720F;
            StartZ = -4222;
            Startyaw = -24000;
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