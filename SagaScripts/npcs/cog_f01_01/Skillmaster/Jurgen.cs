using System;
using System.Collections.Generic;

using SagaMap;
  
using SagaDB.Actors;
using SagaDB.Items;

public class Jurgen : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1053;
        Name = "Jurgen Hartevelt";
        StartX = -8991F;
        StartY = -15307F;
        StartZ = 6295F;
        Startyaw = -5671;
        SetScript(3);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.BookStore);

//Goods
AddGoods(16056); AddGoods(16057); AddGoods(16058); AddGoods(16066); AddGoods(16067); AddGoods(16068); AddGoods(16080); AddGoods(16081); AddGoods(16082); AddGoods(16093); AddGoods(16094); AddGoods(16095); AddGoods(16121); AddGoods(16122); AddGoods(16123); AddGoods(16157); AddGoods(16158); AddGoods(16159); AddGoods(16167); AddGoods(16168); AddGoods(16169); AddGoods(16207); AddGoods(16242); AddGoods(16268); AddGoods(16269); AddGoods(16272); AddGoods(16273); AddGoods(16274); AddGoods(16275); AddGoods(16276); AddGoods(16277); AddGoods(16288); AddGoods(16289); AddGoods(16290); AddGoods(16291); AddGoods(16292); AddGoods(16293); AddGoods(16333); AddGoods(16334); AddGoods(16335); AddGoods(16382); AddGoods(16383); AddGoods(16384); AddGoods(16388); AddGoods(16389); AddGoods(16390); AddGoods(16391); AddGoods(16392); AddGoods(16393); AddGoods(16401); AddGoods(16408); AddGoods(16423); AddGoods(16424); AddGoods(16425); AddGoods(16426); AddGoods(16427); AddGoods(16428); AddGoods(16440); AddGoods(16441); AddGoods(16479); AddGoods(16504); AddGoods(16505); AddGoods(16506); AddGoods(16513); AddGoods(16514); AddGoods(16515); AddGoods(16575); AddGoods(16576); AddGoods(16584); AddGoods(16585); AddGoods(16586); AddGoods(16587); AddGoods(16588); AddGoods(16589); AddGoods(16602); AddGoods(16603); AddGoods(16614); AddGoods(16615); AddGoods(16616); AddGoods(16617); AddGoods(16618); AddGoods(16619); AddGoods(16627); AddGoods(16628); AddGoods(16629); AddGoods(16630); AddGoods(16631); AddGoods(16632); AddGoods(16633); AddGoods(16634); AddGoods(16639); AddGoods(16640); AddGoods(16641); AddGoods(16642); AddGoods(16643); AddGoods(16644); AddGoods(16690); AddGoods(16691); AddGoods(16692); AddGoods(16700); AddGoods(16701); AddGoods(16702); AddGoods(16705); AddGoods(16706); AddGoods(16707); AddGoods(16708); AddGoods(16709); AddGoods(16715); AddGoods(16716); AddGoods(16717); AddGoods(16745); AddGoods(16746); AddGoods(16757); AddGoods(16758); AddGoods(16759); AddGoods(16763); AddGoods(16764); AddGoods(16768); AddGoods(16769); AddGoods(16773); AddGoods(16774); AddGoods(16775); AddGoods(16776); AddGoods(16777); AddGoods(16778); AddGoods(16779); AddGoods(16817); AddGoods(16818); AddGoods(16819); AddGoods(16827); AddGoods(16828); AddGoods(16835); AddGoods(16836); AddGoods(16876); AddGoods(16877); AddGoods(16878); AddGoods(16879); AddGoods(16880); AddGoods(16881); AddGoods(16882); AddGoods(16883); AddGoods(16884); AddGoods(16885); AddGoods(16916); AddGoods(16917); AddGoods(16918); AddGoods(16926); AddGoods(16927); AddGoods(16928); AddGoods(16939); AddGoods(16940); AddGoods(16949); AddGoods(16950); AddGoods(16954); AddGoods(16955); AddGoods(16968); AddGoods(16969); AddGoods(16991); AddGoods(17006); AddGoods(17007); AddGoods(17008); AddGoods(17009); AddGoods(17010); AddGoods(17016); AddGoods(17017); AddGoods(17037); AddGoods(206005); AddGoods(206006); AddGoods(206007); AddGoods(206012); AddGoods(206013); AddGoods(206014); AddGoods(206016); AddGoods(206017); AddGoods(206018); AddGoods(2030005); AddGoods(2030006); AddGoods(2030007); AddGoods(2030012); AddGoods(2030013); AddGoods(2030014); AddGoods(2030015); AddGoods(2030016); AddGoods(2030017); AddGoods(2040005); AddGoods(2040006); AddGoods(2040007); AddGoods(2040012); AddGoods(2040013); AddGoods(2050000); AddGoods(2050001); AddGoods(2050002);
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }

}