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
	@Tag VARCHAR(100),
	@Title VARCHAR(100),
	@Text TEXT,
	@StoryID BIGINT = NULL
)
AS
BEGIN

	/* Checks whether this tag isn't being used for another requirement. */
	--

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

	/* If it's a reproved revision, mark it to increase a revision number. */
	IF EXISTS (SELECT 1 FROM [Tigra].[RequirementRatings] WHERE [RevisionID] = @RevisionID AND [Approved] = 0)
	BEGIN
		SELECT @ReqTypeID = -49;
	END

	/* If it's publishing, get story ID. */
	IF ((@ReqTypeID = -49 OR @ReqTypeID = -99) AND @RevisionID IS NOT NULL AND @StoryID IS NULL)
	BEGIN
		SELECT @StoryID = [LeftRevisionID] FROM [Tigra].[RequirementRelations] WHERE [RightRevisionID] = @RevisionID;
	END

	/* Determines whether revision number should be incremented. */
	IF (COALESCE(@RevisionNumber, 0) < 1)
		OR (DATEDIFF(MINUTE, @RevisionDate, SYSUTCDATETIME()) > 60)
		OR (@RevisionUserID <> @UserID)
		OR (@ReqTypeID = -49)
		OR (@ReqTypeID = -99)
	BEGIN
		SELECT @RevisionID = NULL, @RevisionNumber = (COALESCE(@RevisionNumber, 0) + 1);
	END

	/* Get revision ID to update its data. */
	SELECT @RevisionID = [RevisionID] FROM [Tigra].[RequirementRevisions] WHERE [RequirementID] = @RequirementID AND [RevisionNumber] = @RevisionNumber;
	
	/* Creates revision number if it doesn't exists yet. */
	IF (@RevisionID IS NULL)
	BEGIN
		INSERT INTO [Tigra].[RequirementRevisions] ([RequirementID], [RevisionNumber], [RevisionDate], [UserID], [Message], [Tag], [Title])
		VALUES (@RequirementID, @RevisionNumber, SYSUTCDATETIME(), @UserID, @Message, @Tag, @Title);

		IF (@@ROWCOUNT = 1)
		BEGIN
			SELECT @RevisionID = SCOPE_IDENTITY();
		END
	END
	/* Or update current revision. */
	ELSE
	BEGIN
		UPDATE [Tigra].[RequirementRevisions] SET [RevisionDate] = SYSUTCDATETIME(), [Message] = @Message, [Tag] = @Tag, [Title] = @Title
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

	/* Register relations. */
	IF (@StoryID IS NOT NULL)
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM [Tigra].[RequirementRelations] WHERE [LeftRevisionID] = @StoryID AND [RightRevisionID] = @RevisionID)
		BEGIN
			INSERT INTO [Tigra].[RequirementRelations] ([LeftRevisionID], [RightRevisionID]) VALUES (@StoryID, @RevisionID);
		END
	END

	/* Mark as done. */
	IF (@ReqTypeID = -99)
	BEGIN
		UPDATE [Tigra].[RequirementRevisions] SET [BaselineDate] = SYSUTCDATETIME() WHERE [RevisionID] = @RevisionID;
	END

	/* Commits transaction. */
	COMMIT TRANSACTION;

END
GO
