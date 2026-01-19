-- MySQL dump 10.13  Distrib 8.0.43, for Win64 (x86_64)
--
-- Host: localhost    Database: user_play_db
-- ------------------------------------------------------
-- Server version	8.0.43

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `userplaydata`
--
-- DROP USER IF EXISTS 'JackTheReaperDB'@'%';

-- -- 'JackTheReaperDB' 라는 이름의 사용자를 만들고, 모든 IP(%)에서 접속 가능하게 합니다.
-- -- 비밀번호는 'JTRDBpw12'로 설정합니다.
-- CREATE USER 'JackTheReaperDB'@'%' IDENTIFIED BY 'JTRDBpw12';

-- -- 'JackTheReaperDB' 사용자에게 'JackTheReaperDB' 데이터베이스의 모든 권한을 부여합니다.
-- GRANT ALL PRIVILEGES ON JackTheReaperDB.* TO 'JackTheReaperDB'@'%';

-- -- 변경된 권한을 즉시 적용합니다.
-- FLUSH PRIVILEGES;


-- USE JackTheReaperDB;

DROP TABLE IF EXISTS `userplaydata`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
-- 여기 부분에 아가전에 Model에 
CREATE TABLE `userplaydata` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IsNormal` tinyint DEFAULT NULL,
  `StartTime` BIGINT DEFAULT NULL,
  `EndTime` BIGINT DEFAULT NULL,
  `DeadCount` int DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 1. StageDeathInfo 테이블 생성
CREATE TABLE StageDeathInfo (
    StageId INT NOT NULL AUTO_INCREMENT, -- 유저별 스테이지 사망 로그를 구분하기 위한 id
    PlayresultId INT NOT NULL,           -- 부모인 userplaydata(Id) 참조
    StageName VARCHAR(255),
    DeathCount INT DEFAULT 0,
    PRIMARY KEY (StageId),
    CONSTRAINT FK_Playresult FOREIGN KEY (PlayresultId) 
        REFERENCES userplaydata(Id) ON DELETE CASCADE
);

-- 2. DeathInfo 테이블 생성
CREATE TABLE DeathInfo (
    DeathId INT NOT NULL AUTO_INCREMENT, -- 해당 스테이지에서 n번째 사망 정보를 가져오기 위한 식별 id
    StageId INT NOT NULL,                -- 부모인 StageDeathInfo(StageId) 참조
    EnemyName VARCHAR(255),
    DeathPositionX FLOAT,
    DeathPositionY FLOAT,
    EnemyPositionX FLOAT,
    EnemyPositionY FLOAT,
    PRIMARY KEY (DeathId),
    CONSTRAINT FK_StageDeathInfo_DeathInfo 
        FOREIGN KEY (StageId) REFERENCES StageDeathInfo (StageId) 
        ON DELETE CASCADE
);




/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userplaydata`
--

LOCK TABLES `userplaydata` WRITE;
/*!40000 ALTER TABLE `userplaydata` DISABLE KEYS */;
INSERT INTO `userplaydata` VALUES (1,0,0,10.4,0);
/*!40000 ALTER TABLE `userplaydata` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-08-11  1:18:07
