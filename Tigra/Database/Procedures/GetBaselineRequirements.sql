DECLARE @CellID INT = 1;

DECLARE @LatestBaseline DATETIME2;
SELECT @LatestBaseline = MAX([SetDate]) FROM [Tigra].[Baseline] WHERE [CellID] = @CellID;

SELECT *
FROM [Tigra].[RequirementRevisions] AS v
	INNER JOIN [Tigra].[Requirements] AS r ON r.[RequirementID] = v.[RequirementID]
WHERE r.[CellID] = @CellID
	AND v.[BaselineDate] IS NOT NULL
	AND (@LatestBaseline IS NULL OR v.[BaselineDate] > @LatestBaseline)
;