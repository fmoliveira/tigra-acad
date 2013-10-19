IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'SubmitRating')
BEGIN
	DROP PROCEDURE [Tigra].[SubmitRating];
END
GO

CREATE PROCEDURE [Tigra].[SubmitRating]
(
	@RevisionID BIGINT,
	@UserID INT,
	@RatingA TINYINT,
	@RatingB TINYINT,
	@RatingC TINYINT,
	@Comments VARCHAR(250)
)
AS
BEGIN

	/* Inserts user rating. */
	IF NOT EXISTS (SELECT 1 FROM [Tigra].[UserRatings] WHERE [RevisionID] = @RevisionID AND [UserID] = @UserID)
	BEGIN
		INSERT INTO [Tigra].[UserRatings] (RevisionID, UserID, RatingA, RatingB, RatingC, Comments)
		VALUES (@RevisionID, @UserID, @RatingA, @RatingB, @RatingC, @Comments);
	END

	DECLARE @Members INT, @Ratings INT, @AvgA DECIMAL(3,1), @AvgB DECIMAL(3,1), @AvgC DECIMAL(3,1);

	/* Checks if the whole team has already rated. */
	SELECT @Members = COUNT(t.UserID), @Ratings = COUNT(u.UserID)
		, @AvgA = AVG((u.RatingA * 1.0)), @AvgB = AVG((u.RatingB * 1.0)), @AvgC = AVG((u.RatingC * 1.0))
	FROM [Tigra].[RequirementRevisions] AS v
		INNER JOIN [Tigra].[Requirements] AS r ON r.[RequirementID] = v.[RequirementID]
		INNER JOIN [Tigra].[Teams] AS t ON t.[CellID] = r.[CellID] AND t.UserID <> v.UserId
		LEFT JOIN [Tigra].[UserRatings] AS u ON u.RevisionID = @RevisionID AND u.UserID = t.UserID
	WHERE v.[RevisionID] = @RevisionID;

	/* Then insert the average rating and check if revision was approved. */
	IF (@Members = @Ratings)
	BEGIN
		DELETE FROM [Tigra].[RequirementRatings] WHERE [RevisionID] = @RevisionID;

		INSERT INTO [Tigra].[RequirementRatings] (RevisionID, RatingA, RatingB, RatingC, Approved)
		VALUES (@RevisionID, @AvgA, @AvgB, @AvgC, 0);

		UPDATE [Tigra].[RequirementRatings]
		SET Approved = (CASE WHEN [FinalRating] >= 3 THEN 1 ELSE 0 END)
		WHERE [RevisionID] = @RevisionID;
	END

END
GO
