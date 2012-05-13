using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;
using SagaDB.Items;
using SagaLib;
using SagaDB.Mail;
namespace SagaMap
{
    public abstract class Mailbox : MapItem
    {
        public override void OnClicked(ActorPC pc)
        {
            base.OnClicked(pc);
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            Packets.Server.MailList p1 = new SagaMap.Packets.Server.MailList();
            p1.SetMails(MapServer.charDB.GetMail(SearchType.Receiver, pc.name));
            p1.SetActorID(pc.CurTarget.id);
            eh.C.netIO.SendPacket(p1, eh.C.SessionID);
        }
    }
}
