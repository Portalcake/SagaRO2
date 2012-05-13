  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Cody : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 1149;
        Name = "Cody Isaiah";
        StartX = 8529F;
        StartY = -12445F;
        StartZ = 586F;
        Startyaw = -26688;
        SetScript(2144);
        List<uint> Mobs = new List<uint>();
        Mobs.Add(10002);
        Mobs.Add(10003);
        Mobs.Add(10004);
        AddEnemyInfo(403, 40302, Mobs, 3);
        AddPersonalQuest(403, 0, 0, 3957);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.AcceptPersonalRequest, new personalfunc(OnAcceptPersonalQuest), true);
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 4012);
    }

    public void OnAcceptPersonalQuest(ActorPC pc, uint QID, byte button)
    {
        if (QID == 403)
        {
            switch (button)
            {
                case 2:
                    AddStep(403, 40302);
                    AddStep(403, 40303);
                    PersonalQuestStart(pc);
                    NPCSpeech(pc, 3989);
                    break;
                case 3:
                    NPCSpeech(pc, 3990);
                    break;
            }            
        }
    }

}