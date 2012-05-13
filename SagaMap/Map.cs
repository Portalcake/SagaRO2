using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;


using SagaDB.Actors;
using SagaDB.Items;
using SagaLib;
using SagaMap.Manager;
using SagaMap.Tasks.SystemTasks;
using SagaMap.Tasks.MobTasks.AICommands;
using SagaMap.Scripting;

namespace SagaMap
{
    public class Map
    {
        private string name;
        private int id;

        public int ID { get { return this.id; } }
        public string Name { get { return this.name; } }

        private Dictionary<uint, Actor> actorsByID;
        private Dictionary<uint, List<Actor>> actorsByRegion;
        private Dictionary<string, ActorPC> pcByName;

        private const uint ID_BORDER = 0x80000000;
        private const uint ID_BORDER2 = 0x40000000;

        private uint nextPcId;
        private uint nextNpcId;
        private uint nextItemId;

        public int CattleyaMapID;
        public float CattleyaX;
        public float CattleyaY;
        public float CattleyaZ;

        public enum MOVE_TYPE { START, STOP };
        public enum EVENT_TYPE { APPEAR, DISAPPEAR, CHANGE_STATE, CHAT, SKILL, CHANGE_EQUIP, CHANGE_STATUS, ACTOR_SELECTION, YAW_UPDATE };
        public enum TOALL_EVENT_TYPE { CHAT, UPDATE_TIME_AND_WEATHER };

        private Global.WEATHER_TYPE weather;
        private GameTime gTime;
        private Tasks.SystemTasks.DynamicWeather wTask;

        private HeightMap hMap;

        public HeightMap HeightMap { get { return this.hMap; } }

        public Map(MapInfo info)
        {
            this.id = info.id;
            this.name = info.name;
            this.CattleyaMapID = info.CattleyaMapID;
            this.CattleyaX = info.CattleyaX;
            this.CattleyaY = info.CattleyaY;
            this.CattleyaZ = info.CattleyaZ;

            this.actorsByID = new Dictionary<uint, Actor>();
            this.actorsByRegion = new Dictionary<uint, List<Actor>>();
            this.pcByName = new Dictionary<string, ActorPC>();
            this.nextPcId = 1;
            this.nextNpcId = ID_BORDER + 1;
            this.nextItemId = ID_BORDER2 + 1;


            this.gTime = new GameTime();
            this.gTime.UpdateTime(1, 12, 0);

            this.weather = Global.WEATHER_TYPE.SUNNY;
            this.wTask = new SagaMap.Tasks.SystemTasks.DynamicWeather(this);
            this.wTask.Activate();

            if (info.heightmaps.Count > 0)
                this.hMap = new HeightMap(info.heightmaps[0]);
            else
                this.hMap = null;
        }


        public float[] GetRandomPos()
        {
            if (this.hMap == null)
            {
                float[] ret = new float[3];

                ret[0] = Global.Random.Next(-10000, +10000);
                ret[1] = Global.Random.Next(-10000, +10000);
                ret[2] = 3000;

                    return ret;
            }
            else return this.hMap.GetRandomPos();
        }

        public float GetHeight(float x, float y)
        {
            if (this.HasHeightMap())
            {
                float z = 0.0f;
                if (this.hMap.GetZ(x, y, out z)) return z;
                else return 3000.0f;                
            }
            else return 3000.0f;
        }

        public bool HasHeightMap()
        {
            if (this.hMap == null)
                return false;
            else
                return true;
        }


        public void UpdateWeather(Global.WEATHER_TYPE weather)
        {
            this.weather = weather;

            this.SendEventToAllActors(TOALL_EVENT_TYPE.UPDATE_TIME_AND_WEATHER, null, null, true);
        }

        public void UpdateTime(byte day, byte hour, byte min)
        {
            this.gTime.UpdateTime(day, hour, min);
            this.SendEventToAllActors(TOALL_EVENT_TYPE.UPDATE_TIME_AND_WEATHER, null, null, true);
        }

        public void UpdateTimeAndWeather(byte day, byte hour, byte min, Global.WEATHER_TYPE weather)
        {
            this.gTime.UpdateTime(day, hour, min);
            this.weather = weather;

            this.SendEventToAllActors(TOALL_EVENT_TYPE.UPDATE_TIME_AND_WEATHER, null, null, true);
        }


