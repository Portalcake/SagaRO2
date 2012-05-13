using System;
using System.Collections.Generic;
using System.Text;
using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

namespace SagaMap
{
    public class Shop : Npc
    {
        //protected uint money=0;

        /*public override void OnSelectButton(ActorPC sActor, int button)
        {
            switch (button)
            {
                case 10:
                    //Temporary info. Should lookup npc inventory from DB?
                    sActor.e.OnSendShopList(this.Actor.NPCinv, this.money,this.Actor.id); 
                    break;
                default:
                    break;
            }
        }*/

        public override void OnCreate(bool success)
        {
            if (success)
            {
                I.invisble = false;
                map.OnActorVisibilityChange(I);
                this.map.SendVisibleActorsToActor(I);
            }

            this.Actor.Attribute.script = 3;
            this.Actor.Attribute.icons = new byte[1] { 10 };
        }

    }
}
