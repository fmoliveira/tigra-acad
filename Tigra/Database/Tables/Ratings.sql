IF OBJECT_ID('[Tigra].[Ratings]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[Ratings]
	(
		[RequirementId] INT NOT NULL,
		[Revision] INT NOT NULL,
		[UserId] INT NOT NULL,
		[Modified] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
		[Rating] TINYINT NOT NULL,
		[Comment] VARCHAR(512) NOT NULL,
		CONSTRAINT [pk_TigraRatings_Key] PRIMARY KEY ([RequirementId], [Revision], [UserId]),
		CONSTRAINT [pk_TigraRatings_Uid] FOREIGN KEY ([UserId]) REFERENCES [Tigra].[Users] ([UserId]),
		CONSTRAINT [ck_TigraRatings_Stars] CHECK ([Rating] BETWEEN 1 AND 5)
	);

END
GO
