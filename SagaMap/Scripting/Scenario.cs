using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actors;
using SagaDB.Quest;

namespace SagaMap
{
    public class Scenario :Npc 
    {
        private ActorNPC shadownpc = new ActorNPC(100000, 100, 100, 100, 100);

        public static void SetCurrentScenario(ActorPC pc,uint sceID, uint scenario)
        {
            Packets.Server.InitScenario p = new SagaMap.Packets.Server.InitScenario();
            p.SetUnknown(sceID);
            p.SetScenario(scenario);
            pc.Scenario = scenario;
            SendPacket(pc, p);
        }

        public void StartEvent(ActorPC pc, uint Step)
        {
            Packets.Server.ScenarioEvent p = new SagaMap.Packets.Server.ScenarioEvent();
            p.SetStep(Step);
            p.SetActor(pc.id);
            SendPacket(pc, p);
        }

        public void ScenarioStepComplete(ActorPC pc, uint Step, uint NextStep)
        {
            Packets.Server.ScenarioStepComplete p = new SagaMap.Packets.Server.ScenarioStepComplete();
            pc.Scenario = NextStep;
            p.SetStep(Step);
            p.SetNextStep(NextStep);
            SendPacket(pc, p);
        }

        public void EndEvent(ActorPC pc)
        {
            Packets.Server.ScenarioEventEnd p = new SagaMap.Packets.Server.ScenarioEventEnd();
            p.SetActor(pc.id);
            SendPacket(pc, p);
        }

        public virtual void OnChangeMap(ActorPC pc, byte Mapid)
        {
        }
    }
}

