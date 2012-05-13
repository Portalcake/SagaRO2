-- Installtion for Saga Map Database--
DROP TABLE IF EXISTS `char`;
CREATE TABLE `char` (
  `charID` int(10) UNSIGNED NOT NULL auto_increment,
  `account_id` int(10) UNSIGNED NOT NULL default '0',
  `name` varchar(50) collate utf8_unicode_ci NOT NULL default '',
  `face` varchar(10) character set ascii NOT NULL default '',
  `details` varchar(12) character set ascii NOT NULL default '',
  `sex` tinyint(3) UNSIGNED NOT NULL default '0',
  `race` tinyint(3) UNSIGNED NOT NULL default '0',
  `job` tinyint(3) UNSIGNED NOT NULL default '0',
  `cLevel` int(10) UNSIGNED NOT NULL default '0',
  `jLevel` int(10) UNSIGNED NOT NULL default '0',
  `cEXP` int(10) UNSIGNED NOT NULL default '0',
  `jEXP` int(10) UNSIGNED NOT NULL default '0',
  `pendingDeletion` tinyint(3) UNSIGNED NOT NULL default '0',
  `validationKey` int(10) UNSIGNED NOT NULL default '0',
  `HP` smallint(5) UNSIGNED NOT NULL default '0',
  `maxHP` smallint(5) UNSIGNED NOT NULL default '0',
  `SP` smallint(5) UNSIGNED NOT NULL default '0',
  `maxSP` smallint(5) UNSIGNED NOT NULL default '0',
  `LC` tinyint(3) UNSIGNED NOT NULL default '0',
  `maxLC` tinyint(3) UNSIGNED NOT NULL default '0',
  `LP` tinyint(3) UNSIGNED NOT NULL default '0',
  `maxLP` tinyint(3) UNSIGNED NOT NULL default '0',
  `str` tinyint(3) UNSIGNED NOT NULL default '0',
  `dex` tinyint(3) UNSIGNED NOT NULL default '0',
  `intel` tinyint(3) UNSIGNED NOT NULL default '0',
  `con` tinyint(3) UNSIGNED NOT NULL default '0',
  `luk` tinyint(3) UNSIGNED NOT NULL default '0',
  `stpoints` tinyint(3) UNSIGNED NOT NULL default '0',
  `slots` varchar(14) collate utf8_unicode_ci NOT NULL default '',
  `weaponName` varchar(24) collate utf8_unicode_ci NOT NULL default '',
  `weaponType` int(11) NOT NULL default '0',
  `GMLevel` int(5) UNSIGNED NOT NULL default '0',
  `NumShortcuts` tinyint(3) UNSIGNED NOT NULL default '0',
  `ShortcutIDs` varchar(96) collate utf8_unicode_ci NOT NULL default '',
  `mapID` tinyint(3) UNSIGNED NOT NULL default '0',
  `worldID` tinyint(3) UNSIGNED NOT NULL default '0',
  `x` float NOT NULL default '0',
  `y` float NOT NULL default '0',
  `z` float NOT NULL default '0',
  `yaw` int(10) UNSIGNED NOT NULL default '0',
  `zeny` INT UNSIGNED NOT NULL default '0' ,
  `save_map` TINYINT UNSIGNED NOT NULL default '0' ,
  `save_x` FLOAT NOT NULL default '0' ,
  `save_y` FLOAT NOT NULL default '0' ,
  `save_z` FLOAT NOT NULL default '0' ,
  `sightRange` int(10) UNSIGNED NOT NULL default '0',
  `maxMoveRange` int(10) UNSIGNED NOT NULL default '0',
  `state` tinyint(3) UNSIGNED NOT NULL default '0',
  `stance` tinyint(3) UNSIGNED NOT NULL default '0',
  `guild` int(10) UNSIGNED NOT NULL default '0',
  `party` int(10) UNSIGNED NOT NULL default '0',
  `Scenario` int(10) UNSIGNED NOT NULL DEFAULT '0',
  `online` tinyint(3) UNSIGNED NOT NULL default '0',
  `muted` tinyint(1) UNSIGNED NOT NULL default '0',
   PRIMARY KEY  (`charID`),
   KEY `name` (`name`),
   KEY `account_id` (`account_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=1 ;

DROP TABLE IF EXISTS `inventory`;
CREATE TABLE `inventory` (
  `id` int(10) UNSIGNED NOT NULL auto_increment,
  `charID` int(10) UNSIGNED NOT NULL default '0',
  `nameID` int(10) NOT NULL default '0',
  `creatorName` varchar(50) collate utf8_unicode_ci NOT NULL default '',
  `equip` tinyint(3) NOT NULL default '-1',
  `amount` tinyint(10) UNSIGNED NOT NULL default '0',
  `durability` smallint(5) UNSIGNED NOT NULL default '0',
  PRIMARY KEY  (`id`),
  KEY `charID` (`charID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

DROP TABLE IF EXISTS `weapon`;
CREATE TABLE `weapon` (
  `charID` INT UNSIGNED NOT NULL ,
  `name` VARCHAR( 24 ) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ,
  `level` TINYINT UNSIGNED NOT NULL ,
  `type` TINYINT UNSIGNED NOT NULL ,
  `augeSkillID` INT UNSIGNED NOT NULL ,
  `exp` INT UNSIGNED NOT NULL ,
  `durability` SMALLINT UNSIGNED NOT NULL ,
  `U1` TINYINT UNSIGNED NOT NULL ,
  `active` TINYINT UNSIGNED NOT NULL ,
  `slot1` INT UNSIGNED NOT NULL ,
  `slot2` INT UNSIGNED NOT NULL ,
  `slot3` INT UNSIGNED NOT NULL ,
  `slot4` INT UNSIGNED NOT NULL ,
  `slot5` INT UNSIGNED NOT NULL ,
  `slot6` INT UNSIGNED NOT NULL ,
 KEY (`charID`)
) ENGINE = innodb;

DROP TABLE IF EXISTS `shortcuts`;
CREATE TABLE `shortcuts` (
  `charID` INT UNSIGNED NOT NULL ,
  `slotnumber` TINYINT UNSIGNED NOT NULL ,
  `type` TINYINT UNSIGNED NOT NULL ,
  `itemID` INT UNSIGNED NOT NULL ,
  KEY (`charID`)
) ENGINE = innodb CHARACTER SET utf8 COLLATE utf8_unicode_ci;

DROP TABLE IF EXISTS `skills`;
CREATE TABLE `skills` (
  `id` int(10) UNSIGNED NOT NULL auto_increment,
  `charID` INT UNSIGNED NOT NULL ,
  `type` TINYINT UNSIGNED NOT NULL ,
  `skillID` INT UNSIGNED NOT NULL,
  `exp` INT UNSIGNED NOT NULL,
  `slot` TINYINT UNSIGNED NOT NULL DEFAULT '0',
  PRIMARY KEY  (`id`),
  KEY `charID` (`charID`)
) ENGINE = innodb CHARACTER SET utf8 COLLATE utf8_unicode_ci;

DROP TABLE IF EXISTS `quest`;
CREATE TABLE `quest` (
  `id` int(10) UNSIGNED NOT NULL auto_increment,
  `charID` INT UNSIGNED NOT NULL ,
  `questID` INT UNSIGNED NOT NULL ,
  `step` VARCHAR( 150 ) CHARACTER SET ascii COLLATE ascii_bin NOT NULL ,
  `type` TINYINT UNSIGNED NOT NULL DEFAULT '0',
  PRIMARY KEY  (`id`),
  KEY `charID` (`charID`)
) ENGINE = innodb  CHARACTER SET utf8 COLLATE utf8_unicode_ci;

DROP TABLE IF EXISTS `joblevel`;
CREATE TABLE `joblevel` (
  `charID` INT UNSIGNED NOT NULL ,
  `job` TINYINT UNSIGNED NOT NULL ,
  `level` TINYINT UNSIGNED NOT NULL,  KEY `charID` (`charID`,`job`)
) ENGINE = innodb;

DROP TABLE IF EXISTS `MapInfo`;
CREATE TABLE `MapInfo` (
  `charID` INT UNSIGNED NOT NULL ,
  `map` TINYINT UNSIGNED NOT NULL ,
  `value` TINYINT UNSIGNED NOT NULL,
  KEY `charID` (`charID`,`map`)
) ENGINE = innodb;

DROP TABLE IF EXISTS `storage`;
CREATE TABLE `storage` (
  `id` int(10) UNSIGNED NOT NULL auto_increment,
  `charID` int(10) UNSIGNED NOT NULL default '0',
  `nameID` int(10) NOT NULL default '0',
  `creatorName` varchar(50) collate utf8_unicode_ci NOT NULL default '',
  `equip` tinyint(3) NOT NULL default '-1',
  `amount` tinyint(10) UNSIGNED NOT NULL default '0',
  `durability` smallint(5) UNSIGNED NOT NULL default '0',
  PRIMARY KEY  (`id`),
  KEY (`charID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

DROP TABLE IF EXISTS `mail`;
CREATE TABLE `mail` (
  `mailID` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY ,
  `sender` VARCHAR( 34 ) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL ,
  `receiver` VARCHAR( 34 ) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL ,
  `topic` VARCHAR( 40 ) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL ,
  `date` VARCHAR( 30 ) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL ,
  `content` VARCHAR( 402 ) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL ,
  `read` TINYINT UNSIGNED NOT NULL ,
  `valid` TINYINT UNSIGNED NOT NULL ,
  `zeny` INT UNSIGNED NOT NULL ,
  `itemID` INT UNSIGNED NOT NULL ,
  `creator` VARCHAR( 36 ) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL ,
  `stack` TINYINT UNSIGNED NOT NULL ,
  `durability` SMALLINT UNSIGNED NOT NULL,
  KEY `receiver` (`receiver`),
  KEY `sender` (`sender`)
) ENGINE = innodb DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

DROP TABLE IF EXISTS `market`;
CREATE TABLE `market` (
`id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY ,
`item_id` INT UNSIGNED NOT NULL ,
`item_type` TINYINT UNSIGNED NOT NULL ,
`item_clv` TINYINT UNSIGNED NOT NULL ,
`item_stack` TINYINT UNSIGNED NOT NULL ,
`item_durability` SMALLINT UNSIGNED NOT NULL ,
`owner` VARCHAR( 40 ) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL ,
`price` INT UNSIGNED NOT NULL ,
`expire` VARCHAR( 40 ) NOT NULL ,
`comment` VARCHAR( 1024 ) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL 
) ENGINE = innodb DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;