/* Get the average rating of a given user from all the requirement's revisions it has authored. */

IF OBJECT_ID('[Tigra].[GetUserRating]') IS NOT NULL
BEGIN
	DROP FUNCTION [Tigra].[GetUserRating];
END
GO

CREATE FUNCTION [Tigra].[GetUserRating]
(
	@UserId INT
)
RETURNS INT
AS
BEGIN

	DECLARE @UserRating DECIMAL(5,1);

	SELECT @UserRating = CONVERT(INT, AVG(CONVERT(DECIMAL(5,1), rtg.[Rating])))
	FROM [Tigra].[Users] AS usr
		INNER JOIN [Tigra].[RequirementRevisions] AS rev ON rev.[UserId] = usr.[UserId]
		INNER JOIN [Tigra].[Ratings] AS rtg ON rtg.[RequirementId] = rev.[RequirementId] AND rtg.[Revision] = rev.[Revision];

	RETURN @UserRating;

END
GO
