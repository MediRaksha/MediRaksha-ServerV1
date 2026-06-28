CREATE TABLE Customers
(
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(150) NOT NULL,
    MobileNumber NVARCHAR(20) NOT NULL,
    Email NVARCHAR(150) NULL,
    Gender NVARCHAR(20) NULL,
    Age INT NULL,
    Address NVARCHAR(250) NULL,
    Pincode NVARCHAR(20) NULL,
    DoctorName NVARCHAR(150) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL
);
INSERT INTO Customers
(
    FullName,
    MobileNumber,
    Email,
    Gender,
    Age,
    Address,
    Pincode,
    DoctorName,
    IsActive,
    CreatedDate,
    CreatedBy
)
VALUES
('Raj Kumar','9876543210','raj@gmail.com','Male',35,'ALC Premium, Gowlidody','500032','Dr. Reddy',1,GETDATE(),'Admin'),

('Anita Sharma','9876543211','anita@gmail.com','Female',29,'Kondapur, Hyderabad','500084','Dr. Kumar',1,GETDATE(),'Admin'),

('Rahul Verma','9876543212','rahul@gmail.com','Male',42,'Miyapur, Hyderabad','500049','Dr. Rao',1,GETDATE(),'Admin'),

('Priya Singh','9876543213','priya@gmail.com','Female',31,'Lingampally','500019','Dr. Suresh',1,GETDATE(),'Admin'),

('Mohan Krishna','9876543214','mohan@gmail.com','Male',50,'BHEL, Hyderabad','500072','Dr. Naresh',1,GETDATE(),'Admin');

select * from Customers

CREATE TABLE Medicines
(
    MedicineId INT IDENTITY(1,1) PRIMARY KEY,
    -- Basic Information
    Status NVARCHAR(20) NOT NULL DEFAULT 'Continue',
    ProductType NVARCHAR(50) NOT NULL DEFAULT 'Normal',
    ProductCode NVARCHAR(50) NOT NULL UNIQUE,
    ProductName NVARCHAR(250) NOT NULL,
    Category NVARCHAR(100) NOT NULL,
    PackingStrength NVARCHAR(100) NULL,
    UnitsPerPack INT NOT NULL DEFAULT 10,
    Unit NVARCHAR(20) NOT NULL DEFAULT 'PCS',
    AllowDecimal BIT NOT NULL DEFAULT 0,
    FastSearch NVARCHAR(100) NULL,
    RackNumber NVARCHAR(50) NULL,
    DrugSchedule NVARCHAR(50) DEFAULT 'None',
    DrugFormula NVARCHAR(500) NULL,
    HSNCode NVARCHAR(30) NULL,
    MinSaleQty DECIMAL(18,2) DEFAULT 0,
    ReorderLevel DECIMAL(18,2) DEFAULT 0,
    MaximumQty DECIMAL(18,2) DEFAULT 0,
    -- Tax Information
    LocalTaxType NVARCHAR(30) DEFAULT 'Taxable',
    SGSTPercent DECIMAL(5,2) DEFAULT 0,
    CGSTPercent DECIMAL(5,2) DEFAULT 0,
    CentralTaxType NVARCHAR(30) DEFAULT 'Taxable',
    IGSTPercent DECIMAL(5,2) DEFAULT 0,
    CessPercent DECIMAL(5,2) DEFAULT 0,
    CSRPercent DECIMAL(5,2) DEFAULT 0,
    -- Pricing
    PurchaseRate DECIMAL(18,2) DEFAULT 0,
    Cost DECIMAL(18,2) DEFAULT 0,
    MRP DECIMAL(18,2) DEFAULT 0,
    RateA DECIMAL(18,2) DEFAULT 0,
    RateB DECIMAL(18,2) DEFAULT 0,
    RateC DECIMAL(18,2) DEFAULT 0,
    -- Conversion
    ConversionBox INT DEFAULT 0,
    ConversionCase INT DEFAULT 0,
    AllowNegativeStock BIT DEFAULT 0,
    -- Discounts
    VolumeDiscount DECIMAL(5,2) DEFAULT 0,
    SchemeDays INT DEFAULT 0,
    ProductDiscount DECIMAL(5,2) DEFAULT 0,
    SpecialDiscount DECIMAL(5,2) DEFAULT 0,
    DiscountApplicable BIT DEFAULT 1,
    MaximumDiscountPercent DECIMAL(5,2) DEFAULT 0,
    PurchaseDiscount DECIMAL(5,2) DEFAULT 0,
    SchemeQuantity DECIMAL(18,2) DEFAULT 0,
    FreeScheme NVARCHAR(50) DEFAULT '0+0',
    SchemeValidFrom NVARCHAR(100) DEFAULT 'Full Scheme',
    F6RateEnabled BIT DEFAULT 1,
    MinimumMarginPercent DECIMAL(5,2) DEFAULT 0,
    DiscountLess DECIMAL(5,2) DEFAULT 0,
    Manufacturer NVARCHAR(150) NULL,
    -- Drug Flags
    IsTBDrug BIT DEFAULT 0,
    IsNarcotic BIT DEFAULT 0,
    IsDiscontinued BIT DEFAULT 0,
    -- Audit
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL
);

