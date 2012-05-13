using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;

public class Scenario : SagaMap.Scenario
    {

        public override void OnChangeMap(ActorPC pc, byte Mapid)
        {
            switch (Mapid)
            {
                case 11:
                    if (pc.Scenario == 0)
                    {
                        
			SetCurrentScenario(pc, 1, 0);             
			StartEvent(pc, 10102);
                        ScenarioStepComplete(pc, 101, 0);
			SetCurrentScenario(pc, 2, 201);             
                    }
                    break;
                case 1:
                    if (pc.Scenario == 201)
                    {
                        ScenarioStepComplete(pc, 201, 202);
                        StartEvent(pc, 20210);
                        ScenarioStepComplete(pc, 202, 203);
                    }
                    break;
            }
        }

    }

