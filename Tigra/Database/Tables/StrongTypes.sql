/* This is a dictionary of all strong types used in diagrams by each project.
 * It's used to auto complete strong types so a team can accomplish one more step in standardisation. */

IF OBJECT_ID('[Tigra].[StrongTypes]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[StrongTypes]
	(
		[ProjectId] INT NOT NULL,
		[StrongType] VARCHAR(30) NOT NULL,
		CONSTRAINT [pk_TigraStrongTypes_Key] PRIMARY KEY ([ProjectId], [StrongType]),
		CONSTRAINT [fk_TigraStrongTypes_Pjid] FOREIGN KEY ([ProjectId]) REFERENCES [Tigra].[Projects] ([ProjectId]) ON DELETE CASCADE,
		CONSTRAINT [ck_TigraStrongTypes_Chk] CHECK (CHARINDEX(' ', [StrongType]) = 0)
	);

END
GO
