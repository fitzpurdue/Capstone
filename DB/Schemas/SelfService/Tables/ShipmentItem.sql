CREATE TABLE [SelfService].[ShipmentItem]
(
	[ShipmentItemId] INT NOT NULL PRIMARY KEY IDENTITY,
	[OrderShipmentId] INT NOT NULL,
	[ItemId] INT NOT NULL,

	CONSTRAINT FKShipmentItem_OrderShipmentId FOREIGN KEY (OrderShipmentId)
	REFERENCES [SelfService].[OrderShipment](OrderShipmentId)
	ON DELETE CASCADE,

	CONSTRAINT FKShipmentItem_ItemId FOREIGN KEY (ItemId)
	REFERENCES [SelfService].[Item](ItemId)
	ON DELETE CASCADE

)
