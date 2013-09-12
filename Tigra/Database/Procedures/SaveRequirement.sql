IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'SaveRequirement')
BEGIN
	DROP PROCEDURE [Tigra].[SaveRequirement];
END
GO

CREATE PROCEDURE [Tigra].[SaveRequirement]
(
	@ReqTypeID SMALLINT,
	@CellID INT,
	@RevisionID BIGINT,
	@UserID INT,
	@Message VARCHAR(250),
	@Title VARCHAR(100),
	@Text TEXT
)
AS
BEGIN

	BEGIN TRANSACTION;

	/* Auxiliary variables. */
	DECLARE @RequirementID INT, @RevisionNumber SMALLINT, @RevisionDate DATETIME2, @RevisionUserID INT;
	
	/* Fetches additional data for this revision. */
	IF (@RevisionID IS NOT NULL)
	BEGIN
		SELECT @RequirementID = [RequirementID], @RevisionNumber = [RevisionNumber]
			, @RevisionDate = [RevisionDate], @RevisionUserID = [UserID]
		FROM [Tigra].[RequirementRevisions]
		WHERE [RevisionID] = @RevisionID;
	END
	/* It's a new requirement. */
	ELSE
	BEGIN
		INSERT INTO [Tigra].[Requirements] ([ReqType], [CellID]) VALUES (@ReqTypeID, @CellID);

		IF (@@ROWCOUNT = 1)
		BEGIN
			SELECT @RequirementID = SCOPE_IDENTITY();
		END
	END

	/* Determines whether revision number should be incremented. */
	IF (COALESCE(@RevisionNumber, 0) < 1)
		OR (DATEDIFF(MINUTE, @RevisionDate, SYSUTCDATETIME()) > 10)
		OR (@RevisionUserID <> @UserID)
	BEGIN
		SELECT @RevisionID = NULL, @RevisionNumber = (COALESCE(@RevisionNumber, 0) + 1);
	END

	/* Get revision ID to update its data. */
	SELECT @RevisionID = [RevisionID] FROM [Tigra].[RequirementRevisions] WHERE [RequirementID] = @RequirementID AND [RevisionNumber] = @RevisionNumber;
	
	/* Creates revision number if it doesn't exists yet. */
	IF (@RevisionID IS NULL)
	BEGIN
		INSERT INTO [Tigra].[RequirementRevisions] ([RequirementID], [RevisionNumber], [RevisionDate], [UserID], [Message], [Title])
		VALUES (@RequirementID, @RevisionNumber, SYSUTCDATETIME(), @UserID, @Message, @Title);

		IF (@@ROWCOUNT = 1)
		BEGIN
			SELECT @RevisionID = SCOPE_IDENTITY();
		END
	END
	/* Or update current revision. */
	ELSE
	BEGIN
		UPDATE [Tigra].[RequirementRevisions] SET [RevisionDate] = SYSUTCDATETIME(), [Message] = @Message, [Title] = @Title
		WHERE [RevisionID] = @RevisionID;
	END

	/* Creates text register if not exists. */
	IF NOT EXISTS (SELECT 1 FROM [Tigra].[RequirementTexts] WHERE [RevisionID] = @RevisionID)
	BEGIN
		INSERT INTO [Tigra].[RequirementTexts] ([RevisionID], [Text]) VALUES (@RevisionID, @Text);
	END
	/* Or update existing. */
	ELSE
	BEGIN
		UPDATE [Tigra].[RequirementTexts] SET [Text] = @Text WHERE [RevisionID] = @RevisionID;
	END

	/* Commits transaction. */
	COMMIT TRANSACTION;

END
GO
