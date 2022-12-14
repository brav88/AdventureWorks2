USE [AdventureWorks2019]

CREATE OR ALTER PROCEDURE [dbo].[spGetSales] 	
AS
BEGIN
	SELECT
		YEAR(soh.OrderDate) [Year],
		SUM(OrderQty) AS SumOfSales
	FROM Sales.SalesOrderDetail AS SOD
		INNER JOIN Sales.SalesOrderHeader AS SOH
		ON SOD.SalesOrderID = SOH.SalesOrderID	
	GROUP BY YEAR(soh.OrderDate)
	ORDER BY 1 ASC
END
