/****** Object:  Table [dbo].[Role]    Script Date: 14/09/2017 12:10:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](10) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 14/09/2017 12:10:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[UserAccount] [varchar](50) NOT NULL,
	[UserPassword] [varchar](500) NOT NULL,
	[UserActive] [bit] NULL,
	[UpdatedBy] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 14/09/2017 12:10:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_User_Role] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Role] ON 

GO
INSERT [dbo].[Role] ([id], [Name], [Description]) VALUES (1, N'Admin', N'Administrador del sistema.')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([id], [UserName], [UserAccount], [UserPassword], [UserActive], [UpdatedBy], [CreatedBy], [UpdatedDate], [CreatedDate]) VALUES (1, N'Admin', N'admin@admin.com', N'brMB+NkOsHM81/JOE9nho7tOh8BnIbsg5O1l030BmLGhZmPCBZJWcsUCt5HAODmni1/ikCKBYncuz6gd5AFyIMLNYv8BGrJP3WvO0OecVVcAc4i1v/H+Kh17N87C2WDS', 1, 1, 1, CAST(0x0000A7B5018B7564 AS DateTime), CAST(0x0000A71900B53A26 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (1, 1)
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_User_active]  DEFAULT ((0)) FOR [UserActive]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_User_Role_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_User_Role_Role]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_User_Role_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_User_Role_User]
GO
/****** Object:  Trigger [dbo].[trg_UpdateUser]    Script Date: 14/09/2017 12:10:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[trg_UpdateUser]
ON [dbo].[User]
AFTER UPDATE
AS
    UPDATE dbo.[User]
    SET UpdatedDate = GETDATE()
    WHERE id IN (SELECT DISTINCT id FROM Inserted)


GO
