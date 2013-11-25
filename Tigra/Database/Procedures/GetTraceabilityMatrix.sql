IF EXISTS (SELECT 1 FROM sys.procedures WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'GetTraceabilityMatrix')
BEGIN
	DROP PROCEDURE [Tigra].[GetTraceabilityMatrix];
END
GO

CREATE PROCEDURE [Tigra].[GetTraceabilityMatrix]
(
	@Tag VARCHAR(100)
)
AS
BEGIN

	WITH [TraceabilityMatrix] AS
	(
		SELECT r.[RequirementID], MAX(r.[RevisionNumber]) AS [RevisionNumber]
		FROM [Tigra].[RequirementRevisions] AS v
			INNER JOIN [Tigra].[RequirementRelations] AS l ON l.[RightRevisionID] = v.[RevisionID]
			INNER JOIN [Tigra].[RequirementRelations] AS a ON a.[LeftRevisionID] = l.[LeftRevisionID]
			INNER JOIN [Tigra].[RequirementRevisions] AS r ON r.[RevisionID] = a.[RightRevisionID]
		WHERE v.[Tag] = @Tag AND v.[Published] = 1 AND v.[BaselineDate] IS NOT NULL
		GROUP BY r.[RequirementID]
	)
	SELECT r.[RevisionID], r.[RevisionNumber], r.[RevisionDate], r.[UserID], r.[Tag], r.[Title], t.[Text]
	FROM [TraceabilityMatrix] AS m
		INNER JOIN [Tigra].[RequirementRevisions] AS r ON r.[RequirementID] = m.[RequirementID] AND r.[RevisionNumber] = m.[RevisionNumber]
		INNER JOIN [Tigra].[RequirementTexts] AS t ON t.[RevisionID] = r.[RevisionID]
	ORDER BY [Title];

END
GO
