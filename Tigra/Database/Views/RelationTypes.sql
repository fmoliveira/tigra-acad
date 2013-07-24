IF OBJECT_ID('[Tigra].[RelationTypes]') IS NOT NULL
BEGIN
	DROP VIEW [Tigra].[RelationTypes];
END
GO

CREATE VIEW [Tigra].[RelationTypes]
AS

	SELECT 1 AS RelationTypeId, 'Related To' AS RelationTypeName UNION
	SELECT 2, 'Child Of' UNION
	SELECT 3, 'Inherits From' UNION
	SELECT 4, 'References';

GO
