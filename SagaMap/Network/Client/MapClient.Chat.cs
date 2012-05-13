//#define Preview_Version
using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

using SagaDB;
using SagaDB.Actors;
using SagaDB.Items;
using SagaDB.Mail;
using SagaLib;
using SagaMap.Manager;
using SagaMap.Skills;

namespace SagaMap
{
    public partial class MapClient
    {

        #region "0x04"
        // 0x04 Packets =========================================

        // 04 01
        /// <summary>
        /// Receive the chat from the client, check if its an AtCommand (GM command).
        /// Send the output to all the clients in the area.
        /// </summary>
        public void OnSendChat(SagaMap.Packets.Client.GetChat p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            if (p.isValid())
            {
                if ((byte)this.Char.muted == 0)
                    Pc.OnChat(p.isAtCommand(), p.GetMessageType(), p.GetMessage());
                else
                    this.SendMessage("Saga", "You have been muted for epic failure");
            }
        }

        // 04 02
        /// <summary>
        /// Receive the whisper from the client. Check if the recipient is online from the MapClientManager
        /// Send a whisper to the other player.
        /// </summary>
        public void OnSendWhisper(SagaMap.Packets.Client.GetWhisper p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            if (p.isValid())
            {
                if ((byte)this.Char.muted == 0)
                {
                    string name = p.GetName();
                    while (name.Substring(name.Length - 1) == "\0")
                    {
                        name = name.Substring(0, name.Length - 1);
                    }
                    MapClient client = MapClientManager.Instance.GetClient(name);
                    if (client == null)
                    {
                        SendMessage("Saga", "This character is not online/doesn't exist");
                        return;
                    }
                    ActorPC target = client.Char;
                    if (target != null)
                    {
                        target.e.OnSendWhisper(this.Char.name, p.GetMessage(), 2);
                        this.Char.e.OnSendWhisper(name, p.GetMessage(), 0);
                    }
                    else
                        SendMessage("Saga", "This character is not online/doesn't exist");
                }
                else
                    this.SendMessage("Saga", "You have been muted for epic failure");
            }
        }
        #endregion

    }
}
