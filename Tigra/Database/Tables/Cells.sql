IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'Cells')
BEGIN

	CREATE TABLE [Tigra].[Cells]
	(
		[CellID] INT IDENTITY(1,1) NOT NULL,
		[CellName] VARCHAR(50) NOT NULL,
		[Tag] VARCHAR(50) NOT NULL,
		[Description] VARCHAR(250) NULL,
		CONSTRAINT [pk_Tigra_Cells_Key] PRIMARY KEY ([CellID]),
		CONSTRAINT [un_Tigra_Cells_Cell] UNIQUE ([Tag])
	);

END
GO