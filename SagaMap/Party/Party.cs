using System;
using System.Collections.Generic;
using System.Text;

namespace SagaMap.Party
{
    public class Party
    {
        public enum ELoot_Setting
        {
            LOOT_FINDERS_KEEPS = 1,
            LOOT_ONE_PERSON = 2,
            LOOT_TURN_BASED = 3,
        }

        public enum EXP_Setting
        {
            EXP_CLVL = 1,
            EXP_DAMAGE = 2,
        }

        public MapClient Leader;
        public List<MapClient> Members=new List<MapClient>();
        public ELoot_Setting LootShare;
        public EXP_Setting XPShare;

        public uint AverageCLV
        {
            get
            {
                uint tmp = 0;
                foreach (MapClient client in Members)
                {
                    tmp += client.Char.cLevel;
                }
                return (uint)(tmp / Members.Count);
            }
        }

        public void AddMember(MapClient client)
        {
            if (this.Members.Count != 0)
            {
                if (this.Members.Count >= 2)
                {
                    SendNewMemberInfoToOldMember(client);
                    this.Members.Add(client);
                }
                else
                {
                    this.Members.Add(client);
                    SendPartyInfo(this.Members[0]);
                }
                SendPartyInfo(client);
            }
            else
            {
                this.Members.Add(client);
            }
        }

        private byte GetIndexForClient(MapClient c1, MapClient c2)
        {
            /*int index1, index2;
            index1 = this.Members.IndexOf(c1);
            index2 = this.Members.IndexOf(c2);
            if (c1 == c2) return 1;
            if (index1 < index2) return (byte)(index1 + 2);
            else return (byte)(index1 + 1);*/
            return 1;
        }

        public void RemoveMember(MapClient client, byte index)
        {
            /*foreach (MapClient c in this.Members)
            {
                if (GetIndexForClient(client, c) == index)
                {
                    RemoveMember(this.Members[index]);
                    return;
                }
            }*/
            RemoveMember(this.Members[index]);
            
        }

        public void RemoveMember(MapClient client)
        {
            foreach (MapClient c in this.Members)
            {
                if (c.state == MapClient.SESSION_STATE.LOGGEDOFF) continue;
                Packets.Server.PartyMemberQuit p = new SagaMap.Packets.Server.PartyMemberQuit();
                //p.SetIndex(GetIndexForClient(c, client));
                p.SetIndex((byte)(this.Members.IndexOf(client) + 1));
                p.SetUnknown(1);
                p.SetActorID(client.Char.id);
                c.netIO.SendPacket(p, c.SessionID);
            }
            Packets.Server.PartyDismissed tp = new SagaMap.Packets.Server.PartyDismissed();
            if (client.state != MapClient.SESSION_STATE.LOGGEDOFF) client.netIO.SendPacket(tp, client.SessionID);
            this.Members.Remove(client);
            client.Party = null;
            if (this.Members.Count == 1)
            {
                Packets.Server.PartyDismissed p = new SagaMap.Packets.Server.PartyDismissed();
                this.Members[0].netIO.SendPacket(p, this.Members[0].SessionID);
                this.Members[0].Party = null;
            }
            else
            {
                if (this.Leader == client)
                {
                    this.Leader = this.Members[0];
                }
                foreach (MapClient c in this.Members)
                {
                    if (c.state == MapClient.SESSION_STATE.LOGGEDOFF) continue;
                    SendPartyInfo(c);
                }
            }
        }

        private void SendNewMemberInfoToOldMember(MapClient client)
        {
            Packets.Server.PartyNewMember p = new SagaMap.Packets.Server.PartyNewMember();
            p.SetIndex(1);
            p.SetActorID(client.Char.id);
            p.SetUnknown(1);
            p.SetName(client.Char.name);
            Packets.Server.PartyMemberInfo p1 = new SagaMap.Packets.Server.PartyMemberInfo();
            p1.SetIndex(1);
            p1.SetActorID(client.Char.id);
            p1.SetActorInfo(client.Char);
            foreach (MapClient c in this.Members)
            {
                c.netIO.SendPacket(p, c.SessionID);
                c.netIO.SendPacket(p1, c.SessionID);
            }
        }

        public void UpdateMemberInfo(MapClient client)
        {
            if (!this.Members.Contains(client)) return;
            Packets.Server.PartyMemberInfo p1 = new SagaMap.Packets.Server.PartyMemberInfo();
            p1.SetIndex(1);
            p1.SetActorID(client.Char.id);
            p1.SetActorInfo(client.Char);
            foreach (MapClient c in this.Members)
            {
                c.netIO.SendPacket(p1, c.SessionID);
            }
        }

        private void SendPartyInfo(MapClient client)
        {
            Packets.Server.SendPartyInfo p = new SagaMap.Packets.Server.SendPartyInfo();
            p.SetLeaderIndex((byte)(this.Members.IndexOf(this.Leader) + 1));
            p.SetLeader(Leader.Char.id);
            p.SetSetting1((byte)LootShare);
            //p.SetSetting1(LootShare);
            //p.SetSetting2(1);
            p.SetSetting2((byte)XPShare);
            p.SetSetting3(0);
            p.SetSetting4(0);
            p.SetMemberInfo(this.Members);
            client.netIO.SendPacket(p, client.SessionID);
        }

        public void SendLoot(MapClient client, uint id)
        {
            Packets.Server.PartyMemberLoot p = new SagaMap.Packets.Server.PartyMemberLoot();
            p.SetIndex((byte)(this.Members.IndexOf(client) + 1));
            p.SetActorID(client.Char.id);
            p.SetItemID(id);
            foreach (MapClient c in this.Members)
            {
                if (c != client)
                    c.netIO.SendPacket(p, c.SessionID);
            }
        }

        public void SendPosition(MapClient client)
        {
            Packets.Server.PartyMemberPosition p = new SagaMap.Packets.Server.PartyMemberPosition();
            p.SetIndex(1);
            p.SetActorID(client.Char.id);
            p.SetX(client.Char.x / 1000);
            p.SetY(client.Char.y / 1000);
            foreach (MapClient c in this.Members)
            {
                if (c != client)
                {
                    if (c.Char.mapID == client.Char.mapID)
                        c.netIO.SendPacket(p, c.SessionID);
                }
            }
        }

        public void SetSharing(ELoot_Setting lootShare, EXP_Setting xpShare)
        {
            this.LootShare = lootShare;
            this.XPShare = xpShare;
            foreach (MapClient c in this.Members)
                SendPartyInfo(c);
        }
    }
}
