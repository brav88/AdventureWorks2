USE [AdventureWorks2019]
GO
/****** Object:  StoredProcedure [dbo].[spGetProductCategories]    Script Date: 1/12/2022 21:18:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [dbo].[spGetProductCategories]
	
AS
BEGIN
	SELECT 
		ProductCategoryID Id, 
		[Name] [Desc]
	FROM [Production].[ProductCategory]
	ORDER BY 1 ASC
END
