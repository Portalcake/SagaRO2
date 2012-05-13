using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;
using SagaMap.Bonus;

namespace SagaMap.Skills.Additions.Novice
{
    public class ShortSwordMastery : SagaDB.Actors.Addition
    {
        SkillIDs skillID;
        /// <summary>
        /// Constructor for Addition: Short Sword Mastery
        /// </summary>
        /// <param name="id">Skill ID</param>
        /// <param name="actor">Actor, which this addition get attached to</param>
        public ShortSwordMastery(SkillIDs id, Actor actor)
        {
            this.Name = "ShortSwordMastery";
            this.skillID = id;
            this.AttachedActor = actor;
        }
       
        public override bool IfActivate
        {
            get
            {
                return SagaDB.Items.WeaponFactory.GetActiveWeapon((ActorPC)this.AttachedActor).type == 1;
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
