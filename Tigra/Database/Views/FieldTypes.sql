IF OBJECT_ID('[Tigra].[FieldTypes]') IS NOT NULL
BEGIN
	DROP VIEW [Tigra].[FieldTypes];
END
GO

CREATE VIEW [Tigra].[FieldTypes]
AS

	SELECT 1 AS FieldTypeId, 'Integer' AS FieldTypeName UNION
	SELECT 2, 'Decimal' UNION
	SELECT 3, 'Money' UNION
	SELECT 4, 'String' UNION
	SELECT 5, 'Text' UNION
	SELECT 6, 'RadioGroup' UNION
	SELECT 7, 'CheckBox' UNION
	SELECT 8, 'CheckBoxList' UNION
	SELECT 9, 'Questionnaire';

GO
