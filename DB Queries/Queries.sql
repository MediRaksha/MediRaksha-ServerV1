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