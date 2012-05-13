using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;
namespace Prt_f02
{
    public class Item0 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 5003;
            Name = "Item_0";
            StartX = 17353.45F;
            StartY = -28671.07F;
            StartZ = -4329.406F;
            Startyaw = 17332;
        }
    }

    public class Item1 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 5003;
            Name = "Item_1";
            StartX = 17412.45F;
            StartY = -29360.07F;
            StartZ = -4283.666F;
            Startyaw = 17332;
        }
    }

    public class Item2 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 2;
            Name = "Item_2";
            StartX = -31179.21F;
            StartY = -28954.44F;
            StartZ = 509.038F;
            Startyaw = 17000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item5 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 32;
            Name = "Item_5";
            StartX = -38699.8F;
            StartY = -33262.71F;
            StartZ = 766.758F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item6 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 31;
            Name = "Item_6";
            StartX = -39947.19F;
            StartY = -35752.1F;
            StartZ = 883.017F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item7 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 32;
            Name = "Item_7";
            StartX = -39124.04F;
            StartY = -33991.13F;
            StartZ = 780.872F;
            Startyaw = 3000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item8 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 31;
            Name = "Item_8";
            StartX = -31987.92F;
            StartY = -28186.82F;
            StartZ = 678.377F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item9 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 2;
            Name = "Item_9";
            StartX = -41098.11F;
            StartY = -31414.87F;
            StartZ = 1020.229F;
            Startyaw = 3000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item10 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 32;
            Name = "Item_10";
            StartX = -33726.69F;
            StartY = -26205.48F;
            StartZ = 906.912F;
            Startyaw = 17000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item11 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 31;
            Name = "Item_11";
            StartX = -43066.14F;
            StartY = -32255.49F;
            StartZ = 1062.802F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item12 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 2;
            Name = "Item_12";
            StartX = -32743.6F;
            StartY = -25474.61F;
            StartZ = 905.614F;
            Startyaw = 1000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item13 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 2;
            Name = "Item_13";
            StartX = -31868.07F;
            StartY = -25187.56F;
            StartZ = 861.051F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item14 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 2;
            Name = "Item_14";
            StartX = -31709.96F;
            StartY = -26942.05F;
            StartZ = 680.4F;
            Startyaw = 17000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item15 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 2;
            Name = "Item_15";
            StartX = -30142.73F;
            StartY = -27476.57F;
            StartZ = 447.463F;
            Startyaw = 17000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item16 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 2;
            Name = "Item_16";
            StartX = -30610.79F;
            StartY = -25653.73F;
            StartZ = 673.572F;
            Startyaw = 1000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item17 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 31;
            Name = "Item_17";
            StartX = -28639.52F;
            StartY = -27927.97F;
            StartZ = 321.316F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item18 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 31;
            Name = "Item_18";
            StartX = -28949.46F;
            StartY = -25778.89F;
            StartZ = 483.578F;
            Startyaw = 17000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item19 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 31;
            Name = "Item_19";
            StartX = -27446.24F;
            StartY = -27113.03F;
            StartZ = 253.919F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item20 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 5000;
            Name = "Item_20";
            StartX = 34913.62F;
            StartY = -8111.606F;
            StartZ = -4314.334F;
            Startyaw = -5752;
        }
    }

    public class Item21 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 5000;
            Name = "Item_21";
            StartX = 34701.62F;
            StartY = -8023.606F;
            StartZ = -4314.334F;
            Startyaw = -5752;
        }
    }

    public class Item22 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_22";
            StartX = 31120.44F;
            StartY = -2005.942F;
            StartZ = -3700.745F;
            Startyaw = 0;
        }
    }

    public class Item23 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_23";
            StartX = 32644.54F;
            StartY = -376.697F;
            StartZ = -3736.893F;
            Startyaw = 0;
        }
    }

    public class Item24 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_24";
            StartX = 31933F;
            StartY = 654.029F;
            StartZ = -3707.886F;
            Startyaw = 0;
        }
    }

    public class Item25 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_25";
            StartX = 29120F;
            StartY = -2240F;
            StartZ = -3525.39F;
            Startyaw = 0;
        }
    }

    public class Item26 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_26";
            StartX = 29418.58F;
            StartY = -1113.472F;
            StartZ = -3530.639F;
            Startyaw = 0;
        }
    }

    public class Item27 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_27";
            StartX = 28595.72F;
            StartY = 985.46F;
            StartZ = -3396.463F;
            Startyaw = 0;
        }
    }

    public class Item28 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_28";
            StartX = 32479.97F;
            StartY = 3096.648F;
            StartZ = -3772.521F;
            Startyaw = 0;
        }
    }

    public class Item29 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_29";
            StartX = 29225.7F;
            StartY = 3231.962F;
            StartZ = -3444.372F;
            Startyaw = 0;
        }
    }

    public class Item30 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_30";
            StartX = 27791.01F;
            StartY = 2289.722F;
            StartZ = -3302.716F;
            Startyaw = 0;
        }
    }

    public class Item31 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_31";
            StartX = 28649.7F;
            StartY = 4022.813F;
            StartZ = -3383.837F;
            Startyaw = 0;
        }
    }

    public class Item32 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_32";
            StartX = 31229.23F;
            StartY = 3906.699F;
            StartZ = -3735.523F;
            Startyaw = 0;
        }
    }

    public class Item33 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_33";
            StartX = 27283.2F;
            StartY = 654.609F;
            StartZ = -3266.513F;
            Startyaw = 0;
        }
    }

    public class Item34 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_34";
            StartX = 27880.61F;
            StartY = 4711.958F;
            StartZ = -3350.23F;
            Startyaw = 0;
        }
    }

    public class Item35 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_35";
            StartX = 32705.71F;
            StartY = 4546.244F;
            StartZ = -3741.753F;
            Startyaw = 0;
        }
    }

    public class Item36 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_36";
            StartX = 27086.12F;
            StartY = -2413.416F;
            StartZ = -3274.607F;
            Startyaw = 0;
        }
    }

    public class Item37 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_37";
            StartX = 26894.28F;
            StartY = 3381.904F;
            StartZ = -3251.534F;
            Startyaw = 0;
        }
    }

    public class Item38 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_38";
            StartX = 26442.63F;
            StartY = 4086.812F;
            StartZ = -3249.747F;
            Startyaw = 0;
        }
    }

    public class Item39 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_39";
            StartX = 26091.55F;
            StartY = 1822.143F;
            StartZ = -3239.332F;
            Startyaw = 0;
        }
    }

    public class Item40 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_40";
            StartX = 25402.49F;
            StartY = 2826.995F;
            StartZ = -3234.212F;
            Startyaw = 0;
        }
    }

    public class Item43 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 50;
            Name = "Item_43";
            StartX = 29256F;
            StartY = -31980F;
            StartZ = -4926F;
            Startyaw = -156;
        }
    }

    public class Item44 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 32;
            Name = "Item_44";
            StartX = -37754.04F;
            StartY = -34116.61F;
            StartZ = 747.539F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item45 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 31;
            Name = "Item_45";
            StartX = -38382.33F;
            StartY = -35834.79F;
            StartZ = 785.229F;
            Startyaw = -7000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item46 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 31;
            Name = "Item_46";
            StartX = -38191.59F;
            StartY = -31639.25F;
            StartZ = 857.376F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }


    public class Item49 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 5003;
            Name = "Item_49";
            StartX = 16047.45F;
            StartY = -35885.07F;
            StartZ = -4034.1F;
            Startyaw = 6272;
        }
    }

    public class Item50 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 5003;
            Name = "Item_50";
            StartX = 16101.42F;
            StartY = -37813.93F;
            StartZ = -4020F;
            Startyaw = 37332;
        }
    }

    public class Item51 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 5003;
            Name = "Item_51";
            StartX = 16455.96F;
            StartY = -37633.6F;
            StartZ = -4020F;
            Startyaw = 37332;
        }
    }

    public class Item52 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 5003;
            Name = "Item_52";
            StartX = 13736.82F;
            StartY = -36854.94F;
            StartZ = -3940.503F;
            Startyaw = 3064;
        }
    }

    public class Item53 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 5003;
            Name = "Item_53";
            StartX = 13294.58F;
            StartY = -36981.89F;
            StartZ = -3898.663F;
            Startyaw = 3064;
        }
    }

    public class Item54 : MapItem // Sarah Tristans Tomb
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 1;
            Name = "Sarah Tristan's tomb";
            StartX = 389.641F;
            StartY = -43416.65F;
            StartZ = -951.602F;
            Startyaw = -41888;
        }
    	public override void OnOpen(ActorPC pc)
  	{
        	if (GetQuestStepStatus(pc, 157, 15702) == StepStatus.Active)
        	{
			UpdateQuest(pc, 157, 15702, StepStatus.Completed);    		
        	}
		if (GetQuestStepStatus(pc, 158, 15805) == StepStatus.Active && CountItem(pc, 3987) > 0)
        	{
			TakeItem(pc, 3987, 1);
			UpdateQuest(pc, 158, 15805, StepStatus.Completed); 
	            	QuestCompleted(pc, 158);
	            	SetReward(pc, new rewardfunc(OnReward));  		
        	}
        	SetAnimation(pc, 2);
    	}
	public void OnReward(ActorPC pc, uint QID)
	{
	        if (QID == 158)
		{
	            	GiveExp(pc, 544, 110);
	            	GiveZeny(pc, 360);
	            	RemoveQuest(pc, 158); 
		}                       
	}
    }

    public class Item55 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 31;
            Name = "Item_55";
            StartX = -26951.85F;
            StartY = -25210.47F;
            StartZ = 385.196F;
            Startyaw = 17000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item56 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 32;
            Name = "Item_56";
            StartX = -28315.16F;
            StartY = -25123.55F;
            StartZ = 496.245F;
            Startyaw = 1000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item57 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 32;
            Name = "Item_57";
            StartX = -39719.05F;
            StartY = -36994.1F;
            StartZ = 890.371F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item58 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 31;
            Name = "Item_58";
            StartX = -41455.96F;
            StartY = -33991.13F;
            StartZ = 964.385F;
            Startyaw = 8000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item59 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 2;
            Name = "Item_59";
            StartX = -31837.02F;
            StartY = -23832.4F;
            StartZ = 1008.343F;
            Startyaw = 17000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item60 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 32;
            Name = "Item_60";
            StartX = -30145.58F;
            StartY = -22918.6F;
            StartZ = 970.817F;
            Startyaw = 1000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item61 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 31;
            Name = "Item_61";
            StartX = -28349.05F;
            StartY = -23665.2F;
            StartZ = 694.227F;
            Startyaw = 17000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item62 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_62";
            StartX = 27365.49F;
            StartY = -1513.146F;
            StartZ = -3267.579F;
            Startyaw = 0;
        }
    }

    public class Item63 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_63";
            StartX = 25798.42F;
            StartY = -1734.402F;
            StartZ = -3235.582F;
            Startyaw = 0;
        }
    }

    public class Item64 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_64";
            StartX = 27028.12F;
            StartY = -2775.766F;
            StartZ = -3272.128F;
            Startyaw = 0;
        }
    }

    public class Item65 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_65";
            StartX = 30509.7F;
            StartY = -450.754F;
            StartZ = -3625.635F;
            Startyaw = 0;
        }
    }

    public class Item66 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_66";
            StartX = 30670.61F;
            StartY = 1518.604F;
            StartZ = -3665.753F;
            Startyaw = 0;
        }
    }

    public class Item67 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_67";
            StartX = 29134.89F;
            StartY = 1436.765F;
            StartZ = -3482.474F;
            Startyaw = 0;
        }
    }

    public class Item68 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_68";
            StartX = 32342.83F;
            StartY = 2034.257F;
            StartZ = -3747.473F;
            Startyaw = 0;
        }
    }

    public class Item69 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 4;
            Name = "Item_69";
            StartX = 25917.27F;
            StartY = 234.955F;
            StartZ = -3238.206F;
            Startyaw = 0;
        }
    }

    public class Item70 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 32;
            Name = "Item_70";
            StartX = -39609.15F;
            StartY = -32007.28F;
            StartZ = 895.384F;
            Startyaw = 17000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item71 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 31;
            Name = "Item_71";
            StartX = -42664.67F;
            StartY = -34186F;
            StartZ = 1010.539F;
            Startyaw = 17000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item72 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 32;
            Name = "Item_72";
            StartX = -42282.93F;
            StartY = -32963.95F;
            StartZ = 1013.197F;
            Startyaw = -7000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 166, 16602) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3978);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }
}
