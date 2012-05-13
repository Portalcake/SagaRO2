using System;
using System.Collections.Generic;
using System.Text;

namespace SagaLogin
{
    public class MapServer
    {
        public LoginClient sClient;
        public int worldID;
        public int[] hostedMaps;
        public int playerOnline;
        public byte ping;
        public string IP;
        public short port;
        public DateTime lastPing;
        public DateTime lastPong;

        public MapServer(int worldID,string IP,short port, LoginClient sClient, int[] hostedMaps, int playerOnline, byte ping)
        {
            this.sClient = sClient;
            this.worldID = worldID;
            this.IP = IP;
            this.port = port;
            this.hostedMaps = hostedMaps;
            this.playerOnline = playerOnline;
            this.ping = ping;
            lastPing = DateTime.Now;
            lastPong = DateTime.Now;
        }
        }
    }
