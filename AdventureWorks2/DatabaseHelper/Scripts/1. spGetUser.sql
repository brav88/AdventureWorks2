USE [AdventureWorks2019]
GO
/****** Object:  StoredProcedure [dbo].[spGetUser]    Script Date: 29/9/2022 19:48:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetUser]
	@email		VARCHAR(50),
	@password   VARCHAR(50)
AS
BEGIN	
	SELECT 
		e.BusinessEntityID,
		ea.EmailAddress,
		pp.FirstName + ' ' + pp.LastName [FullName],
		e.JobTitle
	FROM [HumanResources].[Employee] e
		INNER JOIN [Person].[Person] pp
		ON e.BusinessEntityID = pp.BusinessEntityID
		INNER JOIN [Person].[EmailAddress] ea
		ON e.BusinessEntityID = ea.BusinessEntityID
		INNER JOIN [Person].[Password] p
		ON e.BusinessEntityID = p.BusinessEntityID
		WHERE ea.EmailAddress = @email
		AND p.PasswordSalt = @password
END
