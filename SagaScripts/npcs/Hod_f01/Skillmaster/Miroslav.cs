  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
  
using SagaDB.Actors;
using SagaDB.Items;

public class Miroslav : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1049;
        Name = "Miroslav Baum";
        StartX = -6948F;
        StartY = -9448F;
        StartZ = -7149F;
        Startyaw = 28000;
        SetScript(786);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.BookStore);

//Goods
AddGoods(16052); AddGoods(16053); AddGoods(16061); AddGoods(16062); AddGoods(16063); AddGoods(16076); AddGoods(16077); AddGoods(16086); AddGoods(16087); AddGoods(16097); AddGoods(16098); AddGoods(16099); AddGoods(16100); AddGoods(16101); AddGoods(16107); AddGoods(16108); AddGoods(16109); AddGoods(16110); AddGoods(16111); AddGoods(16112); AddGoods(16117); AddGoods(16127); AddGoods(16128); AddGoods(16129); AddGoods(16130); AddGoods(16131); AddGoods(16132); AddGoods(16137); AddGoods(16138); AddGoods(16139); AddGoods(16140); AddGoods(16141); AddGoods(16142); AddGoods(16147); AddGoods(16148); AddGoods(16149); AddGoods(16150); AddGoods(16151); AddGoods(16152); AddGoods(16153); AddGoods(16154); AddGoods(16162); AddGoods(16163); AddGoods(16164); AddGoods(16172); AddGoods(16187); AddGoods(16188); AddGoods(16189); AddGoods(16190); AddGoods(16191); AddGoods(16192); AddGoods(16212); AddGoods(16213); AddGoods(16214); AddGoods(16222); AddGoods(16223); AddGoods(16224); AddGoods(16225); AddGoods(16227); AddGoods(16228); AddGoods(16232); AddGoods(16233); AddGoods(16234); AddGoods(16235); AddGoods(16262); AddGoods(16263); AddGoods(16264); AddGoods(16282); AddGoods(16283); AddGoods(16284); AddGoods(16285); AddGoods(16286); AddGoods(16308); AddGoods(16309); AddGoods(16310); AddGoods(16328); AddGoods(16329); AddGoods(16330); AddGoods(16338); AddGoods(16339); AddGoods(16340); AddGoods(16343); AddGoods(16344); AddGoods(16353); AddGoods(16354); AddGoods(16355); AddGoods(16378); AddGoods(16398); AddGoods(16413); AddGoods(16414); AddGoods(16433); AddGoods(16434); AddGoods(16435); AddGoods(16436); AddGoods(16458); AddGoods(16464); AddGoods(16465); AddGoods(16466); AddGoods(16467); AddGoods(16484); AddGoods(16485); AddGoods(16486); AddGoods(16494); AddGoods(16495); AddGoods(16496); AddGoods(16499); AddGoods(16500); AddGoods(16501); AddGoods(16509); AddGoods(16519); AddGoods(16520); AddGoods(16521); AddGoods(16569); AddGoods(16570); AddGoods(16579); AddGoods(16580); AddGoods(16594); AddGoods(16595); AddGoods(16596); AddGoods(16597); AddGoods(16604); AddGoods(16605); AddGoods(16606); AddGoods(16685); AddGoods(16686); AddGoods(16687); AddGoods(16695); AddGoods(16696); AddGoods(16697); AddGoods(16710); AddGoods(16711); AddGoods(16712); AddGoods(16720); AddGoods(16721); AddGoods(16722); AddGoods(16740); AddGoods(16741); AddGoods(16750); AddGoods(16751); AddGoods(16752); AddGoods(16780); AddGoods(16781); AddGoods(16782); AddGoods(16790); AddGoods(16791); AddGoods(16792); AddGoods(16800); AddGoods(16801); AddGoods(16802); AddGoods(16810); AddGoods(16811); AddGoods(16812); AddGoods(16813); AddGoods(16820); AddGoods(16821); AddGoods(16822); AddGoods(16830); AddGoods(16831); AddGoods(16860); AddGoods(16861); AddGoods(16862); AddGoods(16863); AddGoods(16864); AddGoods(16865); AddGoods(16866); AddGoods(16886); AddGoods(16887); AddGoods(16888); AddGoods(16889); AddGoods(16911); AddGoods(16912); AddGoods(16913); AddGoods(16921); AddGoods(16922); AddGoods(16923); AddGoods(16936); AddGoods(16937); AddGoods(16946); AddGoods(16947); AddGoods(16961); AddGoods(16962); AddGoods(16963); AddGoods(16971); AddGoods(16972); AddGoods(16976); AddGoods(16977); AddGoods(16978); AddGoods(17011); AddGoods(17021); AddGoods(17027); AddGoods(17028); AddGoods(206000); AddGoods(206001); AddGoods(206002); AddGoods(2030000); AddGoods(2030001); AddGoods(2030002); AddGoods(2040000); AddGoods(2040001); AddGoods(2040002); 
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 789);
    }

}
