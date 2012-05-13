using System;
using System.Collections.Generic;
using System.Text;

using SagaMap.Manager;
using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Tasks
{
    public class SpawnOnce : SingleRunTask
    {
        private Mob mob;

        public SpawnOnce(Mob mob)
        {
            this.mob = mob;
            this.dueTime = mob.Delay*1000;
        }

        public override void CallBack(object o)
        {
            ClientManager.EnterCriticalArea();

            try
            {
                //mob.Map.DeleteActor(mob.Actor);
                mob.Actor.e.OnReSpawn();
                if (mob.ai != null)
                {
                    if (mob.Map.GetRegionPlayerCount(mob.Actor.region) != 0)// the ai is only need to be switched on if there is player on that region
                        mob.ai.Start();
                }
                mob.timeSignature.actorID = 0;

                //readd the loot
                mob.Actor.NPCinv = new List<SagaDB.Items.Item>();
                if (MapServer.ScriptManager.templates.ContainsKey(mob.Type))
                {
                    SpawnTemplate st = MapServer.ScriptManager.templates[mob.Type];
                    foreach (Dictionary<string, string> loot in st.lootItems)
                    {
                        int amount = 1;
                        if (loot.ContainsKey("amount")) amount = int.Parse(loot["amount"]);
                        int itemid = int.Parse(loot["id"]);
                        float perc = (float)Config.Instance.DropRate / 100;
                        for (int i = 0; i < amount; ++i)
                            if (Global.Random.Next(0, 10000) <= int.Parse(loot["rate"]) * perc) mob.AddLoot(itemid);
                    }

                }
                // >> set a random position
                float[] pos = ScriptManager.GetRandomPos(mob.Map, mob.StartX, mob.StartY, mob.range);
                mob.Actor.x = pos[0];
                mob.Actor.y = pos[1];
                mob.Actor.z = pos[2];
                mob.Map.RegisterActor(mob.Actor);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
