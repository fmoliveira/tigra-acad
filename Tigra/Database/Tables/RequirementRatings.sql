IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'RequirementRatings')
BEGIN

	CREATE TABLE [Tigra].[RequirementRatings]
	(
		[RevisionID] BIGINT NOT NULL,
		[RatingA] DECIMAL(3,1) NOT NULL CHECK([RatingA] BETWEEN 0 AND 5),
		[RatingB] DECIMAL(3,1) NOT NULL CHECK([RatingB] BETWEEN 0 AND 5),
		[RatingC] DECIMAL(3,1) NOT NULL CHECK([RatingC] BETWEEN 0 AND 5),
		[FinalRating] AS (([RatingA] + [RatingB] + [RatingC]) / 3.0),
		[Approved] BIT NOT NULL,
		CONSTRAINT [pk_Tigra_RequirementRatings_Key] PRIMARY KEY ([RevisionID]),
		CONSTRAINT [fk_Tigra_RequirementRatings_Rev] FOREIGN KEY ([RevisionID]) REFERENCES [Tigra].[RequirementRevisions] ([RevisionID])
	);

END
GO
