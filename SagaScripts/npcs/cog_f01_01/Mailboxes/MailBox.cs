using System;
using System.Collections.Generic;

using SagaMap;

using SagaDB.Actors;
using SagaDB.Items;

public class Mailb0x : MapItem
{
    public override void OnInit()
    {
        Type = 1126;
        MapName = "cog_f01_01";
        Name = "MailBox";
        StartX = -2519.618F;
        StartY = -20829.76F;
        StartZ = 6083.689F;
        Startyaw = -5152;
    }

    public override void OnClicked(ActorPC pc)
    {
        OpenMailBox(pc);
    }
}