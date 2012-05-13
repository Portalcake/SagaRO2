using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaDB.Items;
using SagaLib;

namespace SagaMap.ActorEventHandlers
{
    public class NPC_EventHandler : ActorEventHandler
    {
        public Map map;
        private ActorNPC I;

        public NPC_EventHandler(ActorNPC actor, Map map)
        {
            this.I = actor;
            this.map = map;
        }


        public void OnCreate(bool success)
        {
            if (success)
            {
                I.invisble = false;
                map.OnActorVisibilityChange(I);
            }
        }

        public void OnMapLoaded()
        {

        }

        public void OnReSpawn()
        {
            I.state = 0;
            I.stance = Global.STANCE.REBORN;
        }

        public void OnSelectButton(ActorPC sActor, int button)
        {
        }

        public void OnDie()
        {
            if(this.I.NPCinv==null) this.I.NPCinv= new List<Item>() ;

            I.state = 0;
            if (I.BattleStatus.Status != null) I.BattleStatus.Status.Clear();
            I.stance = Global.STANCE.DIE;
            
        }

        public void OnKick()
        {

        }

        public void OnDelete()
        {

        }

        public void OnActorAppears(Actor dActor)
        {
           this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAT, new Map.ChatArgs(SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.NORMAL, "Hello " + dActor.name), I, false);
        }

        public void OnActorChangesState(Actor aActor, MapEventArgs args)
        {

        }

        public void OnActorStartsMoving(Actor mActor, float[] pos, float[] accel, int yaw, ushort speed, uint delayTime)
        {

        }

        public void OnActorStartsMoving(ActorNPC mActor, byte count, float[] waypoints, ushort speed) 
        {
        
        }
        
        public void OnActorStopsMoving(Actor mActor, float[] pos, int yaw, ushort speed, uint delayTime)
        {

        }

        public void OnActorChat(Actor cActor, MapEventArgs args)
        {

        }

        public void OnActorDisappears(Actor dActor)
        {

        }

        public void OnActorSkillUse(Actor sActor, MapEventArgs args)
        {

        }

        public void OnActorChangeEquip(Actor sActor, MapEventArgs args)
        {

        }


        public void OnAddItem(Item nitem, SagaDB.Items.ITEM_UPDATE_REASON reason) { }

        public void OnSendShopList(List<Item> items, uint money, uint ActorID)
        { }

        public void OnTimeWeatherChange(byte[] gameTime, Global.WEATHER_TYPE weather)
        { }

        public void OnTeleport(float x, float y, float z)
        { }

        public void OnPartyInvite(ActorPC sActor) { }

        public void OnPartyAccept(ActorPC sActor) { }

        public void OnTradeStart(ActorPC sActor) { }

        public void OnTradeStatus(uint targetid, TradeResults status) { }

        public void OnTradeItem(byte Tradeslot, Item TradeItem) { }

        public void OnTradeConfirm() { }

        public void OnTradeZeny(int monies) { }

        public void OnResetTradeItems() { }

        public void PerformTrade() { }

        public void OnSendWhisper(string name, string message,byte flag) { }

        public void OnSendMessage(string from, string message) { }

        public void OnChangeStatus(Actor sActor, MapEventArgs args) { }

        public void OnActorSelection(ActorPC sActor, MapEventArgs args) { }
    }
}
