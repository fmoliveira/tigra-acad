IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'GetBaselineList')
BEGIN
	DROP PROCEDURE [Tigra].[GetBaselineList];
END
GO

CREATE PROCEDURE [Tigra].[GetBaselineList]
(
	@CellID INT
)
AS
BEGIN

	SELECT b.[BaselineID], b.[UserID], b.[SetDate], b.[Message]
	FROM [Tigra].[Baseline] AS b
	WHERE b.[CellID] = @CellID
	ORDER BY [SetDate] DESC;

END
GO
