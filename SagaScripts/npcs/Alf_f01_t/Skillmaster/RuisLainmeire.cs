using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class RuisLainmeire : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1243;
            Name = "Ruis Lainmeire";
            StartX = -5357F;
            StartY = -22558F;
            StartZ = 9799;
            Startyaw = -66416;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.BookStore);

//Goods
AddGoods(16059); AddGoods(16069); AddGoods(16083); AddGoods(16124); AddGoods(16160); AddGoods(16170); AddGoods(16208); AddGoods(16243); AddGoods(16270); AddGoods(16278); AddGoods(16294); AddGoods(16336); AddGoods(16385); AddGoods(16394); AddGoods(16402); AddGoods(16409); AddGoods(16429); AddGoods(16442); AddGoods(16480); AddGoods(16507); AddGoods(16516); AddGoods(16577); AddGoods(16590); AddGoods(16620); AddGoods(16635); AddGoods(16645); AddGoods(16693); AddGoods(16703); AddGoods(16718); AddGoods(16747); AddGoods(16829); AddGoods(16837); AddGoods(16919); AddGoods(16929); AddGoods(16970); AddGoods(17018); AddGoods(206008); AddGoods(206019); AddGoods(2030008); AddGoods(2030018); AddGoods(2040008); AddGoods(2050003); 
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}