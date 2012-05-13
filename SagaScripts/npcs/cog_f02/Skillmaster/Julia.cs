using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Julia : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f02";
		Type = 1218;
		Name = "Julia";
		StartX = -19200F;
		StartY = -15520F;
		StartZ = -23475.08F;
		Startyaw = 8000;
		SetScript(823);
		AddButton(Functions.EverydayConversation, new func(OnButton));
		AddButton(Functions.Shop);

// Goods
AddGoods(51500002); AddGoods(51500003); AddGoods(51500004); AddGoods(51500005); AddGoods(51500006); AddGoods(51500007); AddGoods(51500008); AddGoods(51500009); AddGoods(51500010); AddGoods(51500011); AddGoods(51500012); 
        }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
