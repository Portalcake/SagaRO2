using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f03
{
    public class BrueckeWalter : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f03";
            Type = 1249;
            Name = "Bruecke Walter";
            StartX = -28387F;
            StartY = 6923F;
            StartZ = -564;
            Startyaw = -544;
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