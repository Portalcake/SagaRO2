using System;
using System.Collections.Generic;
using System.Text;

namespace SagaDB.Quest
{
    public enum QuestType
    {
        OfficialQuest,
        PersonalQuest,
    }
    [Serializable]
    public class Quest
    {
        private uint id;
        public uint dbID;
        private Dictionary<uint,Step> steps;

        public override string ToString()
        {
            return id.ToString();
        }

        public uint ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public Dictionary<uint, Step> Steps
        {
            get
            {
                return this.steps;
            }
            set
            {
                this.steps = value;
            }
        }

        public Quest()
        {
            Steps = new Dictionary<uint,Step>();
        }
    }
}
