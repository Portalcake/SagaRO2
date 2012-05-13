@echo off
PATH=C:\Programme\Mono-1.2.3.1\bin;%PATH%

echo **** Creating folder ../monobin ****
mkdir ..\monobin
echo _
echo **** Making SagaLib *****
cd SagaLib
call gmcs Structures.cs Client.cs Encryption.cs Global.cs Logger.cs NetIO.cs  WorldConfig.cs XmlParser.cs .\Properties\AssemblyInfo.cs .\Packets\Server\AskGUID.cs .\Packets\Server\SendKey.cs -pkg:gtk-sharp  -target:library -out:SagaLib.dll
echo _
echo **** Copying SagaLib to ../monobin ****
copy SagaLib.dll ..\monobin
copy exp.xml ..\monobin
copy itemDB.xml ..\monobin
echo _
cd ..

echo **** Making SagaDB*****
cd SagaDB
call gmcs .\Properties\AssemblyInfo.cs ActorDB.cs db4oUserDB.cs db40ActorDB.cs User.cs UserDB.cs .\Actors\Actor.cs  .\Actors\ActorEventHandler.cs .\Actors\ActorItem.cs .\Actors\ActorPC.cs .\Actors\ActorNPC.cs  .\Items\Inventory.cs .\Items\Item.cs .\Items\ItemBonus.cs .\Items\ItemFactory.cs .\Items\Slots.cs -pkg:gtk-sharp -target:library -out:SagaDB.dll -reference:Db4objects.Db4o.dll -reference:../monobin/SagaLib.dll
echo _
echo **** Copying SagaDB to ../monobin ****
copy SagaDB.dll ..\monobin
copy Db4objects.Db4o.dll ..\monobin
copy Db4objects.Db4o.xml ..\monobin
echo _
cd ..

echo **** Making DB4OServer*****
cd DB4OServer
call gmcs .\Properties\AssemblyInfo.cs  DBServerConfig.cs  Program.cs -pkg:gtk-sharp -target:exe -out:DB4OServer.exe  -reference:Db4objects.Db4o.dll -reference:../monobin/SagaLib.dll -reference:../monobin/SagaDB.dll
echo _
echo **** Copying DB4OServer to ../monobin ****
copy DB4OServer.exe ..\monobin
echo _
cd ..

echo **** Making SagaLogin*****
cd SagaLogin
SET CLIENT_PACKETS=.\Packets\Client\CreateChar.cs .\Packets\Client\DeleteChar.cs .\Packets\Client\GetCharData.cs .\Packets\Client\SelectChar.cs .\Packets\Client\SelectServer.cs .\Packets\Client\SendCRC.cs .\Packets\Client\SendGUID.cs .\Packets\Client\SendKey.cs .\Packets\Client\SendLogin.cs .\Packets\Client\SendVersion.cs .\Packets\Client\WantCharList.cs .\Packets\Client\WantServerList.cs
SET MAP_PACKETS=.\Packets\Map\Get\Identify.cs .\Packets\Map\Send\IdentAnswer.cs
SET SERVER_PACKETS=.\Packets\Server\AskCRC.cs  .\Packets\Server\Identify.cs .\Packets\Server\LoginAnswer.cs .\Packets\Server\SendAck.cs .\Packets\Server\SendCharData.cs .\Packets\Server\SendCharList.cs .\Packets\Server\SendError.cs .\Packets\Server\SendServerList.cs .\Packets\Server\SendToMapServer.cs
call gmcs .\Properties\AssemblyInfo.cs  ClientManager.cs  LoginClient.cs LoginConfig.cs LoginServer.cs .\Objects\CharServer.cs .\Objects\MapServer.cs %CLIENT_PACKETS%  %MAP_PACKETS% %SERVER_PACKETS% -pkg:gtk-sharp -target:exe -out:SagaLogin.exe -reference:../monobin/SagaLib.dll -reference:../monobin/SagaDB.dll
echo _
echo **** Copying SagaLogin to ../monobin ****
copy SagaLogin.exe ..\monobin
echo _
cd ..


