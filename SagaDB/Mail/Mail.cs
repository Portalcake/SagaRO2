using System;
using System.Collections.Generic;
using System.Text;

namespace SagaDB.Mail
{
    public enum SearchType
    {
        MailID,
        Sender,
        Receiver,
    }
    [Serializable]    
    public class Mail
    {
        public uint ID;
        public string sender;
        public string receiver;
        public string topic;
        public string content;
        public DateTime date;
        public byte valid;
        public byte read;
        public uint zeny;
        public uint item;
        public string creator;
        public byte stack;
        public ushort durability;
    }
}
