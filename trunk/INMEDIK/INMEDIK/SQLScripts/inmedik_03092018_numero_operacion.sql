/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
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
ALTER TABLE dbo.Payment ADD
	NumOperation nvarchar(50) NULL
GO
ALTER TABLE dbo.Payment SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Payment', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Payment', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Payment', 'Object', 'CONTROL') as Contr_Per 

go

USE [dbINMEDIK]
GO

/****** Object:  View [dbo].[vwCardPayments]    Script Date: 31/08/2018 18:20:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[vwCardPayments]
AS
SELECT        e.id AS EmployeeId, p.Created, pe.Name + ' ' + pe.LastName AS EmployeeName, o.Ticket, p.Amount, p.Commission, o.ClinicId, p.NumOperation
FROM            dbo.Payment AS p INNER JOIN
                         dbo.PaymentType AS pt ON p.PaymentTypeId = pt.id INNER JOIN
                         dbo.Orders AS o ON p.OrderId = o.id INNER JOIN
                         dbo.Employee AS e ON p.EmployeeId = e.id INNER JOIN
                         dbo.Person AS pe ON e.PersonId = pe.id
WHERE        (pt.Name = 'Tarjeta')

GO


