-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: localhost    Database: assetindb
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
INSERT INTO `__efmigrationshistory` VALUES ('20250427134615_InitialCreate','8.0.10'),('20250427213744_AddedAssetRequestProcessDateColumnInAssetRequestTable','8.0.10'),('20250428173707_AddedCompletionStatusColumnAndMadeRequestProcessedDateColumnNullableInAssetRequestTable','8.0.10'),('20250428174631_addedRequestCompletedDateColumnINAssetRequestTable','8.0.10');
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
INSERT INTO `aspnetroles` VALUES ('1','OrganizationOwner','ORGANIZATIONOWNER','537652b8-f2b7-4052-8fd3-116eb1bdacf4'),('2','OrganizationEmployee','ORGANIZATIONEMPLOYEE','b478e79f-8b78-4d62-aa09-7d3eff952f68'),('3','OrganizationAssetManager','ORGANIZATIONASSETMANAGER','f503ccc5-352d-4260-863b-62b2bca1590e'),('4','Vendor','VENDOR','40175156-b3a3-426d-968d-4e7ca3fce34a');
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
INSERT INTO `aspnetuserroles` VALUES ('b93f7185-e069-4bdd-852a-00c4df13844a','1'),('c3992840-42d7-4176-889b-7126b2f17724','3');
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
INSERT INTO `aspnetusers` VALUES ('b93f7185-e069-4bdd-852a-00c4df13844a',NULL,1,NULL,'Buran123@@@','BURAN123@@@','burhanburewala@gmail.com','BURHANBUREWALA@GMAIL.COM',1,'AQAAAAIAAYagAAAAEKC+QJfwSzm5aSgdo2K6rI5sY/Fy9t3oynFNhdz7c6jP+J5M0x+zJlWTuMlV3qWbQQ==','HWLGZSN67A25NKEJ5X6XYYJRNDPRXD6V','8d3c766c-5292-4f8f-96c7-a89c3037dd39',NULL,0,0,NULL,1,0),('c3992840-42d7-4176-889b-7126b2f17724',NULL,1,1,'BuhranAdmin123@@@','BUHRANADMIN123@@@','burhaninsta2@gmail.com','BURHANINSTA2@GMAIL.COM',1,'AQAAAAIAAYagAAAAEB6aOhvadQ5QKkaN+Nanygc8z6zg9h7XoQlz6/a/LOAGz7z80pbIEf470zI7hPqW3w==','HTN2PFQYKAZHTSDIXR7DIOUVX3SXQRI6','f80b0e36-6ffc-4453-8fff-c9f167451449',NULL,0,0,NULL,1,0);
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
) ENGINE=InnoDB AUTO_INCREMENT=110 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `assets`
--

