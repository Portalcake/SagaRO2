ALTER TABLE `login` ADD INDEX(`username`);
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