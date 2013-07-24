/* Enforce the requirement's revision to always increment 1 when it's changed.
 * Never go down, remain the same or skip revision numbers. */

IF EXISTS (SELECT object_id FROM sys.check_constraints WHERE name = 'ck_TigraRequirements_Rev')
BEGIN
	ALTER TABLE [Tigra].[Requirements] DROP CONSTRAINT [ck_TigraRequirements_Rev];
END
GO

IF OBJECT_ID('[Tigra].[GetRequirementRevision]') IS NOT NULL
BEGIN
	DROP FUNCTION [Tigra].[GetRequirementRevision];
END
GO

CREATE FUNCTION [Tigra].[GetRequirementRevision]
(
	@RequirementId INT
)
RETURNS INT
AS
BEGIN

	DECLARE @Revision INT;

	SELECT @Revision = [Revision] FROM [Tigra].[Requirements] WHERE [RequirementId] = @RequirementId;

	RETURN @Revision;

END
GO

ALTER TABLE [Tigra].[Requirements] ADD CONSTRAINT [ck_TigraRequirements_Rev] CHECK ([Revision] = ([Tigra].[GetRequirementRevision]([RequirementId]) + 1));
GO
