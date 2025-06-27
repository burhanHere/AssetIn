-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: assetindb
-- ------------------------------------------------------
-- Server version	8.0.42

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
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20250622145455_RemovedAllMigrationsAndCreatedOne','8.0.10'),('20250622192219_RemovedVendorProcurementDetailsTableAddedVendorProductsTable','8.0.10'),('20250622192539_AddedProductImageCOlumnInVendorProducttable','8.0.10'),('20250623111432_RemoveLostAssetStatus','8.0.10'),('20250623113120_RemoveOutOfOrderAssetStatus','8.0.10'),('20250623215415_AddedProfilePicturePathColumnInVendorTable','8.0.10');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetroleclaims`
--

DROP TABLE IF EXISTS `aspnetroleclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroleclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetroleclaims`
--

LOCK TABLES `aspnetroleclaims` WRITE;
/*!40000 ALTER TABLE `aspnetroleclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroleclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetroles`
--

DROP TABLE IF EXISTS `aspnetroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetroles`
--

LOCK TABLES `aspnetroles` WRITE;
/*!40000 ALTER TABLE `aspnetroles` DISABLE KEYS */;
INSERT INTO `aspnetroles` VALUES ('1','OrganizationOwner','ORGANIZATIONOWNER','045d19a6-bff3-42c1-82be-17039cc1f3a3'),('2','OrganizationEmployee','ORGANIZATIONEMPLOYEE','1e5bc45f-d7ce-4618-8243-1bfe7ab92f2f'),('3','OrganizationAssetManager','ORGANIZATIONASSETMANAGER','d07f9279-0a24-47d3-8ebf-1124218c502f'),('4','Vendor','VENDOR','58525f0e-54ff-4653-bd7e-9a061a2df206');
/*!40000 ALTER TABLE `aspnetroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserclaims`
--

DROP TABLE IF EXISTS `aspnetuserclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserclaims`
--

LOCK TABLES `aspnetuserclaims` WRITE;
/*!40000 ALTER TABLE `aspnetuserclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserlogins`
--

DROP TABLE IF EXISTS `aspnetuserlogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserlogins`
--

LOCK TABLES `aspnetuserlogins` WRITE;
/*!40000 ALTER TABLE `aspnetuserlogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserlogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserroles`
--

DROP TABLE IF EXISTS `aspnetuserroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserroles`
--

LOCK TABLES `aspnetuserroles` WRITE;
/*!40000 ALTER TABLE `aspnetuserroles` DISABLE KEYS */;
INSERT INTO `aspnetuserroles` VALUES ('8d141bb6-21b5-42b1-b42a-dde9668f2cb6','1'),('0069ab7a-2359-4331-90af-965bcb4a0f7b','2'),('0b046f72-8bee-4da2-b638-44d344509c59','2'),('28e6f14b-b087-4bff-8d4b-7bda035685ec','2'),('4455b62a-0ab0-4ce3-9ed9-f009f2c6c4ee','2'),('5a541c31-2d73-43a8-8f44-933ebe87a699','2'),('686c44a1-0955-4ec0-99a9-685852dee2e8','2'),('6b726df0-d530-4700-812b-f7c040e4f9e3','2'),('72351ab4-6944-4570-a50e-89234d492250','2'),('7cb8cc81-baa8-4600-bf02-9320d15d1eeb','2'),('a108227d-a8c3-467d-8d54-778ff5cac83f','2'),('ed729699-667c-45a2-941e-e7d53ae4a033','2'),('f6bf2245-32db-4aad-a1a9-190c5e89a56a','2'),('6ac0619d-85b7-46bb-9fee-56451de3c190','3'),('493b4fb3-89f2-4585-ae4a-dc6468b67f07','4');
/*!40000 ALTER TABLE `aspnetuserroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetusers`
--

DROP TABLE IF EXISTS `aspnetusers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetusers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProfilePicturePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Status` tinyint(1) NOT NULL,
  `DateOfBirth` datetime(6) NOT NULL,
  `DateOfJoining` datetime(6) NOT NULL,
  `Gender` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `OrganizationId` int DEFAULT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`),
  KEY `IX_AspNetUsers_OrganizationId` (`OrganizationId`),
  CONSTRAINT `FK_AspNetUsers_Organizations_OrganizationId` FOREIGN KEY (`OrganizationId`) REFERENCES `organizations` (`OrganizationID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetusers`
--

LOCK TABLES `aspnetusers` WRITE;
/*!40000 ALTER TABLE `aspnetusers` DISABLE KEYS */;
INSERT INTO `aspnetusers` VALUES ('0069ab7a-2359-4331-90af-965bcb4a0f7b','',1,'2004-09-20 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'AhmadMalik','AHMADMALIK','ahmadmalik@assetin.test','AHMADMALIK@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEKkpDHF8DsrPh8UIVYAIA9u51q3jaP/4DFEmVHl8OtylpLWnRyNRIC/NtwlawUdiTg==','Q624TZ5AOE3AY3P26BABO5FGKJXYCWKL','9eb3982b-29fc-4a13-a342-b156e5e5a2d2','+923261009574',0,0,NULL,1,0),('0b046f72-8bee-4da2-b638-44d344509c59','',1,'1999-06-27 00:00:00.000000','0001-01-01 00:00:00.000000','Female',1,'RidaMunawwar','RIDAMUNAWWAR','ridamunawwar@assetin.test','RIDAMUNAWWAR@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEHAFpLPOixGoFsR5wZ3M84UduGVddkV/RAbpkvb5U0rM4bIdgvUStNXQtveAj5Pnzg==','V7CDJ5L5SCWX4GEZVZC6SJMBOXUJCJNE','53d84308-2bc9-4d61-890d-38a229e0e426','+923581564225',0,0,NULL,1,0),('28e6f14b-b087-4bff-8d4b-7bda035685ec','',1,'2005-04-11 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'MuhammadAbdullah','MUHAMMADABDULLAH','muhammadabdullah@assetin.test','MUHAMMADABDULLAH@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEAKVMcBT+2N9iUj8VrUCkItARarrVky3bT+o2OBZOh4pNKYiqtRfK1/1jfvM+KaJwg==','V2XMXDXBEUBHOSXLSYYMLSXVDVEMO4CA','7df7e6c9-04bf-4d25-8496-0533ac5b9be9','+923214948644',0,0,NULL,1,0),('4455b62a-0ab0-4ce3-9ed9-f009f2c6c4ee','',1,'2002-12-07 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'khanamAreeba','KHANAMAREEBA','khanamareeba@assetin.test','KHANAMAREEBA@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEG2I6rMYE0+WgWrAQyWkSk6KzXcvJKC1nrKXUQL/fbOx0jWvB7kj882Y0GyouaTAWQ==','KFAMDWBUM4F4JJFIQLUZAY2KOPUWANKN','47587ab8-4093-48d5-a7cc-020a3e1fec47','+923004653232',0,0,NULL,1,0),('493b4fb3-89f2-4585-ae4a-dc6468b67f07','https://res.cloudinary.com/do4mdspjg/image/upload/v1750898599/bybbg9yypjkuguak6u5b.jpg',1,'2002-12-07 00:00:00.000000','0001-01-01 00:00:00.000000','male',NULL,'_burhan.vendor_','_BURHAN.VENDOR_','vendor@vendor.com','VENDOR@VENDOR.COM',1,'AQAAAAIAAYagAAAAEI8HSiaMpnfc+qjICcL3py3gxAkge+oPXq1fiuZWy8f21kFjECT85DUiWceVRQJTgg==','ZXK2OZONTMJ3P65HIYFCIV6EFDVVZQDQ','99d7a141-eca0-427b-848c-6b8be77979a7','+923004653232',0,0,NULL,1,0),('5a541c31-2d73-43a8-8f44-933ebe87a699','',1,'1997-10-04 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'RanaAsim','RANAASIM','ranaasim@assetin.test','RANAASIM@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEEJHg4ZpgoqoF0BmPZ/829MXRXmj2LWjNjW2AaEvR+pZtAubJ2D7yPvQScCrLxAqkg==','RKV3LI2Y44LBACKEMJ4OFKZ5XDMKI4D7','4e56edb7-db74-4638-8ef8-724fc7653661','+923037223438',0,0,NULL,1,0),('686c44a1-0955-4ec0-99a9-685852dee2e8','',1,'1997-12-08 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'IbtesamHaider','IBTESAMHAIDER','ibtesamhaider@assetin.test','IBTESAMHAIDER@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEOpeODwTljujsYOTIOLDzW9ltDvRWMhoRu9M807cJg/C9nQJvpbAAOGyou5sTapTew==','HRZLAHPS2RGRRZQDT2QLCPGOK2DY5IH6','2b13389a-4c26-4663-bb2e-600618f1e357','+923454742142',0,0,NULL,1,0),('6ac0619d-85b7-46bb-9fee-56451de3c190','https://res.cloudinary.com/do4mdspjg/image/upload/v1750963481/xo9f9wt4hzithk1rf6yk.jpg',1,'2002-12-07 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'L1F21BSCS1059','L1F21BSCS1059','l1f21bscs1059@assetin.test','L1F21BSCS1059@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEMfWc/NmN0nee386gvSdWZ2TgH5e5LFK8zBUxGeF3h6RtXWg6lnmxGs88bSK5+iVjw==','7R4BV5XPZIDBITYNF4AMBPSVOEZG5N7I','087001a8-892f-4217-9b6a-03617c9d895d','+923004653232',0,0,NULL,1,0),('6b726df0-d530-4700-812b-f7c040e4f9e3','',1,'2002-12-07 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'burhan72386','BURHAN72386','burhan72386@assetin.test','BURHAN72386@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEGlQqCpiCOUF60QGXg6BxeTH5ovw/gWmsZMmn+iAcJ9ZAEyjsuYcIlGdtOwy8P1nLQ==','WDP3RJC66KLZQZEMVBSGP5YZYV7FJXNR','04b07f9e-2b67-4f81-9054-bdfc2dac5861','+923004653232',0,0,NULL,1,0),('72351ab4-6944-4570-a50e-89234d492250','',1,'2004-10-28 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'AhmadMohsin','AHMADMOHSIN','ahmadmohsin@assetin.test','AHMADMOHSIN@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEIThBvJ5KiU4ZgFd3PxjM7ClvVeqn4NjOg3dKeKXSi9P+XTsxfEgp7QJ7YL9juf/og==','OWW43CKVZ7ZA65YJMP2LJ3JUVN4MEZH3','115177a9-bf32-48e0-b470-7b2a1d06bab9','+923274760572',0,0,NULL,1,0),('7cb8cc81-baa8-4600-bf02-9320d15d1eeb','',1,'1999-06-27 00:00:00.000000','0001-01-01 00:00:00.000000','Female',1,'RidaMunawwar2','RIDAMUNAWWAR2','ridamunawwar2@assetin.test','RIDAMUNAWWAR2@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEJJwMWl7WpYB/Vci1zjerbycCX6YStqQAgDl4fcQrJfv1Grk50DgWGDqNS7GMSZLYQ==','34X7CQXFNXHTQDS6ALXQ67PTEOAY3VBO','dbb5795d-fb76-421b-bed9-d9c0b86d90b7','+923581564225',0,0,NULL,1,0),('8d141bb6-21b5-42b1-b42a-dde9668f2cb6','https://res.cloudinary.com/do4mdspjg/image/upload/v1750897077/nodxnwhvcoxwv84ksh4t.jpg',1,'2002-12-07 00:00:00.000000','0001-01-01 00:00:00.000000','male',NULL,'_burhan.here_','_BURHAN.HERE_','burhanburewala@gmail.com','BURHANBUREWALA@GMAIL.COM',1,'AQAAAAIAAYagAAAAEIkXNcfcGqXJ3LPaezP/sk6aRznYropV4p8enOXVtwCBNbMtZdv12wL3p6Aep/vXhQ==','DGMLA4APQOMERA2RSUUXWVMARBQTHWMP','48b4c616-b1fb-412e-ab54-91a767976333','+923004653232',0,0,NULL,1,0),('a108227d-a8c3-467d-8d54-778ff5cac83f','',1,'1996-12-31 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'RabeelAnwar','RABEELANWAR','rabeelanwar@assetin.test','RABEELANWAR@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEK1JEfJ9HxrPpw3cA/q/f2/3yx1gZYSweIEcSpwMwrl8Ha4aSVdqUsc5vZGvO+KP9Q==','XIZT4TCWJRGZEP4CKARGX7LIMVAMYHAL','3637cbe1-011b-44e0-97c2-f04b2fca2212','+923458454643',0,0,NULL,1,0),('ed729699-667c-45a2-941e-e7d53ae4a033','',1,'2006-08-04 00:00:00.000000','0001-01-01 00:00:00.000000','Female',1,'AyeshaNadeem','AYESHANADEEM','ayeshanadeem@assetin.test','AYESHANADEEM@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEL5U8q+4dnc5SWHyXeh/cRZ92r8ijq9b99H+TMQJBdd5/XwONxujJlDh4vV4m7cXBw==','KYSHOGLXJ3IDTNTNRYGLTFQTQ7PRGT7D','b25f27c8-89ff-4777-8ef1-475d5ab21a72','+923453214545',0,0,NULL,1,0),('f6bf2245-32db-4aad-a1a9-190c5e89a56a','',1,'2009-04-15 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'ArhamNadeem','ARHAMNADEEM','arhamnadeem@assetin.test','ARHAMNADEEM@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEBkE7e90itd+cmNWYa6YLwpopRlu2Px09TKNaDYpsYSpZIYbHjau3LTbs7YoEwFSEg==','DQ7ETNDFXTLTQFBK4FZ2UF36OJGZC6XW','9019c726-b9d4-4b6d-aba4-731a96b8e91f','+923545465521',0,0,NULL,1,0);
/*!40000 ALTER TABLE `aspnetusers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetusertokens`
--

DROP TABLE IF EXISTS `aspnetusertokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetusertokens` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetusertokens`
--

LOCK TABLES `aspnetusertokens` WRITE;
/*!40000 ALTER TABLE `aspnetusertokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetusertokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `assets`
--

DROP TABLE IF EXISTS `assets`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `assets` (
  `AssetlD` int NOT NULL AUTO_INCREMENT,
  `AssetName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SerialNumber` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Barcode` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Model` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Manufacturer` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedDate` datetime(6) NOT NULL,
  `UpdatedDate` datetime(6) NOT NULL,
  `PurchaseDate` datetime(6) NOT NULL,
  `PurchasePrice` decimal(65,30) NOT NULL,
  `CostPrice` decimal(65,30) NOT NULL,
  `DeletedByOrganization` tinyint(1) NOT NULL,
  `Location` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DepreciationRate` float NOT NULL,
  `Problem` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AssetIdentificationNumber` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `OrganizationID` int NOT NULL,
  `AssetStatusID` int NOT NULL,
  `AssetCatagoryID` int NOT NULL,
  `AssetTypeID` int NOT NULL,
  `ProfilePicturePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`AssetlD`),
  UNIQUE KEY `IX_Assets_AssetIdentificationNumber` (`AssetIdentificationNumber`),
  UNIQUE KEY `IX_Assets_SerialNumber` (`SerialNumber`),
  KEY `IX_Assets_AssetCatagoryID` (`AssetCatagoryID`),
  KEY `IX_Assets_AssetStatusID` (`AssetStatusID`),
  KEY `IX_Assets_AssetTypeID` (`AssetTypeID`),
  KEY `IX_Assets_OrganizationID` (`OrganizationID`),
  CONSTRAINT `FK_Assets_Organizations_OrganizationID` FOREIGN KEY (`OrganizationID`) REFERENCES `organizations` (`OrganizationID`) ON DELETE CASCADE,
  CONSTRAINT `FK_Assets_OrganizationsAssetCatagories_AssetCatagoryID` FOREIGN KEY (`AssetCatagoryID`) REFERENCES `organizationsassetcatagories` (`OrganizationsAssetCatagoryID`) ON DELETE CASCADE,
  CONSTRAINT `FK_Assets_OrganizationsAssetRequestStatuses_AssetStatusID` FOREIGN KEY (`AssetStatusID`) REFERENCES `organizationsassetrequeststatuses` (`OrganizationsAssetRequestStatusID`) ON DELETE CASCADE,
  CONSTRAINT `FK_Assets_OrganizationsAssetTypes_AssetTypeID` FOREIGN KEY (`AssetTypeID`) REFERENCES `organizationsassettypes` (`OrganizationsAssetTypeID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=79 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `assets`
--

LOCK TABLES `assets` WRITE;
/*!40000 ALTER TABLE `assets` DISABLE KEYS */;
INSERT INTO `assets` VALUES (1,'HP 840 G3','This is an asset with category laptop and type fixed.','123456789','8d55fa4bf0','840 G3',' Hewlett-Packard','2025-06-22 17:36:00.762894','2025-06-22 17:36:00.875646','2025-06-22 00:00:00.000000',40000.000000000000000000000000000000,40500.000000000000000000000000000000,0,'AssetIn Tower 1, Lahore',0.2,'','f3ef97b86103462fa41f6be9cd20b09b',1,1,1,1,''),(2,'iPhone 13 Pro','This is an asset with catagory modile and type fixed.','234567891','cec1fcede1','13 Pro',' Hewlett-Packard','2025-06-22 17:52:08.820835','2025-06-22 17:52:08.820835','2025-06-22 00:00:00.000000',150000.000000000000000000000000000000,150500.000000000000000000000000000000,0,'AssetIn Tower 1, Lahore',0.2,'','2d1535ebc189463d8354e68eb0800e9c',1,4,2,1,''),(3,'Lenovo Lcd','This is an LCD monitor from Lenovo, categorized as a fixed type.','345678912','6edd28725c','13 inch','Lenovo','2025-06-22 17:58:57.837153','2025-06-22 17:58:57.837626','2025-06-22 00:00:00.000000',3000.000000000000000000000000000000,3500.000000000000000000000000000000,0,'AssetIn Tower 1, Lahore',0.02,' ','ac8dade1d9694353a204cac601485df0',1,4,3,1,''),(4,'Systems Limited Stocks','trying to store variable assets','567891234','3a6ab657b9','small cap','Systems Limited','2025-06-22 18:04:17.923455','2025-06-22 18:04:17.923455','2025-06-22 00:00:00.000000',230.000000000000000000000000000000,230.000000000000000000000000000000,0,'AssetIn Tower 1, Lahore',0,'','944d38c117a4411c8c2de997f75f32fd',1,4,8,2,''),(5,'HP EliteBook 840 G3','Business laptop for development team.','SN12345601','ABC1234567','840 G3','HP','2025-06-22 10:00:00.000000','2025-06-22 10:00:00.000000','2024-06-15 00:00:00.000000',45000.000000000000000000000000000000,46000.000000000000000000000000000000,0,'Tower A, Floor 1',0.2,'','a1b2c3d4e5f601',1,4,1,1,''),(6,'Samsung Galaxy S21','Work phone for manager.','SN12345602','DEF2345678','S21','Samsung','2025-06-22 10:10:00.000000','2025-06-22 10:10:00.000000','2024-01-10 00:00:00.000000',120000.000000000000000000000000000000,121500.000000000000000000000000000000,0,'Tower A, Floor 2',0.3,'','a1b2c3d4e5f602',1,4,2,1,''),(7,'Dell UltraSharp U2722D','Monitor for design team.','SN12345603','GHI3456789','U2722D','Dell','2025-06-22 10:20:00.000000','2025-06-22 10:20:00.000000','2023-11-12 00:00:00.000000',70000.000000000000000000000000000000,71000.000000000000000000000000000000,0,'Tower B, Floor 3',0.25,'','a1b2c3d4e5f603',1,4,3,1,''),(8,'HP LaserJet Pro M404n','Printer for HR department.','SN12345604','JKL4567890','M404n','HP','2025-06-22 10:30:00.000000','2025-06-22 10:30:00.000000','2023-05-10 00:00:00.000000',40000.000000000000000000000000000000,40500.000000000000000000000000000000,0,'Tower B, Floor 1',0.15,'','a1b2c3d4e5f604',1,4,4,1,''),(9,'Logitech MX Master 3','Ergonomic mouse for CTO.','SN12345605','MNO5678901','MX Master 3','Logitech','2025-06-22 10:40:00.000000','2025-06-22 10:40:00.000000','2024-07-05 00:00:00.000000',18000.000000000000000000000000000000,18200.000000000000000000000000000000,0,'Tower C, Floor 4',0.3,'','a1b2c3d4e5f605',1,4,5,1,''),(10,'Keychron K6','Compact keyboard for developer.','SN12345606','PQR6789012','K6','Keychron','2025-06-22 10:50:00.000000','2025-06-22 10:50:00.000000','2023-08-20 00:00:00.000000',15000.000000000000000000000000000000,15200.000000000000000000000000000000,0,'Tower C, Floor 3',0.25,'','a1b2c3d4e5f606',1,4,6,1,''),(11,'Sony WH-1000XM4','Noise cancelling headset for support.','SN12345607','STU7890123','WH-1000XM4','Sony','2025-06-22 11:00:00.000000','2025-06-22 11:00:00.000000','2023-09-25 00:00:00.000000',55000.000000000000000000000000000000,56000.000000000000000000000000000000,0,'Tower D, Floor 2',0.2,'','a1b2c3d4e5f607',1,4,7,1,''),(12,'Lenovo ThinkPad X1','High performance laptop for finance.','SN12345608','VWX8901234','X1 Carbon','Lenovo','2025-06-22 11:10:00.000000','2025-06-22 11:10:00.000000','2024-02-18 00:00:00.000000',95000.000000000000000000000000000000,96000.000000000000000000000000000000,0,'Tower E, Floor 5',0.22,'','a1b2c3d4e5f608',1,4,1,1,''),(13,'iPhone 14 Pro','Work mobile for CEO.','SN12345609','YZA9012345','14 Pro','Apple','2025-06-22 11:20:00.000000','2025-06-22 11:20:00.000000','2024-12-01 00:00:00.000000',250000.000000000000000000000000000000,252000.000000000000000000000000000000,0,'Tower A, CEO Office',0.35,'','a1b2c3d4e5f609',1,4,2,1,''),(14,'LG 27UL500-W','Backup monitor for guest users.','SN12345610','BCD0123456','27UL500-W','LG','2025-06-22 11:30:00.000000','2025-06-22 11:30:00.000000','2022-10-14 00:00:00.000000',50000.000000000000000000000000000000,51000.000000000000000000000000000000,0,'Tower D, Floor 1',0.2,'','a1b2c3d4e5f610',1,4,3,1,''),(15,'HP ProBook 450 G8','ProBook for software testers.','SN12345711','EFG1234567','450 G8','HP','2025-06-22 12:01:00.000000','2025-06-22 12:01:00.000000','2024-03-10 00:00:00.000000',48000.000000000000000000000000000000,49000.000000000000000000000000000000,0,'Tower A, Lab 1',0.2,'','f1a2b3c4d5e611',1,4,1,1,''),(16,'Realme GT Neo 3','Test mobile for QA automation.','SN12345712','HIK2345678','GT Neo 3','Realme','2025-06-22 12:02:00.000000','2025-06-22 12:02:00.000000','2024-05-12 00:00:00.000000',89000.000000000000000000000000000000,90000.000000000000000000000000000000,0,'Tower B, Lab 2',0.28,'','f1a2b3c4d5e612',1,4,2,1,''),(17,'Samsung Smart Monitor M7','Smart monitor for design presentations.','SN12345713','LMN3456789','M7','Samsung','2025-06-22 12:03:00.000000','2025-06-22 12:03:00.000000','2024-08-22 00:00:00.000000',75000.000000000000000000000000000000,76500.000000000000000000000000000000,0,'Tower C, Conf Room',0.25,'','f1a2b3c4d5e613',1,4,3,1,''),(18,'Canon Pixma G3000','Ink printer for billing.','SN12345714','PQR4567890','G3000','Canon','2025-06-22 12:04:00.000000','2025-06-22 12:04:00.000000','2023-06-10 00:00:00.000000',30000.000000000000000000000000000000,31000.000000000000000000000000000000,0,'Tower B, Floor 1',0.18,'','f1a2b3c4d5e614',1,1,4,1,''),(19,'Logitech G102','Standard mouse for interns.','SN12345715','STU5678901','G102','Logitech','2025-06-22 12:05:00.000000','2025-06-22 12:05:00.000000','2023-09-05 00:00:00.000000',3000.000000000000000000000000000000,3200.000000000000000000000000000000,0,'Tower A, Intern Zone',0.35,'','f1a2b3c4d5e615',1,4,5,1,''),(20,'HP Wired Keyboard K1500','Spare keyboard for reception.','SN12345716','VWX6789012','K1500','HP','2025-06-22 12:06:00.000000','2025-06-22 12:06:00.000000','2024-01-01 00:00:00.000000',2500.000000000000000000000000000000,2600.000000000000000000000000000000,0,'Tower A, Front Desk',0.2,'','f1a2b3c4d5e616',1,4,6,1,''),(21,'Jabra Evolve2 65','Headset for remote calls.','SN12345717','YZA7890123','Evolve2 65','Jabra','2025-06-22 12:07:00.000000','2025-06-22 12:07:00.000000','2024-04-04 00:00:00.000000',32000.000000000000000000000000000000,33000.000000000000000000000000000000,0,'Tower D, Support Desk',0.2,'','f1a2b3c4d5e617',1,4,7,1,''),(22,'Dell Latitude 7420','Laptop for HR managers.','SN12345718','BCD8901234','Latitude 7420','Dell','2025-06-22 12:08:00.000000','2025-06-22 12:08:00.000000','2023-03-12 00:00:00.000000',98000.000000000000000000000000000000,99000.000000000000000000000000000000,0,'Tower E, Floor 2',0.2,'','f1a2b3c4d5e618',1,4,1,1,''),(23,'OnePlus 11R','Support team mobile device.','SN12345719','EFG9012345','11R','OnePlus','2025-06-22 12:09:00.000000','2025-06-22 12:09:00.000000','2023-07-01 00:00:00.000000',100000.000000000000000000000000000000,101500.000000000000000000000000000000,0,'Tower A, Support Room',0.25,'','f1a2b3c4d5e619',1,1,2,1,''),(24,'LG UltraGear 24GN600','Gaming monitor for rendering tests.','SN12345720','HIJ0123456','24GN600','LG','2025-06-22 12:10:00.000000','2025-06-22 12:10:00.000000','2023-02-21 00:00:00.000000',62000.000000000000000000000000000000,63500.000000000000000000000000000000,0,'Tower C, Lab 5',0.3,'','f1a2b3c4d5e620',1,4,3,1,''),(25,'Epson EcoTank L3150','Wireless printer for admin.','SN12345721','KLM1234567','L3150','Epson','2025-06-22 12:11:00.000000','2025-06-22 12:11:00.000000','2024-11-10 00:00:00.000000',28000.000000000000000000000000000000,28500.000000000000000000000000000000,0,'Tower D, Admin',0.15,'','f1a2b3c4d5e621',1,4,4,1,''),(26,'Redragon M601','Spare mouse for students.','SN12345722','NOP2345678','M601','Redragon','2025-06-22 12:12:00.000000','2025-06-22 12:12:00.000000','2023-06-06 00:00:00.000000',2200.000000000000000000000000000000,2300.000000000000000000000000000000,0,'Tower B, IT Lab',0.4,'','f1a2b3c4d5e622',1,4,5,1,''),(27,'Dell KB216','Wired keyboard for training.','SN12345723','QRS3456789','KB216','Dell','2025-06-22 12:13:00.000000','2025-06-22 12:13:00.000000','2023-01-05 00:00:00.000000',1900.000000000000000000000000000000,1950.000000000000000000000000000000,0,'Tower E, Training Hall',0.2,'','f1a2b3c4d5e623',1,4,6,1,''),(28,'HyperX Cloud II','Gaming headset for performance tests.','SN12345724','TUV4567890','Cloud II','HyperX','2025-06-22 12:14:00.000000','2025-06-22 12:14:00.000000','2024-10-22 00:00:00.000000',27000.000000000000000000000000000000,27500.000000000000000000000000000000,0,'Tower B, Dev Floor',0.2,'','f1a2b3c4d5e624',1,4,7,1,''),(29,'Acer Aspire 7','Spare laptop for trainings.','SN12345725','WXZ5678901','Aspire 7','Acer','2025-06-22 12:15:00.000000','2025-06-22 12:15:00.000000','2024-05-02 00:00:00.000000',70000.000000000000000000000000000000,71000.000000000000000000000000000000,0,'Tower C, Floor 2',0.2,'','f1a2b3c4d5e625',1,4,1,1,''),(30,'Vivo V27 Pro','Marketing team phone.','SN12345726','YAB6789012','V27 Pro','Vivo','2025-06-22 12:16:00.000000','2025-06-22 12:16:00.000000','2023-09-29 00:00:00.000000',89000.000000000000000000000000000000,90000.000000000000000000000000000000,0,'Tower D, Marketing',0.3,'','f1a2b3c4d5e626',1,4,2,1,''),(31,'BenQ PD2700U','Color-accurate monitor for graphics team.','SN12345727','CDE7890123','PD2700U','BenQ','2025-06-22 12:17:00.000000','2025-06-22 12:17:00.000000','2023-12-12 00:00:00.000000',85000.000000000000000000000000000000,86500.000000000000000000000000000000,0,'Tower E, Creative Room',0.22,'','f1a2b3c4d5e627',1,4,3,1,''),(32,'Brother HL-L2321D','Monochrome printer for logistics.','SN12345728','FGH8901234','HL-L2321D','Brother','2025-06-22 12:18:00.000000','2025-06-22 12:18:00.000000','2023-03-05 00:00:00.000000',36000.000000000000000000000000000000,37000.000000000000000000000000000000,0,'Tower C, Logistics',0.15,'','f1a2b3c4d5e628',1,4,4,1,''),(33,'Razer DeathAdder V2','Mouse for graphic designer.','SN12345729','IJK9012345','DeathAdder V2','Razer','2025-06-22 12:19:00.000000','2025-06-22 12:19:00.000000','2023-04-04 00:00:00.000000',9500.000000000000000000000000000000,9800.000000000000000000000000000000,0,'Tower A, Design Studio',0.3,'','f1a2b3c4d5e629',1,4,5,1,''),(34,'Microsoft Sculpt Ergonomic','Keyboard for ergonomics test.','SN12345730','LMN0123456','Sculpt Ergonomic','Microsoft','2025-06-22 12:20:00.000000','2025-06-22 12:20:00.000000','2024-11-11 00:00:00.000000',16000.000000000000000000000000000000,16500.000000000000000000000000000000,0,'Tower D, Ergonomics',0.2,'','f1a2b3c4d5e630',1,4,6,1,''),(35,'Computer Table Ikea','This is an asset with type vairable nad catagory office furniture.','12398745','44c6807380','variable top','Ikea','2025-06-25 06:26:44.205737','2025-06-25 06:26:44.205913','2025-06-25 00:00:00.000000',10000.000000000000000000000000000000,10500.000000000000000000000000000000,0,'AssetIn Tower 1, first floor',2,'','abac21d6590d4d79855bbd9f6c22b7b1',1,4,9,2,NULL),(36,'Computer Table Ikea','some description','111111111','302393df5c','Fixed Top','IKEA','2025-06-25 08:58:55.981005','2025-06-25 08:58:55.981111','2025-06-25 00:00:00.000000',7000.000000000000000000000000000000,7500.000000000000000000000000000000,0,'AssetInTower 1, Second Floot.',2,'No Issue Reported.','864f765435ed40c19d88c16e22b2e79a',1,4,9,2,'https://res.cloudinary.com/do4mdspjg/image/upload/v1750841935/ga9mzdyxrnfetae4n1he.jpg'),(37,'Microsoft Office 2013 Enterprise Licence','This is a variable asset with cata gory as software licence. Did some changes in teh description to check update asset Functionality.','456789321','0f56977a78','2013 Enterprise','Microsoft','2025-06-25 09:06:20.891908','2025-06-27 15:00:39.279291','2025-06-24 00:00:00.000000',2000.000000000000000000000000000000,2000.000000000000000000000000000000,0,'AssetInTower 1, Second Floot.',0,'No Issue Reported.','9fb429ee93234de596cb1d6ed53cca74',1,4,10,2,'https://res.cloudinary.com/do4mdspjg/image/upload/v1750842380/zn1fnjpfgom4o2nxdfsa.jpg'),(38,'Dell Latitude 5540','Business-grade laptop with Intel i7, 16GB RAM','DL-JAN-001','BC-JAN-001','Latitude 5540','Dell','2025-01-05 00:00:00.000000','2025-01-05 00:00:00.000000','2025-01-01 00:00:00.000000',1200.000000000000000000000000000000,1100.000000000000000000000000000000,0,'Head Office - IT Room',15,'No Issue Reported.','AIN-JAN-001',1,4,1,1,''),(39,'HP EliteBook 845 G9','Lightweight laptop for mobile professionals','HP-JAN-002','BC-JAN-002','EliteBook 845 G9','HP','2025-01-10 00:00:00.000000','2025-01-10 00:00:00.000000','2025-01-08 00:00:00.000000',1350.000000000000000000000000000000,1250.000000000000000000000000000000,0,'Branch Office - Room 204',15,'No Issue Reported.','AIN-JAN-002',1,4,1,1,''),(40,'Apple iPhone 14','Company mobile for field team','APL-MAR-001','BC-MAR-001','iPhone 14','Apple','2025-03-05 00:00:00.000000','2025-03-05 00:00:00.000000','2025-03-01 00:00:00.000000',999.000000000000000000000000000000,950.000000000000000000000000000000,0,'Head Office - Reception',20,'No Issue Reported.','AIN-MAR-001',1,4,2,2,''),(41,'Samsung Galaxy S23','Android phone for operations','SS-MAR-002','BC-MAR-002','Galaxy S23','Samsung','2025-03-06 00:00:00.000000','2025-03-06 00:00:00.000000','2025-03-03 00:00:00.000000',899.000000000000000000000000000000,870.000000000000000000000000000000,0,'Operations Department',20,'No Issue Reported.','AIN-MAR-002',1,4,2,2,''),(42,'LG UltraFine 27\"','27-inch high-res monitor for design team','LG-MAR-003','BC-MAR-003','UltraFine 27UL850','LG','2025-03-08 00:00:00.000000','2025-03-08 00:00:00.000000','2025-03-06 00:00:00.000000',500.000000000000000000000000000000,480.000000000000000000000000000000,0,'Design Studio',12,'No Issue Reported.','AIN-MAR-003',1,4,3,1,''),(43,'HP LaserJet Pro M404','Black & white office printer','HP-MAR-004','BC-MAR-004','LaserJet Pro M404dn','HP','2025-03-10 00:00:00.000000','2025-03-10 00:00:00.000000','2025-03-08 00:00:00.000000',320.000000000000000000000000000000,310.000000000000000000000000000000,0,'Admin Office - Room 3A',10,'No Issue Reported.','AIN-MAR-004',1,4,4,1,''),(44,'Logitech MX Master 3','Wireless mouse with ergonomic design','LG-MAR-005','BC-MAR-005','MX Master 3','Logitech','2025-03-11 00:00:00.000000','2025-03-11 00:00:00.000000','2025-03-10 00:00:00.000000',99.000000000000000000000000000000,95.000000000000000000000000000000,0,'IT Inventory',30,'No Issue Reported.','AIN-MAR-005',1,4,5,2,''),(45,'Microsoft Ergonomic Keyboard','Split keyboard for comfort typing','MS-MAR-006','BC-MAR-006','Ergo Keyboard 4000','Microsoft','2025-03-13 00:00:00.000000','2025-03-13 00:00:00.000000','2025-03-12 00:00:00.000000',75.000000000000000000000000000000,70.000000000000000000000000000000,0,'Head Office - Support',25,'No Issue Reported.','AIN-MAR-006',1,4,6,2,''),(46,'Sony WH-1000XM5','Noise cancelling headset for calls','SN-APR-001','BC-APR-001','WH-1000XM5','Sony','2025-04-02 00:00:00.000000','2025-04-02 00:00:00.000000','2025-04-01 00:00:00.000000',350.000000000000000000000000000000,340.000000000000000000000000000000,0,'Conference Room',18,'No Issue Reported.','AIN-APR-001',1,4,7,2,''),(47,'Office Desk - Walnut Finish','Executive desk with storage','FD-APR-002','BC-APR-002','ExecutiveDeskX','Ikea','2025-04-05 00:00:00.000000','2025-04-05 00:00:00.000000','2025-04-03 00:00:00.000000',450.000000000000000000000000000000,430.000000000000000000000000000000,0,'CEO Office',10,'No Issue Reported.','AIN-APR-002',1,4,9,1,''),(48,'Microsoft 365 E3 License','Office productivity suite annual license','MS-APR-003','BC-APR-003','M365-E3','Microsoft','2025-04-08 00:00:00.000000','2025-04-08 00:00:00.000000','2025-04-05 00:00:00.000000',200.000000000000000000000000000000,200.000000000000000000000000000000,0,'Digital Assets',0,'No Issue Reported.','AIN-APR-003',1,4,10,1,''),(49,'Apple MacBook Pro 16\"','High-end MacBook for video editing and dev','MBP-JAN-003','BC-JAN-003','MacBook Pro M3 Max','Apple','2025-01-12 00:00:00.000000','2025-01-12 00:00:00.000000','2025-01-10 00:00:00.000000',3200.000000000000000000000000000000,3100.000000000000000000000000000000,0,'Creative Studio',10,'No Issue Reported.','AIN-JAN-003',1,4,1,1,''),(50,'Lenovo ThinkPad P1 Gen 6','Mobile workstation for engineers','LEN-JAN-004','BC-JAN-004','ThinkPad P1 Gen 6','Lenovo','2025-01-20 00:00:00.000000','2025-01-20 00:00:00.000000','2025-01-18 00:00:00.000000',2800.000000000000000000000000000000,2700.000000000000000000000000000000,0,'Engineering Department',12,'No Issue Reported.','AIN-JAN-004',1,4,1,1,''),(51,'Samsung Galaxy Z Fold5','Foldable phone for executives','SS-MAR-007','BC-MAR-007','Galaxy Z Fold5','Samsung','2025-03-04 00:00:00.000000','2025-03-04 00:00:00.000000','2025-03-02 00:00:00.000000',1800.000000000000000000000000000000,1750.000000000000000000000000000000,0,'Executive Lounge',18,'No Issue Reported.','AIN-MAR-007',1,1,2,2,''),(52,'Apple iPhone 15 Pro Max','Flagship mobile for directors','APL-MAR-008','BC-MAR-008','iPhone 15 Pro Max','Apple','2025-03-06 00:00:00.000000','2025-03-06 00:00:00.000000','2025-03-04 00:00:00.000000',1600.000000000000000000000000000000,1550.000000000000000000000000000000,0,'Director’s Office',20,'No Issue Reported.','AIN-MAR-008',1,4,2,2,''),(53,'Eizo ColorEdge CG319X','Color-accurate 32\" monitor for creatives','EZ-MAR-009','BC-MAR-009','ColorEdge CG319X','Eizo','2025-03-09 00:00:00.000000','2025-03-09 00:00:00.000000','2025-03-07 00:00:00.000000',2400.000000000000000000000000000000,2300.000000000000000000000000000000,0,'Design Lab',10,'No Issue Reported.','AIN-MAR-009',1,4,3,1,''),(54,'HP Color LaserJet Enterprise MFP','Advanced network printer','HP-MAR-010','BC-MAR-010','Color LaserJet M776dn','HP','2025-03-11 00:00:00.000000','2025-03-11 00:00:00.000000','2025-03-09 00:00:00.000000',2200.000000000000000000000000000000,2150.000000000000000000000000000000,0,'Admin Dept.',12,'No Issue Reported.','AIN-MAR-010',1,4,4,1,''),(55,'Logitech MX Master 3S','Premium ergonomic wireless mouse','LG-MAR-011','BC-MAR-011','MX Master 3S','Logitech','2025-03-13 00:00:00.000000','2025-03-13 00:00:00.000000','2025-03-12 00:00:00.000000',149.000000000000000000000000000000,140.000000000000000000000000000000,0,'Tech Room',15,'No Issue Reported.','AIN-MAR-011',1,4,5,2,''),(56,'Keychron Q6 Pro','High-end mechanical keyboard','KCH-MAR-012','BC-MAR-012','Q6 Pro','Keychron','2025-03-14 00:00:00.000000','2025-03-14 00:00:00.000000','2025-03-13 00:00:00.000000',200.000000000000000000000000000000,190.000000000000000000000000000000,0,'Security Office',20,'No Issue Reported.','AIN-MAR-012',1,4,6,2,''),(57,'Bose QuietComfort Ultra','Top-tier headset for noise cancellation','BOSE-APR-004','BC-APR-004','QC Ultra','Bose','2025-04-03 00:00:00.000000','2025-04-03 00:00:00.000000','2025-04-01 00:00:00.000000',450.000000000000000000000000000000,430.000000000000000000000000000000,0,'Audio Studio',10,'No Issue Reported.','AIN-APR-004',1,4,7,2,''),(58,'Modular Standing Desk Pro','Smart adjustable office desk','FD-APR-005','BC-APR-005','SmartDesk Pro','Autonomous','2025-04-07 00:00:00.000000','2025-04-07 00:00:00.000000','2025-04-06 00:00:00.000000',950.000000000000000000000000000000,920.000000000000000000000000000000,0,'R&D Office',10,'No Issue Reported.','AIN-APR-005',1,4,9,1,''),(59,'Adobe Creative Cloud - Enterprise','All-access annual creative suite license','ADOBE-APR-006','BC-APR-006','Creative Cloud Ent.','Adobe','2025-04-09 00:00:00.000000','2025-04-09 00:00:00.000000','2025-04-08 00:00:00.000000',1200.000000000000000000000000000000,1200.000000000000000000000000000000,0,'Design Team - Software Vault',0,'No Issue Reported.','AIN-APR-006',1,4,10,1,''),(67,'Apple MacBook Pro 16\"','High-end MacBook for video editing and dev','c8a9f8b9b0','BC-JAN-003','MacBook Pro M3 Max','Apple','2025-01-12 00:00:00.000000','2025-01-12 00:00:00.000000','2025-01-10 00:00:00.000000',9600.000000000000000000000000000000,9300.000000000000000000000000000000,0,'Creative Studio',10,'No Issue Reported.','c8a9f8b9b07d4e5b9a8c78c86b8398d1',1,4,1,1,''),(68,'Lenovo ThinkPad P1 Gen 6','Mobile workstation for engineers','9c2e472f30','BC-JAN-004','ThinkPad P1 Gen 6','Lenovo','2025-01-20 00:00:00.000000','2025-01-20 00:00:00.000000','2025-01-18 00:00:00.000000',8400.000000000000000000000000000000,8100.000000000000000000000000000000,0,'Engineering Department',12,'No Issue Reported.','9c2e472f303a4a5db3e7a2921dbeffea',1,4,1,1,''),(69,'Samsung Galaxy Z Fold5','Foldable phone for executives','69b6fef8bc','BC-MAR-007','Galaxy Z Fold5','Samsung','2025-03-04 00:00:00.000000','2025-03-04 00:00:00.000000','2025-03-02 00:00:00.000000',5400.000000000000000000000000000000,5250.000000000000000000000000000000,0,'Executive Lounge',18,'No Issue Reported.','69b6fef8bc8643fc97c5878c2285e93a',1,4,2,2,''),(70,'Apple iPhone 15 Pro Max','Flagship mobile for directors','b22e245ed3','BC-MAR-008','iPhone 15 Pro Max','Apple','2025-03-06 00:00:00.000000','2025-03-06 00:00:00.000000','2025-03-04 00:00:00.000000',4800.000000000000000000000000000000,4650.000000000000000000000000000000,0,'Director’s Office',20,'No Issue Reported.','b22e245ed3404f99a6b8b2540a9c0e73',1,4,2,2,''),(71,'Eizo ColorEdge CG319X','Color-accurate 32\" monitor for creatives','05b7463c28','BC-MAR-009','ColorEdge CG319X','Eizo','2025-03-09 00:00:00.000000','2025-03-09 00:00:00.000000','2025-03-07 00:00:00.000000',7200.000000000000000000000000000000,6900.000000000000000000000000000000,0,'Design Lab',10,'No Issue Reported.','05b7463c288241eb8326db238eb8b623',1,4,3,1,''),(72,'HP Color LaserJet Enterprise MFP','Advanced network printer','1b9db86e99','BC-MAR-010','Color LaserJet M776dn','HP','2025-03-11 00:00:00.000000','2025-03-11 00:00:00.000000','2025-03-09 00:00:00.000000',6600.000000000000000000000000000000,6450.000000000000000000000000000000,0,'Admin Dept.',12,'No Issue Reported.','1b9db86e99c142cf85c90373c1016e3c',1,4,4,1,''),(73,'Logitech MX Master 3S','Premium ergonomic wireless mouse','7b43ad6b0a','BC-MAR-011','MX Master 3S','Logitech','2025-03-13 00:00:00.000000','2025-03-13 00:00:00.000000','2025-03-12 00:00:00.000000',447.000000000000000000000000000000,420.000000000000000000000000000000,0,'Tech Room',15,'No Issue Reported.','7b43ad6b0ab3403092e5dba10b7b9b0b',1,4,5,2,''),(74,'Keychron Q6 Pro','High-end mechanical keyboard','0d7675c451','BC-MAR-012','Q6 Pro','Keychron','2025-03-14 00:00:00.000000','2025-03-14 00:00:00.000000','2025-03-13 00:00:00.000000',600.000000000000000000000000000000,570.000000000000000000000000000000,0,'Security Office',20,'No Issue Reported.','0d7675c451cd4d3bb6fbb0c46a820d74',1,4,6,2,''),(75,'Bose QuietComfort Ultra','Top-tier headset for noise cancellation','2d497c4db1','BC-APR-004','QC Ultra','Bose','2025-04-03 00:00:00.000000','2025-04-03 00:00:00.000000','2025-04-01 00:00:00.000000',1350.000000000000000000000000000000,1290.000000000000000000000000000000,0,'Audio Studio',10,'No Issue Reported.','2d497c4db1d14a02ac9c6c61f6430833',1,4,7,2,''),(76,'Modular Standing Desk Pro','Smart adjustable office desk','3b7e0637c9','BC-APR-005','SmartDesk Pro','Autonomous','2025-04-07 00:00:00.000000','2025-04-07 00:00:00.000000','2025-04-06 00:00:00.000000',2850.000000000000000000000000000000,2760.000000000000000000000000000000,0,'R&D Office',10,'No Issue Reported.','3b7e0637c94b46aab83f8f9876fe85fc',1,4,9,1,''),(77,'Adobe Creative Cloud - Enterprise','All-access annual creative suite license','51cb6f848f','BC-APR-006','Creative Cloud Ent.','Adobe','2025-04-09 00:00:00.000000','2025-04-09 00:00:00.000000','2025-04-08 00:00:00.000000',3600.000000000000000000000000000000,3600.000000000000000000000000000000,0,'Design Team - Software Vault',0,'No Issue Reported.','51cb6f848f304d0fa62c4ccbe27fba7b',1,4,10,1,''),(78,'name','some description','913782465','a124bb6862','g3','burhan','2025-06-26 15:42:15.970918','2025-06-26 15:42:15.971061','2025-06-26 00:00:00.000000',200.000000000000000000000000000000,200.000000000000000000000000000000,1,'some place',2,'No Issue Reported.','0eb96114a57847c6b10d7162a6d6d1c3',1,4,1,1,'https://res.cloudinary.com/do4mdspjg/image/upload/v1750952535/wtznu4ylmwf0cq0gremd.jpg');
/*!40000 ALTER TABLE `assets` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organizations`
--

DROP TABLE IF EXISTS `organizations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `organizations` (
  `OrganizationID` int NOT NULL AUTO_INCREMENT,
  `OrganizationLogo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `OrganizationName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedDate` datetime(6) NOT NULL,
  `ActiveOrganization` tinyint(1) NOT NULL,
  `OrganizationDomain` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`OrganizationID`),
  KEY `IX_Organizations_UserID` (`UserID`),
  CONSTRAINT `FK_Organizations_AspNetUsers_UserID` FOREIGN KEY (`UserID`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizations`
--

LOCK TABLES `organizations` WRITE;
/*!40000 ALTER TABLE `organizations` DISABLE KEYS */;
INSERT INTO `organizations` VALUES (1,'https://res.cloudinary.com/do4mdspjg/image/upload/v1750970181/htpusq2mkt1tkb2qmlw6.jpg','AssetIn','This is a test organization.','2025-06-22 15:00:33.403701',1,'@assetin.test','8d141bb6-21b5-42b1-b42a-dde9668f2cb6');
/*!40000 ALTER TABLE `organizations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organizationsassetassignreturns`
--

DROP TABLE IF EXISTS `organizationsassetassignreturns`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `organizationsassetassignreturns` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `AssignedAt` datetime(6) NOT NULL,
  `ReturnedAt` datetime(6) NOT NULL,
  `Notes` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AssignedToUserID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AssignedByUserID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CheckInByUserID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `CheckInNotes` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AssetID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_OrganizationsAssetAssignReturns_AssetID` (`AssetID`),
  KEY `IX_OrganizationsAssetAssignReturns_AssignedByUserID` (`AssignedByUserID`),
  KEY `IX_OrganizationsAssetAssignReturns_AssignedToUserID` (`AssignedToUserID`),
  KEY `IX_OrganizationsAssetAssignReturns_CheckInByUserID` (`CheckInByUserID`),
  CONSTRAINT `FK_OrganizationsAssetAssignReturns_AspNetUsers_AssignedByUserID` FOREIGN KEY (`AssignedByUserID`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_OrganizationsAssetAssignReturns_AspNetUsers_AssignedToUserID` FOREIGN KEY (`AssignedToUserID`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_OrganizationsAssetAssignReturns_AspNetUsers_CheckInByUserID` FOREIGN KEY (`CheckInByUserID`) REFERENCES `aspnetusers` (`Id`),
  CONSTRAINT `FK_OrganizationsAssetAssignReturns_Assets_AssetID` FOREIGN KEY (`AssetID`) REFERENCES `assets` (`AssetlD`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationsassetassignreturns`
--

LOCK TABLES `organizationsassetassignreturns` WRITE;
/*!40000 ALTER TABLE `organizationsassetassignreturns` DISABLE KEYS */;
INSERT INTO `organizationsassetassignreturns` VALUES (1,'2025-06-23 21:39:55.397208','0001-01-01 00:00:00.000000','burthan is a good boy.','8d141bb6-21b5-42b1-b42a-dde9668f2cb6','8d141bb6-21b5-42b1-b42a-dde9668f2cb6',NULL,'',1),(2,'2025-06-27 00:31:11.557968','0001-01-01 00:00:00.000000','dome notes','8d141bb6-21b5-42b1-b42a-dde9668f2cb6','8d141bb6-21b5-42b1-b42a-dde9668f2cb6',NULL,'',51),(3,'2025-06-27 00:31:54.912511','0001-01-01 00:00:00.000000','thedntgn','8d141bb6-21b5-42b1-b42a-dde9668f2cb6','8d141bb6-21b5-42b1-b42a-dde9668f2cb6',NULL,'',18),(4,'2025-06-27 00:32:19.001888','0001-01-01 00:00:00.000000','jnrxn','8d141bb6-21b5-42b1-b42a-dde9668f2cb6','8d141bb6-21b5-42b1-b42a-dde9668f2cb6',NULL,'',23);
/*!40000 ALTER TABLE `organizationsassetassignreturns` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organizationsassetcatagories`
--

DROP TABLE IF EXISTS `organizationsassetcatagories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `organizationsassetcatagories` (
  `OrganizationsAssetCatagoryID` int NOT NULL AUTO_INCREMENT,
  `OrganizationsAssetCatagoryName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `OrganizationsID` int NOT NULL,
  PRIMARY KEY (`OrganizationsAssetCatagoryID`),
  KEY `IX_OrganizationsAssetCatagories_OrganizationsID` (`OrganizationsID`),
  CONSTRAINT `FK_OrganizationsAssetCatagories_Organizations_OrganizationsID` FOREIGN KEY (`OrganizationsID`) REFERENCES `organizations` (`OrganizationID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationsassetcatagories`
--

LOCK TABLES `organizationsassetcatagories` WRITE;
/*!40000 ALTER TABLE `organizationsassetcatagories` DISABLE KEYS */;
INSERT INTO `organizationsassetcatagories` VALUES (1,'Laptop',1),(2,'Mobile',1),(3,'Moniter',1),(4,'Printer',1),(5,'Mouse',1),(6,'Keyboard',1),(7,'Headphone',1),(8,'stocks',1),(9,'Office Furniture',1),(10,'Software Licences',1);
/*!40000 ALTER TABLE `organizationsassetcatagories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organizationsassetmaintanences`
--

DROP TABLE IF EXISTS `organizationsassetmaintanences`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `organizationsassetmaintanences` (
  `OrganizationsAssetMaintanenceID` int NOT NULL AUTO_INCREMENT,
  `SentDate` datetime(6) NOT NULL,
  `RetunDate` datetime(6) NOT NULL,
  `Problem` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `MaintanenceResult` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AssetID` int NOT NULL,
  PRIMARY KEY (`OrganizationsAssetMaintanenceID`),
  KEY `IX_OrganizationsAssetMaintanences_AssetID` (`AssetID`),
  CONSTRAINT `FK_OrganizationsAssetMaintanences_Assets_AssetID` FOREIGN KEY (`AssetID`) REFERENCES `assets` (`AssetlD`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationsassetmaintanences`
--

LOCK TABLES `organizationsassetmaintanences` WRITE;
/*!40000 ALTER TABLE `organizationsassetmaintanences` DISABLE KEYS */;
/*!40000 ALTER TABLE `organizationsassetmaintanences` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organizationsassetrequests`
--

DROP TABLE IF EXISTS `organizationsassetrequests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `organizationsassetrequests` (
  `OrganizationsAssetRequestID` int NOT NULL AUTO_INCREMENT,
  `Title` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RequestDate` datetime(6) NOT NULL,
  `RequestStatus` int NOT NULL,
  `RequestProcessedDate` datetime(6) NOT NULL,
  `CompletionStatus` tinyint(1) NOT NULL,
  `RequestCompletedDate` datetime(6) NOT NULL,
  `UserID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AssetAssignmentId` int DEFAULT NULL,
  `OrganizationID` int NOT NULL,
  PRIMARY KEY (`OrganizationsAssetRequestID`),
  KEY `IX_OrganizationsAssetRequests_AssetAssignmentId` (`AssetAssignmentId`),
  KEY `IX_OrganizationsAssetRequests_OrganizationID` (`OrganizationID`),
  KEY `IX_OrganizationsAssetRequests_UserID` (`UserID`),
  CONSTRAINT `FK_OrganizationsAssetRequests_AspNetUsers_UserID` FOREIGN KEY (`UserID`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_OrganizationsAssetRequests_Organizations_OrganizationID` FOREIGN KEY (`OrganizationID`) REFERENCES `organizations` (`OrganizationID`) ON DELETE CASCADE,
  CONSTRAINT `FK_OrganizationsAssetRequests_OrganizationsAssetAssignReturns_A~` FOREIGN KEY (`AssetAssignmentId`) REFERENCES `organizationsassetassignreturns` (`ID`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationsassetrequests`
--

LOCK TABLES `organizationsassetrequests` WRITE;
/*!40000 ALTER TABLE `organizationsassetrequests` DISABLE KEYS */;
INSERT INTO `organizationsassetrequests` VALUES (1,'Need a mouse pad','Need a mouse pad','2025-06-22 15:02:13.798406',5,'0001-01-01 00:00:00.000000',0,'0001-01-01 00:00:00.000000','8d141bb6-21b5-42b1-b42a-dde9668f2cb6',NULL,1),(2,'Need a mouse pad','Need a mouse padNeed a mouse padNeed a mouse padNeed a mouse padNeed a mouse padNeed a mouse pad','2025-06-22 15:02:53.643518',3,'2025-06-22 15:03:32.502052',0,'0001-01-01 00:00:00.000000','8d141bb6-21b5-42b1-b42a-dde9668f2cb6',NULL,1),(3,'request form keyboard','need a full size external keyboard from my laptop.','2025-06-22 15:04:32.530226',3,'2025-06-22 15:05:12.975215',0,'0001-01-01 00:00:00.000000','4455b62a-0ab0-4ce3-9ed9-f009f2c6c4ee',NULL,1),(4,'Request for LCD Moniter','I need LCD moniter as my laptop screeen is not enought fro my daily multitasking adn development requirments.','2025-06-22 18:12:32.491903',4,'2025-06-23 13:43:24.386487',1,'2025-06-23 21:40:15.020520','8d141bb6-21b5-42b1-b42a-dde9668f2cb6',1,1),(5,'requets for phone','i need a good phone with a good camera for company\'s marketing work','2025-06-26 18:45:45.639319',2,'0001-01-01 00:00:00.000000',0,'0001-01-01 00:00:00.000000','6ac0619d-85b7-46bb-9fee-56451de3c190',NULL,1),(6,'test request ','soem request here','2025-06-26 19:20:53.236204',4,'2025-06-26 19:31:35.562390',1,'2025-06-27 00:32:19.052797','8d141bb6-21b5-42b1-b42a-dde9668f2cb6',4,1),(7,'some request','some requestsome requestsome requestsome requestsome requestsome request','2025-06-26 19:21:08.929097',4,'2025-06-26 19:31:17.515208',1,'2025-06-27 00:31:54.946556','8d141bb6-21b5-42b1-b42a-dde9668f2cb6',3,1),(8,'some requestsome request','some requestsome requestsome requestsome requestsome requestsome requestsome requestsome requestsome requestsome requestsome requestsome requestsome requestsome requestsome requestsome requestsome requestsome requestsome requestsome requestsome requestsome request','2025-06-26 19:21:17.190431',4,'2025-06-26 19:30:50.150225',1,'2025-06-27 00:31:11.702685','8d141bb6-21b5-42b1-b42a-dde9668f2cb6',2,1);
/*!40000 ALTER TABLE `organizationsassetrequests` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organizationsassetrequeststatuses`
--

DROP TABLE IF EXISTS `organizationsassetrequeststatuses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `organizationsassetrequeststatuses` (
  `OrganizationsAssetRequestStatusID` int NOT NULL AUTO_INCREMENT,
  `OrganizationsAssetRequestStatusName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`OrganizationsAssetRequestStatusID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationsassetrequeststatuses`
--

LOCK TABLES `organizationsassetrequeststatuses` WRITE;
/*!40000 ALTER TABLE `organizationsassetrequeststatuses` DISABLE KEYS */;
INSERT INTO `organizationsassetrequeststatuses` VALUES (1,'Accepted'),(2,'Pending'),(3,'Declined'),(4,'Fulfilled'),(5,'Canceled');
/*!40000 ALTER TABLE `organizationsassetrequeststatuses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organizationsassetretirements`
--

DROP TABLE IF EXISTS `organizationsassetretirements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `organizationsassetretirements` (
  `OrganizationsAssetRetirementID` int NOT NULL AUTO_INCREMENT,
  `RetirementReason` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RetirementDate` datetime(6) NOT NULL,
  `Condition` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AssetID` int NOT NULL,
  PRIMARY KEY (`OrganizationsAssetRetirementID`),
  KEY `IX_OrganizationsAssetRetirements_AssetID` (`AssetID`),
  CONSTRAINT `FK_OrganizationsAssetRetirements_Assets_AssetID` FOREIGN KEY (`AssetID`) REFERENCES `assets` (`AssetlD`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationsassetretirements`
--

LOCK TABLES `organizationsassetretirements` WRITE;
/*!40000 ALTER TABLE `organizationsassetretirements` DISABLE KEYS */;
/*!40000 ALTER TABLE `organizationsassetretirements` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organizationsassetstatuses`
--

DROP TABLE IF EXISTS `organizationsassetstatuses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `organizationsassetstatuses` (
  `OrganizationsAssetStatusID` int NOT NULL AUTO_INCREMENT,
  `OrganizationsAssetStatusName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`OrganizationsAssetStatusID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationsassetstatuses`
--

LOCK TABLES `organizationsassetstatuses` WRITE;
/*!40000 ALTER TABLE `organizationsassetstatuses` DISABLE KEYS */;
INSERT INTO `organizationsassetstatuses` VALUES (1,'Assigned'),(2,'Retired'),(3,'Under Maintenance'),(4,'Available');
/*!40000 ALTER TABLE `organizationsassetstatuses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organizationsassettypes`
--

DROP TABLE IF EXISTS `organizationsassettypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `organizationsassettypes` (
  `OrganizationsAssetTypeID` int NOT NULL AUTO_INCREMENT,
  `OrganizationsAssetTypeName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `OrganizationsID` int NOT NULL,
  PRIMARY KEY (`OrganizationsAssetTypeID`),
  KEY `IX_OrganizationsAssetTypes_OrganizationsID` (`OrganizationsID`),
  CONSTRAINT `FK_OrganizationsAssetTypes_Organizations_OrganizationsID` FOREIGN KEY (`OrganizationsID`) REFERENCES `organizations` (`OrganizationID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationsassettypes`
--

LOCK TABLES `organizationsassettypes` WRITE;
/*!40000 ALTER TABLE `organizationsassettypes` DISABLE KEYS */;
INSERT INTO `organizationsassettypes` VALUES (1,'Fixed',1),(2,'Variable',1);
/*!40000 ALTER TABLE `organizationsassettypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vendorproducts`
--

DROP TABLE IF EXISTS `vendorproducts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vendorproducts` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ProductName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Price` decimal(65,30) NOT NULL,
  `Model` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `VendorID` int NOT NULL,
  `ProductImage` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`ID`),
  KEY `IX_VendorProducts_VendorID` (`VendorID`),
  CONSTRAINT `FK_VendorProducts_Vendors_VendorID` FOREIGN KEY (`VendorID`) REFERENCES `vendors` (`VendorID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vendorproducts`
--

LOCK TABLES `vendorproducts` WRITE;
/*!40000 ALTER TABLE `vendorproducts` DISABLE KEYS */;
INSERT INTO `vendorproducts` VALUES (1,'bruahn','sxdcfvghb',415.000000000000000000000000000000,'840 G3',1,'https://res.cloudinary.com/do4mdspjg/image/upload/v1750724013/jx5bkafcadi5jkcmhpif.jpg'),(2,'Hp 450 G3','Best for home use.',65000.000000000000000000000000000000,'g3',1,''),(3,'Hp 450 G3','Best for home use.',65000.000000000000000000000000000000,'g3',1,''),(4,'Hp 450 G3','Best for home use.',65000.000000000000000000000000000000,'g3',1,'');
/*!40000 ALTER TABLE `vendorproducts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vendors`
--

DROP TABLE IF EXISTS `vendors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vendors` (
  `VendorID` int NOT NULL AUTO_INCREMENT,
  `VendorName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `OfficeAddress` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ContactPerson` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Status` tinyint(1) NOT NULL,
  `UserID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProfilePicturePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`VendorID`),
  UNIQUE KEY `IX_Vendors_Email` (`Email`),
  KEY `IX_Vendors_UserID` (`UserID`),
  CONSTRAINT `FK_Vendors_AspNetUsers_UserID` FOREIGN KEY (`UserID`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vendors`
--

LOCK TABLES `vendors` WRITE;
/*!40000 ALTER TABLE `vendors` DISABLE KEYS */;
INSERT INTO `vendors` VALUES (1,'SW Computers','Hafeeez center, 3rd floor, shor 40.','+923004653232','swcomputers@somedomain.com','Idrees',1,'493b4fb3-89f2-4585-ae4a-dc6468b67f07','https://res.cloudinary.com/do4mdspjg/image/upload/v1750898997/dwzq61zzpxdcmahwsmgi.jpg');
/*!40000 ALTER TABLE `vendors` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-27 23:39:08
