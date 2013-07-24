/* #admin:eugenia${contato@fmoliveira.com.br}%Tigra.SoftwareEngineering; */

IF OBJECT_ID('[Tigra].[Users]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[Users]
	(
		[UserId] INT IDENTITY(1,1) NOT NULL,
		[Username] VARCHAR(20) NOT NULL,
		[Password] CHAR(64) NOT NULL,
		[EmailAddress] VARCHAR(80) NOT NULL,
		[Photo] VARBINARY(MAX) NULL,
		CONSTRAINT [pk_TigraUsers_Uid] PRIMARY KEY ([UserId]),
		CONSTRAINT [un_TigraUsers_Uname] UNIQUE ([Username]),
		CONSTRAINT [un_TigraUsers_Email] UNIQUE ([EmailAddress])
	);

END
GO
