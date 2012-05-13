using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class Raid_Nifingr : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1017;
            Name = "Raid_Nifingr";
            StartX = -6422F;
            StartY = 6986F;
            StartZ = 7636;
            Startyaw = -77040;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.Smith);
            AddButton(Functions.Shop);

//Goods
AddGoods(100101); AddGoods(100102); AddGoods(100103); AddGoods(100104); AddGoods(100105); AddGoods(400091); AddGoods(400092); AddGoods(400093); AddGoods(400094); AddGoods(400095); AddGoods(300141); AddGoods(300142); AddGoods(300143); AddGoods(300144); AddGoods(300145); AddGoods(500121); AddGoods(500122); AddGoods(500123); AddGoods(500124); AddGoods(500125); AddGoods(570284); AddGoods(570285); AddGoods(570286); AddGoods(570287); AddGoods(570288); AddGoods(700130); AddGoods(700131); AddGoods(700132); AddGoods(800114); AddGoods(800115); AddGoods(800116); AddGoods(2010006); AddGoods(2010015); AddGoods(2010024); AddGoods(2010039); AddGoods(2010048); 
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}