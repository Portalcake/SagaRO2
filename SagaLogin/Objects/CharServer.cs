using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using SagaDB;

namespace SagaLogin
{
    public class CharServer
    {
        public enum Status
        {
            OK,
            CROWDED,
            MAINTENANCE,
            OVERLOADED,
            UNKNOWN,
        }
        public string worldname;
        public Status ping;
        public byte playerCount;
        public byte worldID;
        public List<MapServer> mapServers;
        public ActorDB charDB;
        //int serverID;

        public CharServer(string worldName, byte mPing, byte mPlayerCount, byte worldID, ActorDB charDB)
        {
            this.worldname = worldName;
            this.ping = (Status)mPing;
            this.playerCount = mPlayerCount;
            this.worldID = worldID;
            this.charDB = charDB;
            this.mapServers = new List<MapServer>();

        }
        public void AddMapServer(MapServer newServer)
        {
            this.mapServers.Add(newServer);
        }

        public void DeleteMapServer(MapServer delServer)
        {
            this.mapServers.Remove(delServer);
        }

        public MapServer GetMapServer(int mapID)
        {
            foreach(MapServer server in this.mapServers)
            {
                for (int i = 0; i < server.hostedMaps.Length; i++)
                    if (server.hostedMaps[i] == mapID) return server;
            }
            return null;
        }

        public int GetPlayerCount()
        {
            return this.playerCount;
        }

        public Status GetPing()
        {
            return this.ping;
        }

        public bool MapsAreNotHostedYet(int[] hostedMaps)
        {
            foreach (MapServer server in this.mapServers)
            {
                for (int i = 0; i < 12; i++)
                  for(int j = 0; j < 12; j++)
                    if(server.hostedMaps[i] != 0)
                        if (server.hostedMaps[i] == hostedMaps[j]) return false;
            }

            return true;
        }
    }
}
