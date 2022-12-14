USE [AdventureWorks2019]
GO
/****** Object:  StoredProcedure [dbo].[spUpdatePhotoPath]    Script Date: 6/10/2022 20:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[spUpdatePhotoPath]
	@photoPath VARCHAR(50),
	@businessEntityID INT
AS
	BEGIN
		UPDATE [Person].[Person] SET PhotoPath = @photoPath 
		WHERE BusinessEntityID = @businessEntityID
	END
