/*
   miércoles, 31 de octubre de 201804:30:26 p. m.
   User: usrINMEDIK
   Server: 192.168.1.77
   Database: dbINMEDIK
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
USE [dbINMEDIK]
GO
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.OrderPackage
	DROP CONSTRAINT FK_OrderPackage_Clinic
GO
ALTER TABLE dbo.Clinic SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Clinic', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Clinic', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Clinic', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.OrderPackage
	DROP CONSTRAINT FK_OrderPackage_Employee
GO
ALTER TABLE dbo.Employee SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Employee', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Employee', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Employee', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.OrderPackage
	DROP CONSTRAINT FK_OrderPackage_Package
GO
ALTER TABLE dbo.Package SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Package', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Package', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Package', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.OrderPackage
	DROP CONSTRAINT FK_OrderPackage_Orders
GO
ALTER TABLE dbo.Orders SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Orders', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Orders', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Orders', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.OrderPackage
	DROP CONSTRAINT DF_OrderPackage_Created
GO
ALTER TABLE dbo.OrderPackage
	DROP CONSTRAINT DF_OrderPackage_Updated
GO
CREATE TABLE dbo.Tmp_OrderPackage
	(
	Id int NOT NULL IDENTITY (1, 1),
	OrderId int NOT NULL,
	PackageId int NOT NULL,
	MedicId int NOT NULL,
	ClinicId int NOT NULL,
	Scheduled datetime NULL,
	Medicname varchar(50) NULL,
	Created datetime NOT NULL,
	Updated datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_OrderPackage SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_OrderPackage ADD CONSTRAINT
	DF_OrderPackage_Created DEFAULT (getdate()) FOR Created
GO
ALTER TABLE dbo.Tmp_OrderPackage ADD CONSTRAINT
	DF_OrderPackage_Updated DEFAULT (getdate()) FOR Updated
GO
SET IDENTITY_INSERT dbo.Tmp_OrderPackage OFF
GO
IF EXISTS(SELECT * FROM dbo.OrderPackage)
	 EXEC('INSERT INTO dbo.Tmp_OrderPackage (OrderId, PackageId, MedicId, ClinicId, Scheduled, Medicname, Created, Updated)
		SELECT OrderId, PackageId, MedicId, ClinicId, Scheduled, Medicname, Created, Updated FROM dbo.OrderPackage WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.OrderPackage
GO
EXECUTE sp_rename N'dbo.Tmp_OrderPackage', N'OrderPackage', 'OBJECT' 
GO
ALTER TABLE dbo.OrderPackage ADD CONSTRAINT
	PK_OrderPackage_1 PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.OrderPackage ADD CONSTRAINT
	FK_OrderPackage_Orders FOREIGN KEY
	(
	OrderId
	) REFERENCES dbo.Orders
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.OrderPackage ADD CONSTRAINT
	FK_OrderPackage_Package FOREIGN KEY
	(
	PackageId
	) REFERENCES dbo.Package
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.OrderPackage ADD CONSTRAINT
	FK_OrderPackage_Employee FOREIGN KEY
	(
	MedicId
	) REFERENCES dbo.Employee
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.OrderPackage ADD CONSTRAINT
	FK_OrderPackage_Clinic FOREIGN KEY
	(
	ClinicId
	) REFERENCES dbo.Clinic
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.OrderPackage', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.OrderPackage', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.OrderPackage', 'Object', 'CONTROL') as Contr_Per 

go

USE [dbINMEDIK]
GO

/****** Object:  Table [dbo].[CancelledPackages]    Script Date: 04/12/2018 04:55:14 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CancelledPackages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderPackageId] [int] NULL,
	[PackageId] [int] NULL,
	[NewOrder] [int] NULL,
	[OldOrder] [int] NULL,
	[Reason] [varchar](100) NULL,
	[Created] [datetime] NULL,
	[Updated] [datetime] NULL,
	[ClinicId] [int] NULL,
	[IsCanceled] [bit] NULL,
	[Refund] [numeric](8, 2) NULL,
 CONSTRAINT [PK__Cancelle__3214EC07A56A3D4E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CancelledPackages]  WITH CHECK ADD  CONSTRAINT [FK_CancelledPackages_OrderPackage] FOREIGN KEY([OrderPackageId])
REFERENCES [dbo].[OrderPackage] ([Id])
GO

ALTER TABLE [dbo].[CancelledPackages] CHECK CONSTRAINT [FK_CancelledPackages_OrderPackage]
GO

ALTER TABLE [dbo].[CancelledPackages]  WITH CHECK ADD  CONSTRAINT [FK_CancelledPackages_Orders] FOREIGN KEY([OldOrder])
REFERENCES [dbo].[Orders] ([id])
GO

ALTER TABLE [dbo].[CancelledPackages] CHECK CONSTRAINT [FK_CancelledPackages_Orders]
GO

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.Product.code', N'Tmp_Code', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Product.Tmp_Code', N'Code', 'COLUMN' 
GO
ALTER TABLE dbo.Product SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Product', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Product', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Product', 'Object', 'CONTROL') as Contr_Per 
