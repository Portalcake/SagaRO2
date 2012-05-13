-- Installtion for Saga Login Database--
DROP TABLE IF EXISTS `login`;
CREATE TABLE `login` (
  `account_id` int(10) NOT NULL auto_increment,
  `username` varchar(25) collate utf8_unicode_ci NOT NULL default '',
  `password` varchar(50) collate utf8_unicode_ci NOT NULL default '',
  `sex` tinyint(3) unsigned NOT NULL default '1',
  `lastlogin` varchar(25) collate utf8_unicode_ci NOT NULL default '',
  `Banned` tinyint(3) NOT NULL default '0',
  PRIMARY KEY  (`account_id`),
  KEY `username` (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
--
-- Procedures
--
DELIMITER $$
--
CREATE DEFINER=`saga`@`localhost` PROCEDURE `sagaLoginAddUser`(
name varchar(25),
pass varchar(50),
gender tinyint(3),
last varchar(25))
BEGIN
INSERT INTO `login` (`username`,`password`,`sex`,`lastlogin`) VALUES (name,pass,gender,last);
END$$

CREATE DEFINER=`saga`@`localhost` PROCEDURE `sagaLoginGetAccountId`(name varchar(20))
BEGIN
SELECT `account_id` FROM `login` WHERE `username`=name;
END$$

CREATE DEFINER=`saga`@`localhost` PROCEDURE `sagaLoginGetUser`(name varchar(20))
BEGIN
SELECT * FROM `login` WHERE `username`=name LIMIT 1;
END$$

CREATE DEFINER=`saga`@`localhost` PROCEDURE `sagaLoginUpdateUser`(
name varchar(25),
pass varchar(50),
gender tinyint(3),
last varchar(25))
BEGIN
UPDATE `login` SET `password`=pass, `sex`=gender, `lastlogin`=last WHERE `username`=name;
END$$

--
DELIMITER ;
--