CREATE TABLE Sales
(
    SaleId INT IDENTITY(1001,1) PRIMARY KEY,
    InvoiceNumber NVARCHAR(30) NOT NULL UNIQUE,
    CustomerId INT NULL,
    SaleDate DATETIME NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    DiscountAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    TaxAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    NetAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    Remarks NVARCHAR(500) NULL,
    IsCancelled BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL,
    CONSTRAINT FK_Sales_Customers
        FOREIGN KEY (CustomerId)
        REFERENCES Customers(CustomerId)
);

INSERT INTO Sales
(
    InvoiceNumber,
    CustomerId,
    SaleDate,
    TotalAmount,
    DiscountAmount,
    TaxAmount,
    NetAmount,
    Remarks,
    IsCancelled,
    CreatedDate,
    CreatedBy
)
VALUES
('TXN-1001',1,'2026-06-20',1050.00,50.00,126.00,1126.00,'Regular Sale',0,GETDATE(),'Admin'),
('TXN-1002',2,'2026-06-21',850.00,25.00,96.00,921.00,'Regular Sale',0,GETDATE(),'Admin'),
('TXN-1003',3,'2026-06-22',1200.00,100.00,132.00,1232.00,'Regular Sale',0,GETDATE(),'Admin'),
('TXN-1004',4,'2026-06-23',1750.00,150.00,192.00,1792.00,'Regular Sale',0,GETDATE(),'Admin'),
('TXN-1005',5,'2026-06-24',2200.00,200.00,240.00,2240.00,'Regular Sale',0,GETDATE(),'Admin');

CREATE TABLE SaleItems
(
    SaleItemId INT IDENTITY(1,1) PRIMARY KEY,
    SaleId INT NOT NULL,
	MedicineId INT NOT NULL,
    Quantity DECIMAL(18,2) NOT NULL,
    Rate DECIMAL(18,2) NOT NULL,
    DiscountPercent DECIMAL(5,2) DEFAULT 0,
    DiscountAmount DECIMAL(18,2) DEFAULT 0,
    TaxPercent DECIMAL(5,2) DEFAULT 0,
    TaxAmount DECIMAL(18,2) DEFAULT 0,
    Amount DECIMAL(18,2) NOT NULL,
	CreatedDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_SaleItems_Sales
        FOREIGN KEY (SaleId)
        REFERENCES Sales(SaleId),
    CONSTRAINT FK_SaleItems_Medicines
        FOREIGN KEY (MedicineId)
        REFERENCES Medicines(MedicineId)
);

INSERT INTO SaleItems
(
    SaleId,
    MedicineId,
    Quantity,
    Rate,
    DiscountPercent,
    DiscountAmount,
    TaxPercent,
    TaxAmount,
    Amount,
    CreatedDate
)
VALUES
(1001,1,10,35,5,17.50,12,40.95,373.45,GETDATE()),
(1001,2,5,45,2,4.50,12,26.46,246.96,GETDATE()),

(1002,3,3,120,5,18.00,12,41.04,383.04,GETDATE()),
(1002,4,6,90,3,16.20,18,94.28,618.08,GETDATE()),

(1003,5,8,50,2,8.00,5,19.60,411.60,GETDATE()),
(1003,6,4,150,5,30.00,12,68.40,638.40,GETDATE()),

(1004,7,10,25,0,0.00,5,12.50,262.50,GETDATE()),
(1004,8,3,110,5,16.50,18,56.43,369.93,GETDATE()),

(1005,1,20,35,10,70.00,12,75.60,705.60,GETDATE()),
(1005,3,5,120,5,30.00,12,68.40,638.40,GETDATE());

Create PROCEDURE [dbo].[sp_GetProductDataByName]
(
    @ProductName VARCHAR(100) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Medicines
    WHERE IsActive = 1
    AND
    (
        @ProductName IS NULL
        OR ProductName LIKE '' + @ProductName + '%'
    )
    ORDER BY ProductName;
END



CREATE TABLE Users
(
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    IsActive BIT DEFAULT 1
)

INSERT INTO Users
(
    UserName,
    Email,
    Password,
    Role
)
VALUES
(
    'Admin',
    'admin@gmail.com',
    '123',
    'Admin'
)

CREATE TABLE UserRefreshTokens
(
    RefreshTokenId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    RefreshToken NVARCHAR(500) NOT NULL,
    ExpiryDate DATETIME NOT NULL,
    IsRevoked BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),

    FOREIGN KEY(UserId)
    REFERENCES Users(UserId)
);

