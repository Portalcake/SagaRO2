using System;
using System.Collections.Generic;
using System.Text;

using SagaDB;
using SagaDB.Actors;

using SagaLib;

namespace SagaDB.Items
{
    [Serializable]
    public class Bonus
    {
        private ADDITION_BONUS mBonus;
        private int mValue;
        
        public ADDITION_BONUS Effect { get { return mBonus; } set { this.mBonus = value; } }
        public int Value { get { return this.mValue; } set { this.mValue = value; } }

        public override string ToString()
        {
            return this.Effect.ToString();
        }
    }
}
