using System;
using System.Collections.Generic;
using System.Text;

namespace SagaDB.Items
{
    public enum MarketSearchOption
    {
        Owner,
        ItemType,
        Name,
        CLv,
    }
    public class MarketplaceItem
    {
        public uint id;
        public Item item;
        public string owner;
        public uint price;
        public DateTime expire;
        public string comment;
    }
}
