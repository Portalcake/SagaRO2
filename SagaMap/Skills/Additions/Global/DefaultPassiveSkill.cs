using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;
using SagaMap.Bonus;

namespace SagaMap.Skills.Additions.Global
{
    public class DefaultPassiveSkill : SagaDB.Actors.Addition
    {
        SkillIDs skillID;
        bool activate;
        /// <summary>
        /// Constructor for Addition: Short Sword Mastery
        /// </summary>
        /// <param name="id">Skill ID</param>
        /// <param name="actor">Actor, which this addition get attached to</param>
        public DefaultPassiveSkill(SkillIDs id, Actor actor,string name, bool ifActivate)
        {
            this.Name = name;
            this.skillID = id;
            this.AttachedActor = actor;
            this.activate = ifActivate;
        }
       
        public override bool IfActivate
        {
            get
            {
                return activate;
            }
        }

        public override void AdditionEnd()
        {
            BonusHandler.Instance.SkillAddAddition(this.AttachedActor, (uint)this.skillID, true);
        }

        public override void AdditionStart()
        {
            BonusHandler.Instance.SkillAddAddition(this.AttachedActor, (uint)this.skillID, false);
        }
    }
}
