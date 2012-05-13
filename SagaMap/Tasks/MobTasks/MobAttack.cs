using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actors;
using SagaMap;
using SagaMap.Scripting;

namespace SagaMap.Tasks
{
    public class MobAttack : MultiRunTask
    {
        private Mob mob;
        public Actor dActor;

        public MobAttack(Mob mob, int aDelay, Actor dActor)
        {
            this.dueTime = 0;
            this.period = aDelay;
            this.mob = mob;
            this.dActor = dActor;
        }

        public override void CallBack(object o)
        {
            ClientManager.EnterCriticalArea();
            try
            {
                if (mob.Actor.stance == Global.STANCE.DIE || dActor.stance == Global.STANCE.DIE || !mob.Hate.ContainsKey(dActor.id) || mob.Actor.Tasks.ContainsKey("Stunned"))
                {
                    if (mob.Hate.ContainsKey(dActor.id)) mob.Hate.Remove(dActor.id);
                    if (this.Activated()) this.Deactivate();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
                if (dActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)dActor;
                    if (pc.HP == 0)
                    {
                        if (mob.Hate.ContainsKey(dActor.id)) mob.Hate.Remove(dActor.id);
                        if (this.Activated()) this.Deactivate();
                        ClientManager.LeaveCriticalArea();
                        return;
                    }
                }
                Actor sActor = (Actor)mob.Actor;
                Map.SkillArgs args = new Map.SkillArgs(1, 0, 1, dActor.id, 0);
                Skills.SkillHandler.CastSkill(ref sActor, ref dActor, ref args);
                mob.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, args, sActor, false);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
