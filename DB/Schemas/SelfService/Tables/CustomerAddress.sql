CREATE TABLE [SelfService].[CustomerAddress]
(
	[CustomerAddressId] INT NOT NULL PRIMARY KEY IDENTITY,
	[CustomerId] INT,
	[Address] VARCHAR(100),
	[City] VARCHAR(50),
	[State] VARCHAR(5),
	[Zip] VARCHAR(10),
	CONSTRAINT FKCustomerAddress_CustomerId FOREIGN KEY (CustomerId)
	REFERENCES SelfService.Customer(CustomerId)
	ON DELETE CASCADE
)
