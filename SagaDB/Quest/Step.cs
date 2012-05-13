using System;
using System.Collections.Generic;
using System.Text;

namespace SagaDB.Quest
{
    [Serializable]
    public class Step
    {
        public byte step;
        private uint id;
        private byte status;
        public uint nextStep;
        public Dictionary<byte, byte> SubSteps;


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

        public byte Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public Step()
        {
        }
    }
}
