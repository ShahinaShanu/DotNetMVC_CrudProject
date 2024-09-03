CREATE TABLE EmployeeMaster (
    Row_Id INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeCode VARCHAR(8) NOT NULL UNIQUE,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50),
    CountryId INT FOREIGN KEY REFERENCES Country(Row_Id),
    StateId INT FOREIGN KEY REFERENCES State(Row_Id),
    CityId INT FOREIGN KEY REFERENCES City(Row_Id),
    EmailAddress VARCHAR(100) NOT NULL UNIQUE,
    MobileNumber VARCHAR(15) NOT NULL UNIQUE,
    PanNumber VARCHAR(12) NOT NULL UNIQUE,
    PassportNumber VARCHAR(20) NOT NULL UNIQUE,
    ProfileImage NVARCHAR(100),
    Gender TINYINT,
    IsActive BIT NOT NULL,
    DateOfBirth DATE NOT NULL,
    DateOfJoinee DATE,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedDate DATETIME,
    IsDeleted BIT NOT NULL DEFAULT 0,
    DeletedDate DATETIME
);


