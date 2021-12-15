CREATE TABLE [SelfService].[OrderItem]
(
	[OrderItemId] INT NOT NULL PRIMARY KEY IDENTITY,
	[OrderId] INT NOT NULL,
	[ItemId] INT NOT NULL,
	CONSTRAINT FKOrderItem_OrderId FOREIGN KEY (OrderId)
	REFERENCES [SelfService].[Order](OrderId)
	ON DELETE CASCADE
)
