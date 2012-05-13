using System;
using System.Collections.Generic;
using System.Text;

namespace SagaDB.Actors
{
    [Serializable]
    public class ActorItem : Actor
    {
        public uint itemtype;
        public ActorItem()
        {
            this.type = ActorType.Item;
        }
    }
}
