USE [dbINMEDIK]
GO

/****** Object:  View [dbo].[vwSales]    Script Date: 05/09/18 16:32:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwSales]
AS
SELECT        dbo.Orders.id AS OrderId, ISNULL(dbo.Orders.Ticket, '') AS OrderTicket, ISNULL(CAST(dbo.Orders.Ticket AS varchar) + ' - ' + CAST(dbo.Payment.id AS varchar), '') AS Ticket, dbo.Clinic.id AS ClinicId, 
                         dbo.Clinic.Name AS ClinicName, dbo.Employee.id AS EmployeeId, dbo.Person.Name AS EmployeeName, dbo.Person.LastName AS EmployeeLastName, 
                         dbo.Person.Name + ' ' + dbo.Person.LastName AS EmployeeFullName, dbo.Patient.id AS PatientId, P.Name AS PatientName, P.LastName AS PatientLastName, P.Name + ' ' + P.LastName AS PatientFullName, 
                         dbo.Orders.Total, dbo.Payment.Amount, dbo.Orders.Discount, dbo.Orders.Paid, dbo.Orders.IsCanceled, FORMAT(dbo.Payment.Created, 'dd/MM/yyyy HH:mm:ss') AS sCreated, 
                         dbo.Payment.Created AS PaymentCreated
FROM            dbo.Orders INNER JOIN
                         dbo.Payment ON dbo.Orders.id = dbo.Payment.OrderId INNER JOIN
                         dbo.Clinic ON dbo.Payment.ClinicId = dbo.Clinic.id INNER JOIN
                         dbo.Employee ON dbo.Payment.EmployeeId = dbo.Employee.id INNER JOIN
                         dbo.Person ON dbo.Employee.PersonId = dbo.Person.id INNER JOIN
                         dbo.Patient ON dbo.Orders.PatientId = dbo.Patient.id INNER JOIN
                         dbo.Person AS P ON dbo.Patient.PersonId = P.id

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[25] 4[36] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Orders"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Payment"
            Begin Extent = 
               Top = 6
               Left = 285
               Bottom = 136
               Right = 494
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Clinic"
            Begin Extent = 
               Top = 6
               Left = 532
               Bottom = 136
               Right = 741
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employee"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Person"
            Begin Extent = 
               Top = 138
               Left = 285
               Bottom = 268
               Right = 494
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Patient"
            Begin Extent = 
               Top = 138
               Left = 532
               Bottom = 268
               Right = 741
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "P"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 247
            End
            DisplayFlags = 280
         ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwSales'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'   TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1800
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 600
         Or = 450
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwSales'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwSales'
GO

