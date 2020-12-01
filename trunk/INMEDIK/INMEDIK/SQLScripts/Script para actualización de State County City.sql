/* Para evitar posibles problemas de pérdida de datos, debe revisar este script detalladamente antes de ejecutarlo fuera del contexto del diseñador de base de datos.*/
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
ALTER TABLE dbo.City
	DROP CONSTRAINT FK_City_State
GO
ALTER TABLE dbo.State SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.State', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.State', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.State', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.City SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.City', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.City', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.City', 'Object', 'CONTROL') as Contr_Per 
GO


/* Para evitar posibles problemas de pérdida de datos, debe revisar este script detalladamente antes de ejecutarlo fuera del contexto del diseñador de base de datos.*/
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
ALTER TABLE dbo.State
	DROP CONSTRAINT PK_State
GO
ALTER TABLE dbo.State SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.State', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.State', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.State', 'Object', 'CONTROL') as Contr_Per 
GO


/* Para evitar posibles problemas de pérdida de datos, debe revisar este script detalladamente antes de ejecutarlo fuera del contexto del diseñador de base de datos.*/
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
CREATE TABLE dbo.Tmp_State
	(
	id int NOT NULL,
	Name varchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_State SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.State)
	 EXEC('INSERT INTO dbo.Tmp_State (id, Name)
		SELECT id, Name FROM dbo.State WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.State
GO
EXECUTE sp_rename N'dbo.Tmp_State', N'State', 'OBJECT' 
GO
COMMIT
select Has_Perms_By_Name(N'dbo.State', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.State', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.State', 'Object', 'CONTROL') as Contr_Per 
GO


UPDATE State SET Name = 'Coahuila' WHERE id = 5
UPDATE State SET Name = 'Colima' WHERE id = 6
UPDATE State SET Name = 'Chiapas' WHERE id = 7
UPDATE State SET Name = 'Chihuahua' WHERE id = 8
GO


/* Para evitar posibles problemas de pérdida de datos, debe revisar este script detalladamente antes de ejecutarlo fuera del contexto del diseñador de base de datos.*/
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
CREATE TABLE dbo.Tmp_State
	(
	id int NOT NULL IDENTITY (1, 1),
	Name varchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_State SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_State ON
GO
IF EXISTS(SELECT * FROM dbo.State)
	 EXEC('INSERT INTO dbo.Tmp_State (id, Name)
		SELECT id, Name FROM dbo.State WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_State OFF
GO
DROP TABLE dbo.State
GO
EXECUTE sp_rename N'dbo.Tmp_State', N'State', 'OBJECT' 
GO
COMMIT
select Has_Perms_By_Name(N'dbo.State', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.State', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.State', 'Object', 'CONTROL') as Contr_Per 
GO



/* Para evitar posibles problemas de pérdida de datos, debe revisar este script detalladamente antes de ejecutarlo fuera del contexto del diseñador de base de datos.*/
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
ALTER TABLE dbo.State ADD CONSTRAINT
	PK_State PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.State SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.State', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.State', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.State', 'Object', 'CONTROL') as Contr_Per 
GO

/* Para evitar posibles problemas de pérdida de datos, debe revisar este script detalladamente antes de ejecutarlo fuera del contexto del diseñador de base de datos.*/
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
ALTER TABLE dbo.State ADD CONSTRAINT
	FK_State_State FOREIGN KEY
	(
	id
	) REFERENCES dbo.State
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.State SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.State', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.State', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.State', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.City ADD CONSTRAINT
	FK_City_State FOREIGN KEY
	(
	StateId
	) REFERENCES dbo.State
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.City SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.City', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.City', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.City', 'Object', 'CONTROL') as Contr_Per 

GO
ALTER TABLE dbo.County ADD
	Code int NULL
GO 

ALTER TABLE dbo.City ADD
	Code int NULL
GO 