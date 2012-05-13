using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;
using SagaMap.Bonus;

namespace SagaMap.Skills.Additions.Recruit
{
    public class Designation : SagaDB.Actors.Addition
    {
        SkillIDs skillID;
        DateTime endTime;
        /// <summary>
        /// Constructor for Addition: RapidRun
        /// </summary>
        /// <param name="id">Skill ID</param>
        /// <param name="actor">Actor, which this addition get attached to</param>
        public Designation(SkillIDs id, Actor actor)
        {
            this.Name = "Designation";
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
            this.endTime = DateTime.Now + new TimeSpan(0, 1, 0);
            InitTimer(60000, 0);
            SkillHandler.AddStatusIcon(this.AttachedActor, (uint)this.skillID, 60000);
            BonusHandler.Instance.SkillAddAddition(this.AttachedActor, (uint)this.skillID, false);
            TimerStart();
        }

        public override void OnTimerEnd()
        {
            this.AdditionEnd();           
        }
    }
}
