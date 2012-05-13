using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class HugoHelfried : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1104;
            Name = "HugoHelfried";
            StartX = -1712F;
            StartY = -2F;
            StartZ = 8450;
            Startyaw = -61224;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.Shop);

//Goods
AddGoods(4101); AddGoods(2585); AddGoods(2586); AddGoods(2587); AddGoods(2588); AddGoods(51500002); AddGoods(51500003); AddGoods(51500004); AddGoods(51500005); AddGoods(51500006); AddGoods(51500007); AddGoods(51500008); AddGoods(51500009); AddGoods(51500010); AddGoods(51500011); AddGoods(51500012); 
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}