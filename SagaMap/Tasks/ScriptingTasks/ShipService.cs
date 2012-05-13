using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actors;
using SagaMap.Manager;

namespace SagaMap.Tasks
{
    public class ShipService : MultiRunTask
    {
        private Map orimap;
        private float[] dstpos;
        private Map dstmap;
        public ushort speed = 600;
        bool init = false;

        private Map ShipMap;
        private int step;
        private int substep;
        private Actor shipActor;
        private Ship mship;

        private List<Actor> oriPassenger;

        private List<float[]> waypointsApp = new List<float[]>();
        private List<int> yawsApp = new List<int>();
        private List<float[]> waypointsDep = new List<float[]>();
        private List<int> yawsDep = new List<int>();

        public List<float[]> CurrentWaypoints
        {
            get
            {
                List<float[]> tmp = new List<float[]>();
                switch (step)
                {
                    case 0:
                        switch (substep)
                        {
                            case 0:
                                tmp.Add(new float[3] { shipActor.x, shipActor.y, shipActor.z });
                                tmp.Add(waypointsApp[1]);
                                tmp.Add(waypointsApp[2]);
                                break;
                            case 1:
                                tmp.Add(new float[3] { shipActor.x, shipActor.y, shipActor.z });
                                tmp.Add(waypointsApp[2]);
                                break;
                            default:
                                tmp.Add(new float[3] { shipActor.x, shipActor.y, shipActor.z });
                                tmp.Add(new float[3] { shipActor.x, shipActor.y, shipActor.z });
                                break;
                        }
                        break;
                    case 2:
                        switch (substep)
                        {
                            case 0:
                                tmp.Add(new float[3] { shipActor.x, shipActor.y, shipActor.z });
                                tmp.Add(waypointsDep[1]);
                                tmp.Add(waypointsDep[2]);
                                break;
                            case 1:
                                tmp.Add(new float[3] { shipActor.x, shipActor.y, shipActor.z });
                                tmp.Add(waypointsDep[2]);
                                break;
                            default:
                                tmp.Add(new float[3] { shipActor.x, shipActor.y, shipActor.z });
                                tmp.Add(waypointsDep[2]);
                                break;
                        }
                        break;
                    default:
                        tmp.Add(new float[3] { shipActor.x, shipActor.y, shipActor.z });
                        tmp.Add(waypointsDep[2]);                                
                        break;
                };
                return tmp;
            }
        }

        public List<int> CurrentYaws
        {
            get
            {
                List<int> tmp = new List<int>();
                switch (step)
                {
                    case 0:
                        switch (substep)
                        {
                            case 0:
                                tmp.Add(shipActor.yaw);
                                tmp.Add(yawsApp[1]);
                                tmp.Add(yawsApp[2]);
                                break;
                            case 1:
                                tmp.Add(shipActor.yaw);
                                tmp.Add(yawsApp[2]);
                                break;
                            default:
                                tmp.Add(shipActor.yaw);
                                tmp.Add(yawsApp[2]);
                                break;
                        }
                        break;
                    case 2:
                        switch (substep)
                        {
                            case 0:
                                tmp.Add(shipActor.yaw);
                                tmp.Add(yawsDep[1]);
                                tmp.Add(yawsDep[2]);
                                break;
                            case 1:
                                tmp.Add(shipActor.yaw);
                                tmp.Add(yawsDep[2]);
                                break;
                            default:
                                tmp.Add(shipActor.yaw);
                                tmp.Add(yawsDep[2]);
                                break;
                        }
                        break;
                    default:
                        tmp.Add(shipActor.yaw);
                        tmp.Add(yawsDep[2]);
                        break;
                };
                return tmp;
            }
        }


        public void AddApprochWaypoint(float x, float y, float z, int yaw)
        {
            if (waypointsApp.Count == 3)
            {
                Logger.ShowDebug("Approching Waypoints cannot be more than 3", null);
            }
            else
            {
                float[] tmp = new float[3] { x, y, z };
                waypointsApp.Add(tmp);
                yawsApp.Add(yaw);
            }
        }

        public void AddDepartureWaypoint(float x, float y, float z, int yaw)
        {
            if (waypointsDep.Count == 3)
            {
                Logger.ShowDebug("Departure Waypoints cannot be more than 3", null);
            }
            else
            {
                float[] tmp = new float[3] { x, y, z };
                waypointsDep.Add(tmp);
                yawsDep.Add(yaw);
            }
        }

        //10|2468.964|446.603|611.224
        public ShipService(byte orimapid, float[]dstpos, byte dstmapid, Actor ship)
        {
            this.dueTime = 1000;
            this.period = 1000;
            this.step = 0;
            this.substep = 0;
            MapManager.Instance.GetMap(orimapid, out orimap);
            MapManager.Instance.GetMap(dstmapid, out dstmap);
            this.dstpos = dstpos;
            this.shipActor = ship;
            this.mship = (Ship)ship.e;            
            MapInfo info = new MapInfo();
            info.id = 10;
            info.name = "ship zone";
            info.heightmaps = new List<HeightMapInfo>();
            ShipMap = new Map(info);
            oriPassenger = new List<Actor>();
            
        }

