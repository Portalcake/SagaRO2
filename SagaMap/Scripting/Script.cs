using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;
using SagaDB.Items;
using SagaMap.Manager;

namespace SagaMap
{
    public class Script
    {
        public static void Say( Npc npc, string sentence )
        {
            npc.Map.SendEventToAllActorsWhoCanSeeActor( Map.EVENT_TYPE.CHAT, new Map.ChatArgs( SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.NORMAL, sentence ), npc.Actor, false );
        }

        public static void GiveItem( Npc npc, ActorPC receiver, int itemID )
        {
            Item nItem = new Item( itemID );
            nItem.creatorName = npc.Name;
            if( nItem == null )
            {
                SagaLib.Logger.ShowWarning( "Script error: cannot create item with ID " + itemID, null );
                return;
            }
            npc.Map.AddItemToActor(receiver, nItem, ITEM_UPDATE_REASON.NPC_GAVE);
        }

        public static void Warp( ActorPC pc, byte mapid, float x, float y, float z )
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            eh.C.map.SendActorToMap( pc, mapid, x, y, z );
        }

        public static void Warp( ActorPC pc, byte mapid )
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            Map map;
            if( !MapManager.Instance.GetMap( mapid, out map ) ) return;
            float[] coord = map.GetRandomPos();
            eh.C.map.SendActorToMap( pc, mapid, coord[0], coord[1], coord[2] );
        }
    }
}
