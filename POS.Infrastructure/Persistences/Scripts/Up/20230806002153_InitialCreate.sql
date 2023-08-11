IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categories] (
    [CategoryId] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NULL,
    [Description] nvarchar(max) NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK__Categori__19093A0BD10B69EE] PRIMARY KEY ([CategoryId])
);
GO

CREATE TABLE [Departments] (
    [DepartmentId] int NOT NULL IDENTITY,
    [Name] varchar(100) NOT NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK__Departme__B2079BEDDC4C2BEA] PRIMARY KEY ([DepartmentId])
);
GO

CREATE TABLE [DocumentTypes] (
    [DocumentTypeId] int NOT NULL IDENTITY,
    [Code] varchar(10) NULL,
    [Name] varchar(255) NULL,
    [Abbreviation] varchar(5) NULL,
    [State] int NULL,
    CONSTRAINT [PK__Document__DBA390E1706A9C46] PRIMARY KEY ([DocumentTypeId])
);
GO

CREATE TABLE [Menus] (
    [MenuId] int NOT NULL IDENTITY,
    [Name] varchar(150) NULL,
    [Icon] varchar(50) NULL,
    [URL] varchar(150) NULL,
    [FatherId] int NULL,
    [State] int NULL,
    CONSTRAINT [PK__Menus__C99ED230C8098F30] PRIMARY KEY ([MenuId])
);
GO

CREATE TABLE [Roles] (
    [RoleId] int NOT NULL IDENTITY,
    [Description] varchar(50) NULL,
    [State] int NULL,
    CONSTRAINT [PK__Roles__8AFACE1AB26511B3] PRIMARY KEY ([RoleId])
);
GO

CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [UserName] varchar(50) NULL,
    [Password] varchar(max) NULL,
    [Email] varchar(max) NULL,
    [Image] varchar(max) NULL,
    [AuthType] varchar(15) NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK__Users__19093A0BD10B69EE] PRIMARY KEY ([UserId])
);
GO

CREATE TABLE [Provinces] (
    [ProvinceId] int NOT NULL IDENTITY,
    [Name] varchar(100) NOT NULL,
    [DepartmentId] int NOT NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK__Province__FD0A6F83158EFCF8] PRIMARY KEY ([ProvinceId]),
    CONSTRAINT [FK__Provinces__Depar__52593CB8] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([DepartmentId])
);
GO

CREATE TABLE [Clients] (
    [ClientId] int NOT NULL IDENTITY,
    [Name] varchar(100) NULL,
    [DocumentTypeId] int NOT NULL,
    [DocumentNumber] varchar(20) NULL,
    [Address] varchar(max) NULL,
    [Phone] varchar(20) NULL,
    [Email] varchar(255) NULL,
    [State] int NOT NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK__Clients__E67E1A242B1383CE] PRIMARY KEY ([ClientId]),
    CONSTRAINT [FK__Clients__Documen__4BAC3F29] FOREIGN KEY ([DocumentTypeId]) REFERENCES [DocumentTypes] ([DocumentTypeId])
);
GO

CREATE TABLE [Providers] (
    [ProviderId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(255) NOT NULL,
    [DocumentTypeId] int NOT NULL,
    [DocumentNumber] varchar(20) NOT NULL,
    [Address] nvarchar(max) NULL,
    [Phone] nvarchar(15) NOT NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK__Provider__B54C687DA1314900] PRIMARY KEY ([ProviderId]),
    CONSTRAINT [FK__Providers__Docum__5165187F] FOREIGN KEY ([DocumentTypeId]) REFERENCES [DocumentTypes] ([DocumentTypeId])
);
GO

CREATE TABLE [MenuRoles] (
    [MenuRolId] int NOT NULL IDENTITY,
    [RoleId] int NULL,
    [MenuId] int NULL,
    [State] int NULL,
    CONSTRAINT [PK__MenuRole__6640AD0C9151FDC0] PRIMARY KEY ([MenuRolId]),
    CONSTRAINT [FK_MenuRoles_Menu] FOREIGN KEY ([MenuId]) REFERENCES [Menus] ([MenuId]),
    CONSTRAINT [FK_MenuRoles_Roles] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([RoleId])
);
GO

CREATE TABLE [Districts] (
    [DistrictId] int NOT NULL IDENTITY,
    [ProvinceId] int NOT NULL,
    [Name] varchar(100) NOT NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK__District__85FDA4C606F47DE5] PRIMARY KEY ([DistrictId]),
    CONSTRAINT [FK_Districts_Provinces] FOREIGN KEY ([ProvinceId]) REFERENCES [Provinces] ([ProvinceId])
);
GO