        public override void CallBack(object o)
        {
            ClientManager.EnterCriticalArea();

            if (waypointsApp.Count == 0)
            {
                ClientManager.LeaveCriticalArea();
                return;
            }
            try
            {
                float[] src;
                float[] dst;
                        
                switch (step)
                {

                    #region Approching
                    case 0:
                        if (substep == 0 && init == false)
                        {
                            this.shipActor.x = waypointsApp[0][0];
                            this.shipActor.y = waypointsApp[0][1];
                            this.shipActor.z = waypointsApp[0][2];
                            this.shipActor.yaw = yawsApp[0];                            
                            this.orimap.MoveActor(Map.MOVE_TYPE.START, this.shipActor, null, 0, null, 0, this.speed);
                            init = true;
                        }
                        src = new float[3] { this.shipActor.x, this.shipActor.y, this.shipActor.z };
                        dst = this.waypointsApp[substep + 1];
                        if (MobAI.GetDistance(src, dst) > 0)
                        {
                            float[] diff;
                            int yaw = MobAI.GetYawFromVector(MobAI.GetUnitVector(src, dst));
                            if (MobAI.GetDistance(src, dst) > this.speed) diff = MobAI.Add(src, MobAI.ScalarProduct(MobAI.GetUnitVector(src, dst), this.speed));
                            else diff = dst;
                            src = diff;
                            this.shipActor.x = src[0];
                            this.shipActor.y = src[1];
                            this.shipActor.z = src[2];
                        }
                        else
                        {
                            substep++;
                            init = false;
                            this.shipActor.x = waypointsApp[substep][0];
                            this.shipActor.y = waypointsApp[substep][1];
                            this.shipActor.z = waypointsApp[substep][2];                            
                            this.shipActor.yaw = yawsApp[substep];
                            if (substep != 2) this.orimap.MoveActor(Map.MOVE_TYPE.START, this.shipActor, null, 0, null, 0, this.speed);
                            if (substep == 2)
                            {
                                substep = 0;
                                step++;
                                foreach (Actor i in oriPassenger)
                                {
                                    try
                                    {
                                        ShipMap.SendActorToMap(i, dstmap, dstpos[0], dstpos[1], dstpos[2]);
                                    }
                                    catch (Exception e)
                                    {
                                        Logger.ShowError(e);
                                    }
                                }
                                oriPassenger.Clear();
                            }
                        }
                        break;
                    #endregion

                    #region Parking
                    case 1:
                        if (substep < 120)
                        {
                            substep++;
                        }
                        else
                        {
                            //Logger.ShowInfo("Departing");
                            oriPassenger = GetPC(orimap.GetActorsArea(shipActor, 1500f, false));
                            foreach (Actor i in oriPassenger)
                            {
                                orimap.SendActorToMap(i, ShipMap, 2468.964f, 446.603f, 611.224f);
                            }
                            this.shipActor.x = waypointsDep[0][0];
                            this.shipActor.y = waypointsDep[0][1];
                            this.shipActor.z = waypointsDep[0][2];
                            this.shipActor.yaw = yawsDep[0];
                            step++;
                            substep = 0;                         
                        }
                        break;
                    #endregion

                    #region Departure
                    case 2:
                        src = new float[3] { this.shipActor.x, this.shipActor.y, this.shipActor.z };
                        dst = this.waypointsDep[substep + 1];
                        if (substep == 0 && init == false)
                        {
                            this.orimap.MoveActor(Map.MOVE_TYPE.START, this.shipActor, null, 0, null, 0, this.speed);
                            init = true;
                        }
                        if (MobAI.GetDistance(src, dst) > 0)
                        {
                            float[] diff;
                            int yaw = MobAI.GetYawFromVector(MobAI.GetUnitVector(src, dst));
                            if (MobAI.GetDistance(src, dst) > this.speed) diff = MobAI.Add(src, MobAI.ScalarProduct(MobAI.GetUnitVector(src, dst), this.speed));
                            else diff = dst;
                            src = diff;
                            this.shipActor.x = src[0];
                            this.shipActor.y = src[1];
                            this.shipActor.z = src[2];
                        }
                        else
                        {
                            substep++;
                            init = false;
                            this.shipActor.x = waypointsDep[substep][0];
                            this.shipActor.y = waypointsDep[substep][1];
                            this.shipActor.z = waypointsDep[substep][2];
                            this.shipActor.yaw = yawsDep[substep];
                            if (substep != 2) this.orimap.MoveActor(Map.MOVE_TYPE.START, this.shipActor, null, 0, null, 0, this.speed);
                            init = false;
                            if (substep == 2)
                            {
                                substep = 0;
                                step=0;                                
                            }
                        }
                        break;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }

            ClientManager.LeaveCriticalArea();
        }

        private List<Actor> GetPC(List<Actor> actors)
        {
            List<Actor> tmp = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (i.type == ActorType.PC)
                    tmp.Add(i);
            }
            return tmp;
        }
    }
}
