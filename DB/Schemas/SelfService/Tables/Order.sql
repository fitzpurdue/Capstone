CREATE TABLE [SelfService].[Order]
(
	[OrderId] INT NOT NULL PRIMARY KEY IDENTITY,
	[CustomerId] INT NULL,
	[OrderDate] DateTime DEFAULT GETDATE(),
	[OrderStatus] INT DEFAULT(0) NOT NULL, 
    CONSTRAINT FKOrder_CustomerId FOREIGN KEY (CustomerId)
	REFERENCES SelfService.Customer(CustomerId)
	ON DELETE SET NULL
)
