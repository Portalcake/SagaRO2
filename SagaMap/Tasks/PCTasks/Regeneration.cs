using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;

namespace SagaMap.Tasks
{
    public class Regeneration : MultiRunTask
    {
        private MapClient client;
        public ushort hp;
        public ushort sp;
        public Regeneration(MapClient client,ushort hp,ushort sp,int period)
        {
            this.dueTime = 1000;
            this.period = period;
            this.client = client;
            this.hp = hp;
            this.sp = sp;
        }

        public override void CallBack(object o)
        {
            ClientManager.EnterCriticalArea();
            if (client.Char.HP == client.Char.maxHP && client.Char.SP == client.Char.maxSP)
            {
                ClientManager.LeaveCriticalArea();
                return;
            }
            if (client.Char.state == 1)
            {
                try
                {
                    if (this.sp != 0)
                        client.Char.SP = (ushort)(client.Char.SP + this.sp + client.Char.BattleStatus.spregbonus + client.Char.BattleStatus.spregskill);
                    if (client.Char.SP > client.Char.maxSP) client.Char.SP = client.Char.maxSP;
                    client.SendCharStatus(0);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                ClientManager.LeaveCriticalArea();
                return;
            }
            if (client.Char.stance == Global.STANCE.DIE)
            {
                ClientManager.LeaveCriticalArea();
                return;
            }
            try
            {
                if (this.hp != 0)
                    client.Char.HP = (ushort)(client.Char.HP + this.hp + client.Char.BattleStatus.hpregbonus + client.Char.BattleStatus.spregskill);
                if (this.sp != 0)
                    client.Char.SP = (ushort)(client.Char.SP + this.sp + client.Char.BattleStatus.spregbonus + client.Char.BattleStatus.hpregskill);
                if (client.Char.HP > client.Char.maxHP) client.Char.HP = client.Char.maxHP;
                if (client.Char.SP > client.Char.maxSP) client.Char.SP = client.Char.maxSP;
                client.SendCharStatus(0);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
