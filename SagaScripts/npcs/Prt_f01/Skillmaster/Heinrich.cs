using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Heinrich : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1050;
            Name = "Heinrich Hansel";
            StartX = 13497F;
            StartY = 78968F;
            StartZ = 5081;
            Startyaw = 81668;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.BookStore);
            AddGoods(16054); AddGoods(16055); AddGoods(16064); AddGoods(16065); AddGoods(16078);
            AddGoods(16079); AddGoods(16088); AddGoods(16089); AddGoods(16090); AddGoods(16091);
            AddGoods(16092); AddGoods(16102); AddGoods(16103); AddGoods(16104); AddGoods(16105);
            AddGoods(16106); AddGoods(16118); AddGoods(16119); AddGoods(16120); AddGoods(16155);
            AddGoods(16156); AddGoods(16165); AddGoods(16166); AddGoods(16173); AddGoods(16174);
            AddGoods(16175); AddGoods(16176); AddGoods(16193); AddGoods(16194); AddGoods(16195);
            AddGoods(16196); AddGoods(16202); AddGoods(16203); AddGoods(16204); AddGoods(16205);
            AddGoods(16206); AddGoods(16215); AddGoods(16216); AddGoods(16217); AddGoods(16226);
            AddGoods(16229); AddGoods(16230); AddGoods(16231); AddGoods(16236); AddGoods(16237);
            AddGoods(16238); AddGoods(16239); AddGoods(16240); AddGoods(16241); AddGoods(16265);
            AddGoods(16266); AddGoods(16267); AddGoods(16287); AddGoods(16311); AddGoods(16312);
            AddGoods(16313); AddGoods(16331); AddGoods(16332); AddGoods(16341); AddGoods(16342);
            AddGoods(16345); AddGoods(16346); AddGoods(16347); AddGoods(16356); AddGoods(16357);
            AddGoods(16358); AddGoods(16373); AddGoods(16374); AddGoods(16375); AddGoods(16376);
            AddGoods(16377); AddGoods(16379); AddGoods(16380); AddGoods(16381); AddGoods(16399);
            AddGoods(16400); AddGoods(16403); AddGoods(16404); AddGoods(16405); AddGoods(16406);
            AddGoods(16407); AddGoods(16415); AddGoods(16416); AddGoods(16417); AddGoods(16437);
            AddGoods(16438); AddGoods(16439); AddGoods(16468); AddGoods(16469); AddGoods(16474);
            AddGoods(16475); AddGoods(16476); AddGoods(16477); AddGoods(16478); AddGoods(16487);
            AddGoods(16488); AddGoods(16489); AddGoods(16497); AddGoods(16498); AddGoods(16502);
            AddGoods(16503); AddGoods(16510); AddGoods(16511); AddGoods(16512); AddGoods(16522);
            AddGoods(16523); AddGoods(16524); AddGoods(16549); AddGoods(16550); AddGoods(16551);
            AddGoods(16552); AddGoods(16553); AddGoods(16571); AddGoods(16572); AddGoods(16573);
            AddGoods(16574); AddGoods(16581); AddGoods(16582); AddGoods(16583); AddGoods(16598);
            AddGoods(16599); AddGoods(16600); AddGoods(16601); AddGoods(16607); AddGoods(16608);
            AddGoods(16609); AddGoods(16624); AddGoods(16625); AddGoods(16626); AddGoods(16688);
            AddGoods(16689); AddGoods(16698); AddGoods(16699); AddGoods(16713); AddGoods(16714);
            AddGoods(16723); AddGoods(16724); AddGoods(16725); AddGoods(16742); AddGoods(16743);
            AddGoods(16744); AddGoods(16753); AddGoods(16754); AddGoods(16755); AddGoods(16756);
            AddGoods(16760); AddGoods(16761); AddGoods(16762); AddGoods(16765); AddGoods(16766);
            AddGoods(16767); AddGoods(16770); AddGoods(16771); AddGoods(16772); AddGoods(16783);
            AddGoods(16784); AddGoods(16785); AddGoods(16793); AddGoods(16794); AddGoods(16795);
            AddGoods(16803); AddGoods(16804); AddGoods(16805); AddGoods(16814); AddGoods(16815);
            AddGoods(16816); AddGoods(16823); AddGoods(16824); AddGoods(16825); AddGoods(16826);
            AddGoods(16832); AddGoods(16833); AddGoods(16834); AddGoods(16890); AddGoods(16914);
            AddGoods(16915); AddGoods(16924); AddGoods(16925); AddGoods(16938); AddGoods(16948);
            AddGoods(16951); AddGoods(16952); AddGoods(16953); AddGoods(16964); AddGoods(16965);
            AddGoods(16966); AddGoods(16967); AddGoods(16973); AddGoods(16974); AddGoods(16975);
            AddGoods(16979); AddGoods(16980); AddGoods(16981); AddGoods(16986); AddGoods(16987);
            AddGoods(16988); AddGoods(16989); AddGoods(16990); AddGoods(17012); AddGoods(17013);
            AddGoods(17014); AddGoods(17015); AddGoods(17029); AddGoods(17030); AddGoods(17031);
            AddGoods(17032); AddGoods(17033); AddGoods(17034); AddGoods(17035); AddGoods(17036);
            AddGoods(206003); AddGoods(206004); AddGoods(206010); AddGoods(206011); AddGoods(206015);                 AddGoods(2030003); AddGoods(2030004); AddGoods(2030010); AddGoods(2030011); 
            AddGoods(2040003); AddGoods(2040004); AddGoods(2040010); AddGoods(2040011);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}