using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Noel : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 1213;
        Name = "Noel";
        StartX = 19296F;
        StartY = 14720F;
        StartZ = 2144;
        Startyaw = 40000;
        SetScript(3968);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.Shop);

//Goods
AddGoods(51500002); AddGoods(51500003); AddGoods(51500004); AddGoods(51500005); AddGoods(51500006); AddGoods(51500007); AddGoods(51500008); AddGoods(51500009); AddGoods(51500010); AddGoods(51500011); AddGoods(51500012); 
        }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 3969);
    }

}