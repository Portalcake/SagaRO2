using System;
using System.Collections.Generic;

using SagaMap;

using SagaDB.Actors;
using SagaDB.Items;
namespace Prt_f04
{
    public class Item0 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_0";
            StartX = 50448.31F;
            StartY = 37533.09F;
            StartZ = -11856.88F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item1 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_1";
            StartX = 49177.44F;
            StartY = 42685.32F;
            StartZ = -12271.17F;
            Startyaw = 96;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item4 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_4";
            StartX = 41130.94F;
            StartY = 37642.01F;
            StartZ = -12062.14F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item5 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_5";
            StartX = 43858.89F;
            StartY = 30670.44F;
            StartZ = -11117.42F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item6 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_6";
            StartX = 36176.56F;
            StartY = 33780.54F;
            StartZ = -12295.93F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item7 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_7";
            StartX = 34308.89F;
            StartY = 36489.4F;
            StartZ = -12259.19F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item8 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_8";
            StartX = 31656.15F;
            StartY = 36228.95F;
            StartZ = -12391.67F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item9 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_9";
            StartX = 27621.04F;
            StartY = 31440.9F;
            StartZ = -12272.98F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item10 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_10";
            StartX = 20554.62F;
            StartY = 28124.86F;
            StartZ = -11964.12F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item11 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5000;
            Name = "Item_11";
            StartX = 20885.26F;
            StartY = 28377.78F;
            StartZ = -11981.27F;
            Startyaw = -25208;
        }
    }

    public class Item12 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5000;
            Name = "Item_12";
            StartX = 20749.26F;
            StartY = 28237.78F;
            StartZ = -11981.27F;
            Startyaw = -25208;
        }
    }

    public class Item13 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_13";
            StartX = 18730.34F;
            StartY = 25756.56F;
            StartZ = -10740.41F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item14 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_14";
            StartX = 24138.96F;
            StartY = 25071.68F;
            StartZ = -11785.23F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item15 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_15";
            StartX = 16234.12F;
            StartY = 33379.86F;
            StartZ = -10533.06F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item16 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_16";
            StartX = 42394.73F;
            StartY = 44854.56F;
            StartZ = -13012.53F;
            Startyaw = -2000;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item17 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_17";
            StartX = 46031.18F;
            StartY = 48439.84F;
            StartZ = -12468.55F;
            Startyaw = -28472;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item18 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_18";
            StartX = 38793.96F;
            StartY = 49446.15F;
            StartZ = -13265.29F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item19 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_19";
            StartX = 33894.74F;
            StartY = 48232.9F;
            StartZ = -12538.05F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item20 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_20";
            StartX = 26218.6F;
            StartY = 41582.26F;
            StartZ = -9932.333F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item21 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_21";
            StartX = 20220.84F;
            StartY = 45704.99F;
            StartZ = -9172.882F;
            Startyaw = 1416;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item22 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_22";
            StartX = 14627.46F;
            StartY = 30685.12F;
            StartZ = -10683.49F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item23 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_23";
            StartX = 39111.32F;
            StartY = 27905.15F;
            StartZ = -10829.47F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item24 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_24";
            StartX = 39877.3F;
            StartY = 24150.53F;
            StartZ = -10275.35F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item25 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_25";
            StartX = 36197.94F;
            StartY = 24726.05F;
            StartZ = -10967.52F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item26 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_26";
            StartX = 43738F;
            StartY = 22146.56F;
            StartZ = -9747.591F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item27 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_27";
            StartX = 30821.32F;
            StartY = 20002.31F;
            StartZ = -11398.48F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item28 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_28";
            StartX = 25955.01F;
            StartY = 21666.5F;
            StartZ = -10357.85F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item29 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_29";
            StartX = 12613.04F;
            StartY = 39674.05F;
            StartZ = -8798.381F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item30 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_30";
            StartX = 10671.86F;
            StartY = 44901.16F;
            StartZ = -7744.74F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item35 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_35";
            StartX = 37555.6F;
            StartY = 40145.94F;
            StartZ = -12442.35F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item36 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_36";
            StartX = 32098.28F;
            StartY = 39128.26F;
            StartZ = -12320.48F;
            Startyaw = 704;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item37 : MapItem
    {

    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_37";
            StartX = 22610.71F;
            StartY = 38722.65F;
            StartZ = -9652.273F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item38 : MapItem
    {
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 6;
            Name = "Item_38";
            StartX = 29092F;
            StartY = -49896F;
            StartZ = -12052F;
            Startyaw = 0;
        }
    }

    public class Item39 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_39";
            StartX = 27825.89F;
            StartY = 45650.7F;
            StartZ = -11105.71F;
            Startyaw = 1140;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item40 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_40";
            StartX = 22053.7F;
            StartY = 35994.29F;
            StartZ = -10274.96F;
            Startyaw = -12;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item41 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_41";
            StartX = 31891.91F;
            StartY = 29120.9F;
            StartZ = -12196.35F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item42 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_42";
            StartX = 26389.54F;
            StartY = 28789.15F;
            StartZ = -12072.75F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item43 : MapItem
    {
  	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_43";
            StartX = 34682.42F;
            StartY = 42122.53F;
            StartZ = -12498.52F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item44 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_44";
            StartX = 31699.1F;
            StartY = 32000.49F;
            StartZ = -12331.07F;
            Startyaw = 0;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

    public class Item45 : MapItem
    {
    	
        public override void OnInit()
        {
            MapName = "Prt_f04";
            Type = 5;
            Name = "Item_45";
            StartX = 15070.4F;
            StartY = 42766.5F;
            StartZ = -7892.979F;
            Startyaw = -44;
        }
    	public override void OnOpen(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 201, 20102) == StepStatus.Active)
        	{
           		ClearNPCItem();
            		AddNPCItem(3993);
            		SendLootList(pc);
        	}
        	SetAnimation(pc, 2);
    	}
    }

}