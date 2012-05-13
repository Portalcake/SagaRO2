using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class NegusTradjie : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1251;
            Name = "Negus Tradjie";
            StartX = 13133F;
            StartY = -43139F;
            StartZ = -23440;
            Startyaw = 7736;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.Shop);

//Goods
AddGoods(2589); AddGoods(2590); AddGoods(51500005); AddGoods(51500006); AddGoods(51500007); AddGoods(100099); AddGoods(100100); AddGoods(100101); AddGoods(400089); AddGoods(400090); AddGoods(400091); AddGoods(300139); AddGoods(300140); AddGoods(300141); AddGoods(500119); AddGoods(500120); AddGoods(500121); AddGoods(570282); AddGoods(570283); AddGoods(570284); AddGoods(700129); AddGoods(700130); AddGoods(800113); AddGoods(800114); AddGoods(2010004); AddGoods(2010013); AddGoods(2010022); AddGoods(2010037); AddGoods(2010046); 
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}