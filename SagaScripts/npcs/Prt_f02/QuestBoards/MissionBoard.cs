using System; 
using System.Collections.Generic; 

using SagaMap; 
  
using SagaDB.Actors; 
using SagaDB.Items; 

public class MissionBoardPrt02_1 : MissionBoardPrt01 
{ 
    public override void OnSub() 
    { 
	MapName = "Prt_f02";
	Type = 1127;
        Name = "MissionBoardPrt02_1"; 
        StartX = -16349.6F;
        StartY = -31370.86F;
        StartZ = -848.037F;
        Startyaw = 0;
    } 
} 

public class MissionBoardPrt02_2 : MissionBoardPrt01 
{ 
    public override void OnSub() 
    { 
	MapName = "Prt_f02";
	Type = 1127;
        Name = "MissionBoardPrt02_2"; 
        StartX = 36958.82F;
        StartY = -16677.64F;
        StartZ = -4000.709F;
        Startyaw = -26000;
    } 
}

public class MissionBoardPrt02_3 : MissionBoardPrt01 
{ 
    public override void OnSub() 
    { 
	MapName = "Prt_f02";
	Type = 1127;
        Name = "MissionBoardPrt02_3"; 
        StartX = -12746.03F;
        StartY = 3551.381F;
        StartZ = -619.241F;
        Startyaw = -38540;
    } 
}
