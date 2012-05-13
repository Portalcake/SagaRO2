using System;
using System.Collections.Generic;

using SagaMap;

using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
public class Elisa : Npc
{
public override void OnInit()
{
MapName = "Prt_f01";
Type = 1229;
Name = "Elisa";
StartX = 6727F;
StartY = 88722F;
StartZ = 4205;
Startyaw = 59000;
SetScript(3);
AddButton(Functions.EverydayConversation, new func(OnButton));

AddButton(Functions.Supply);
SupplyMenuID = 3;

//Exchange
// Create Bunny band
AddSupplyProduct(12,3489,1);
AddSupplyMatrial(12,9449,10); // Pearl x10
AddSupplyMatrial(12,9418,20); // Four leaf clover x20
AddSupplyMatrial(12,9410,10); // Tight Jellopy x10
AddSupplyMatrial(12,10475,20); // Rabbit Hair x20
//Add 1200Rufi

// Create Cat Mask
AddSupplyProduct(13,3578,1);
AddSupplyMatrial(13,9428,20); // Cat foot stamp x20
AddSupplyMatrial(13,9429,20); // Cat jewel x20
AddSupplyMatrial(13,9419,10); // Cat Bell x10
//Add 1200 Rufi

// Create Little Flower Headband
AddSupplyProduct(14,3505,1);
AddSupplyMatrial(14,1800000,20); // Black lace x20
AddSupplyMatrial(14,1700096,20); // Elegant fabric x20 -- Unsure on the item, used High Grade Fabric.
//Add 1200 Ruffi

// Create Laboratory Hat
AddSupplyProduct(15,640003,1);
AddSupplyMatrial(15,9511,10); // Short leather x10
AddSupplyMatrial(15,9409,10); // Hard Jellopy 10
AddSupplyMatrial(15,9470,10); // Potemkin's Shell x10
AddSupplyMatrial(15,9512,1); // Burned candle x1
//Add 1200 Ruffi
}

public void OnButton(ActorPC pc)
{
NPCChat(pc, 823);
}
}
}