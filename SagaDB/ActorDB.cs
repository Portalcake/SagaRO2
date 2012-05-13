using System;
using System.Collections.Generic;
using System.Text;


using SagaDB.Actors;
using SagaDB.Items;

namespace SagaDB
{
    public interface ActorDB
    {       

        /// <summary>
        /// Write the given character to the database.
        /// </summary>
        /// <param name="user">Character that needs to be writen.</param>
        void SaveChar(ActorPC aChar);

        void CreateChar(ref ActorPC aChar,int account_id);

        void DeleteChar(ActorPC aChar);

        ActorPC GetChar(uint charID);

        void NewItem(ActorPC pc, Item item);

        void UpdateItem(ActorPC pc, Item item);

        void DeleteItem(ActorPC pc, Item item);

        void NewSkill(ActorPC pc, SkillType type, SkillInfo skill);

        void UpdateSkill(ActorPC pc, SkillType type, SkillInfo skill);

        void DeleteSkill(ActorPC pc, SkillInfo skill);

        void NewQuest(ActorPC pc, Quest.QuestType type, Quest.Quest quest);

        void UpdateQuest(ActorPC pc, Quest.QuestType type, Quest.Quest quest);

        void DeleteQuest(ActorPC pc, Quest.Quest quest);

        void NewJobLevel(ActorPC pc, JobType type, byte level);

        void UpdateJobLevel(ActorPC pc, JobType type, byte level);
        
        void DeleteJobLevel(ActorPC pc, JobType type);

        void NewMapInfo(ActorPC pc, byte mapID, byte value);

        void UpdateMapInfo(ActorPC pc, byte mapID, byte value);

        void DeleteMapInfo(ActorPC pc, byte mapID);

        void NewStorage(ActorPC pc, Item item);

        void UpdateStorage(ActorPC pc, Item item);

        void DeleteStorage(ActorPC pc, Item item);        

        bool CharExists(byte worldID, string name);

        uint[] GetCharIDs(int account_id);

        void SaveNpc(ActorNPC aNpc);

        void NewMail(Mail.Mail mail);

        void SaveMail(Mail.Mail mail);

        string GetCharName(uint id);

        List<Mail.Mail> GetMail(Mail.SearchType type, string value);

        void DeleteMail(uint id);

        void RegisterMarketItem(MarketplaceItem item);

        void DeleteMarketItem(MarketplaceItem item);

        List<MarketplaceItem> SearchMarketItem(MarketSearchOption option, ushort pageindex, object vars);

        MarketplaceItem GetMarketItem(uint id);
        
        //void CreateNpc(ActorNPC aNpc);

        void DeleteNpc(ActorNPC aNpc);

        ActorNPC GetNpc(string scriptName);

        bool Connect();

        bool isConnected();
    }
}
