IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'GetRequirementDetails')
BEGIN
	DROP PROCEDURE [Tigra].[GetRequirementDetails];
END
GO

CREATE PROCEDURE [Tigra].[GetRequirementDetails]
(
	@Tag VARCHAR(100),
	@BaselineDate DATETIME2 = NULL
)
AS
BEGIN

	DECLARE @RevisionNumber INT;

	IF (@BaselineDate IS NULL)
	BEGIN
		SELECT @RevisionNumber = MAX([RevisionNumber]) FROM [Tigra].[RequirementRevisions] WHERE [Tag] = @Tag;
	END

	SELECT r.[ReqType], v.[RevisionID], r.[RequirementID], v.[RevisionNumber], v.[RevisionDate], v.[UserID], v.[Tag], v.[Title], t.[Text]
	FROM [Tigra].[RequirementRevisions] AS v
		INNER JOIN [Tigra].[Requirements] AS r ON r.[RequirementID] = v.[RequirementID]
		INNER JOIN [Tigra].[RequirementTexts] AS t ON t.[RevisionID] = v.[RevisionID]
	WHERE v.[Tag] = @Tag AND v.[RevisionNumber] = @RevisionNumber;

END
GO
