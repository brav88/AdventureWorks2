USE [AdventureWorks2019]

CREATE TABLE [Person].[PersonAccess](
	[BusinessEntityID] [int] NOT NULL,
	[Controller] [varchar](50) NULL,
	[Action] [varchar](50) NULL,
	[DatabaseAction] [varchar](50) NULL
) ON [PRIMARY]
GO

ALTER TABLE [Person].[PersonAccess]  WITH CHECK ADD  CONSTRAINT [FK_Person_PersonAccess] FOREIGN KEY([BusinessEntityID])
REFERENCES [Person].[Person] ([BusinessEntityID])
GO

