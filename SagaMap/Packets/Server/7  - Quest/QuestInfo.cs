using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class QuestInfo : Packet
    {
        public QuestInfo()
        {
            this.data = new byte[91];
            this.ID = 0x0701;
            this.offset = 4;            
        }

       
        public void SetQuestInfo(List<SagaDB.Quest.Quest> quests)
        {
            this.PutByte((byte)quests.Count, 4);
            byte steps = 0;
            byte count = 0;
            foreach (SagaDB.Quest.Quest i in quests)
            {
                this.PutUInt(i.ID, (ushort)(5 + count * 4));
                byte[] tmp = new byte[91 + ((i.Steps.Count + steps) * 14)];
                this.data.CopyTo(tmp, 0);
                this.data = tmp;
                this.PutUShort((byte)i.Steps.Count, (ushort)(85 + count));
                int k = 0;
                foreach (SagaDB.Quest.Step j in i.Steps.Values)
                {
                    this.PutUInt(j.ID, (ushort)(91 + (k + steps) * 10 ));
                    this.PutByte((byte)j.Status, (ushort)(95 + (k + steps) * 10 ));
                    this.PutUInt(j.nextStep, (ushort)(96 + (k + steps) * 10 ));
                    this.PutByte((byte)j.step, (ushort)(100 + (k + steps) * 10 ));
                    if (j.step == 2 && k == 0) j.step= 0;
                    k++;
                }
                steps += (byte)i.Steps.Count;
                count += 1;
            }
        }
    }
}