CREATE PROCEDURE sp_GetUserByEmail
(
    @Email NVARCHAR(150)
)
AS
BEGIN

    SELECT
        UserId,
        UserName,
        Email,
        Password,
        Role
    FROM Users
    WHERE Email = @Email
    AND IsActive = 1

END
select * from UserRefreshTokens
CREATE PROCEDURE sp_SaveRefreshToken
(
    @UserId INT,
    @RefreshToken NVARCHAR(500),
    @ExpiryDate DATETIME
)
AS
BEGIN

    INSERT INTO UserRefreshTokens
    (
        UserId,
        RefreshToken,
        ExpiryDate
    )
    VALUES
    (
        @UserId,
        @RefreshToken,
        @ExpiryDate
    )

END

CREATE PROCEDURE sp_GetSalesTransactions
(
    @PageNumber INT,
    @PageSize INT
)
AS
BEGIN

    SET NOCOUNT ON;

    SELECT
        ROW_NUMBER() OVER(ORDER BY S.SaleDate DESC) AS SrNo,
        S.SaleId,
        S.InvoiceNumber,
        C.FullName AS CustomerName,
        C.MobileNumber,
        S.SaleDate,
        C.Address,
        C.Gender,
        COUNT(SI.SaleItemId) AS Products,
        S.NetAmount
    FROM Sales S
    INNER JOIN Customers C
        ON S.CustomerId = C.CustomerId
    LEFT JOIN SaleItems SI
        ON S.SaleId = SI.SaleId
    GROUP BY
        S.SaleId,
        S.InvoiceNumber,
        C.FullName,
        C.MobileNumber,
        S.SaleDate,
        C.Address,
        C.Gender,
        S.NetAmount
    ORDER BY S.SaleDate DESC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY

END

Exec sp_GetSalesTransactions 1,2

CREATE PROCEDURE sp_GetSaleDetails
(
    @SaleId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Header Details

    SELECT
        S.SaleId,
        S.InvoiceNumber,
        S.SaleDate,
        C.FullName,
        C.MobileNumber,
        C.Gender,
        C.Address,
        S.TotalAmount,
        S.DiscountAmount,
        S.TaxAmount,
        S.NetAmount,
        S.Remarks
    FROM Sales S
    INNER JOIN Customers C
        ON S.CustomerId = C.CustomerId
    WHERE S.SaleId = @SaleId;


    -- Item Details

    SELECT
        SI.SaleItemId,
        M.ProductName,
        SI.Quantity,
        SI.Rate,
        SI.DiscountPercent,
        SI.DiscountAmount,
        SI.TaxPercent,
        SI.TaxAmount,
        SI.Amount
    FROM SaleItems SI
    INNER JOIN Medicines M
        ON SI.MedicineId = M.MedicineId
    WHERE SI.SaleId = @SaleId;
END

EXEC sp_GetSaleDetails 1001

SELECT name
FROM sys.tables
SELECT *
FROM sys.procedures
WHERE name='sp_FilterTransactions'

CREATE PROCEDURE sp_FilterTransactions
(
    @SearchText NVARCHAR(100) = NULL,
    @SearchBy NVARCHAR(20) = NULL,
    @FromDate DATE = NULL,
    @ToDate DATE = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 15
)
AS
BEGIN

    SELECT
        ROW_NUMBER() OVER(ORDER BY S.SaleDate DESC) AS SrNo,
        C.FullName AS CustomerName,
        C.MobileNumber,
        S.SaleDate,
        C.Address,
        C.Gender,
        COUNT(SI.SaleItemId) AS Products,
        S.NetAmount,
        S.SaleId
    FROM Sales S
    INNER JOIN Customers C
        ON S.CustomerId = C.CustomerId
    INNER JOIN SaleItems SI
        ON S.SaleId = SI.SaleId
    WHERE
    (
        @SearchText IS NULL
        OR C.FullName LIKE '%' + @SearchText + '%'
        OR C.MobileNumber LIKE '%' + @SearchText + '%'
    )
    AND
    (
        @FromDate IS NULL
        OR S.SaleDate >= @FromDate
    )
    AND
    (
        @ToDate IS NULL
        OR S.SaleDate <= @ToDate
    )
    GROUP BY
        S.SaleId,
        C.FullName,
        C.MobileNumber,
        S.SaleDate,
        C.Address,
        C.Gender,
        S.NetAmount
    ORDER BY S.SaleDate DESC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY

END