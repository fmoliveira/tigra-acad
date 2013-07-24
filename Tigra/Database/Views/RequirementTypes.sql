IF OBJECT_ID('[Tigra].[RequirementTypes]') IS NOT NULL
BEGIN
	DROP VIEW [Tigra].[RequirementTypes];
END
GO

CREATE VIEW [Tigra].[RequirementTypes]
AS

	SELECT 1 AS RequirementTypeId, 'Text' AS RequirementTypeName UNION
	SELECT 2, 'Diagram' UNION
	SELECT 3, 'Use Case';

GO
