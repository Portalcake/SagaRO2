using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;
using SagaMap.Bonus;

namespace SagaMap.Skills.Additions.Global
{
    public class DefaultStance : SagaDB.Actors.Addition
    {
        SkillIDs skillID;
        
        public DefaultStance(SkillIDs id, Actor actor,string name)
        {
            this.Name = name;
            this.skillID = id;
            this.AttachedActor = actor;            
        }
      
        public override void AdditionEnd()
        {
            SkillHandler.RemoveStatusIcon(this.AttachedActor, (uint)this.skillID);
            BonusHandler.Instance.SkillAddAddition(this.AttachedActor, (uint)this.skillID, true);
        }

        public override void AdditionStart()
        {
            SkillHandler.AddStatusIcon(this.AttachedActor, (uint)this.skillID, 0);
            BonusHandler.Instance.SkillAddAddition(this.AttachedActor, (uint)this.skillID, false);
        }
    }
}
