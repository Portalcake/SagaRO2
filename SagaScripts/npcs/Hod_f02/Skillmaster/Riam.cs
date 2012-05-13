using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Hod02
{
    public class Riam : Npc
    {
        public override void OnInit()
        {
            MapName = "Hod_f02";
            Type = 1214;
            Name = "Riam";
            StartX = 38300F;
            StartY = -27196F;
            StartZ = 3504;
            Startyaw = 0;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.Shop);

//Goods
AddGoods(51500002); AddGoods(51500003); AddGoods(51500004); AddGoods(51500005); AddGoods(51500006); AddGoods(51500007); AddGoods(51500008); AddGoods(51500009); AddGoods(51500010); AddGoods(51500011); AddGoods(51500012);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}