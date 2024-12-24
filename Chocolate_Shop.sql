CREATE DATABASE Chocolate_Shop;
USE Chocolate_Shop;

CREATE TABLE [Role] (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
	RoleName NVARCHAR(100)
);

CREATE TABLE Account (
    AccountID INT IDENTITY(1,1) PRIMARY KEY,
	RoleID INT,
    Gmail VARCHAR(100),
    Password VARCHAR(100),
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
	Gender BIT,
    Phone VARCHAR(20),
    Address NVARCHAR(255),
    BirthDay DATE,
	AccountImage NVARCHAR(MAX),
	Status INT,
	FOREIGN KEY (RoleID) REFERENCES Role(RoleID)
);

CREATE TABLE AddressShip (
    AddressShipID INT IDENTITY(1,1) PRIMARY KEY,
	AccountID INT,
	Name NVARCHAR(100),
	Phone VARCHAR(20),
	Address NVARCHAR(255),
	FOREIGN KEY (AccountID) REFERENCES Account(AccountID)
);

CREATE TABLE Category (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(255),
    Description NVARCHAR(MAX)
);


CREATE TABLE Product (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(255),
	Price DECIMAL(10, 2),
	Quantity INT,
    CategoryID INT,
    ProductImage NVARCHAR(MAX),
    Description NVARCHAR(MAX),
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);

CREATE TABLE [Order] (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    AccountID INT,
	ProductID INT,
	QuantityPerUnit INT,
    UnitPrice DECIMAL(10, 2),
    OrderDate DATETIME,
	RequiredDate DATETIME,
	ShippedDate DATETIME,
	AddressShipID INT,
	Type INT,
	FOREIGN KEY (AccountID) REFERENCES Account(AccountID),
	FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
	FOREIGN KEY (AddressShipID) REFERENCES AddressShip(AddressShipID)
);

CREATE TABLE Comment (
    CommentID INT IDENTITY(1,1) PRIMARY KEY,
    Content NVARCHAR(MAX),
    ProductID INT,
    AccountID INT,
    CommentTime DATETIME,
	FOREIGN KEY (AccountID) REFERENCES Account(AccountID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

CREATE TABLE RateProduct (
    AccountID INT,
    ProductID INT,
    Rate INT,
    PRIMARY KEY (AccountID, ProductID),
    FOREIGN KEY (AccountID) REFERENCES Account(AccountID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

CREATE TABLE ChatBox (
    ChatBoxID INT IDENTITY(1,1) PRIMARY KEY,
    AccountID INT,
    SupplierID INT,
    FOREIGN KEY (AccountID) REFERENCES Account(AccountID),
);

CREATE TABLE Chat (
    ChatID INT IDENTITY(1,1) PRIMARY KEY,
    ChatBoxID INT,
    ChatTime DATETIME,
    Content NVARCHAR(MAX),
    ChatBy NVARCHAR(50),
    FOREIGN KEY (ChatBoxID) REFERENCES ChatBox(ChatBoxID)
);

INSERT INTO Category (CategoryName, Description) VALUES 
('Pastries', 'This category contains various types of pastries such as traditional pastries, cupcakes, cookies, etc.'),
('Breads', 'This category contains various types of breads such as sandwiches, baguettes, toast, etc.'),
('Cakes', 'This category contains various types of cakes such as birthday cakes, mousse cakes, fruit cakes, etc.'),
('Fruit Pies', 'This category contains various types of pies made with fruit such as fruit tarts, fruit pudding, etc.');

INSERT INTO [Role] (RoleName)
VALUES ('Admin'),
       ('User');
