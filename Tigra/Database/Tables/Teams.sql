IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'Teams')
BEGIN

	CREATE TABLE [Tigra].[Teams]
	(
		[CellID] INT NOT NULL,
		[UserID] INT NOT NULL,
		[RoleID] INT NOT NULL,
		CONSTRAINT [pk_Tigra_Teams_Key] PRIMARY KEY ([CellID], [UserID]),
		CONSTRAINT [fk_Tigra_Teams_Cell] FOREIGN KEY ([CellID]) REFERENCES [Tigra].[Cells] ([CellID]),
		CONSTRAINT [fk_Tigra_Teams_User] FOREIGN KEY ([UserID]) REFERENCES [Tigra].[UserAccounts] ([UserID]),
		CONSTRAINT [fk_Tigra_Teams_Role] FOREIGN KEY ([RoleID]) REFERENCES [Tigra].[Roles] ([RoleID])
	);

END
GO