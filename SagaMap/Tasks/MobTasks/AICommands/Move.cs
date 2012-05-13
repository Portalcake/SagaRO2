using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actors;
using SagaMap;
using SagaMap.Scripting;

namespace SagaMap.Tasks.MobTasks.AICommands
{
    public class Move : AICommand
    {
        private CommandStatus status;
        private float[] dest;
        private Mob mob;

        public Move(Mob mob, float[] dest)
        {
            this.mob = mob;
            this.dest = dest;
        }
        public string GetName() { return "Move"; }
        public void Update(object para)
        {
            try
            {
                if (mob.Map.HasHeightMap())
                {
                    if (mob.Map.HeightMap.water_level != 0)
                    {
                        switch (mob.LivingSpace)
                        {
                            case Mob.Space.Land:
                                if (dest[2] < mob.Map.HeightMap.water_level)
                                {
                                    this.Status = CommandStatus.FINISHED;
                                    return;
                                }
                                break;
                            case Mob.Space.Water :
                                if (dest[2] >= mob.Map.HeightMap.water_level)
                                {
                                    this.Status = CommandStatus.FINISHED;
                                    return;
                                }
                                break;
                        }
                    }
                }
                if (mob.WalkSpeed == 0 || mob.Actor.Tasks.ContainsKey("Freezing") || mob.Actor.BattleStatus.Additions.ContainsKey("LowerBodyParalysis")) return;
                float[] src = new float[3] { mob.Actor.x, mob.Actor.y, mob.Actor.z };
                float[] dst = dest;
                if (MobAI.GetDistance(src, dst) > 0)
                {
                    float[] diff;
                    int yaw = MobAI.GetYawFromVector(MobAI.GetUnitVector(src, dst));
                    if (MobAI.GetDistance(src, dst) > mob.WalkSpeed) diff = MobAI.Add(src, MobAI.ScalarProduct(MobAI.GetUnitVector(src, dst), mob.WalkSpeed));
                    else diff = dst;
                    this.mob.Map.MoveActor(Map.MOVE_TYPE.START, this.mob.Actor, src, yaw, dst, 0, mob.mWalkSpeed);
                    src = diff;
                    this.mob.Actor.yaw = yaw;
                    this.mob.Actor.x = src[0];
                    this.mob.Actor.y = src[1];
                    this.mob.Actor.z = src[2];
                }
                else
                {
                    this.mob.Map.MoveActor(Map.MOVE_TYPE.STOP, this.mob.Actor, src, this.mob.Actor.yaw, src, 0, mob.mWalkSpeed);
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
