using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaDB.Actors
{
    public interface MapEventArgs { }
    public interface ActorEventHandler
    {
        void OnCreate(bool success);

        void OnReSpawn();

        void OnMapLoaded();

        void OnDie();

        void OnKick();

        void OnDelete();

        void OnActorAppears(Actor aActor);

        void OnActorChangesState(Actor aActor, MapEventArgs args);

        void OnActorStartsMoving(Actor mActor, float[] pos, float[] accel, int yaw, ushort speed, uint delayTime);

        void OnActorStartsMoving(ActorNPC mActor, byte count, float[] waypoints,ushort speed);

        void OnActorStopsMoving(Actor mActor, float[] pos, int yaw, ushort speed, uint delayTime);

        void OnActorChat(Actor cActor, MapEventArgs args);

        void OnActorDisappears(Actor dActor);

        void OnActorSkillUse(Actor sActor, MapEventArgs args);

        void OnActorChangeEquip(Actor sActor, MapEventArgs args);

        void OnSelectButton(ActorPC sActor, int button);

        void OnAddItem(Item nItem, ITEM_UPDATE_REASON reason);

        void OnSendShopList(List<Item> items, uint money,uint ActorID);

        void OnTimeWeatherChange(byte[] gameTime, Global.WEATHER_TYPE weather);

        void OnTeleport(float x, float y, float z);

        void OnPartyInvite(ActorPC sActor);

        void OnPartyAccept(ActorPC sActor);

        void OnTradeStart(ActorPC sActor);

        void OnTradeStatus(uint targetid, TradeResults status);

        void OnTradeItem(byte Tradeslot, Item TradeItem);

        void OnTradeConfirm();

        void OnTradeZeny(int monies);

        void OnResetTradeItems();

        void PerformTrade();

        void OnSendWhisper(string name, string message, byte flag);

        void OnSendMessage(string from, string message);

        void OnChangeStatus(Actor sActor, MapEventArgs args);

        void OnActorSelection(ActorPC sActor, MapEventArgs args);
    }
}