        public Actor GetActor(uint id)
        {
            try
            {
                return actorsByID[id];
            }
            catch(Exception)
            {
                return null;
            }
        }

        public ActorPC GetPC(string name)
        {
            try
            {
                return pcByName[name];
            }
            catch (Exception) 
            {
                return null;
            }
        }

        private uint GetNewActorID(ActorType type)
        {
            // get an unused actorID
            uint newID = 0;
            uint startID = 0;

            if (type == ActorType.PC) {
                newID = this.nextPcId;
                startID = this.nextPcId;
            }
            else {
                if (type == ActorType.NPC)
                {
                    newID = this.nextNpcId;
                    startID = this.nextNpcId;
                }
                else
                {
                    newID = this.nextItemId;
                    startID = this.nextItemId;                
                }
            }

            while (this.actorsByID.ContainsKey(newID))
            {
                newID++;

                if(newID >= ID_BORDER2 && type == ActorType.PC)
                    newID = 1;

                if(newID >= UInt32.MaxValue)
                    newID = ID_BORDER + 1;

                if (newID == startID) return 0;
            }

            if (type == ActorType.PC)
                this.nextPcId = newID + 1;
            else
                if(type==ActorType .NPC)
                    this.nextNpcId = newID + 1;
                else
                    this.nextItemId = newID + 1;
                

            return newID;
        }

        public bool RegisterActor(Actor nActor)
        {
            // default: no success
            bool succes = false;
            
            // set the actorID and the actor's region on this map
            uint newID = this.GetNewActorID(nActor.type);

            if (newID != 0)
            {
                nActor.id = newID;
                nActor.region = this.GetRegion(nActor.x, nActor.y, nActor.z);

                // make the actor invisible (when the actor is ready: set it to false & call OnActorVisibilityChange)
                nActor.invisble = true;

                // add the new actor to the tables
                this.actorsByID.Add(nActor.id, nActor);

                if (nActor.type == ActorType.PC && !this.pcByName.ContainsKey(nActor.name))
                    this.pcByName.Add(nActor.name, (ActorPC)nActor);

                if (!this.actorsByRegion.ContainsKey(nActor.region))
                    this.actorsByRegion.Add(nActor.region, new List<Actor>());

                this.actorsByRegion[nActor.region].Add(nActor);

                succes = true;
            }

            nActor.e.OnCreate(succes);
            return succes;
        }

        public bool RegisterActor(Actor nActor,uint SessionID)
        {
            // default: no success
            bool succes = false;

            // set the actorID and the actor's region on this map
            uint newID = SessionID;

            if (newID != 0)
            {
                nActor.id = newID;
                nActor.region = this.GetRegion(nActor.x, nActor.y, nActor.z);
                if (GetRegionPlayerCount(nActor.region) == 0 && nActor.type == ActorType.PC)
                {
                    MobAIToggle(nActor.region, true);
                }
                
                // make the actor invisible (when the actor is ready: set it to false & call OnActorVisibilityChange)
                nActor.invisble = true;

                // add the new actor to the tables
                if (!this.actorsByID.ContainsKey(nActor.id)) this.actorsByID.Add(nActor.id, nActor);

                if (nActor.type == ActorType.PC && !this.pcByName.ContainsKey(nActor.name))
                    this.pcByName.Add(nActor.name, (ActorPC)nActor);

                if (!this.actorsByRegion.ContainsKey(nActor.region))
                    this.actorsByRegion.Add(nActor.region, new List<Actor>());

                this.actorsByRegion[nActor.region].Add(nActor);

                succes = true;
            }

            nActor.e.OnCreate(succes);
            return succes;
        }

        public void OnActorVisibilityChange(Actor dActor)
        {
            if (dActor.invisble)
                this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.DISAPPEAR, null, dActor, false);
 
