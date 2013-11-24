IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'SelectRequirement')
BEGIN
	DROP PROCEDURE [Tigra].[SelectRequirement];
END
GO

CREATE PROCEDURE [Tigra].[SelectRequirement]
(
	@Cell VARCHAR(50),
	@LeftTag VARCHAR(100),
	@RightTag VARCHAR(100),
	@UserID INT,
	@Message VARCHAR(250)
)
AS
BEGIN

	BEGIN TRANSACTION;

	DECLARE @CellID INT, @LeftRevisionID BIGINT, @RightRevisionID BIGINT, @NewRightID BIGINT;

	SELECT @CellID = [CellID] FROM [Tigra].[Cells] WHERE [Tag] = @Cell;
	SELECT @LeftRevisionID = MAX([RevisionID]) FROM [Tigra].[RequirementRevisions] WHERE [Tag] = @LeftTag;
	SELECT @RightRevisionID = MAX([RevisionID]) FROM [Tigra].[RequirementRevisions] WHERE [Tag] = @RightTag;

	/* Cria a nova revisão. */
	INSERT INTO [Tigra].[RequirementRevisions] ([RequirementID], [RevisionNumber], [RevisionDate], [UserID], [Message], [Tag], [Title])
	SELECT [RequirementID], ([RevisionNumber] + 1), SYSUTCDATETIME(), @UserID, @Message, [Tag], [Title]
	FROM [Tigra].[RequirementRevisions]
	WHERE [RevisionID] = @RightRevisionID;
	
	SELECT @NewRightID = MAX([RevisionID]) FROM [Tigra].[RequirementRevisions] WHERE [Tag] = @RightTag;
	
	/* Copia o texto. */
	INSERT INTO [Tigra].[RequirementTexts] ([RevisionID], [Text])
	SELECT @NewRightID, [Text] FROM [Tigra].[RequirementTexts] WHERE [RevisionID] = @RightRevisionID;

	/* Cria o relacionamento. */
	INSERT INTO [Tigra].[RequirementRelations] ([LeftRevisionID], [RightRevisionID])
	VALUES (@LeftRevisionID, @NewRightID);

	COMMIT TRANSACTION;

END
GO
