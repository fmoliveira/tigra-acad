IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'ArchiveRequirement')
BEGIN
	DROP PROCEDURE [Tigra].[ArchiveRequirement];
END
GO

CREATE PROCEDURE [Tigra].[ArchiveRequirement]
(
	@CellID INT,
	@Tag VARCHAR(100)
)
AS
BEGIN

	DECLARE @RevisionID BIGINT;
	SELECT @RevisionID = MAX(v.[RevisionID])
	FROM [Tigra].[Requirements] AS r
		INNER JOIN [Tigra].[RequirementRevisions] AS v ON v.[RequirementID] = r.[RequirementID]
	WHERE r.[CellID] = @CellID AND v.[Tag] = @Tag;

	UPDATE [Tigra].[RequirementRevisions]
	SET [Archived] = 1
	WHERE [RevisionID] = @RevisionID;

END
GO
