BEGIN TRANSACTION;
GO

DROP TABLE [MenuRoles];
GO

DROP TABLE [PurcharseDetails];
GO

DROP TABLE [SaleDetails];
GO

DROP TABLE [UserRoles];
GO

DROP TABLE [UsersBranchOffices];
GO

DROP TABLE [Menus];
GO

DROP TABLE [Purcharses];
GO

DROP TABLE [Products];
GO

DROP TABLE [Sales];
GO

DROP TABLE [Roles];
GO

DROP TABLE [BranchOffices];
GO

DROP TABLE [Categories];
GO

DROP TABLE [Providers];
GO

DROP TABLE [Clients];
GO

DROP TABLE [Users];
GO

DROP TABLE [Business];
GO

DROP TABLE [DocumentTypes];
GO

DROP TABLE [Districts];
GO

DROP TABLE [Provinces];
GO

DROP TABLE [Departments];
GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20230806002153_InitialCreate';
GO

COMMIT;
GO

