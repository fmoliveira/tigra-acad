IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'GetRequirementsIndex')
BEGIN
	DROP PROCEDURE [Tigra].[GetRequirementsIndex];
END
GO

CREATE PROCEDURE [Tigra].[GetRequirementsIndex]
(
	@CellID INT,
	@ReqTypeID SMALLINT,
	@BaselineDate DATETIME2 = NULL
)
AS
BEGIN

	DECLARE @BaselineID INT;

	/* If baseline ID is set, then retrieve its set date. */
	IF (@BaselineID IS NOT NULL)
	BEGIN
		SELECT @BaselineDate = SetDate FROM [Tigra].[Baseline] WHERE @BaselineID = BaselineID;
	END

	/* If not date is set, retrieve current version. */
	IF (@BaselineDate IS NULL)
	BEGIN
		SELECT @BaselineDate = SYSUTCDATETIME();
	END

	/* Select data. */
	;WITH LatestReqs AS
	(
		SElECT r.[RequirementID], MAX(v.[RevisionNumber]) AS [RevisionNumber]
		FROM [Tigra].[Requirements] AS r
			INNER JOIN [Tigra].[RequirementRevisions] AS v ON v.RequirementID = r.RequirementID
		WHERE r.[CellID] = @CellID AND r.ReqType = @ReqTypeID AND v.[RevisionDate] <= @BaselineDate
		GROUP BY r.RequirementID
	)
	SELECT r.[RequirementID], v.[RevisionID], r.[RevisionNumber], v.[RevisionDate], v.[UserID], v.[Tag], v.[Title], v.[Published]
	FROM LatestReqs AS r
		INNER JOIN [Tigra].[RequirementRevisions] AS v ON v.[RequirementID] = r.[RequirementID] AND v.[RevisionNumber] = r.[RevisionNumber]
	ORDER BY [Title] ASC;

END
GO
