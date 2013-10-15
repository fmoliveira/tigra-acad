IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'GetRatingsIndex')
BEGIN
	DROP PROCEDURE [Tigra].[GetRatingsIndex];
END
GO

CREATE PROCEDURE [Tigra].[GetRatingsIndex]
(
	@CellID INT,
	@UserID INT
)
AS
BEGIN

	/* Select data. */
	;WITH LatestReqs AS
	(
		SElECT r.[RequirementID], MAX(v.[RevisionNumber]) AS [RevisionNumber]
		FROM [Tigra].[Requirements] AS r
			INNER JOIN [Tigra].[RequirementRevisions] AS v ON v.RequirementID = r.RequirementID
		WHERE r.[CellID] = @CellID
		GROUP BY r.RequirementID
	)
	SELECT r.[RequirementID], r.[RevisionNumber], v.[RevisionDate], v.[UserID], v.[Tag], v.[Title]
		, u.[FinalRating] AS [UserFinalRating]
	FROM LatestReqs AS r
		INNER JOIN [Tigra].[RequirementRevisions] AS v ON v.[RequirementID] = r.[RequirementID] AND v.[RevisionNumber] = r.[RevisionNumber]
		LEFT JOIN [Tigra].[RequirementRatings] AS t ON t.[RevisionID] = v.[RevisionID]
		LEFT JOIN [Tigra].[UserRatings] AS u ON u.[RevisionID] = v.[RevisionID]
	WHERE v.[Published] = 1 AND t.[FinalRating] IS NULL
	ORDER BY [Title] ASC;

END
GO
