-- ShipmentAddress does not reference CustomerAddress as a customer
-- could remove this. So we create a static entry.

CREATE TABLE [SelfService].[OrderShipment]
(
	[OrderShipmentId] INT NOT NULL PRIMARY KEY IDENTITY,
	[OrderId] INT NOT NULL,
	[ShipmentAddress] VARCHAR(100),
	[DateShipped] DateTime Null,
	CONSTRAINT FKOrderShipment_OrderId FOREIGN KEY (OrderId)
	REFERENCES [SelfService].[Order](OrderId)
	ON DELETE CASCADE,
	



)
