using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actors;
using SagaMap;
using SagaMap.Scripting;

namespace SagaMap.Tasks.MobTasks.AICommands
{
    public class Chase : AICommand
    {
        private CommandStatus status;
        private Actor dest;
        private Mob mob;
        private float[] lastdst = new float[3];
        public Chase(Mob mob, Actor dest)
        {
            this.mob = mob;
            this.dest = dest;
            this.Status = CommandStatus.INIT;
        }
        public string GetName() { return "Chase"; }
        public void Update(object para)
        {
            try
            {
                ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)dest.e;
                if (eh.C.state == MapClient.SESSION_STATE.LOGGEDOFF)
                {
                    this.status = CommandStatus.FINISHED;
                    return;
                }
                if (mob.Map.HasHeightMap())
                {
                    if (mob.Map.HeightMap.water_level != 0)
                    {
                        switch (mob.LivingSpace)
                        {
                            case Mob.Space.Land:
                                if (dest.z < mob.Map.HeightMap.water_level)
                                {
                                    if (mob.Hate.ContainsKey(dest.id)) mob.Hate.Remove(dest.id);
                                    this.Status = CommandStatus.FINISHED;
                                    return;
                                }
                                break;
                            case Mob.Space.Water:
                                if (dest.z >= mob.Map.HeightMap.water_level)
                                {
                                    if (mob.Hate.ContainsKey(dest.id)) mob.Hate.Remove(dest.id);
                                    this.Status = CommandStatus.FINISHED;
                                    return;
                                }
                                break;
                        }
                    }
                }
                
                if (mob.RunSpeed == 0 || mob.Actor.Tasks.ContainsKey("Freezing") || mob.Actor.BattleStatus.Additions.ContainsKey("LowerBodyParalysis")) return;
                float[] src = new float[3] { mob.Actor.x, mob.Actor.y, mob.Actor.z };
                float[] dst = new float[3] { dest.x, dest.y, dest.z };
                if (MobAI.GetDistance2(src, dst) > (150 + mob.Size))
                {
                    int yaw = MobAI.GetYawFromVector(MobAI.GetUnitVector(src, dst));
                    dst = MobAI.Add(dst, MobAI.Inverse(MobAI.ScalarProduct(MobAI.GetUnitVector(src, dst), (80 + mob.Size))));

                    float[] diff;
                    if (MobAI.GetDistance(src, dst) > mob.RunSpeed) diff = MobAI.Add(src, MobAI.ScalarProduct(MobAI.GetUnitVector(src, dst), mob.RunSpeed));
                    else diff = new float[3] { dst[0], dst[1], dst[2] };
                    if (dst[0] != lastdst[0] || dst[1] != lastdst[1] || dst[2] != lastdst[2]) this.mob.Map.MoveActor(Map.MOVE_TYPE.START, this.mob.Actor, src, yaw, dst, 0, mob.mRunSpeed);
                    //src = diff;
                    lastdst = dst;
                    this.mob.Actor.yaw = yaw;
                    this.mob.Actor.x = diff[0];
                    this.mob.Actor.y = diff[1];
                    this.mob.Actor.z = diff[2];
                    if (this.mob.Map.HasHeightMap())
                    {
                        if ((this.mob.LivingSpace == Mob.Space.Amphibian || this.mob.LivingSpace == Mob.Space.Land) && diff[2] > this.mob.Map.HeightMap.water_level)
                            this.mob.Actor.z = mob.Map.GetHeight(diff[0], diff[1]);
                    }                                        
                }
                else
                {
                    this.mob.Map.MoveActor(Map.MOVE_TYPE.STOP, this.mob.Actor, src, this.mob.Actor.yaw, src, 0, mob.mRunSpeed);
                    this.status = CommandStatus.FINISHED;
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, null);
            }

        }
        public CommandStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        public void Dispose()
        {
            this.status = CommandStatus.FINISHED;
        }

    }
}
