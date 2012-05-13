using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actors;
using SagaDB.Quest;

namespace SagaMap
{
    public class Ship :Npc 
    {
        Tasks.ShipService shipservice;
        public Ship()
        {
            
        }

        public List<float[]> CurrentWaypoints
        {
            get
            {
                return shipservice.CurrentWaypoints;
            }
        }

        public List<int> CurrentYaws
        {
            get
            {
                return shipservice.CurrentYaws;
            }
        }

        public ushort Speed
        {
            get
            {
                return shipservice.speed; 
            }
        }

        public void AddApprochWaypoint(float x, float y, float z, int yaw)
        {
            shipservice.AddApprochWaypoint(x, y, z, yaw);
        }

        public void AddDepartureWaypoint(float x, float y, float z, int yaw)
        {
            shipservice.AddDepartureWaypoint(x, y, z, yaw);
        }

        public void Init(byte orimapid,float dstx,float dsty,float dstz, byte dstmapid)
        {
            float[] pos = new float[3] { dstx, dsty, dstz };
            shipservice = new SagaMap.Tasks.ShipService(orimapid, pos, dstmapid, this.Actor);            
        }

        public void Start()
        {
            shipservice.Activate();
        }

        public override void Dispose()
        {
            if (shipservice.Activated())
                shipservice.Deactivate();
        }
    }
}

