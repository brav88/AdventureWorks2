USE [AdventureWorks2019]
GO
/****** Object:  StoredProcedure [dbo].[spGetSubProductCategory]    Script Date: 1/12/2022 21:18:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [dbo].[spGetSubProductCategory]
	@ProductCategoryID INT
AS
BEGIN
	SELECT 
		ProductSubcategoryID Id, 
		[Name] [Desc]
	FROM [Production].[ProductSubcategory]
	WHERE ProductCategoryID = @ProductCategoryID
	ORDER BY 1 ASC
END
