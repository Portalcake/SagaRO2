using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actors;
using SagaMap;
using SagaMap.Tasks.MobTasks.AICommands;
using SagaMap.Scripting;

namespace SagaMap.Tasks
{
    public class MobAI : MultiRunTask
    {
       public enum Activity
        {
            BUSY,
            LAZY,
       }

        private Activity m_activity;
        private Mob mob;
        public Dictionary<string,AICommand> commands;

        public MobAI(Mob mob)
        {
            this.dueTime = 1000;
            this.period = 1000;//process 1 command every second
            this.mob = mob;
            this.commands = new Dictionary<string,AICommand>();
            this.commands.Add("Attack", new Attack(mob));
            this.AIActivity = Activity.LAZY;
        }

        public Activity AIActivity
        {
            get
            {
                return this.m_activity;
            }
            set
            {
                if (m_activity != value)
                {
                    m_activity = value;
                    switch (m_activity)
                    {
                        case Activity.LAZY :
                            this.dueTime = 0;
                            this.period = 1000;
                            if (this.Activated())
                            {
                                this.Deactivate();
                                this.Activate();
                            }
                            break;
                        case Activity.BUSY :
                            this.dueTime = 0;
                            this.period = 200;
                            if (this.Activated())
                            {
                                this.Deactivate();
                                this.Activate();
                            }
                            break;
                    }
                }
            }
        }

        public void Start()
        {
            this.mob.Damage.Clear();
            this.mob.Hate.Clear();//Hate table should be cleard at respawn
            this.mob.Actor.BattleStatus.Status = new List<uint>();
            this.commands = new Dictionary<string, AICommand>();
            this.commands.Add("Attack", new Attack(mob));
            this.AIActivity = Activity.LAZY;
            this.Activate();
        }

        public override void CallBack(object o)
        {
            List<string> deletequeue = new List<string>();
            //ClientManager.EnterCriticalArea();
            try
            {
                
                string[] keys;
                if (mob.Actor.stance ==  Global.STANCE.DIE)
                {
                    this.Pause();
                    //ClientManager.LeaveCriticalArea();
                    return;
                }
                if (mob.Actor.Tasks.ContainsKey("Stunned"))
                {
                    //ClientManager.LeaveCriticalArea();
                    return;
                }
                if (commands.Count == 1)
                {
                    Attack att =(Attack) commands["Attack"];
                    if (mob.Hate.Count == 0)
                    {
                        if (mob.Actor.HP != mob.Actor.maxHP) mob.Actor.HP = mob.Actor.maxHP;// it heals itself, if there's no enemy.
                        if (this.mob.Map.GetRegionPlayerCount(this.mob.Actor.region) == 0)
                        {
                            this.Pause();
                            //ClientManager.LeaveCriticalArea();
                            return;
                        }
                        if (Global.Random.Next(0, 100) <= 15 && this.mob.WalkSpeed > 0 && this.mob.Map.HasHeightMap())
                        {
                            float[] src = new float[3] { mob.Actor.x, mob.Actor.y, mob.Actor.z };
                            float[] dst = new float[3];
                            float[] univec = GetRandomUnitVector();
                            if (GetDistance(src, new float[3] { mob.StartX, mob.StartY, mob.StartZ }) > mob.range)
                            {
                                univec = GetUnitVector(src, new float[3] { mob.StartX, mob.StartY, mob.StartZ });
                            }
                            dst = Add(src, ScalarProduct(univec, 500));
                            dst[2] = mob.Map.GetHeight(dst[0], dst[1]);
                            if ((!(this.mob.LivingSpace == Mob.Space.Amphibian || this.mob.LivingSpace == Mob.Space.Land) && dst[2] < this.mob.Map.HeightMap.water_level))
                                dst[2] = Global.Random.Next((int)dst[2], (int)this.mob.Map.HeightMap.water_level);
                            Move move = new Move(this.mob, dst);
                            commands.Add("Move", move);
                        }
                        if (!this.mob.Map.HasHeightMap())
                        {
                            float[] src = new float[3] { mob.Actor.x, mob.Actor.y, mob.Actor.z };
                            float[] dst = new float[3];
                            float[] univec = new float[3];
                            if (GetDistance(src, new float[3] { mob.StartX, mob.StartY, mob.StartZ }) > 0)
                            {
                                univec = GetUnitVector(src, new float[3] { mob.StartX, mob.StartY, mob.StartZ });
                                dst = Add(src, ScalarProduct(univec, 500));
                                Move move = new Move(this.mob, dst);
                                commands.Add("Move", move);
                            }
                        }
                    }
                }

                keys = new string[commands.Count];
                commands.Keys.CopyTo(keys, 0);
                int count = commands.Count;
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        string j;
                        j = keys[i];
                        if (commands[j].Status != CommandStatus.FINISHED && commands[j].Status != CommandStatus.DELETING && commands[j].Status != CommandStatus.PAUSED)
                            commands[j].Update(null);
                        if (commands[j].Status == CommandStatus.FINISHED)
                        {
                            deletequeue.Add(j);
                            commands[j].Status = CommandStatus.DELETING;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                foreach (string i in deletequeue)
                {
                    commands.Remove(i);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, null);
                Logger.ShowError(ex.StackTrace, null);
            }
            //ClientManager.LeaveCriticalArea();
        }

        public void Pause()
        {
            try
            {

                this.Deactivate();
                foreach (string i in commands.Keys)
                {
                    commands[i].Dispose();
                }
                commands.Clear();
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex,null);
            }
            
        }

