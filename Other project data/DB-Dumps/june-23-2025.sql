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
INSERT INTO `__efmigrationshistory` VALUES ('20250622145455_RemovedAllMigrationsAndCreatedOne','8.0.10'),('20250622192219_RemovedVendorProcurementDetailsTableAddedVendorProductsTable','8.0.10'),('20250622192539_AddedProductImageCOlumnInVendorProducttable','8.0.10');
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
INSERT INTO `aspnetroles` VALUES ('1','OrganizationOwner','ORGANIZATIONOWNER','cc3ad06b-7160-44a5-93b5-195321e3d89b'),('2','OrganizationEmployee','ORGANIZATIONEMPLOYEE','f5cd44a9-7cb4-4612-988a-4a9bbc667e43'),('3','OrganizationAssetManager','ORGANIZATIONASSETMANAGER','22ec94a9-a17f-4526-850f-ba394e75a388'),('4','Vendor','VENDOR','1ffda9d9-c29f-4fe2-bd82-555f54c77396');
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
INSERT INTO `aspnetuserroles` VALUES ('8d141bb6-21b5-42b1-b42a-dde9668f2cb6','1'),('0069ab7a-2359-4331-90af-965bcb4a0f7b','2'),('0b046f72-8bee-4da2-b638-44d344509c59','2'),('28e6f14b-b087-4bff-8d4b-7bda035685ec','2'),('4455b62a-0ab0-4ce3-9ed9-f009f2c6c4ee','2'),('5a541c31-2d73-43a8-8f44-933ebe87a699','2'),('686c44a1-0955-4ec0-99a9-685852dee2e8','2'),('72351ab4-6944-4570-a50e-89234d492250','2'),('a108227d-a8c3-467d-8d54-778ff5cac83f','2'),('ed729699-667c-45a2-941e-e7d53ae4a033','2'),('f6bf2245-32db-4aad-a1a9-190c5e89a56a','2');
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
INSERT INTO `aspnetusers` VALUES ('0069ab7a-2359-4331-90af-965bcb4a0f7b','',1,'2004-09-20 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'AhmadMalik','AHMADMALIK','ahmadmalik@assetin.test','AHMADMALIK@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEKkpDHF8DsrPh8UIVYAIA9u51q3jaP/4DFEmVHl8OtylpLWnRyNRIC/NtwlawUdiTg==','Q624TZ5AOE3AY3P26BABO5FGKJXYCWKL','9eb3982b-29fc-4a13-a342-b156e5e5a2d2','+923261009574',0,0,NULL,1,0),('0b046f72-8bee-4da2-b638-44d344509c59','',1,'1999-06-27 00:00:00.000000','0001-01-01 00:00:00.000000','Female',1,'RidaMunawwar','RIDAMUNAWWAR','ridamunawwar@assetin.test','RIDAMUNAWWAR@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEHAFpLPOixGoFsR5wZ3M84UduGVddkV/RAbpkvb5U0rM4bIdgvUStNXQtveAj5Pnzg==','V7CDJ5L5SCWX4GEZVZC6SJMBOXUJCJNE','53d84308-2bc9-4d61-890d-38a229e0e426','+923581564225',0,0,NULL,1,0),('28e6f14b-b087-4bff-8d4b-7bda035685ec','',1,'2005-04-11 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'MuhammadAbdullah','MUHAMMADABDULLAH','muhammadabdullah@assetin.test','MUHAMMADABDULLAH@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEAKVMcBT+2N9iUj8VrUCkItARarrVky3bT+o2OBZOh4pNKYiqtRfK1/1jfvM+KaJwg==','V2XMXDXBEUBHOSXLSYYMLSXVDVEMO4CA','7df7e6c9-04bf-4d25-8496-0533ac5b9be9','+923214948644',0,0,NULL,1,0),('4455b62a-0ab0-4ce3-9ed9-f009f2c6c4ee','',1,'2002-12-07 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'khanamAreeba','KHANAMAREEBA','khanamareeba@assetin.test','KHANAMAREEBA@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEG2I6rMYE0+WgWrAQyWkSk6KzXcvJKC1nrKXUQL/fbOx0jWvB7kj882Y0GyouaTAWQ==','KFAMDWBUM4F4JJFIQLUZAY2KOPUWANKN','47587ab8-4093-48d5-a7cc-020a3e1fec47','+923004653232',0,0,NULL,1,0),('5a541c31-2d73-43a8-8f44-933ebe87a699','',1,'1997-10-04 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'RanaAsim','RANAASIM','ranaasim@assetin.test','RANAASIM@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEEJHg4ZpgoqoF0BmPZ/829MXRXmj2LWjNjW2AaEvR+pZtAubJ2D7yPvQScCrLxAqkg==','RKV3LI2Y44LBACKEMJ4OFKZ5XDMKI4D7','4e56edb7-db74-4638-8ef8-724fc7653661','+923037223438',0,0,NULL,1,0),('686c44a1-0955-4ec0-99a9-685852dee2e8','',1,'1997-12-08 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'IbtesamHaider','IBTESAMHAIDER','ibtesamhaider@assetin.test','IBTESAMHAIDER@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEOpeODwTljujsYOTIOLDzW9ltDvRWMhoRu9M807cJg/C9nQJvpbAAOGyou5sTapTew==','HRZLAHPS2RGRRZQDT2QLCPGOK2DY5IH6','2b13389a-4c26-4663-bb2e-600618f1e357','+923454742142',0,0,NULL,1,0),('72351ab4-6944-4570-a50e-89234d492250','',1,'2004-10-28 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'AhmadMohsin','AHMADMOHSIN','ahmadmohsin@assetin.test','AHMADMOHSIN@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEIThBvJ5KiU4ZgFd3PxjM7ClvVeqn4NjOg3dKeKXSi9P+XTsxfEgp7QJ7YL9juf/og==','OWW43CKVZ7ZA65YJMP2LJ3JUVN4MEZH3','115177a9-bf32-48e0-b470-7b2a1d06bab9','+923274760572',0,0,NULL,1,0),('8d141bb6-21b5-42b1-b42a-dde9668f2cb6','',1,'2002-12-07 00:00:00.000000','0001-01-01 00:00:00.000000','male',NULL,'_burhan.here_','_BURHAN.HERE_','burhanburewala@gmail.com','BURHANBUREWALA@GMAIL.COM',1,'AQAAAAIAAYagAAAAEIkXNcfcGqXJ3LPaezP/sk6aRznYropV4p8enOXVtwCBNbMtZdv12wL3p6Aep/vXhQ==','DGMLA4APQOMERA2RSUUXWVMARBQTHWMP','48b4c616-b1fb-412e-ab54-91a767976333','+923004653232',0,0,NULL,1,0),('a108227d-a8c3-467d-8d54-778ff5cac83f','',1,'1996-12-31 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'RabeelAnwar','RABEELANWAR','rabeelanwar@assetin.test','RABEELANWAR@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEK1JEfJ9HxrPpw3cA/q/f2/3yx1gZYSweIEcSpwMwrl8Ha4aSVdqUsc5vZGvO+KP9Q==','XIZT4TCWJRGZEP4CKARGX7LIMVAMYHAL','3637cbe1-011b-44e0-97c2-f04b2fca2212','+923458454643',0,0,NULL,1,0),('ed729699-667c-45a2-941e-e7d53ae4a033','',1,'2006-08-04 00:00:00.000000','0001-01-01 00:00:00.000000','Female',1,'AyeshaNadeem','AYESHANADEEM','ayeshanadeem@assetin.test','AYESHANADEEM@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEL5U8q+4dnc5SWHyXeh/cRZ92r8ijq9b99H+TMQJBdd5/XwONxujJlDh4vV4m7cXBw==','KYSHOGLXJ3IDTNTNRYGLTFQTQ7PRGT7D','b25f27c8-89ff-4777-8ef1-475d5ab21a72','+923453214545',0,0,NULL,1,0),('f6bf2245-32db-4aad-a1a9-190c5e89a56a','',1,'2009-04-15 00:00:00.000000','0001-01-01 00:00:00.000000','Male',1,'ArhamNadeem','ARHAMNADEEM','arhamnadeem@assetin.test','ARHAMNADEEM@ASSETIN.TEST',1,'AQAAAAIAAYagAAAAEBkE7e90itd+cmNWYa6YLwpopRlu2Px09TKNaDYpsYSpZIYbHjau3LTbs7YoEwFSEg==','DQ7ETNDFXTLTQFBK4FZ2UF36OJGZC6XW','9019c726-b9d4-4b6d-aba4-731a96b8e91f','+923545465521',0,0,NULL,1,0);
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
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `assets`
--

LOCK TABLES `assets` WRITE;
/*!40000 ALTER TABLE `assets` DISABLE KEYS */;
INSERT INTO `assets` VALUES (1,'HP 840 G3','This is an asset with category laptop and type fixed.','123456789','8d55fa4bf0','840 G3',' Hewlett-Packard','2025-06-22 17:36:00.762894','2025-06-22 17:36:00.875646','2025-06-22 00:00:00.000000',40000.000000000000000000000000000000,40500.000000000000000000000000000000,0,'AssetIn Tower 1, Lahore',0.2,'','f3ef97b86103462fa41f6be9cd20b09b',1,4,1,1,''),(2,'iPhone 13 Pro','This is an asset with catagory modile and type fixed.','234567891','cec1fcede1','13 Pro',' Hewlett-Packard','2025-06-22 17:52:08.820835','2025-06-22 17:52:08.820835','2025-06-22 00:00:00.000000',150000.000000000000000000000000000000,150500.000000000000000000000000000000,0,'AssetIn Tower 1, Lahore',0.2,'','2d1535ebc189463d8354e68eb0800e9c',1,4,2,1,''),(3,'Lenovo Lcd','This is an LCD monitor from Lenovo, categorized as a fixed type.','345678912','6edd28725c','13 inch','Lenovo','2025-06-22 17:58:57.837153','2025-06-22 17:58:57.837626','2025-06-22 00:00:00.000000',3000.000000000000000000000000000000,3500.000000000000000000000000000000,0,'AssetIn Tower 1, Lahore',0.02,' ','ac8dade1d9694353a204cac601485df0',1,4,3,1,''),(4,'Systems Limited Stocks','trying to store variable assets','567891234','3a6ab657b9','small cap','Systems Limited','2025-06-22 18:04:17.923455','2025-06-22 18:04:17.923455','2025-06-22 00:00:00.000000',230.000000000000000000000000000000,230.000000000000000000000000000000,0,'AssetIn Tower 1, Lahore',0,'','944d38c117a4411c8c2de997f75f32fd',1,4,8,2,'');
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
INSERT INTO `organizations` VALUES (1,'','AssetIn','This is a test organization.','2025-06-22 15:00:33.403701',1,'@assetin.test','8d141bb6-21b5-42b1-b42a-dde9668f2cb6');
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationsassetassignreturns`
--

LOCK TABLES `organizationsassetassignreturns` WRITE;
/*!40000 ALTER TABLE `organizationsassetassignreturns` DISABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationsassetcatagories`
--

LOCK TABLES `organizationsassetcatagories` WRITE;
/*!40000 ALTER TABLE `organizationsassetcatagories` DISABLE KEYS */;
INSERT INTO `organizationsassetcatagories` VALUES (1,'Laptop',1),(2,'Mobile',1),(3,'Moniter',1),(4,'Printer',1),(5,'Mouse',1),(6,'Keyboard',1),(7,'Headphone',1),(8,'stocks',1);
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
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationsassetrequests`
--

LOCK TABLES `organizationsassetrequests` WRITE;
/*!40000 ALTER TABLE `organizationsassetrequests` DISABLE KEYS */;
INSERT INTO `organizationsassetrequests` VALUES (1,'Need a mouse pad','Need a mouse pad','2025-06-22 15:02:13.798406',5,'0001-01-01 00:00:00.000000',0,'0001-01-01 00:00:00.000000','8d141bb6-21b5-42b1-b42a-dde9668f2cb6',NULL,1),(2,'Need a mouse pad','Need a mouse padNeed a mouse padNeed a mouse padNeed a mouse padNeed a mouse padNeed a mouse pad','2025-06-22 15:02:53.643518',3,'2025-06-22 15:03:32.502052',0,'0001-01-01 00:00:00.000000','8d141bb6-21b5-42b1-b42a-dde9668f2cb6',NULL,1),(3,'request form keyboard','need a full size external keyboard from my laptop.','2025-06-22 15:04:32.530226',3,'2025-06-22 15:05:12.975215',0,'0001-01-01 00:00:00.000000','4455b62a-0ab0-4ce3-9ed9-f009f2c6c4ee',NULL,1),(4,'Request for LCD Moniter','I need LCD moniter as my laptop screeen is not enought fro my daily multitasking adn development requirments.','2025-06-22 18:12:32.491903',2,'0001-01-01 00:00:00.000000',0,'0001-01-01 00:00:00.000000','8d141bb6-21b5-42b1-b42a-dde9668f2cb6',NULL,1);
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
INSERT INTO `organizationsassetstatuses` VALUES (1,'Assigned'),(2,'Retired'),(3,'Under Maintenance'),(4,'Available'),(5,'Lost'),(6,'Out Of Order');
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vendorproducts`
--

LOCK TABLES `vendorproducts` WRITE;
/*!40000 ALTER TABLE `vendorproducts` DISABLE KEYS */;
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
  PRIMARY KEY (`VendorID`),
  UNIQUE KEY `IX_Vendors_Email` (`Email`),
  KEY `IX_Vendors_UserID` (`UserID`),
  CONSTRAINT `FK_Vendors_AspNetUsers_UserID` FOREIGN KEY (`UserID`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vendors`
--

LOCK TABLES `vendors` WRITE;
/*!40000 ALTER TABLE `vendors` DISABLE KEYS */;
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

-- Dump completed on 2025-06-23  1:00:03