            else
                this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.APPEAR, null, dActor, false);
        }

        public void SendTimeWeatherToActor(Actor sActor)
        {
            // send time&weather to the actor
            sActor.e.OnTimeWeatherChange(this.gTime.GetTime(), this.weather);
        }

        public void DeleteActor(Actor dActor)
        {
            this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.DISAPPEAR, null, dActor, false);

            if (dActor.type == ActorType.PC && this.pcByName.ContainsKey(dActor.name))
                this.pcByName.Remove(dActor.name);

            this.actorsByID.Remove(dActor.id);

            if (this.actorsByRegion.ContainsKey(dActor.region))
            {
                this.actorsByRegion[dActor.region].Remove(dActor);
                if (GetRegionPlayerCount(dActor.region) == 0 && dActor.type == ActorType.PC)
                {
                    MobAIToggle(dActor.region, false);
                }
            }
                
            dActor.e.OnDelete();
        }

        // make sure only 1 thread at a time is executing this method
        public void MoveActor(MOVE_TYPE mType, Actor mActor, float[] pos, int yaw, float[] accel, uint delayTime, ushort speed)
        {
            try
            {
                // check wheter the destination is in range, if not kick the client
                if (!this.MoveStepIsInRange(mActor, pos))
                {
                    Logger.ShowError("MoveStep is not in range", null);
                    mActor.e.OnKick();
                    return;
                }

                if (mActor.type == ActorType.NPC)
                {
                    ActorNPC npc = (ActorNPC)mActor;
                    if (npc.npcType >= 50000)
                    {
                        foreach (Actor actor in this.actorsByID.Values)
                        {
                            if (actor.type != ActorType.PC) continue;
                            actor.e.OnActorStartsMoving((ActorNPC)mActor, 0, null, speed);
                        }
                        return;
                    }
                }

                //scroll through all actors that "could" see the mActor at "from"
                //or are going "to see" mActor, or are still seeing mActor
                for (short deltaY = -1; deltaY <= 1; deltaY++)
                {
                    for (short deltaX = -1; deltaX <= 1; deltaX++)
                    {
                        uint region = (uint)(mActor.region + (deltaX * 1000000) + deltaY);
                        if (!this.actorsByRegion.ContainsKey(region)) continue;

                        foreach (Actor actor in this.actorsByRegion[region])
                        {
                            //if (actor.id == mActor.id) continue;

                            // A) INFORM OTHER ACTORS

                            //actor "could" see mActor at its "from" position
                            if (this.ACanSeeB(actor, mActor))
                            {
                                //actor will still be able to see mActor
                                if (this.ACanSeeB(actor, mActor, pos[0], pos[1]))
                                {
                                    if (mType == MOVE_TYPE.START)
                                        if (mActor.type == ActorType.PC)
                                        {
                                            actor.e.OnActorStartsMoving(mActor, pos, accel, yaw, speed, delayTime);
                                        }
                                        else
                                        {
                                            mActor.yaw = yaw;
                                            actor.e.OnActorStartsMoving((ActorNPC)mActor, (byte)(accel.Length / 3), accel, speed);
                                        }
                                    else
                                        actor.e.OnActorStopsMoving(mActor, pos, yaw, speed, delayTime);
                                }
                                //actor won't be able to see mActor anymore
                                else actor.e.OnActorDisappears(mActor);
                            }
                            //actor "could not" see mActor, but will be able to see him now
                            else if (this.ACanSeeB(actor, mActor, pos[0], pos[1]))
                            {
                                actor.e.OnActorAppears(mActor);
                                actor.e.OnActorChangesState(mActor, null);

                                //send move / move stop
                                if (mType == MOVE_TYPE.START)
                                    actor.e.OnActorStartsMoving(mActor, pos, accel, yaw, speed, delayTime);
                                else
                                    actor.e.OnActorStopsMoving(mActor, pos, yaw, speed, delayTime);
                            }

                            // B) INFORM mActor
                            //mActor "could" see actor on its "from" position
                            if (this.ACanSeeB(mActor, actor))
                            {
                                //mActor won't be able to see actor anymore
                                if (!this.ACanSeeB(mActor, pos[0], pos[1], actor))
                                    mActor.e.OnActorDisappears(actor);
                                //mAactor will still be able to see actor
                                else { }
                            }

                            else if (this.ACanSeeB(mActor, pos[0], pos[1], actor))
                            {
                                //mActor "could not" see actor, but will be able to see him now
                                //send pcinfo
                                mActor.e.OnActorAppears(actor);
                                //send state
                                mActor.e.OnActorChangesState(actor, null);
                            }
                        }
                    }
                }

                //update x/y/z/yaw of the actor
                mActor.x = pos[0];
                mActor.y = pos[1];
                if (mActor.type == ActorType.NPC) mActor.z = this.GetHeight(pos[0], pos[1]);
                else mActor.z = pos[2];
                mActor.yaw = yaw;


                //update the region of the actor
                uint newRegion = this.GetRegion(pos[0], pos[1], pos[2]);
                if (mActor.region != newRegion)
                {
                    this.actorsByRegion[mActor.region].Remove(mActor);
                    //turn off all the ai if the old region has no player on it
                    if (GetRegionPlayerCount(mActor.region) == 0 && mActor.type == ActorType.PC)
                    {
                        MobAIToggle(mActor.region, false);
                    }
                    mActor.region = newRegion;
                    if (GetRegionPlayerCount(mActor.region) == 0 && mActor.type == ActorType.PC)
                    {
                        MobAIToggle(mActor.region, true);
                    }

                    if (!this.actorsByRegion.ContainsKey(newRegion))
                        this.actorsByRegion.Add(newRegion, new List<Actor>());

                    this.actorsByRegion[newRegion].Add(mActor);
                }
            }
            catch(Exception)
            { }
        }

        public int GetRegionPlayerCount(uint region)
        {
            List<Actor> actors;
            int count = 0;
            if (!this.actorsByRegion.ContainsKey(region)) return 0;
            actors = this.actorsByRegion[region];
            List<int> removelist = new List<int>();
            for (int i = 0; i < actors.Count; i++)
            {
                Actor actor;
                if (actors[i] == null)
                {
                    removelist.Add(i);
                    continue;
                }
                actor = actors[i];
                if (actor.type == ActorType.PC) count++;
            }
            foreach (int i in removelist)
            {
                actors.RemoveAt(i);
            }
            return count;
        }

        public void MobAIToggle(uint region, bool toggle)
        {
            List<Actor> actors;
            if (!this.actorsByRegion.ContainsKey(region)) return;
            actors = this.actorsByRegion[region];
            foreach (Actor actor in actors)
            {
                if (actor.type == ActorType.NPC)
                {
                    ActorNPC npc = (ActorNPC)actor;
                    if (npc.npcType >= 10000 && npc.npcType <50000)
                    {
                        Mob mob = (Mob)npc.e;
                        if (mob.ai == null) continue;
                        switch (toggle)
                        {
                            case true:
                                mob.ai.Start();
                                break;
                            case false:
                                Tasks.MobTasks.AICommands.Attack att = null;
                                if (mob.ai.commands.ContainsKey("Attack"))
                                    att = (Tasks.MobTasks.AICommands.Attack)mob.ai.commands["Attack"];
                                if (mob.ai.commands.ContainsKey("Chase")) continue;
                                if (att != null)
                                {
                                    if (att.active == true) continue;
                                }
                                mob.ai.Pause();
                                break;
                        }
                    }
                }
            }
            
        }

        public bool MoveStepIsInRange(Actor mActor, float[] to)
        {
            /* Disabled, until we have something better
            if (System.Math.Abs(mActor.x - to[0]) > mActor.maxMoveRange) return false;
            if (System.Math.Abs(mActor.y - to[1]) > mActor.maxMoveRange) return false;
            //we don't check for z , yet, to allow falling from great hight
            //if (System.Math.Abs(mActor.z - to[2]) > mActor.maxMoveRange) return false;
            */
             return true;
        }


        public uint GetRegion(float x, float y, float z)
        {

            uint REGION_DIAMETER = Global.MAX_SIGHT_RANGE;

            // best case we should now load the size of the map from a config file, however that's not
            // possible yet, so we just create a region code off the values x/y

            /*
            values off x/y are like:
            x = -20 500.0f
            y =   1 000.0f
            
            before we convert them to uints we make them positive, and store the info wheter they were negative
            x  = - 25 000.0f;
            nx = 1;
            y  =  1 000.0f;
            ny = 0;
            
            no we convert them to uints
             
            ux = 25 000;
            nx = 1;
            uy =  1 000;
            ny = 0;
            
            now we do ux = (uint) ( ux / REGION_DIAMETER ) [the same for uy]
            we have:
             
            ux = 2;
            nx = 1;
            uy = 0;
            ny = 0;
             
            off this data we generate the region code:
             > we use a uint as region code
             > max value of an uint32 is 4 294 967 295
             > the syntax of the region code is ux[5digits].uy[5digits]
             if(!nx) ux = ux + 50000;
             else ux = 50000 - ux;
             if(!ny) uy = uy + 50000;
             else uy = 50000 - uy;
  
            uint regionCode = 49998 50001
            uint regionCode = 4999850001

            Note: 
             We inform an Actor(Player) about all other Actors in its own region and the 8 regions around
             this region. Because of this REGION_DIAMETER has to be MAX_SIGHT_RANGE (or greater).
             Also check SVN/SagaMap/doc/mapRegions.bmp
            */
            // init nx,ny
            bool nx = false;
            bool ny = false;
            // make x,y positive
            if (x < 0) { x = x - (2 * x); nx = true; }
            if (y < 0) { y = y - (2 * y); ny = true; }
            // convert x,y to uints
            uint ux = (uint)x;
            uint uy = (uint)y;
            // divide through REGION_DIAMETER
            ux = (uint)(ux / REGION_DIAMETER);
            uy = (uint)(uy / REGION_DIAMETER);
            // calc ux
            if (ux > 49999) ux = 49999;
            if (!nx) ux = ux + 50000;
            else ux = 50000 - ux;
            // calc uy
            if (uy > 49999) uy = 49999;
            if (!ny) uy = uy + 50000;
            else uy = 50000 - uy;
            // finally generate the region code and return it
            return (uint)((ux * 1000000) + uy);
        }

        public bool ACanSeeB(Actor A, Actor B)
        {
            if (B.invisble) return false;
            if (B.type == ActorType.NPC)
            {
                ActorNPC npc = (ActorNPC)B;
                if (npc.npcType >= 50000)
                    return true;
            }
            if (System.Math.Abs(A.x - B.x) > A.sightRange) return false;
            if (System.Math.Abs(A.y - B.y) > A.sightRange) return false;
            return true;
        }

        public bool ACanSeeB(Actor A, Actor B, float bx, float by)
        {
            if (B.invisble) return false;
            if (System.Math.Abs(A.x - bx) > A.sightRange) return false;
            if (System.Math.Abs(A.y - by) > A.sightRange) return false;
            return true;
        }

        public bool ACanSeeB(Actor A, float ax, float ay, Actor B)
        {
            if (B.invisble) return false;
            if (System.Math.Abs(ax - B.x) > A.sightRange) return false;
            if (System.Math.Abs(ay - B.y) > A.sightRange) return false;
            return true;
        }

        public bool ACanSeeB(Actor A, Actor B, float sightrange)
        {
            if (B.invisble) return false;
            if (System.Math.Abs(A.x - B.x) > sightrange) return false;
            if (System.Math.Abs(A.y - B.y) > sightrange) return false;
            return true;
        }

        public void SendVisibleActorsToActor(Actor jActor)
        {
            //search all actors which can be seen by jActor and tell jActor about them
            for (short deltaY = -1; deltaY <= 1; deltaY++)
            {
                for (short deltaX = -1; deltaX <= 1; deltaX++)
                {
                    uint region = (uint)(jActor.region + (deltaX * 1000000) + deltaY);
                    if (!this.actorsByRegion.ContainsKey(region)) continue;

                    foreach (Actor actor in this.actorsByRegion[region])
                    {
                        if (actor.id == jActor.id) continue;

                        //check wheter jActor can see actor, if yes: inform jActor
                        if (this.ACanSeeB(jActor, actor))
                        {
                            jActor.e.OnActorAppears(actor);
                            jActor.e.OnActorChangesState(actor, null);
                        }
                            
                    }
                }
            }
            foreach (Actor i in this.actorsByID.Values)
            {
                if (i.type == ActorType.NPC)
                {
                    ActorNPC npc = (ActorNPC)i;
                    if (npc.npcType >= 50000)
                    {
                        jActor.e.OnActorAppears(i);
                        jActor.e.OnActorChangesState(i, null);
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void TeleportActor(Actor sActor, float x, float y, float z)
        {
            this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.DISAPPEAR, null, sActor, false);

            this.actorsByRegion[sActor.region].Remove(sActor);
            if (GetRegionPlayerCount(sActor.region) == 0 && sActor.type == ActorType.PC)
            {
                MobAIToggle(sActor.region, false);
            }
                
            sActor.x = x;
            sActor.y = y;
            sActor.z = z;
            sActor.region = this.GetRegion(x, y, z);
            if (GetRegionPlayerCount(sActor.region) == 0 && sActor.type == ActorType.PC)
            {
                MobAIToggle(sActor.region, true);
            }
                
            if (!this.actorsByRegion.ContainsKey(sActor.region)) this.actorsByRegion.Add(sActor.region, new List<Actor>());
            this.actorsByRegion[sActor.region].Add(sActor);

            sActor.e.OnTeleport(x, y, z);

            this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.APPEAR, null, sActor, false);
            this.SendVisibleActorsToActor(sActor);
        }

        public void SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE etype, MapEventArgs args, Actor sActor, bool sendToSourceActor)
        {
            try
            {
                for (short deltaY = -1; deltaY <= 1; deltaY++)
                {
                    for (short deltaX = -1; deltaX <= 1; deltaX++)
                    {
                        uint region = (uint)(sActor.region + (deltaX * 1000000) + deltaY);
                        if (!this.actorsByRegion.ContainsKey(region)) continue;

                        foreach (Actor actor in this.actorsByRegion[region])
                        {
                            try
                            {
                                if (!sendToSourceActor && (actor.id == sActor.id)) continue;

                                if (this.ACanSeeB(actor, sActor))
                                {
                                    switch (etype)
                                    {
                                        case EVENT_TYPE.APPEAR:
                                            actor.e.OnActorAppears(sActor);
                                            actor.e.OnActorChangesState(sActor, null);
                                            break;

                                        case EVENT_TYPE.DISAPPEAR:
                                            actor.e.OnActorDisappears(sActor);
                                            break;

                                        case EVENT_TYPE.CHANGE_STATE:
                                            actor.e.OnActorChangesState(sActor, args);
                                            break;

                                        case EVENT_TYPE.CHAT:
                                            actor.e.OnActorChat(sActor, args);
                                            break;

                                        case EVENT_TYPE.SKILL:
                                            actor.e.OnActorSkillUse(sActor, args);
                                            break;

                                        case EVENT_TYPE.CHANGE_EQUIP:
                                            actor.e.OnActorChangeEquip(sActor, args);
                                            break;

                                        case EVENT_TYPE.CHANGE_STATUS:
                                            actor.e.OnChangeStatus(sActor, args);
                                            break;
                                        case EVENT_TYPE.ACTOR_SELECTION:
                                            Map.ActorSelArgs arg = (Map.ActorSelArgs)args;
                                            Actor target = this.GetActor(arg.target);
                                            if (target != null)
                                            {
                                                if (actor == sActor)//broadcast disabled temporaryly, which crashes the client
                                                    if (this.ACanSeeB(actor, target))
                                                        actor.e.OnActorSelection((ActorPC)sActor, args);
                                            }
                                            break;
                                        case EVENT_TYPE.YAW_UPDATE :
                                            if (actor.type == ActorType.PC)
                                            {
                                                ActorPC pc = (ActorPC)actor;
                                                ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
                                                Packets.Server.UpdateActorYaw p1 = new SagaMap.Packets.Server.UpdateActorYaw();
                                                p1.SetActor(sActor.id);
                                                p1.SetYaw(sActor.yaw);
                                                eh.C.netIO.SendPacket(p1, eh.C.SessionID);
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowError(ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void SendEventToAllActors(TOALL_EVENT_TYPE etype, MapEventArgs args, Actor sActor, bool sendToSourceActor)
        {
            foreach (Actor actor in this.actorsByID.Values)
            {
                if(sActor != null) if (!sendToSourceActor && (actor.id == sActor.id)) continue;

                switch (etype)
                {
                    case TOALL_EVENT_TYPE.CHAT:
                        actor.e.OnActorChat(sActor, args);
                        break;

                    case TOALL_EVENT_TYPE.UPDATE_TIME_AND_WEATHER:
                        actor.e.OnTimeWeatherChange(this.gTime.GetTime(), this.weather);
                        break;

                    default:
                        break;
                }
            }
        }

        public void SendActorToMap(Actor mActor, Map newMap, float x, float y, float z)
        {
            // todo: add support for multiple map servers

            // obtain the new map
            byte mapid = (byte)newMap.id;
            if (mapid == mActor.mapID)
            {
                TeleportActor(mActor, x, y, z);
                return;
            }
            
            // delete the actor from this map
            this.DeleteActor(mActor);

            // update the actor
            mActor.mapID = mapid;
            mActor.x = x;
            mActor.y = y;
            mActor.z = z;
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)mActor.e;
            eh.C.map = newMap;

            // register the actor in the new map
            if (mActor.type != ActorType.PC)
            {
                newMap.RegisterActor(mActor);
            }
            else
            {
                newMap.RegisterActor(mActor, mActor.id);
            }
        }

        public void SendActorToMap(Actor mActor, byte mapid, float x, float y, float z)
        {
            // todo: add support for multiple map servers

            // obtain the new map
            Map newMap;
            if (mapid == mActor.mapID)
            {
                TeleportActor(mActor, x, y, z);
                return;
            }
            if (!MapManager.Instance.GetMap(mapid, out newMap))
                return;
            // delete the actor from this map
            this.DeleteActor(mActor);
            if (x == 0f && y == 0f && z == 0f)
            {
                float[] pos = newMap.GetRandomPos();
                x = pos[0];
                y = pos[1];
                z = pos[2];
            }
            // update the actor
            mActor.mapID = mapid;
            mActor.x = x;
            mActor.y = y;
            mActor.z = z;
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)mActor.e;
            eh.C.map = newMap;
           
            // register the actor in the new map
            if (mActor.type != ActorType.PC)
            {
                newMap.RegisterActor(mActor);
            }
            else
            {
                newMap.RegisterActor(mActor,mActor.id);
            }
        }

        private void SendActorToActor(Actor mActor, Actor tActor)
        {
            if (mActor.mapID == tActor.mapID)
                this.TeleportActor(mActor, tActor.x, tActor.y, tActor.z);
            else
                this.SendActorToMap(mActor, tActor.mapID, tActor.x, tActor.y, tActor.z);
        }

        public void AddItemToActor(Actor destinationActor, Item nItem, ITEM_UPDATE_REASON reason)
        {
            destinationActor.e.OnAddItem(nItem, reason);
            
        }

        public void RemoveItemFromActorPC(ActorPC pc, byte index, int itemID, byte amount, ITEM_UPDATE_REASON reason)
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            DeleteItemResult res;
            byte namount;
            Item item = pc.inv.GetItem(CONTAINER_TYPE.INVENTORY, index);
            res = pc.inv.DeleteItem(CONTAINER_TYPE.INVENTORY, index, itemID, amount,out namount);
           
            if (res == DeleteItemResult.ERROR || res == DeleteItemResult.WRONG_ITEMID) return;
            switch (res)
            {
                case DeleteItemResult.NOT_ALL_DELETED:
                    Packets.Server.UpdateItem p2 = new SagaMap.Packets.Server.UpdateItem();
                    p2.SetContainer(CONTAINER_TYPE.INVENTORY);
                    p2.SetItemIndex(index);
                    p2.SetAmount(namount);
                    p2.SetUpdateType(SagaMap.Packets.Server.ITEM_UPDATE_TYPE.AMOUNT);
                    p2.SetUpdateReason(reason);
                    eh.C.netIO.SendPacket(p2, eh.C.SessionID);
                    MapServer.charDB.UpdateItem(pc, item);
                    break;
                case DeleteItemResult.ALL_DELETED:
                    Packets.Server.DeleteItem delI = new SagaMap.Packets.Server.DeleteItem();
                    delI.SetContainer(CONTAINER_TYPE.INVENTORY);
                    delI.SetAmount(1);
                    delI.SetIndex(index);
                    eh.C.netIO.SendPacket(delI, eh.C.SessionID);
                    MapServer.charDB.DeleteItem(pc, item);
                    break;
            }
        }

        public void RemoveItemFromActorPC(ActorPC pc, int itemID, byte amount,ITEM_UPDATE_REASON reason)
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            DeleteItemResult res;
            byte namount, index;
            Item item = pc.inv.GetItem(CONTAINER_TYPE.INVENTORY, itemID);
            res = pc.inv.DeleteItem(CONTAINER_TYPE.INVENTORY, itemID, amount, out index, out namount);
            if (res == DeleteItemResult.ERROR || res == DeleteItemResult.WRONG_ITEMID) return;
            switch (res)
            {
                case DeleteItemResult.NOT_ALL_DELETED:
                    Packets.Server.UpdateItem p2 = new SagaMap.Packets.Server.UpdateItem();
                    p2.SetContainer(CONTAINER_TYPE.INVENTORY);
                    p2.SetItemIndex(index);
                    p2.SetAmount(namount);
                    p2.SetUpdateType(SagaMap.Packets.Server.ITEM_UPDATE_TYPE.AMOUNT);
                    p2.SetUpdateReason(reason);
                    eh.C.netIO.SendPacket(p2, eh.C.SessionID);
                    MapServer.charDB.UpdateItem(pc, item);                    
                    break;
                case DeleteItemResult.ALL_DELETED:
                    Packets.Server.DeleteItem delI = new SagaMap.Packets.Server.DeleteItem();
                    delI.SetContainer(CONTAINER_TYPE.INVENTORY);
                    delI.SetAmount(1);
                    delI.SetIndex(index);
                    eh.C.netIO.SendPacket(delI, eh.C.SessionID);
                    MapServer.charDB.DeleteItem(pc, item);                    
                    break;
            }
        }

        public List<Actor> GetActorsArea(Actor sActor, float range, bool includeSourceActor)
        {
            List<Actor> actors = new List<Actor>();
            for (short deltaY = -1; deltaY <= 1; deltaY++)
            {
                for (short deltaX = -1; deltaX <= 1; deltaX++)
                {
                    uint region = (uint)(this.GetRegion(sActor.x, sActor.y, sActor.z) + (deltaX * 1000000) + deltaY);
                    if (!this.actorsByRegion.ContainsKey(region)) continue;

                    foreach (Actor actor in this.actorsByRegion[region])
                    {
                        if (!includeSourceActor && (actor.id == sActor.id)) continue;

                        if (this.ACanSeeB(actor, sActor, range))
                        {
                            actors.Add(actor);
                        }
                    }
                }
            }
            return actors;
        }


        public struct ChatArgs : MapEventArgs
        {
            public Packets.Server.SendChat.MESSAGE_TYPE mType;
            public string text;


            public ChatArgs(Packets.Server.SendChat.MESSAGE_TYPE mType, string text)
            {
                this.mType = mType;
                this.text = text;
            }
        }

        public struct ActorSelArgs : MapEventArgs
        {
            public uint target;
            public ActorSelArgs(uint target)
            {
                this.target = target;
            }
        }

        public struct SkillArgs : MapEventArgs
        {
            public enum AttackResult { Normal, Critical, Miss, Block, Heal = 6, Nodamage }

            public byte skillType;
            public AttackResult isCritical;
            public Skills.SkillIDs skillID;
            public uint targetActorID;
            public uint damage;
            public bool casting;
            public bool castcancel;
            public bool failed;

            public SkillArgs(byte skillType, AttackResult isCritical, uint skillID, uint targetActorID, uint damage)
            {
                this.skillType = skillType;
                this.isCritical = isCritical;
                this.skillID = (Skills.SkillIDs)skillID;
                this.targetActorID = targetActorID;
                this.damage = damage;
                this.casting = false;
                this.castcancel = false;
                this.failed = false;
            }
        }

        public struct StatusArgs : MapEventArgs
        {
            public enum EventType { Add, Remove };
            public EventType type;
            public struct StatusInfo
            {
                public uint SkillID;
                public uint time;
                public StatusInfo(uint SID, uint time)
                {
                    this.SkillID = SID;
                    this.time = time;
                }
            }
            public List<StatusInfo> StatusList;
            public StatusArgs(EventType type, List<StatusInfo> SList)
            {
                this.type = type;
                this.StatusList = SList;
            }
        }

        public struct ChangeEquipArgs : MapEventArgs
        {
            public SagaDB.Items.EQUIP_SLOT eSlot;
            public int itemID;

            public ChangeEquipArgs(SagaDB.Items.EQUIP_SLOT eSlot, int itemID)
            {
                this.eSlot = eSlot;
                this.itemID = itemID;
            }
        }
    }
}
