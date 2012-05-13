using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;
using SagaMap.Bonus;

namespace SagaMap.Skills.Additions.Global
{
    public class DefaultBuff : SagaDB.Actors.Addition
    {
        SkillIDs skillID = 0;
        DateTime endTime;
        int lifeTime;
        uint additionID = 0;

        public DefaultBuff(SkillIDs id, Actor actor, string name,int lifetime, Addition.AdditionType type)
        {
            this.Name = name;
            this.skillID = id;
            this.AttachedActor = actor;
            this.lifeTime = lifetime;
            this.MyType = type;
        }

        public DefaultBuff(int additionID, Actor actor, string name, int lifetime, Addition.AdditionType type)
        {
            this.Name = name;
            this.additionID = (uint)additionID;
            this.AttachedActor = actor;
            this.lifeTime = lifetime;
            this.MyType = type;
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
                return lifeTime;
            }
        }

        public override void AdditionEnd()
        {
            SkillHandler.RemoveAddition(this.AttachedActor, this, true);
            if (this.skillID != 0)
            {
                BonusHandler.Instance.SkillAddAddition(this.AttachedActor, (uint)this.skillID, true);
                SkillHandler.RemoveStatusIcon(this.AttachedActor, (uint)this.skillID);
            }
            if (this.additionID != 0)
            {
                BonusHandler.Instance.AddAddition(this.AttachedActor, this.additionID, true);
                SkillHandler.RemoveStatusIcon(this.AttachedActor, this.additionID);
            }
            TimerEnd();
        }

        public override void AdditionStart()
        {
            this.endTime = DateTime.Now + new TimeSpan(0, lifeTime / 60000, (lifeTime / 1000) % 60);
            InitTimer(lifeTime, 0);
            if (this.skillID != 0)
            {
                SkillHandler.AddStatusIcon(this.AttachedActor, (uint)this.skillID, (uint)lifeTime);
                BonusHandler.Instance.SkillAddAddition(this.AttachedActor, (uint)this.skillID, false);
            }
            if (this.additionID != 0)
            {
                SkillHandler.AddStatusIcon(this.AttachedActor, this.additionID, (uint)lifeTime);
                BonusHandler.Instance.AddAddition(this.AttachedActor, this.additionID, false);
            }
            TimerStart();
        }

        public override void OnTimerEnd()
        {
            this.AdditionEnd();           
        }
    }
}
