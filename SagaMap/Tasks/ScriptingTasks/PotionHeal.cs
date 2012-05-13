using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actors;

namespace SagaMap.Tasks
{
    public class PotionHeal : MultiRunTask
    {
        private ActorPC pc;
        private ushort hp;
        private ushort hp2;
        private ushort sp;
        private ushort sp2;
        private int lifetime;
        private int counter=0;
        private uint skillid;
        private byte skilltype;
        public PotionHeal(ActorPC pc, ushort hp, ushort hp2, ushort sp, ushort sp2, int period, int lifetime,uint skillid,byte skilltype)
        {
            this.dueTime = 1000;
            this.period = period;
            this.pc = pc;
            this.hp = hp;
            this.hp2 = hp2;
            this.sp = sp;
            this.sp2 = sp2;
            this.lifetime = lifetime;
            this.skillid = skillid;
            this.skilltype =skilltype;
        }

        public override void  CallBack(object o)
        {
            ClientManager.EnterCriticalArea();
            
            uint damage;
            ushort hpv=(ushort)Global.Random.Next(hp,hp2);
            ushort spv = (ushort)Global.Random.Next(sp, sp2);
            pc.HP += hpv;
            pc.SP += spv;
            if (pc.HP > pc.maxHP) pc.HP = pc.maxHP;
            if (pc.SP > pc.maxSP) pc.SP = pc.maxSP;
            if(hpv !=0)damage =hpv; else damage =spv;
            SendResult(pc,new Map.SkillArgs(this.skilltype,0,this.skillid,pc.id,damage));
            counter++;
            if (counter == this.lifetime)
            {
                Skills.SkillHandler.RemoveStatusIcon(pc, this.skillid);
                this.Deactivate();
                pc.Tasks.Remove("PotionHeal");
            }

            ClientManager.LeaveCriticalArea();
        }

        private void SendResult(ActorPC pc,Map.SkillArgs args)
        {
            SagaMap.ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            Packets.Server.ItemActive packet = new SagaMap.Packets.Server.ItemActive();
            packet.SetSkillType(args.skillType);
            packet.SetContainer(0);
            packet.SetIndex(0);
            packet.SetSkillID((uint)args.skillID );
            packet.SetSActor(pc.id);
            packet.SetDActor(pc.id );
            packet.SetValue(args.damage);
            eh.C.netIO.SendPacket(packet, eh.C.SessionID);
            eh.C.SendCharStatus(0);
            
        }
    }
}
