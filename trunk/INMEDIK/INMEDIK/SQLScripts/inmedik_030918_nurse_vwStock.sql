USE [dbINMEDIK]
GO

/****** Object:  View [dbo].[vwStock]    Script Date: 05/09/2018 05:11:38 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[vwStock]
AS
SELECT        s.ConceptId, c.Name, SUM(s.InStock) AS InStock, s.ClinicId, p.Nurse
FROM            dbo.Stock AS s INNER JOIN
                         dbo.Concept AS c ON c.id = s.ConceptId INNER JOIN
                         dbo.Product AS p ON c.id = p.ConceptId
GROUP BY s.ConceptId, c.Name, s.ClinicId, p.Nurse

GO


