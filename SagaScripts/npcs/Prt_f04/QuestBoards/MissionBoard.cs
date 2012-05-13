using System; 
using System.Collections.Generic; 

using SagaMap; 
  
using SagaDB.Actors; 
using SagaDB.Items; 

public class MissionBoardPrt04_1 : MissionBoardPrt01 
{ 
    public override void OnSub() 
    { 
	MapName = "Prt_f04";
	Type = 1127;
        Name = "MissionBoardPrt04_1"; 
        StartX = 32643.1F;
        StartY = -35579.56F;
        StartZ = -11201.72F;
        Startyaw = -73472;
    }
} 

public class MissionBoardPrt04_2 : MissionBoardPrt01 
{ 
    public override void OnSub() 
    { 
	MapName = "Prt_f04";
	Type = 1127;
        Name = "MissionBoardPrt04_2"; 
        StartX = -18342.25F;
        StartY = -12556.22F;
        StartZ = -11443.41F;
        Startyaw = -59240;
    } 
}

public class MissionBoardPrt04_3 : MissionBoardPrt01 
{ 
    public override void OnSub() 
    { 
	MapName = "Prt_f04";
	Type = 1127;
        Name = "MissionBoardPrt04_3"; 
        StartX = 44656.59F;
        StartY = 38808.92F;
        StartZ = -12072F;
        Startyaw = -25192;
    } 
}