SET MAP_FILES=%MAP_FILES% "ActorEventHandlers\Item_EventHandler.cs" 
SET MAP_FILES=%MAP_FILES% "ActorEventHandlers\NPC_EventHandler.cs" 
SET MAP_FILES=%MAP_FILES% "ActorEventHandlers\PC_EventHandler.cs" 
SET MAP_FILES=%MAP_FILES% "AtCommand.cs" 
SET MAP_FILES=%MAP_FILES% "ClientManager.cs" 
SET MAP_FILES=%MAP_FILES% "LevelManager.cs" 
SET MAP_FILES=%MAP_FILES% "LoginSession.cs" 
SET MAP_FILES=%MAP_FILES% "Map.cs" 
SET MAP_FILES=%MAP_FILES% "MapClient.cs" 
SET MAP_FILES=%MAP_FILES% "MapConfig.cs" 
SET MAP_FILES=%MAP_FILES% "Npc.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\3 - Map\GetStatUpdate.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\4 - Chat\GetWhisper.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\3 - Map\SendChangeState.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\4 - Chat\GetChat.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\3 - Map\SendDemandMapID.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\1,2 - Login\SendGUID.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\1,2 - Login\SendIdentity.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\1,2 - Login\SendKey.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\3 - Map\SendMapLoaded.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\3 - Map\SendMoveStart.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\3 - Map\SendMoveStop.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\3 - Map\SendUsePortal.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\5 - Items\DeleteItem.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\5 - Items\SortInvList.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\5 - Items\MoveItem.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\6 - NPC and Battle\GetSelectButton.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\6 - NPC and Battle\GetTargetAttribute.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\7 - Quest\WantQuestGroupList.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\8 - Trade\GetTrade.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\8 - Trade\GetTradeItem.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\9 - Skill\UseOffensiveSkill.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\A - Shortcuts\AddShortcut.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Client\A - Shortcuts\DelShortcut.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Login\Get\IdentAnswer.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Login\Get\SendGUID.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Login\Get\SendKey.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Login\Send\Identify.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\1,2 - Login\AskCRC.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\1,2 - Login\Identify.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\3 - Map\ActorSetEquip.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\3 - Map\P0315.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\3 - Map\ActorChangeState.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\3 - Map\LevelUp.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\3 - Map\ActorMoveStart.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\3 - Map\ActorMoveStop.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\3 - Map\ActorTeleport.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\3 - Map\ActorPlayerInfo.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\1,2 - Login\SendAck.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\3 - Map\CharStatus.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\3 - Map\SendTime.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\4 - Chat\SendChat.cs" 
SET MAP_FILES=%MAP_FILES% "MapServer.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\3 - Map\SendIdent.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\3 - Map\ActorPCInfo.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\3 - Map\SendStart.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\3 - Map\ActorDelete.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\4 - Chat\SendSystemMessage.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\4 - Chat\SendWhisper.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\5 - Items\AddItem.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\5 - Items\DeleteItem.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\5 - Items\ListEquipment.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\5 - Items\ListInventory.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\5 - Items\MoveItem.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\5 - Items\MoveReply.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\5 - Items\SetInventorySlotCount.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\5 - Items\UpdateItem.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\6 - NPC and Battle\ActorNPCInfo.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\6 - NPC and Battle\NPCChat.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\6 - NPC and Battle\SendNpcInventory.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\8 - Trade\Trade.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\8 - Trade\TradeCancel.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\8 - Trade\TradeConfirm.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\8 - Trade\TradeItem.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\8 - Trade\TradeItemOther.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\8 - Trade\TradeOther.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\8 - Trade\TradeResult.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\8 - Trade\TradeZeny.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\8 - Trade\TradeZenyOther.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\9 - Skills\OffensiveSkill.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\A - Shortcuts\SendAddShortcut.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\A - Shortcuts\SendDelShortcut.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\A - Shortcuts\SendShortcutList.cs" 
SET MAP_FILES=%MAP_FILES% "Packets\Server\GenericPacket.cs" 
SET MAP_FILES=%MAP_FILES% "PortalManager.cs" 
SET MAP_FILES=%MAP_FILES% "Properties\AssemblyInfo.cs" 
SET MAP_FILES=%MAP_FILES% "MapManager.cs" 
SET MAP_FILES=%MAP_FILES% "Script.cs" 
SET MAP_FILES=%MAP_FILES% "ScriptManager.cs" 
SET MAP_FILES=%MAP_FILES% "TaskManager.cs" 
echo **** Making SagaMap*****
cd SagaMap
call gmcs %MAP_FILES% -pkg:gtk-sharp -target:exe -out:SagaMap.exe -reference:../monobin/SagaLib.dll -reference:../monobin/SagaDB.dll
echo _
echo **** Copying SagaMap to ../monobin ****
copy SagaMap.exe ..\monobin
echo _
cd ..


echo ** BATCH FINISHED - CHECK FOR ERRORS **

pause