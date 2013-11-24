IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'GetBaselineRequirementDetails')
BEGIN
	DROP PROCEDURE [Tigra].[GetBaselineRequirementDetails];
END
GO

CREATE PROCEDURE [Tigra].[GetBaselineRequirementDetails]
(
	@RevisionID BIGINT
)
AS
BEGIN

	SELECT r.[ReqType], v.[RevisionID], r.[RequirementID], v.[RevisionNumber], v.[RevisionDate]
		, v.[Published], v.[UserID], v.[Tag], v.[Title], t.[Text], v.[BaselineDate]
	FROM [Tigra].[RequirementRevisions] AS v
		INNER JOIN [Tigra].[Requirements] AS r ON r.[RequirementID] = v.[RequirementID]
		INNER JOIN [Tigra].[RequirementTexts] AS t ON t.[RevisionID] = v.[RevisionID]
	WHERE v.[RevisionID] = @RevisionID;

END
GO