CREATE TABLE [Sales] (
    [SaleId] int NOT NULL IDENTITY,
    [ClientId] int NULL,
    [UserId] int NULL,
    [SaleDate] datetime2 NULL,
    [Tax] decimal(18,2) NULL,
    [Total] decimal(18,2) NULL,
    [State] int NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK__Sales__1EE3C3FF189AA26E] PRIMARY KEY ([SaleId]),
    CONSTRAINT [FK__Sales__ClientId__59063A47] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([ClientId]),
    CONSTRAINT [FK__Sales__UserId__59FA5E80] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
);
GO

CREATE TABLE [Products] (
    [ProductId] int NOT NULL IDENTITY,
    [Code] nvarchar(max) NULL,
    [Name] nvarchar(50) NULL,
    [Stock] int NOT NULL,
    [Image] nvarchar(max) NULL,
    [SellPrice] decimal(18,2) NOT NULL,
    [CategoryId] int NOT NULL,
    [ProviderId] int NOT NULL,
    [State] int NOT NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK__Products__B40CC6CDE0B0AAE1] PRIMARY KEY ([ProductId]),
    CONSTRAINT [FK__Products__Catego__4F7CD00D] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]),
    CONSTRAINT [FK__Products__Provid__5070F446] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([ProviderId])
);
GO

CREATE TABLE [Purcharses] (
    [PurcharseId] int NOT NULL IDENTITY,
    [ProviderId] int NULL,
    [UserId] int NULL,
    [PurcharseDate] datetime2 NULL,
    [Tax] decimal(18,2) NULL,
    [Total] decimal(18,2) NULL,
    [State] int NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK__Purchars__A98C674B6C884DE6] PRIMARY KEY ([PurcharseId]),
    CONSTRAINT [FK__Purcharse__Provi__5535A963] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([ProviderId]),
    CONSTRAINT [FK__Purcharse__UserI__5629CD9C] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
);
GO

CREATE TABLE [Business] (
    [BusinessId] int NOT NULL IDENTITY,
    [Code] varchar(100) NOT NULL,
    [Ruc] varchar(11) NOT NULL,
    [BusinessName] varchar(100) NOT NULL,
    [Logo] varchar(max) NOT NULL,
    [DistrictId] int NOT NULL,
    [Address] varchar(max) NOT NULL,
    [Email] varchar(100) NOT NULL,
    [CreationDate] datetime NOT NULL,
    [Phone] varchar(100) NOT NULL,
    [Vision] varchar(max) NULL,
    [Mision] varchar(max) NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK__Business__F1EAA36E43C55705] PRIMARY KEY ([BusinessId]),
    CONSTRAINT [FK__Business__Distri__4AB81AF0] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([DistrictId])
);
GO

CREATE TABLE [SaleDetails] (
    [SaleDetailId] int NOT NULL IDENTITY,
    [SaleId] int NULL,
    [ProductId] int NULL,
    [Quantity] int NULL,
    [Price] decimal(18,2) NULL,
    [Discount] decimal(18,2) NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK__SaleDeta__70DB14FE32D7198B] PRIMARY KEY ([SaleDetailId]),
    CONSTRAINT [FK__SaleDetai__Produ__571DF1D5] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]),
    CONSTRAINT [FK__SaleDetai__SaleI__5812160E] FOREIGN KEY ([SaleId]) REFERENCES [Sales] ([SaleId])
);
GO

CREATE TABLE [PurcharseDetails] (
    [PurcharseDetailId] int NOT NULL IDENTITY,
    [PurcharseId] int NULL,
    [ProductId] int NULL,
    [Quantity] int NULL,
    [Price] decimal(18,2) NULL,
    [AuditCreateUser] int NOT NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] int NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] int NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK__Purchars__7353248BA65F057B] PRIMARY KEY ([PurcharseDetailId]),
    CONSTRAINT [FK__Purcharse__Produ__534D60F1] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]),
    CONSTRAINT [FK__Purcharse__Purch__5441852A] FOREIGN KEY ([PurcharseId]) REFERENCES [Purcharses] ([PurcharseId])
);
GO

CREATE TABLE [BranchOffices] (
    [BranchOfficeId] int NOT NULL IDENTITY,
    [Code] varchar(100) NOT NULL,
    [BusinessId] int NOT NULL,
    [Name] varchar(100) NOT NULL,
    [Description] varchar(100) NOT NULL,
    [DistrictId] int NOT NULL,
    [Address] varchar(max) NOT NULL,
    [Email] varchar(100) NOT NULL,
    [Phone] varchar(100) NOT NULL,
    [State] int NOT NULL,
    CONSTRAINT [PK__BranchOf__27247FF9AFD4029D] PRIMARY KEY ([BranchOfficeId]),
    CONSTRAINT [FK__BranchOff__Busin__48CFD27E] FOREIGN KEY ([BusinessId]) REFERENCES [Business] ([BusinessId]),
    CONSTRAINT [FK__BranchOff__Distr__49C3F6B7] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([DistrictId])
);
GO

