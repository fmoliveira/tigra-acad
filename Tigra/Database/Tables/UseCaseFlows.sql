IF OBJECT_ID('[Tigra].[UseCaseFlows]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[UseCaseFlows]
	(
		[FlowId] BIGINT IDENTITY(1,1) NOT NULL,
		[RequirementId] INT NOT NULL,
		[Revision] INT NOT NULL,
		[FlowGroup] TINYINT NOT NULL,
		[FlowNumber] TINYINT NOT NULL,
		[StepNumber] TINYINT NOT NULL,
		[Description] VARCHAR(250),
		[ParentId] BIGINT NULL,
		CONSTRAINT [pk_TigraUseCaseFlows_Key] PRIMARY KEY ([FlowId]),
		CONSTRAINT [un_TigraUseCaseFlows_Unq] UNIQUE ([RequirementId], [Revision], [FlowGroup], [FlowNumber], [StepNumber]),
		CONSTRAINT [fk_TigraUseCaseFlows_Rev] FOREIGN KEY ([RequirementId], [Revision]) REFERENCES [Tigra].[UseCases] ([RequirementId], [Revision]),
		CONSTRAINT [fk_TigraUseCaseFlows_Pfid] FOREIGN KEY ([ParentId]) REFERENCES [Tigra].[UseCaseFlows] ([FlowId])
	);

END
GO
