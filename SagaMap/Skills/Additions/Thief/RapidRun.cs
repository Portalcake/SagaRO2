using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;
using SagaMap.Bonus;

namespace SagaMap.Skills.Additions.Thief
{
    public class RapidRun : SagaDB.Actors.Addition
    {
        SkillIDs skillID;
        DateTime endTime;
        /// <summary>
        /// Constructor for Addition: RapidRun
        /// </summary>
        /// <param name="id">Skill ID</param>
        /// <param name="actor">Actor, which this addition get attached to</param>
        public RapidRun(SkillIDs id, Actor actor)
        {
            this.Name = "RapidRun";
            this.skillID = id;
            this.AttachedActor = actor;
        }

        public override int RestLifeTime
        {
            get
            {
                return (int)(this.endTime - DateTime.Now).TotalMilliseconds;
            }
        }

        public override int TotalLifeTime
        {
            get
            {
                return 30000;
            }
        }

        public override void AdditionEnd()
        {
            SkillHandler.RemoveAddition(this.AttachedActor, this, true);
            BonusHandler.Instance.SkillAddAddition(this.AttachedActor, (uint)this.skillID, true);
            SkillHandler.RemoveStatusIcon(this.AttachedActor, (uint)this.skillID);
            TimerEnd();
        }

        public override void AdditionStart()
        {
            this.endTime = DateTime.Now + new TimeSpan(0, 0, 15);
            InitTimer(15000, 0);
            SkillHandler.AddStatusIcon(this.AttachedActor, (uint)this.skillID, 15000);
            BonusHandler.Instance.SkillAddAddition(this.AttachedActor, (uint)this.skillID, false);
            TimerStart();
        }

        public override void OnTimerEnd()
        {
            this.AdditionEnd();           
        }
    }
}
