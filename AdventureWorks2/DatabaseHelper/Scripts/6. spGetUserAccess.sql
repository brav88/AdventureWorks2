USE [AdventureWorks2019]
GO

CREATE OR ALTER PROCEDURE [dbo].[spGetUserAccess]
	@BusinessEntityID INT
AS
BEGIN
	SELECT * FROM Person.PersonAccess
	WHERE BusinessEntityID = @BusinessEntityID
END
GO