LOCK TABLES `assets` WRITE;
/*!40000 ALTER TABLE `assets` DISABLE KEYS */;
INSERT INTO `assets` VALUES (1,'LG UltraFine 27\"','27-inch 4K monitor for graphic design.','27UL-XYZ001','123456789001','27UL850','LG','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-07-08 00:00:00.000000',786.480000000000000000000000000000,653.660000000000000000000000000000,1,'Warehouse - Rack 2',8,'Color calibration needed.','27-50-001',1,4,1,1),(2,'LG UltraFine 27\"','27-inch 4K monitor for graphic design.','27UL-XYZ002','123456789002','27UL850','LG','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-11-07 00:00:00.000000',1086.700000000000000000000000000000,957.550000000000000000000000000000,0,'Warehouse - Rack 2',8,'Color calibration needed.','27-50-002',1,4,1,1),(3,'Dell Latitude 7420','Business laptop with Intel i7, 16GB RAM.','LATI-XYZ003','123456789003','Latitude 7420','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-07-14 00:00:00.000000',1386.920000000000000000000000000000,1014.100000000000000000000000000000,1,'Head Office - 2nd Floor',8,'Battery needs replacement.','LA-20-003',1,4,1,1),(4,'Samsung Monitor 27\"','27-inch business monitor.','S27R-XYZ004','123456789004','S27R650','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-07-07 00:00:00.000000',487.140000000000000000000000000000,380.570000000000000000000000000000,0,'IT Lab - Section A',10,'Brightness adjustment issue.','S2-50-004',1,4,1,1),(5,'LG UltraFine 27\"','27-inch 4K monitor for graphic design.','27UL-XYZ005','123456789005','27UL850','LG','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-11-30 00:00:00.000000',787.360000000000000000000000000000,654.500000000000000000000000000000,1,'IT Lab - Section A',11,'Color calibration needed.','27-50-005',1,4,1,1),(6,'Lenovo ThinkPad T14','Durable business laptop.','THIN-XYZ006','123456789006','ThinkPad T14','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-10-07 00:00:00.000000',1087.580000000000000000000000000000,958.480000000000000000000000000000,0,'Warehouse - Rack 1',8,'Keyboard replacement done.','TH-14-006',1,4,1,1),(15,'Lenovo ThinkCentre M720','Compact desktop for offices.','THIN-XYZ015','123456789015','ThinkCentre M720','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-04-18 00:00:00.000000',1389.560000000000000000000000000000,1016.640000000000000000000000000000,1,'Warehouse - Rack 3',10,'USB port not working.','TH-20-015',1,4,1,1),(16,'HP ProDesk 600','Desktop PC for business environments.','PROD-XYZ016','123456789016','ProDesk 600','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-09-28 00:00:00.000000',489.780000000000000000000000000000,382.840000000000000000000000000000,0,'Head Office - 3rd Floor',13,'Power supply replaced.','PR-00-016',1,4,1,1),(17,'LG UltraFine 27\"','27-inch 4K monitor for graphic design.','27UL-XYZ017','123456789017','27UL850','LG','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-05-25 00:00:00.000000',790.000000000000000000000000000000,657.050000000000000000000000000000,1,'IT Lab - Section A',14,'Color calibration needed.','27-50-017',1,4,1,1),(18,'HP EliteBook 840','Lightweight business laptop with Intel i5.','ELIT-XYZ018','123456789018','EliteBook 840','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-11-26 00:00:00.000000',1090.220000000000000000000000000000,961.290000000000000000000000000000,0,'Head Office - 2nd Floor',13,'Screen flickering issue.','EL-40-018',1,4,1,1),(19,'LG UltraFine 27\"','27-inch 4K monitor for graphic design.','27UL-XYZ019','123456789019','27UL850','LG','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-07-27 00:00:00.000000',1390.440000000000000000000000000000,1017.490000000000000000000000000000,1,'Head Office - 2nd Floor',14,'Color calibration needed.','27-50-019',1,4,1,1),(20,'Lenovo ThinkPad T14','Durable business laptop.','THIN-XYZ020','123456789020','ThinkPad T14','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-09-13 00:00:00.000000',490.660000000000000000000000000000,383.600000000000000000000000000000,0,'Head Office - 1st Floor',13,'Keyboard replacement done.','TH-14-020',1,4,1,1),(21,'Samsung Monitor 27\"','27-inch business monitor.','S27R-XYZ021','123456789021','S27R650','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-03-03 00:00:00.000000',790.880000000000000000000000000000,657.890000000000000000000000000000,1,'Warehouse - Rack 2',11,'Brightness adjustment issue.','S2-50-021',1,4,1,1),(22,'Samsung Monitor 24\"','24-inch LED Monitor for office use.','S24F-XYZ022','123456789022','S24F350','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-11-28 00:00:00.000000',1091.100000000000000000000000000000,962.230000000000000000000000000000,0,'Warehouse - Rack 1',9,'Minor dead pixel detected.','S2-50-022',1,4,1,1),(23,'Samsung Monitor 27\"','27-inch business monitor.','S27R-XYZ023','123456789023','S27R650','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-10-21 00:00:00.000000',1391.320000000000000000000000000000,1018.340000000000000000000000000000,1,'IT Lab - Section B',9,'Brightness adjustment issue.','S2-50-023',1,4,1,1),(24,'HP ProDesk 600','Desktop PC for business environments.','PROD-XYZ024','123456789024','ProDesk 600','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-01-27 00:00:00.000000',491.540000000000000000000000000000,384.360000000000000000000000000000,0,'Head Office - 2nd Floor',11,'Power supply replaced.','PR-00-024',1,4,1,1),(25,'Lenovo ThinkCentre M720','Compact desktop for offices.','THIN-XYZ025','123456789025','ThinkCentre M720','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-03-02 00:00:00.000000',791.760000000000000000000000000000,658.740000000000000000000000000000,1,'Head Office - 1st Floor',13,'USB port not working.','TH-20-025',1,4,1,1),(26,'Dell Latitude 7420','Business laptop with Intel i7, 16GB RAM.','LATI-XYZ026','123456789026','Latitude 7420','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-06-18 00:00:00.000000',1091.980000000000000000000000000000,963.160000000000000000000000000000,0,'IT Lab - Section B',10,'Battery needs replacement.','LA-20-026',1,4,1,1),(27,'Lenovo ThinkPad T14','Durable business laptop.','THIN-XYZ027','123456789027','ThinkPad T14','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-04-18 00:00:00.000000',1392.200000000000000000000000000000,1019.190000000000000000000000000000,1,'Warehouse - Rack 3',15,'Keyboard replacement done.','TH-14-027',1,4,1,1),(28,'Dell Latitude 7420','Business laptop with Intel i7, 16GB RAM.','LATI-XYZ028','123456789028','Latitude 7420','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-06-02 00:00:00.000000',492.420000000000000000000000000000,385.120000000000000000000000000000,0,'Warehouse - Rack 1',11,'Battery needs replacement.','LA-20-028',1,4,1,1),(29,'Samsung Monitor 24\"','24-inch LED Monitor for office use.','S24F-XYZ029','123456789029','S24F350','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-04-09 00:00:00.000000',792.640000000000000000000000000000,659.590000000000000000000000000000,1,'IT Lab - Section B',11,'Minor dead pixel detected.','S2-50-029',1,4,1,1),(30,'Samsung Monitor 27\"','27-inch business monitor.','S27R-XYZ030','123456789030','S27R650','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-06-07 00:00:00.000000',1092.860000000000000000000000000000,964.100000000000000000000000000000,0,'Warehouse - Rack 2',10,'Brightness adjustment issue.','S2-50-030',1,4,1,1),(31,'Samsung Monitor 24\"','24-inch LED Monitor for office use.','S24F-XYZ031','123456789031','S24F350','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-10-11 00:00:00.000000',1393.080000000000000000000000000000,1020.040000000000000000000000000000,1,'Warehouse - Rack 2',12,'Minor dead pixel detected.','S2-50-031',1,4,1,1),(32,'Dell Precision 3550','Mobile workstation for engineers.','PREC-XYZ032','123456789032','Precision 3550','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-12-04 00:00:00.000000',493.300000000000000000000000000000,385.890000000000000000000000000000,0,'Head Office - 1st Floor',13,'Trackpad unresponsive occasionally.','PR-50-032',1,4,1,1),(33,'HP ProDesk 600','Desktop PC for business environments.','PROD-XYZ033','123456789033','ProDesk 600','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-07-15 00:00:00.000000',793.520000000000000000000000000000,660.440000000000000000000000000000,1,'Head Office - 3rd Floor',15,'Power supply replaced.','PR-00-033',1,4,1,1),(34,'Dell Latitude 7420','Business laptop with Intel i7, 16GB RAM.','LATI-XYZ034','123456789034','Latitude 7420','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-12-01 00:00:00.000000',1093.740000000000000000000000000000,965.040000000000000000000000000000,0,'Head Office - 3rd Floor',14,'Battery needs replacement.','LA-20-034',1,4,1,1),(35,'Lenovo ThinkPad T14','Durable business laptop.','THIN-XYZ035','123456789035','ThinkPad T14','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-07-22 00:00:00.000000',1393.960000000000000000000000000000,1020.880000000000000000000000000000,1,'Head Office - 1st Floor',10,'Keyboard replacement done.','TH-14-035',1,4,1,1),(36,'Lenovo ThinkCentre M720','Compact desktop for offices.','THIN-XYZ036','123456789036','ThinkCentre M720','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-05-07 00:00:00.000000',494.170000000000000000000000000000,386.640000000000000000000000000000,0,'IT Lab - Section A',12,'USB port not working.','TH-20-036',1,4,1,1),(37,'Dell Latitude 7420','Business laptop with Intel i7, 16GB RAM.','LATI-XYZ037','123456789037','Latitude 7420','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-07-23 00:00:00.000000',794.390000000000000000000000000000,661.280000000000000000000000000000,1,'Head Office - 1st Floor',11,'Battery needs replacement.','LA-20-037',1,4,1,1),(38,'Dell Precision 3550','Mobile workstation for engineers.','PREC-XYZ038','123456789038','Precision 3550','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-06-18 00:00:00.000000',1094.610000000000000000000000000000,965.960000000000000000000000000000,0,'Head Office - 3rd Floor',14,'Trackpad unresponsive occasionally.','PR-50-038',1,4,1,1),(39,'Lenovo ThinkCentre M720','Compact desktop for offices.','THIN-XYZ039','123456789039','ThinkCentre M720','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-10-30 00:00:00.000000',1394.830000000000000000000000000000,1021.730000000000000000000000000000,1,'Warehouse - Rack 2',14,'USB port not working.','TH-20-039',1,4,1,1),(40,'Dell OptiPlex 7080','Business desktop with Intel i7 and SSD.','OPTI-XYZ040','123456789040','OptiPlex 7080','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-08-15 00:00:00.000000',495.050000000000000000000000000000,387.400000000000000000000000000000,0,'Head Office - 3rd Floor',9,'No issues reported.','OP-80-040',1,4,1,1),(41,'LG UltraFine 27\"','27-inch 4K monitor for graphic design.','27UL-XYZ041','123456789041','27UL850','LG','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-05-12 00:00:00.000000',795.270000000000000000000000000000,662.130000000000000000000000000000,1,'IT Lab - Section B',14,'Color calibration needed.','27-50-041',1,4,1,1),(42,'LG UltraFine 27\"','27-inch 4K monitor for graphic design.','27UL-XYZ042','123456789042','27UL850','LG','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-03-30 00:00:00.000000',1095.490000000000000000000000000000,966.900000000000000000000000000000,0,'Warehouse - Rack 3',13,'Color calibration needed.','27-50-042',1,4,1,1),(43,'Dell Precision 3550','Mobile workstation for engineers.','PREC-XYZ043','123456789043','Precision 3550','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-06-05 00:00:00.000000',1395.710000000000000000000000000000,1022.570000000000000000000000000000,1,'IT Lab - Section B',14,'Trackpad unresponsive occasionally.','PR-50-043',1,4,1,1),(44,'Dell OptiPlex 7080','Business desktop with Intel i7 and SSD.','OPTI-XYZ044','123456789044','OptiPlex 7080','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-12-03 00:00:00.000000',495.930000000000000000000000000000,388.160000000000000000000000000000,0,'IT Lab - Section A',10,'No issues reported.','OP-80-044',1,4,1,1),(45,'Dell OptiPlex 7080','Business desktop with Intel i7 and SSD.','OPTI-XYZ045','123456789045','OptiPlex 7080','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-09-06 00:00:00.000000',796.150000000000000000000000000000,662.980000000000000000000000000000,1,'IT Lab - Section A',12,'No issues reported.','OP-80-045',1,4,1,1),(46,'Dell Precision 3550','Mobile workstation for engineers.','PREC-XYZ046','123456789046','Precision 3550','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-09-12 00:00:00.000000',1096.370000000000000000000000000000,967.840000000000000000000000000000,0,'Head Office - 3rd Floor',13,'Trackpad unresponsive occasionally.','PR-50-046',1,4,1,1),(47,'Samsung Monitor 27\"','27-inch business monitor.','S27R-XYZ047','123456789047','S27R650','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-02-15 00:00:00.000000',1396.590000000000000000000000000000,1023.420000000000000000000000000000,1,'Head Office - 2nd Floor',13,'Brightness adjustment issue.','S2-50-047',1,4,1,1),(48,'HP ProDesk 600','Desktop PC for business environments.','PROD-XYZ048','123456789048','ProDesk 600','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-01-30 00:00:00.000000',496.810000000000000000000000000000,388.920000000000000000000000000000,0,'Warehouse - Rack 1',15,'Power supply replaced.','PR-00-048',1,4,1,1),(49,'Lenovo ThinkPad T14','Durable business laptop.','THIN-XYZ049','123456789049','ThinkPad T14','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-06-02 00:00:00.000000',797.030000000000000000000000000000,663.830000000000000000000000000000,1,'Warehouse - Rack 3',11,'Keyboard replacement done.','TH-14-049',1,4,1,1),(50,'LG UltraFine 27\"','27-inch 4K monitor for graphic design.','27UL-XYZ050','123456789050','27UL850','LG','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-03-12 00:00:00.000000',1097.250000000000000000000000000000,968.770000000000000000000000000000,0,'Warehouse - Rack 3',14,'Color calibration needed.','27-50-050',1,4,1,1),(51,'LG UltraFine 27\"','27-inch 4K monitor for graphic design.','27UL-XYZ051','123456789051','27UL850','LG','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-12-11 00:00:00.000000',1397.470000000000000000000000000000,1024.270000000000000000000000000000,1,'Head Office - 3rd Floor',8,'Color calibration needed.','27-50-051',1,4,1,1),(52,'Dell Latitude 7420','Business laptop with Intel i7, 16GB RAM.','LATI-XYZ052','123456789052','Latitude 7420','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-03-21 00:00:00.000000',497.690000000000000000000000000000,389.680000000000000000000000000000,0,'Warehouse - Rack 3',8,'Battery needs replacement.','LA-20-052',1,4,1,1),(53,'Dell Precision 3550','Mobile workstation for engineers.','PREC-XYZ053','123456789053','Precision 3550','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-09-04 00:00:00.000000',797.910000000000000000000000000000,664.680000000000000000000000000000,1,'Head Office - 2nd Floor',11,'Trackpad unresponsive occasionally.','PR-50-053',1,4,1,1),(54,'Samsung Monitor 24\"','24-inch LED Monitor for office use.','S24F-XYZ054','123456789054','S24F350','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-10-29 00:00:00.000000',1098.130000000000000000000000000000,969.710000000000000000000000000000,0,'Head Office - 2nd Floor',10,'Minor dead pixel detected.','S2-50-054',1,4,1,1),(55,'Dell OptiPlex 7080','Business desktop with Intel i7 and SSD.','OPTI-XYZ055','123456789055','OptiPlex 7080','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-08-22 00:00:00.000000',1398.350000000000000000000000000000,1025.120000000000000000000000000000,1,'Warehouse - Rack 2',10,'No issues reported.','OP-80-055',1,4,1,1),(56,'LG UltraFine 27\"','27-inch 4K monitor for graphic design.','27UL-XYZ056','123456789056','27UL850','LG','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-09-18 00:00:00.000000',498.570000000000000000000000000000,390.450000000000000000000000000000,0,'IT Lab - Section B',9,'Color calibration needed.','27-50-056',1,4,1,1),(57,'HP EliteBook 840','Lightweight business laptop with Intel i5.','ELIT-XYZ057','123456789057','EliteBook 840','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-10-23 00:00:00.000000',798.790000000000000000000000000000,665.530000000000000000000000000000,1,'Warehouse - Rack 2',8,'Screen flickering issue.','EL-40-057',1,4,1,1),(58,'Dell Precision 3550','Mobile workstation for engineers.','PREC-XYZ058','123456789058','Precision 3550','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-12-28 00:00:00.000000',1099.010000000000000000000000000000,970.650000000000000000000000000000,0,'Warehouse - Rack 1',8,'Trackpad unresponsive occasionally.','PR-50-058',1,4,1,1),(59,'Dell Latitude 7420','Business laptop with Intel i7, 16GB RAM.','LATI-XYZ059','123456789059','Latitude 7420','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-07-01 00:00:00.000000',1399.230000000000000000000000000000,1025.970000000000000000000000000000,1,'Head Office - 3rd Floor',15,'Battery needs replacement.','LA-20-059',1,4,1,1),(60,'LG UltraFine 27\"','27-inch 4K monitor for graphic design.','27UL-XYZ060','123456789060','27UL850','LG','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-08-13 00:00:00.000000',499.450000000000000000000000000000,391.210000000000000000000000000000,0,'Head Office - 1st Floor',14,'Color calibration needed.','27-50-060',1,4,1,1),(61,'Dell Latitude 7420','Business laptop with Intel i7, 16GB RAM.','LATI-XYZ061','123456789061','Latitude 7420','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-03-02 00:00:00.000000',799.670000000000000000000000000000,666.380000000000000000000000000000,1,'Warehouse - Rack 3',12,'Battery needs replacement.','LA-20-061',1,4,1,1),(62,'HP EliteBook 840','Lightweight business laptop with Intel i5.','ELIT-XYZ062','123456789062','EliteBook 840','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-09-09 00:00:00.000000',1099.890000000000000000000000000000,971.590000000000000000000000000000,0,'IT Lab - Section A',13,'Screen flickering issue.','EL-40-062',1,4,1,1),(63,'Dell Latitude 7420','Business laptop with Intel i7, 16GB RAM.','LATI-XYZ063','123456789063','Latitude 7420','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-08-06 00:00:00.000000',1400.110000000000000000000000000000,1026.820000000000000000000000000000,1,'Warehouse - Rack 3',9,'Battery needs replacement.','LA-20-063',1,4,1,1),(64,'Lenovo ThinkCentre M720','Compact desktop for offices.','THIN-XYZ064','123456789064','ThinkCentre M720','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-02-03 00:00:00.000000',500.330000000000000000000000000000,391.970000000000000000000000000000,0,'Warehouse - Rack 1',8,'USB port not working.','TH-20-064',1,4,1,1),(65,'HP EliteBook 840','Lightweight business laptop with Intel i5.','ELIT-XYZ065','123456789065','EliteBook 840','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-05-15 00:00:00.000000',800.550000000000000000000000000000,667.230000000000000000000000000000,1,'Head Office - 1st Floor',11,'Screen flickering issue.','EL-40-065',1,4,1,1),(66,'Dell Precision 3550','Mobile workstation for engineers.','PREC-XYZ066','123456789066','Precision 3550','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-03-28 00:00:00.000000',1100.770000000000000000000000000000,972.530000000000000000000000000000,0,'Head Office - 3rd Floor',15,'Trackpad unresponsive occasionally.','PR-50-066',1,4,1,1),(67,'Lenovo ThinkCentre M720','Compact desktop for offices.','THIN-XYZ067','123456789067','ThinkCentre M720','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-03-21 00:00:00.000000',1400.990000000000000000000000000000,1027.670000000000000000000000000000,1,'IT Lab - Section A',8,'USB port not working.','TH-20-067',1,4,1,1),(68,'Lenovo ThinkCentre M720','Compact desktop for offices.','THIN-XYZ068','123456789068','ThinkCentre M720','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-12-16 00:00:00.000000',501.210000000000000000000000000000,392.730000000000000000000000000000,0,'Warehouse - Rack 3',14,'USB port not working.','TH-20-068',1,4,1,1),(69,'HP EliteBook 840','Lightweight business laptop with Intel i5.','ELIT-XYZ069','123456789069','EliteBook 840','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-03-27 00:00:00.000000',801.430000000000000000000000000000,668.080000000000000000000000000000,1,'Head Office - 3rd Floor',10,'Screen flickering issue.','EL-40-069',1,4,1,1),(70,'Samsung Monitor 27\"','27-inch business monitor.','S27R-XYZ070','123456789070','S27R650','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-11-04 00:00:00.000000',1101.650000000000000000000000000000,973.470000000000000000000000000000,0,'Head Office - 1st Floor',13,'Brightness adjustment issue.','S2-50-070',1,4,1,1),(71,'Samsung Monitor 24\"','24-inch LED Monitor for office use.','S24F-XYZ071','123456789071','S24F350','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-12-05 00:00:00.000000',1401.870000000000000000000000000000,1028.530000000000000000000000000000,1,'Warehouse - Rack 1',11,'Minor dead pixel detected.','S2-50-071',1,4,1,1),(72,'HP EliteBook 840','Lightweight business laptop with Intel i5.','ELIT-XYZ072','123456789072','EliteBook 840','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-02-01 00:00:00.000000',502.090000000000000000000000000000,393.500000000000000000000000000000,0,'IT Lab - Section B',12,'Screen flickering issue.','EL-40-072',1,4,1,1),(73,'LG UltraFine 27\"','27-inch 4K monitor for graphic design.','27UL-XYZ073','123456789073','27UL850','LG','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-04-05 00:00:00.000000',802.300000000000000000000000000000,668.920000000000000000000000000000,1,'Warehouse - Rack 3',9,'Color calibration needed.','27-50-073',1,4,1,1),(74,'Dell Latitude 7420','Business laptop with Intel i7, 16GB RAM.','LATI-XYZ074','123456789074','Latitude 7420','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-03-14 00:00:00.000000',1102.520000000000000000000000000000,974.400000000000000000000000000000,0,'Head Office - 2nd Floor',13,'Battery needs replacement.','LA-20-074',1,4,1,1),(75,'HP ProDesk 600','Desktop PC for business environments.','PROD-XYZ075','123456789075','ProDesk 600','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-06-04 00:00:00.000000',1402.740000000000000000000000000000,1029.370000000000000000000000000000,1,'Warehouse - Rack 1',13,'Power supply replaced.','PR-00-075',1,4,1,1),(76,'Dell OptiPlex 7080','Business desktop with Intel i7 and SSD.','OPTI-XYZ076','123456789076','OptiPlex 7080','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-04-25 00:00:00.000000',502.960000000000000000000000000000,394.250000000000000000000000000000,0,'Head Office - 2nd Floor',8,'No issues reported.','OP-80-076',1,4,1,1),(77,'HP EliteBook 840','Lightweight business laptop with Intel i5.','ELIT-XYZ077','123456789077','EliteBook 840','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-03-05 00:00:00.000000',803.180000000000000000000000000000,669.770000000000000000000000000000,1,'Warehouse - Rack 3',9,'Screen flickering issue.','EL-40-077',1,4,1,1),(78,'HP EliteBook 840','Lightweight business laptop with Intel i5.','ELIT-XYZ078','123456789078','EliteBook 840','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-07-01 00:00:00.000000',1103.400000000000000000000000000000,975.340000000000000000000000000000,0,'Warehouse - Rack 3',11,'Screen flickering issue.','EL-40-078',1,4,1,1),(79,'HP ProDesk 600','Desktop PC for business environments.','PROD-XYZ079','123456789079','ProDesk 600','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-12-09 00:00:00.000000',1403.620000000000000000000000000000,1030.220000000000000000000000000000,1,'Warehouse - Rack 3',11,'Power supply replaced.','PR-00-079',1,4,1,1),(80,'Samsung Monitor 24\"','24-inch LED Monitor for office use.','S24F-XYZ080','123456789080','S24F350','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-04-23 00:00:00.000000',503.840000000000000000000000000000,395.020000000000000000000000000000,0,'IT Lab - Section A',14,'Minor dead pixel detected.','S2-50-080',1,4,1,1),(81,'HP ProDesk 600','Desktop PC for business environments.','PROD-XYZ081','123456789081','ProDesk 600','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-06-24 00:00:00.000000',804.060000000000000000000000000000,670.620000000000000000000000000000,1,'IT Lab - Section A',10,'Power supply replaced.','PR-00-081',1,4,1,1),(82,'Samsung Monitor 27\"','27-inch business monitor.','S27R-XYZ082','123456789082','S27R650','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-05-07 00:00:00.000000',1104.280000000000000000000000000000,976.280000000000000000000000000000,0,'Head Office - 2nd Floor',12,'Brightness adjustment issue.','S2-50-082',1,4,1,1),(83,'Lenovo ThinkCentre M720','Compact desktop for offices.','THIN-XYZ083','123456789083','ThinkCentre M720','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-04-04 00:00:00.000000',1404.500000000000000000000000000000,1031.070000000000000000000000000000,1,'Warehouse - Rack 2',9,'USB port not working.','TH-20-083',1,4,1,1),(84,'Dell OptiPlex 7080','Business desktop with Intel i7 and SSD.','OPTI-XYZ084','123456789084','OptiPlex 7080','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-09-27 00:00:00.000000',504.720000000000000000000000000000,395.780000000000000000000000000000,0,'Warehouse - Rack 1',15,'No issues reported.','OP-80-084',1,4,1,1),(85,'LG UltraFine 27\"','27-inch 4K monitor for graphic design.','27UL-XYZ085','123456789085','27UL850','LG','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-09-28 00:00:00.000000',804.940000000000000000000000000000,671.480000000000000000000000000000,1,'IT Lab - Section A',11,'Color calibration needed.','27-50-085',1,4,1,1),(86,'HP ProDesk 600','Desktop PC for business environments.','PROD-XYZ086','123456789086','ProDesk 600','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-06-05 00:00:00.000000',1105.160000000000000000000000000000,977.220000000000000000000000000000,0,'Head Office - 3rd Floor',13,'Power supply replaced.','PR-00-086',1,4,1,1),(87,'Dell Latitude 7420','Business laptop with Intel i7, 16GB RAM.','LATI-XYZ087','123456789087','Latitude 7420','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-06-09 00:00:00.000000',1405.380000000000000000000000000000,1031.920000000000000000000000000000,1,'Warehouse - Rack 3',15,'Battery needs replacement.','LA-20-087',1,4,1,1),(88,'Samsung Monitor 24\"','24-inch LED Monitor for office use.','S24F-XYZ088','123456789088','S24F350','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-07-15 00:00:00.000000',505.600000000000000000000000000000,396.540000000000000000000000000000,0,'Warehouse - Rack 3',9,'Minor dead pixel detected.','S2-50-088',1,4,1,1),(89,'Lenovo ThinkPad T14','Durable business laptop.','THIN-XYZ089','123456789089','ThinkPad T14','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-04-08 00:00:00.000000',805.820000000000000000000000000000,672.330000000000000000000000000000,1,'Warehouse - Rack 3',8,'Keyboard replacement done.','TH-14-089',1,4,1,1),(90,'HP EliteBook 840','Lightweight business laptop with Intel i5.','ELIT-XYZ090','123456789090','EliteBook 840','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-04-17 00:00:00.000000',1106.040000000000000000000000000000,978.160000000000000000000000000000,0,'Head Office - 1st Floor',8,'Screen flickering issue.','EL-40-090',1,4,1,1),(91,'Samsung Monitor 24\"','24-inch LED Monitor for office use.','S24F-XYZ091','123456789091','S24F350','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-02-04 00:00:00.000000',1406.260000000000000000000000000000,1032.780000000000000000000000000000,1,'Head Office - 1st Floor',9,'Minor dead pixel detected.','S2-50-091',1,4,1,1),(92,'Samsung Monitor 24\"','24-inch LED Monitor for office use.','S24F-XYZ092','123456789092','S24F350','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-03-03 00:00:00.000000',506.480000000000000000000000000000,397.310000000000000000000000000000,0,'IT Lab - Section B',11,'Minor dead pixel detected.','S2-50-092',1,4,1,1),(93,'Samsung Monitor 27\"','27-inch business monitor.','S27R-XYZ093','123456789093','S27R650','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-05-21 00:00:00.000000',806.700000000000000000000000000000,673.180000000000000000000000000000,1,'Head Office - 2nd Floor',12,'Brightness adjustment issue.','S2-50-093',1,4,1,1),(94,'HP EliteBook 840','Lightweight business laptop with Intel i5.','ELIT-XYZ094','123456789094','EliteBook 840','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-11-04 00:00:00.000000',1106.920000000000000000000000000000,979.100000000000000000000000000000,0,'Warehouse - Rack 2',9,'Screen flickering issue.','EL-40-094',1,4,1,1),(95,'Dell Precision 3550','Mobile workstation for engineers.','PREC-XYZ095','123456789095','Precision 3550','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-07-10 00:00:00.000000',1407.140000000000000000000000000000,1033.630000000000000000000000000000,1,'Warehouse - Rack 2',11,'Trackpad unresponsive occasionally.','PR-50-095',1,4,1,1),(96,'HP EliteBook 840','Lightweight business laptop with Intel i5.','ELIT-XYZ096','123456789096','EliteBook 840','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-05-08 00:00:00.000000',507.360000000000000000000000000000,398.070000000000000000000000000000,0,'IT Lab - Section B',13,'Screen flickering issue.','EL-40-096',1,4,1,1),(97,'Lenovo ThinkPad T14','Durable business laptop.','THIN-XYZ097','123456789097','ThinkPad T14','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-12-27 00:00:00.000000',807.580000000000000000000000000000,674.030000000000000000000000000000,1,'IT Lab - Section B',11,'Keyboard replacement done.','TH-14-097',1,4,1,1),(98,'HP ProDesk 600','Desktop PC for business environments.','PROD-XYZ098','123456789098','ProDesk 600','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-08-27 00:00:00.000000',1107.800000000000000000000000000000,980.040000000000000000000000000000,0,'Head Office - 1st Floor',11,'Power supply replaced.','PR-00-098',1,4,1,1),(99,'Samsung Monitor 27\"','27-inch business monitor.','S27R-XYZ099','123456789099','S27R650','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-07-21 00:00:00.000000',1408.020000000000000000000000000000,1034.480000000000000000000000000000,1,'Warehouse - Rack 2',12,'Brightness adjustment issue.','S2-50-099',1,4,1,1),(100,'Samsung Monitor 24\"','24-inch LED Monitor for office use.','S24F-XYZ100','123456789100','S24F350','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-08-26 00:00:00.000000',508.240000000000000000000000000000,398.840000000000000000000000000000,0,'Warehouse - Rack 3',9,'Minor dead pixel detected.','S2-50-100',1,4,1,1),(101,'Samsung Monitor 24\"','24-inch LED Monitor for office use.','S24F-XYZ001','123456789001','S24F350','Samsung','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-05-09 00:00:00.000000',808.460000000000000000000000000000,674.890000000000000000000000000000,1,'Warehouse - Rack 2',11,'Minor dead pixel detected.','S2-50-001',1,4,1,1),(102,'Lenovo ThinkPad T14','Durable business laptop.','THIN-XYZ007','123456789007','ThinkPad T14','Lenovo','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-10-27 00:00:00.000000',1108.680000000000000000000000000000,980.980000000000000000000000000000,0,'Warehouse - Rack 3',10,'Keyboard replacement done.','TH-14-007',1,4,1,1),(103,'Dell Latitude 7420','Business laptop with Intel i7, 16GB RAM.','LATI-XYZ008','123456789008','Latitude 7420','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-06-07 00:00:00.000000',1408.900000000000000000000000000000,1035.330000000000000000000000000000,1,'IT Lab - Section B',13,'Battery needs replacement.','LA-20-008',1,4,1,1),(104,'Dell Latitude 7420','Business laptop with Intel i7, 16GB RAM.','LATI-XYZ009','123456789009','Latitude 7420','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-10-17 00:00:00.000000',509.120000000000000000000000000000,399.600000000000000000000000000000,0,'Head Office - 3rd Floor',12,'Battery needs replacement.','LA-20-009',1,4,1,1),(105,'HP ProDesk 600','Desktop PC for business environments.','PROD-XYZ010','123456789010','ProDesk 600','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-03-11 00:00:00.000000',809.340000000000000000000000000000,675.740000000000000000000000000000,1,'Warehouse - Rack 3',8,'Power supply replaced.','PR-00-010',1,4,1,1),(106,'HP EliteBook 840','Lightweight business laptop with Intel i5.','ELIT-XYZ011','123456789011','EliteBook 840','HP','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-06-26 00:00:00.000000',1109.560000000000000000000000000000,981.920000000000000000000000000000,0,'Head Office - 1st Floor',9,'Screen flickering issue.','EL-40-011',1,4,1,1),(107,'Dell OptiPlex 7080','Business desktop with Intel i7 and SSD.','OPTI-XYZ012','123456789012','OptiPlex 7080','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-07-13 00:00:00.000000',1409.780000000000000000000000000000,1036.190000000000000000000000000000,1,'Head Office - 2nd Floor',12,'No issues reported.','OP-80-012',1,4,1,1),(108,'Dell Latitude 7420','Business laptop with Intel i7, 16GB RAM.','LATI-XYZ013','123456789013','Latitude 7420','Dell','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2024-10-03 00:00:00.000000',510.000000000000000000000000000000,400.370000000000000000000000000000,0,'Head Office - 2nd Floor',15,'Battery needs replacement.','LA-20-013',1,4,1,1),(109,'LG UltraFine 27\"','27-inch 4K monitor for graphic design.','27UL-XYZ014','123456789014','27UL850','LG','2025-04-26 16:06:01.000000','2025-04-26 16:06:01.000000','2025-08-09 00:00:00.000000',810.220000000000000000000000000000,676.590000000000000000000000000000,1,'IT Lab - Section B',9,'Color calibration needed.','27-50-014',1,4,1,1);
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
  `UserID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`OrganizationID`),
  KEY `IX_Organizations_UserID` (`UserID`),
  CONSTRAINT `FK_Organizations_AspNetUsers_UserID` FOREIGN KEY (`UserID`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizations`