CREATE TABLE [UserRoles] (
    [UserRoleId] int NOT NULL IDENTITY,
    [RoleId] int NULL,
    [UserId] int NULL,
    [State] int NULL,
    [BranchOfficeId] int NULL,
    CONSTRAINT [PK__UserRole__3D978A35FE4D756B] PRIMARY KEY ([UserRoleId]),
    CONSTRAINT [FK__UserRoles__Branc__5AEE82B9] FOREIGN KEY ([BranchOfficeId]) REFERENCES [BranchOffices] ([BranchOfficeId]),
    CONSTRAINT [FK__UserRoles__RoleI__5BE2A6F2] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([RoleId]),
    CONSTRAINT [FK__UserRoles__UserI__5CD6CB2B] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
);
GO

CREATE TABLE [UsersBranchOffices] (
    [UserBranchOfficeId] int NOT NULL IDENTITY,
    [BranchOfficeId] int NULL,
    [UserId] int NULL,
    [State] int NULL,
    CONSTRAINT [PK__UsersBra__7D1E804A51C934BA] PRIMARY KEY ([UserBranchOfficeId]),
    CONSTRAINT [FK__UsersBran__Branc__5DCAEF64] FOREIGN KEY ([BranchOfficeId]) REFERENCES [BranchOffices] ([BranchOfficeId]),
    CONSTRAINT [FK__UsersBran__UserI__5EBF139D] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
);
GO

CREATE INDEX [IX_BranchOffices_BusinessId] ON [BranchOffices] ([BusinessId]);
GO

CREATE INDEX [IX_BranchOffices_DistrictId] ON [BranchOffices] ([DistrictId]);
GO

CREATE INDEX [IX_Business_DistrictId] ON [Business] ([DistrictId]);
GO

CREATE INDEX [IX_Clients_DocumentTypeId] ON [Clients] ([DocumentTypeId]);
GO

CREATE INDEX [IX_Districts_ProvinceId] ON [Districts] ([ProvinceId]);
GO

CREATE INDEX [IX_MenuRoles_MenuId] ON [MenuRoles] ([MenuId]);
GO

CREATE INDEX [IX_MenuRoles_RoleId] ON [MenuRoles] ([RoleId]);
GO

CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
GO

CREATE INDEX [IX_Products_ProviderId] ON [Products] ([ProviderId]);
GO

CREATE INDEX [IX_Providers_DocumentTypeId] ON [Providers] ([DocumentTypeId]);
GO

CREATE INDEX [IX_Provinces_DepartmentId] ON [Provinces] ([DepartmentId]);
GO

CREATE INDEX [IX_PurcharseDetails_ProductId] ON [PurcharseDetails] ([ProductId]);
GO

CREATE INDEX [IX_PurcharseDetails_PurcharseId] ON [PurcharseDetails] ([PurcharseId]);
GO

CREATE INDEX [IX_Purcharses_ProviderId] ON [Purcharses] ([ProviderId]);
GO

CREATE INDEX [IX_Purcharses_UserId] ON [Purcharses] ([UserId]);
GO

CREATE INDEX [IX_SaleDetails_ProductId] ON [SaleDetails] ([ProductId]);
GO

CREATE INDEX [IX_SaleDetails_SaleId] ON [SaleDetails] ([SaleId]);
GO

CREATE INDEX [IX_Sales_ClientId] ON [Sales] ([ClientId]);
GO

CREATE INDEX [IX_Sales_UserId] ON [Sales] ([UserId]);
GO

CREATE INDEX [IX_UserRoles_BranchOfficeId] ON [UserRoles] ([BranchOfficeId]);
GO

CREATE INDEX [IX_UserRoles_RoleId] ON [UserRoles] ([RoleId]);
GO

CREATE INDEX [IX_UserRoles_UserId] ON [UserRoles] ([UserId]);
GO

CREATE INDEX [IX_UsersBranchOffices_BranchOfficeId] ON [UsersBranchOffices] ([BranchOfficeId]);
GO

CREATE INDEX [IX_UsersBranchOffices_UserId] ON [UsersBranchOffices] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230806002153_InitialCreate', N'7.0.3');
GO

COMMIT;
GO