        public void OnBeenAttacked(Actor sActor, MapEventArgs args)
        {
            Map.SkillArgs arg=(Map.SkillArgs) args ;
            if (this.Activated() == false)
            {
                this.Start();
            }
            if (mob.Hate.ContainsKey(sActor.id))
            {
                byte tmp = (byte)(arg.damage / 10);
                if (tmp < 20) tmp = 20;
                if (mob.Hate[sActor.id] < tmp) mob.Hate[sActor.id] = tmp;
            }
            else
            {
                byte tmp = (byte)(arg.damage / 10);
                if (tmp < 20) tmp = 20;
                mob.Hate.Add(sActor.id, tmp);
            }
            if (mob.Damage.ContainsKey(sActor.id))
            {
                mob.Damage[sActor.id] = (ushort)(mob.Damage[sActor.id] + arg.damage);
            }
            else mob.Damage.Add(sActor.id, (ushort)(arg.damage));
        }

        public void OnSkillUse(Actor sActor, MapEventArgs args)
        {
            Map.SkillArgs arg = (Map.SkillArgs)args;
            switch (arg.skillID)
            {
                case SagaMap.Skills.SkillIDs.ActDead:
                case SagaMap.Skills.SkillIDs.ActDead2:
                case SagaMap.Skills.SkillIDs.ActDead3:
                case SagaMap.Skills.SkillIDs.ActDead4:
                case SagaMap.Skills.SkillIDs.ActDead5:
                    if (this.mob.Hate.ContainsKey(sActor.id))
                    {
                        if (Global.Random.Next(0, 99) <= 50)
                            this.mob.Hate.Remove(sActor.id);
                    }
                    break; 
            }
        }

        #region Vector Operations

        public static float[] GetRandomUnitVector()
        {
            float[] univec = new float[3];
            int rand = Global.Random.Next(0, 359);
            if (rand == 0 || rand == 90 || rand == 180 || rand == 270)
            {
                switch (rand)
                {
                    case 0:
                        univec[1] = 1;
                        break;
                    case 90:
                        univec[0] = 1;
                        break;
                    case 180:
                        univec[1] = -1;
                        break;
                    case 270:
                        univec[0] = -1;
                        break;
                }
            }
            else
            {
                if (rand < 180) univec[1] = 1; else univec[1] = -1;
                univec[0] = (float)Math.Tan((rand * 3.14f) / 180);
                univec = GetUnitVector(new float[3] { 0, 0, 0 }, univec);
            }
            return univec;
        }

        public static float GetDistance(float[] src, float[] dst)
        {
            return (float)Math.Sqrt(Pow((dst[0] - src[0]), 2) + Pow((dst[1] - src[1]), 2) + Pow((dst[2] - src[2]), 2));
        }

        public static float GetDistance2(float[] src, float[] dst)
        {
            return (float)Math.Sqrt(Pow((dst[0] - src[0]), 2) + Pow((dst[1] - src[1]), 2));
        }

        public static float[] GetUnitVector(float[] src, float[] dst)
        {
            float[] diff = new float[3];
            float distance = GetDistance(src, dst);
            diff[0] = (dst[0] - src[0]) / distance;
            diff[1] = (dst[1] - src[1]) / distance;
            diff[2] = (dst[2] - src[2]) / distance;
            return diff;
        }

        public static ushort GetYawFromVector(float[] vec)
        {
            double angle = Math.Atan2(vec[1], vec[0]) * 32678 / Math.PI;
            return (ushort)angle;
        }

        public static float[] Inverse(float[] src)
        {
            float[] dst = new float[3];
            dst[0] = -src[0];
            dst[1] = -src[1];
            dst[2] = -src[2];
            return dst;
        }

        public static float[] ScalarProduct(float[] src, float scalar)
        {
            float[] res = new float[3];
            res[0] = src[0] * scalar;
            res[1] = src[1] * scalar;
            res[2] = src[2] * scalar;
            return res;
        }

        public static int[] ScalarProduct(int[] src, int scalar)
        {
            int[] res = new int[3];
            res[0] = (int)(src[0] * scalar);
            res[1] = (int)(src[1] * scalar);
            res[2] = (int)(src[2] * scalar);
            return res;
        }


        public static float Pow(float x, int y)
        {
            return (float)Math.Pow(x, y);
        }

        public static float[] Add(float[] v1, float[] v2)
        {
            float[] dst = new float[3];
            dst[0] = v1[0] + v2[0];
            dst[1] = v1[1] + v2[1];
            dst[2] = v1[2] + v2[2];
            return dst;
        }
#endregion

        

    }
}