--

LOCK TABLES `organizations` WRITE;
/*!40000 ALTER TABLE `organizations` DISABLE KEYS */;
INSERT INTO `organizations` VALUES (1,'','Burhan','Burhan ki organization 1','2025-04-27 13:52:23.293130',1,'b93f7185-e069-4bdd-852a-00c4df13844a'),(2,'','Burhan 2','Burhan ki organization 2','2025-04-27 13:52:45.320101',1,'b93f7185-e069-4bdd-852a-00c4df13844a');
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
  `AssetID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_OrganizationsAssetAssignReturns_AssetID` (`AssetID`),
  KEY `IX_OrganizationsAssetAssignReturns_AssignedByUserID` (`AssignedByUserID`),
  KEY `IX_OrganizationsAssetAssignReturns_AssignedToUserID` (`AssignedToUserID`),
  CONSTRAINT `FK_OrganizationsAssetAssignReturns_AspNetUsers_AssignedByUserID` FOREIGN KEY (`AssignedByUserID`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_OrganizationsAssetAssignReturns_AspNetUsers_AssignedToUserID` FOREIGN KEY (`AssignedToUserID`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationsassetcatagories`
--

LOCK TABLES `organizationsassetcatagories` WRITE;
/*!40000 ALTER TABLE `organizationsassetcatagories` DISABLE KEYS */;
INSERT INTO `organizationsassetcatagories` VALUES (1,'Laptop',1);
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
  `UserID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RequestProcessedDate` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `CompletionStatus` tinyint(1) NOT NULL DEFAULT '0',
  `RequestCompletedDate` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`OrganizationsAssetRequestID`),
  KEY `IX_OrganizationsAssetRequests_UserID` (`UserID`),
  CONSTRAINT `FK_OrganizationsAssetRequests_AspNetUsers_UserID` FOREIGN KEY (`UserID`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationsassetrequests`
--

LOCK TABLES `organizationsassetrequests` WRITE;
/*!40000 ALTER TABLE `organizationsassetrequests` DISABLE KEYS */;
INSERT INTO `organizationsassetrequests` VALUES (1,'New Laptop Request','Requesting a new laptop for development work.','2025-04-01 00:00:00.000000',2,'b93f7185-e069-4bdd-852a-00c4df13844a','1000-01-01 00:00:00.000000',0,'0001-01-01 00:00:00.000000'),(2,'Projector Replacement','The conference room projector is malfunctioning.','2025-04-02 00:00:00.000000',2,'c3992840-42d7-4176-889b-7126b2f17724','1000-01-01 00:00:00.000000',0,'0001-01-01 00:00:00.000000'),(3,'Office Chair','Need a new ergonomic office chair.','2025-04-03 00:00:00.000000',1,'b93f7185-e069-4bdd-852a-00c4df13844a','2025-04-05 00:00:00.000000',0,'0001-01-01 00:00:00.000000'),(4,'Monitor Upgrade','Requesting a larger monitor for design work.','2025-04-03 00:00:00.000000',1,'c3992840-42d7-4176-889b-7126b2f17724','2025-04-06 00:00:00.000000',0,'0001-01-01 00:00:00.000000'),(5,'Software License','Need license for Adobe Photoshop.','2025-04-04 00:00:00.000000',2,'b93f7185-e069-4bdd-852a-00c4df13844a','1000-01-01 00:00:00.000000',0,'0001-01-01 00:00:00.000000'),(6,'Printer Maintenance','Office printer needs maintenance service.','2025-04-04 00:00:00.000000',1,'c3992840-42d7-4176-889b-7126b2f17724','2025-04-07 00:00:00.000000',0,'0001-01-01 00:00:00.000000'),(7,'Network Cable Request','Need extra LAN cables.','2025-04-05 00:00:00.000000',2,'b93f7185-e069-4bdd-852a-00c4df13844a','1000-01-01 00:00:00.000000',0,'0001-01-01 00:00:00.000000'),(8,'Desktop Upgrade','Requesting additional RAM for desktop.','2025-04-05 00:00:00.000000',2,'c3992840-42d7-4176-889b-7126b2f17724','1000-01-01 00:00:00.000000',0,'0001-01-01 00:00:00.000000'),(9,'Security Software','Requesting installation of antivirus.','2025-04-06 00:00:00.000000',1,'b93f7185-e069-4bdd-852a-00c4df13844a','2025-04-09 00:00:00.000000',0,'0001-01-01 00:00:00.000000'),(10,'External Hard Drive','Need a 1TB external drive for backup.','2025-04-06 00:00:00.000000',2,'c3992840-42d7-4176-889b-7126b2f17724','1000-01-01 00:00:00.000000',0,'0001-01-01 00:00:00.000000'),(11,'Office Renovation Supplies','Requesting supplies for small office renovation.','2025-04-07 00:00:00.000000',2,'b93f7185-e069-4bdd-852a-00c4df13844a','1000-01-01 00:00:00.000000',0,'0001-01-01 00:00:00.000000'),(12,'Coffee Machine Repair','Coffee machine leaking, needs repair.','2025-04-08 00:00:00.000000',1,'c3992840-42d7-4176-889b-7126b2f17724','2025-04-10 00:00:00.000000',0,'0001-01-01 00:00:00.000000'),(13,'Team Outing Funds','Requesting approval for team building activity.','2025-04-09 00:00:00.000000',2,'b93f7185-e069-4bdd-852a-00c4df13844a','1000-01-01 00:00:00.000000',0,'0001-01-01 00:00:00.000000'),(14,'Cloud Storage Subscription','Upgrade cloud storage plan for department.','2025-04-10 00:00:00.000000',1,'c3992840-42d7-4176-889b-7126b2f17724','2025-04-12 00:00:00.000000',0,'0001-01-01 00:00:00.000000');
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
-- Table structure for table `organizationsdomains`
--

DROP TABLE IF EXISTS `organizationsdomains`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `organizationsdomains` (
  `OrganizationsDomainID` int NOT NULL AUTO_INCREMENT,
  `Domain` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `OrganizationsID` int NOT NULL,
  PRIMARY KEY (`OrganizationsDomainID`),
  KEY `IX_OrganizationsDomains_OrganizationsID` (`OrganizationsID`),
  CONSTRAINT `FK_OrganizationsDomains_Organizations_OrganizationsID` FOREIGN KEY (`OrganizationsID`) REFERENCES `organizations` (`OrganizationID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationsdomains`
--

LOCK TABLES `organizationsdomains` WRITE;
/*!40000 ALTER TABLE `organizationsdomains` DISABLE KEYS */;
/*!40000 ALTER TABLE `organizationsdomains` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vendorprocurementdetails`
--

DROP TABLE IF EXISTS `vendorprocurementdetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vendorprocurementdetails` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ProductName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductCatagory` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductStatus` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `OrderStatus` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductPrice` float NOT NULL,
  `GrandTotal` float NOT NULL,
  `DispachDate` datetime(6) NOT NULL,
  `HardlnvoiceImagePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `VendorID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_VendorProcurementDetails_VendorID` (`VendorID`),
  CONSTRAINT `FK_VendorProcurementDetails_Vendors_VendorID` FOREIGN KEY (`VendorID`) REFERENCES `vendors` (`VendorID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vendorprocurementdetails`
--

LOCK TABLES `vendorprocurementdetails` WRITE;
/*!40000 ALTER TABLE `vendorprocurementdetails` DISABLE KEYS */;
/*!40000 ALTER TABLE `vendorprocurementdetails` ENABLE KEYS */;
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

-- Dump completed on 2025-04-28 12:55:53
