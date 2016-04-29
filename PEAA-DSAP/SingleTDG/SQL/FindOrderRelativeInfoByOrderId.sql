USE Northwind
SELECT [Order Details].OrderID, [Products].ProductName, [Order Details].Quantity, [Categories].CategoryID, [Categories].CategoryName, [Order Details].Discount
FROM [Order Details], [Products], [Categories]
WHERE [Order Details].ProductID = [Products].ProductID AND [Products].CategoryID = [Categories].CategoryID
AND [Order Details].OrderID = 10693