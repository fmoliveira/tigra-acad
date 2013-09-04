IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'Requirements')
BEGIN

	CREATE TABLE [Tigra].[Requirements]
	(
		[RequirementID] INT IDENTITY(1,1) NOT NULL,
		[ReqType] TINYINT NOT NULL,
		[CellID] INT NOT NULL,
		CONSTRAINT [pk_Tigra_Requirements_Key] PRIMARY KEY ([RequirementID]),
		CONSTRAINT [fk_Tigra_Requirements_Cell] FOREIGN KEY ([CellID]) REFERENCES [Tigra].[Cells] ([CellID])
	);

END
GO