using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

namespace cog_f01_02
{
    public class Mailbox : MapItem
    {
     	public override void OnInit()
     	{
     	    Type = 1126;
     	    MapName = "cog_f01_02";
     	    Name = "MailBox";
     	    StartX = -2733.447F;
     	    StartY = -18794.96F;
     	    StartZ = 356F;
   	    Startyaw = -6152;
    	}

        public override void OnClicked(ActorPC pc)
        {
            OpenMailBox(pc);
        }
    }
}