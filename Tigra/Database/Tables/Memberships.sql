IF OBJECT_ID('[Tigra].[Memberships]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[Memberships]
	(
		[UserId] INT NOT NULL,
		[ProjectId] INT NOT NULL,
		[RoleId] INT NOT NULL,
		CONSTRAINT [pk_TigraMemberships_Key] PRIMARY KEY ([UserId], [ProjectId], [RoleId]),
		CONSTRAINT [fk_TigraMemberships_UserId] FOREIGN KEY ([UserId]) REFERENCES [Tigra].[Users] ([UserId]),
		CONSTRAINT [fk_TigraMemberships_ProjectId] FOREIGN KEY ([UserId]) REFERENCES [Tigra].[Projects] ([ProjectId]),
		CONSTRAINT [fk_TigraMemberships_RoleId] FOREIGN KEY ([UserId]) REFERENCES [Tigra].[Roles] ([RoleId])
	);

END
GO
