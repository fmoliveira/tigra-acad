IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'GetLatestRequirements')
BEGIN
	DROP PROCEDURE [Tigra].[GetLatestRequirements];
END
GO

CREATE PROCEDURE [Tigra].[GetLatestRequirements]
(
	@CellID INT,
	@BaselineDate DATETIME2 = NULL,
	@Type TINYINT /* 1 = stories, 2 = work in progress, 3 = work awaiting revision */
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
		SElECT TOP 5 r.[RequirementID], MAX(v.[RevisionNumber]) AS [RevisionNumber]
		FROM [Tigra].[Requirements] AS r
			INNER JOIN [Tigra].[RequirementRevisions] AS v ON v.RequirementID = r.RequirementID
		WHERE r.[CellID] = @CellID AND v.[RevisionDate] <= @BaselineDate AND
			(
				(@Type = 1 AND r.[ReqType] = -1)
			)
		GROUP BY r.RequirementID
		ORDER BY MAX(v.[RevisionDate])
	)
	SELECT r.[RequirementID], r.[RevisionNumber], v.[RevisionDate], v.[UserID], v.[Tag], v.[Title], SUBSTRING(t.[Text], 0, 200) AS [Text]
	FROM LatestReqs AS r
		INNER JOIN [Tigra].[RequirementRevisions] AS v ON v.[RequirementID] = r.[RequirementID] AND v.[RevisionNumber] = r.[RevisionNumber]
		INNER JOIN [Tigra].[RequirementTexts] AS t ON t.[RevisionID] = v.[RevisionID]
	ORDER BY [Title] ASC;

END
GO
