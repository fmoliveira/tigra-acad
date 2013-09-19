IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'GetRequirementHistory')
BEGIN
	DROP PROCEDURE [Tigra].[GetRequirementHistory];
END
GO

CREATE PROCEDURE [Tigra].[GetRequirementHistory]
(
	@Tag VARCHAR(100),
	@BaselineDate DATETIME2 = NULL
)
AS
BEGIN

	DECLARE @RequirementID INT;
	SELECT @RequirementID = MAX([RequirementID]) FROM [Tigra].[RequirementRevisions] WHERE [Tag] = @Tag;

	SELECT v.[RevisionNumber], v.[RevisionDate], v.[Message], v.[UserID], v.[Tag], v.[Title], t.[Text]
	FROM [Tigra].[RequirementRevisions] AS v
		INNER JOIN [Tigra].[Requirements] AS r ON r.[RequirementID] = v.[RequirementID]
		INNER JOIN [Tigra].[RequirementTexts] AS t ON t.[RevisionID] = v.[RevisionID]
	WHERE v.[RequirementID] = @RequirementID
	ORDER BY v.[RevisionNumber] ASC;

END
GO
