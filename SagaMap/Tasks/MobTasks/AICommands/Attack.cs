using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actors;
using SagaMap;
using SagaMap.Scripting;

namespace SagaMap.Tasks.MobTasks.AICommands
{
    public class Attack : AICommand
    {
        private CommandStatus status;
        private Mob mob;
        private Actor dest;
        private MobAttack attacktask;
        private bool attacking;
        public bool active;
        public Attack(Mob mob)
        {
            this.mob = mob;
            this.Status = CommandStatus.INIT;            
            attacktask = new MobAttack(mob, mob.ASPD, dest);
        }

        public string GetName() { return "Attack"; }

        private Actor CurrentTarget()
        {
            uint id = 0;
            ushort hate = 0;
            Actor tmp = null;
            uint[] ids = new uint[mob.Hate.Keys.Count];
            mob.Hate.Keys.CopyTo(ids, 0);
            for (uint i = 0; i < mob.Hate.Keys.Count; i++)
            {
                if (hate < mob.Hate[ids[i]])
                {
                    hate = mob.Hate[ids[i]];
                    id = ids[i];
                    tmp = mob.Map.GetActor(id);
                    if (tmp == null)
                    {
                        mob.Hate.Remove(id);
                        id = 0;
                        hate = 0;
                        active = false;
                        i = 0;
                    }
                    else
                    {
                        active = true;
                    }
                }
            }
            if (id != 0)
            {
                tmp = mob.Map.GetActor(id);
                if (tmp != null)
                {
                    if (tmp.stance == Global.STANCE.DIE)
                    {
                        mob.Hate.Remove(tmp.id);
                        id = 0;
                        active = false;
                    }
                }
            }
            if (id == 0)
            {
                active = false;
                return null;
            }
            if (dest != null)
            {
                if (dest.id != id) if (attacktask.Activated() == true) attacktask.Deactivate();
            }
            return tmp;
        }

        public void Update(object para)
        {
            dest = CurrentTarget();
            if (dest == null)
            {
                mob.ai.AIActivity =  MobAI.Activity.LAZY;
                if (mob.ai.commands.ContainsKey("Chase") == true) mob.ai.commands.Remove("Chase"); ;
                return;
            }
            mob.ai.AIActivity =  MobAI.Activity.BUSY;
            if (mob.ai.commands.ContainsKey("Move") == true) mob.ai.commands.Remove("Move");
            attacktask.dActor = dest;
            if (mob.ai.commands.ContainsKey("Chase") == true)
            {
                if (attacktask.Activated() == true) attacktask.Deactivate();
                attacking = false;
                return;
            }
            if (dest.stance == Global.STANCE.DIE)
            {
                if (mob.Hate.ContainsKey(dest.id)) mob.Hate.Remove(dest.id);
                if (attacktask.Activated() == true) attacktask.Deactivate();
                attacktask = null;
                return;
            }
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)dest.e;
            if (eh.C.state == MapClient.SESSION_STATE.LOGGEDOFF)
            {
                if (mob.Hate.ContainsKey(dest.id)) mob.Hate.Remove(dest.id);
                if (attacktask.Activated() == true) attacktask.Deactivate();
                attacktask = null;
                return;
            }
            float[] src = new float[3] { mob.Actor.x, mob.Actor.y, mob.Actor.z };
            float[] dst = new float[3] { dest.x, dest.y, dest.z };
            if (MobAI.GetDistance2(src, dst) > (150 + (mob.Size)))
            {
                Chase chase = new Chase(this.mob, dest);
                mob.ai.commands.Add("Chase", chase);
                if (attacktask.Activated() == true) attacktask.Deactivate();
                attacking = false;
            }
            else
            {
                if (attacktask.Activated() == false) attacktask.Activate();
                attacking = true;
            }
        }

        public CommandStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public void Dispose()
        {
            if (dest == null) return;
            if (attacking == true && attacktask != null) attacktask.Deactivate();
            attacktask = null;
            this.status = CommandStatus.FINISHED;
        }
    }
}
