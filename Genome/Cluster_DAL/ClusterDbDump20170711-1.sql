-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: cluster_db
-- ------------------------------------------------------
-- Server version	5.7.17-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `cluster`
--

DROP TABLE IF EXISTS `cluster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cluster` (
  `idcluster` int(11) NOT NULL AUTO_INCREMENT,
  `adresse_ip` varchar(15) DEFAULT NULL,
  `etat_noeud` int(11) DEFAULT NULL,
  `type_noeud` int(11) DEFAULT NULL,
  PRIMARY KEY (`idcluster`),
  KEY `FK_cluster_etat_idx` (`etat_noeud`),
  KEY `FK_cluster_type_noeud_idx` (`type_noeud`),
  CONSTRAINT `FK_cluster_etat_noeud` FOREIGN KEY (`etat_noeud`) REFERENCES `etat_noeud` (`Index`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_cluster_type_noeud` FOREIGN KEY (`type_noeud`) REFERENCES `type_noeud` (`Index`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cluster`
--

LOCK TABLES `cluster` WRITE;
/*!40000 ALTER TABLE `cluster` DISABLE KEYS */;
INSERT INTO `cluster` VALUES (1,'192.168.0.25',7,2);
/*!40000 ALTER TABLE `cluster` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `cluster_view`
--

DROP TABLE IF EXISTS `cluster_view`;
/*!50001 DROP VIEW IF EXISTS `cluster_view`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `cluster_view` AS SELECT 
 1 AS `IP`,
 1 AS `ETAT`,
 1 AS `ROLE`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `etat_noeud`
--

DROP TABLE IF EXISTS `etat_noeud`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `etat_noeud` (
  `Index` int(11) NOT NULL AUTO_INCREMENT,
  `Etat` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `etat_noeud`
--

LOCK TABLES `etat_noeud` WRITE;
/*!40000 ALTER TABLE `etat_noeud` DISABLE KEYS */;
INSERT INTO `etat_noeud` VALUES (4,'connected'),(5,'work_in_progress'),(6,'work_done'),(7,'not_connected');
/*!40000 ALTER TABLE `etat_noeud` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `type_noeud`
--

DROP TABLE IF EXISTS `type_noeud`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `type_noeud` (
  `Index` int(11) NOT NULL AUTO_INCREMENT,
  `type` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `type_noeud`
--

LOCK TABLES `type_noeud` WRITE;
/*!40000 ALTER TABLE `type_noeud` DISABLE KEYS */;
INSERT INTO `type_noeud` VALUES (1,'noeud'),(2,'orchestrateur');
/*!40000 ALTER TABLE `type_noeud` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'cluster_db'
--
/*!50003 DROP FUNCTION IF EXISTS `maj_cluster` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cluster_admin`@`localhost` FUNCTION `maj_cluster`(ip_noeud varchar(15),etat int, role int) RETURNS int(11)
BEGIN

DECLARE adresse varchar(15);

SELECT adresse_ip into adresse from cluster where ip_noeud = adresse_ip;

IF adresse is null then Insert into cluster (adresse_ip,etat_noeud,type_noeud) values (ip_noeud , etat , role);
elseif adresse is not null then Update cluster set etat_noeud = etat , type_noeud = role where adresse_ip = ip_noeud; 
END IF;

RETURN 1;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Final view structure for view `cluster_view`
--

/*!50001 DROP VIEW IF EXISTS `cluster_view`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`cluster_admin`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `cluster_view` AS select `cluster`.`adresse_ip` AS `IP`,`e`.`Etat` AS `ETAT`,`t`.`type` AS `ROLE` from ((`cluster` left join `etat_noeud` `e` on((`e`.`Index` = `cluster`.`etat_noeud`))) left join `type_noeud` `t` on((`t`.`Index` = `cluster`.`type_noeud`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-07-11  0:17:13
