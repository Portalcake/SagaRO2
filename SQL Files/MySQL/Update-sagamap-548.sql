DROP TABLE IF EXISTS `storage`;
CREATE TABLE `storage` (
  `charID` int(10) unsigned NOT NULL default '0',
  `nameID` int(10) NOT NULL default '0',
  `creatorName` varchar(50) collate utf8_unicode_ci NOT NULL default '',
  `equip` tinyint(3) NOT NULL default '-1',
  `amount` tinyint(10) unsigned NOT NULL default '0',
  `durability` smallint(5) unsigned NOT NULL default '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;