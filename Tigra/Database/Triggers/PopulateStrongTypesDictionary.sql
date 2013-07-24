/* This trigger will populate the strong types dictionary automatically as diagram attributes are filled. */

IF OBJECT_ID('[Tigra].[PopulateStrongTypesDictionary]') IS NOT NULL
BEGIN
	DROP TRIGGER [Tigra].[PopulateStrongTypesDictionary];
END
GO

CREATE TRIGGER [Tigra].[PopulateStrongTypesDictionary] ON [Tigra].[DiagramAttributes] AFTER INSERT, UPDATE
AS

	DECLARE @ProjectId INT, @StrongType VARCHAR(30);
	
	SELECT @ProjectId = req.[ProjectId], @StrongType = atb.[StrongType]
	FROM inserted AS atb
		INNER JOIN [Tigra].[Requirements] AS req ON req.[RequirementId] = atb.[RequirementId];

	IF NOT EXISTS (SELECT 1 FROM [Tigra].[StrongTypes] WHERE [ProjectId] = @ProjectId AND [StrongType] = @StrongType)
	BEGIN
		INSERT INTO [Tigra].[StrongTypes] ([ProjectId], [StrongType]) VALUES (@ProjectId, @StrongType);
	END

GO
