IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'GetRequirementsForBaseline')
BEGIN
	DROP PROCEDURE [Tigra].[GetRequirementsForBaseline];
END
GO

CREATE PROCEDURE [Tigra].[GetRequirementsForBaseline]
(
	@CellID INT
)
AS
BEGIN

	DECLARE @LatestBaseline DATETIME2;
	SELECT @LatestBaseline = MAX(SetDate) FROM [Tigra].[Baseline];

	;WITH LatestReqs AS
	(
		SElECT r.[RequirementID], MAX(v.[RevisionNumber]) AS [RevisionNumber]
		FROM [Tigra].[Requirements] AS r
			INNER JOIN [Tigra].[RequirementRevisions] AS v ON v.RequirementID = r.RequirementID
		WHERE r.[CellID] = @CellID AND r.ReqType = -2
		GROUP BY r.RequirementID
	)
	SELECT r.[RequirementID], v.[RevisionID], r.[RevisionNumber], v.[RevisionDate], v.[UserID], v.[Tag], v.[Title]
		, v.[Published], v.[BaselineDate]
	FROM LatestReqs AS r
		INNER JOIN [Tigra].[RequirementRevisions] AS v ON v.[RequirementID] = r.[RequirementID] AND v.[RevisionNumber] = r.[RevisionNumber]
	WHERE v.[BaselineDate] IS NOT NULL AND (@LatestBaseline IS NULL OR v.[BaselineDate] < @LatestBaseline)
	ORDER BY [Title] ASC;

END
GO
