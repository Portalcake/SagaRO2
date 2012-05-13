using System;
using System.Collections.Generic;

using SagaMap;

using SagaDB.Actors;
using SagaDB.Items;

public class Mailbox : MapItem
{
    public override void OnInit()
    {
        Type = 1124;
        MapName = "Prt_f01";
        Name = "MailBox";
        StartX = 14424.12F;
        StartY = 77071.38F;
        StartZ = 4988F;
        Startyaw = -20;
    }

    public override void OnClicked(ActorPC pc)
    {
        OpenMailBox(pc);
    }
}