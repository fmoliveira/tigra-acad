IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'SubmitRating')
BEGIN
	DROP PROCEDURE [Tigra].[SubmitRating];
END
GO

CREATE PROCEDURE [Tigra].[SubmitRating]
(
	@RevisionID BIGINT,
	@UserID INT,
	@Approved BIT,
	@Comments VARCHAR(250)
)
AS
BEGIN

	/* Inserts user rating. */
	IF NOT EXISTS (SELECT 1 FROM [Tigra].[UserRatings] WHERE [RevisionID] = @RevisionID AND [UserID] = @UserID)
	BEGIN
		INSERT INTO [Tigra].[UserRatings] (RevisionID, UserID, Approved, Comments)
		VALUES (@RevisionID, @UserID, @Approved, @Comments);
	END

	/* Updates requirement rating. */
	DELETE FROM [Tigra].[RequirementRatings] WHERE [RevisionID] = @RevisionID;
	
	INSERT INTO [Tigra].[RequirementRatings] (RevisionID, Approved)
	VALUES (@RevisionID, @Approved);

END
GO
