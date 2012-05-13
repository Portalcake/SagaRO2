using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class Migo_Alfred : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1020;
            Name = "Migo_Alfred";
            StartX = -13576F;
            StartY = -18460F;
            StartZ = 9514;
            Startyaw = -27696;
            SetScript(823);
            SetSavePoint(20, -14232f, -18221f, 9549);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.Kafra);
            AddButton(Functions.EnterShip,new func(OnButton2));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

        public void OnButton2(ActorPC pc)
        {
          Warp(pc, 5, 13715.05f, 77742.77f, 5120f);
        }
    }
}