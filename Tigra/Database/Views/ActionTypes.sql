IF OBJECT_ID('[Tigra].[ActionTypes]') IS NOT NULL
BEGIN
	DROP VIEW [Tigra].[ActionTypes];
END
GO

CREATE VIEW [Tigra].[ActionTypes]
AS

	SELECT 1 AS ActionTypeId, 'Login' AS ActionTypeName UNION
	SELECT 2, 'Logout' UNION
	SELECT 3, 'Browse Project' UNION
	SELECT 4, 'View Requirement';

GO
