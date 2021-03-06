IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'GetBaselineRequirements')
BEGIN
	DROP PROCEDURE [Tigra].[GetBaselineRequirements];
END
GO

CREATE PROCEDURE [Tigra].[GetBaselineRequirements]
(
	@BaselineID INT
)
AS
BEGIN

	DECLARE @BaselineDate DATETIME2;
	SELECT @BaselineDate = SetDate FROM [Tigra].[Baseline] WHERE [BaselineID] = @BaselineID;

	WITH [BaselineRevisions] AS
	(
		SELECT v.[RequirementID], MAX(v.[RevisionNumber]) AS [RevisionNumber]
		FROM [Tigra].[Requirements] AS r
			INNER JOIN [Tigra].[RequirementRevisions] AS v ON v.[RequirementID] = r.[RequirementID]
		WHERE v.[BaselineDate] <= @BaselineDate AND r.[ReqType] = -2
		GROUP BY v.[RequirementID]
	)
	SELECT v.[RevisionID], v.[RevisionNumber], v.[UserID], v.[Title], v.[BaselineDate]
	FROM [BaselineRevisions] AS b
		INNER JOIN [Tigra].[RequirementRevisions] AS v ON v.[RequirementID] = b.[RequirementID] AND v.[RevisionNumber] = b.[RevisionNumber]
	ORDER BY Title;

END
GO
