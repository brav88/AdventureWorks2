USE [AdventureWorks2019]
GO
/****** Object:  StoredProcedure [dbo].[spGetProducts]    Script Date: 1/12/2022 21:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [dbo].[spGetProducts]
	@ProductSubcategoryID INT
AS
BEGIN
	SELECT 
		[Name],
		[ProductNumber],
		[Color],
		[ListPrice]
	FROM [Production].[Product]
	WHERE ProductSubcategoryID = @ProductSubcategoryID
	ORDER BY 1 ASC
END
