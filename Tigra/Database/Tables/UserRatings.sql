IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'UserRatings')
BEGIN

	CREATE TABLE [Tigra].[UserRatings]
	(
		[RevisionID] BIGINT IDENTITY(1,1) NOT NULL,
		[UserID] INT NOT NULL,
		[RatingA] TINYINT NOT NULL CHECK([RatingA] BETWEEN 1 AND 5),
		[RatingB] TINYINT NOT NULL CHECK([RatingB] BETWEEN 1 AND 5),
		[RatingC] TINYINT NOT NULL CHECK([RatingC] BETWEEN 1 AND 5),
		[FinalRating] AS (([RatingA] + [RatingB] + [RatingC]) / 3.0),
		[Comments] VARCHAR(250) NOT NULL,
		CONSTRAINT [pk_Tigra_UserRatings_Key] PRIMARY KEY ([RevisionID], [UserID]),
		CONSTRAINT [fk_Tigra_UserRatings_Rev] FOREIGN KEY ([RevisionID]) REFERENCES [Tigra].[RequirementRevisions] ([RevisionID]),
		CONSTRAINT [fk_Tigra_UserRatings_Usr] FOREIGN KEY ([UserID]) REFERENCES [Tigra].[UserAccounts] ([UserId])
	);

END
GO
