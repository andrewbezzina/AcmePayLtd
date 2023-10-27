CREATE TABLE Transactions (
    Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Uuid UNIQUEIDENTIFIER NOT NULL,
	Amount DECIMAL NOT NULL,
	Currency NVARCHAR(3) NOT NULL,
	CardholderNumber NVARCHAR(255) NOT NULL,
    HolderName NVARCHAR(255) NOT NULL, 
	ExpirationMonth INT NOT NULL,
	ExpirationYear Int NOT NULL,
	CVV INT NOT NULL,
	AuthorizeOrderReference NVARCHAR(50),
	VoidOrderReference NVARCHAR(50),
	CaptureOrderReference NVARCHAR(50),
	Status INT NOT NULL,
    DateCreated DATE NOT NULL,
	DateUpdated DATE NOT NULL
);

GO

CREATE INDEX Tansaction_Uuid_Key_Index
ON Transactions (Uuid);

GO
