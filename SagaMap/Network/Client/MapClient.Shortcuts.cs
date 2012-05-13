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

        #region "0x0A"
        // 0x0A Packets =========================================

        // 0A 01
        public void OnAddShortcut(SagaMap.Packets.Client.AddShortcut p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            try
            {
                ActorPC.Shortcut sc = new ActorPC.Shortcut();
                Packets.Server.SendAddShortcut sendPacket = new SagaMap.Packets.Server.SendAddShortcut();
                sendPacket.SetType(p.GetShortcutType());
                sendPacket.SetSlot(p.GetSlotNumber());
                sendPacket.SetID(p.GetIDNumber());
                sc.type = p.GetShortcutType();
                sc.ID = p.GetIDNumber();
                if (this.Char.ShorcutIDs.ContainsKey(p.GetSlotNumber())) this.Char.ShorcutIDs.Remove(p.GetSlotNumber());
                this.Char.ShorcutIDs.Add(p.GetSlotNumber(), sc);
                this.netIO.SendPacket(sendPacket, this.SessionID);
            }
            catch (Exception ex)
            {
                Logger.ShowWarning("slotid = " + p.GetSlotNumber() + " idfrompacket(p) = " + p.GetIDNumber(), null);
                Logger.ShowError(ex, null);
            }


        }

        // 0A 02
        public void OnDelShortcut(SagaMap.Packets.Client.DelShortcut p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;

            Packets.Server.SendDelShortcut sendPacket = new SagaMap.Packets.Server.SendDelShortcut();
            sendPacket.SetSlot(p.GetSlotNumber());
            this.netIO.SendPacket(sendPacket, this.SessionID);
            try
            {
                this.Char.ShorcutIDs.Remove(p.GetSlotNumber());
            }
            catch (Exception ex)
            {
                byte tmp = p.GetSlotNumber();
                Logger.ShowError("Cannot delete shortcut of slotid:" + p.GetSlotNumber(), null);
                Logger.ShowError(ex, null);
            }


        }
        #endregion

    }
}
