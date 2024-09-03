USE [NeoSoft_Shahina]
GO
/****** Object:  StoredProcedure [dbo].[stp_Emp_InsertEmployee]    Script Date: 8/20/2024 3:34:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[stp_Emp_InsertEmployee]
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @CountryId INT,
    @StateId INT,
    @CityId INT,
    @EmailAddress VARCHAR(100),
    @MobileNumber VARCHAR(15),
    @PanNumber VARCHAR(12),
    @PassportNumber VARCHAR(20),
    @ProfileImage NVARCHAR(100),
    @Gender TINYINT,
    @DateOfBirth DATE,
    @DateOfJoinee DATE,
	@IsActive bit
	
AS
BEGIN
  
  Declare @EmployeeCode as varchar(8);

   SELECT @EmployeeCode = RIGHT('00' + CAST(ISNULL(MAX(CAST(SUBSTRING(EmployeeCode, 3, LEN(EmployeeCode)) AS INT)), 0) + 1 AS VARCHAR), 3)
    FROM EmployeeMaster;

    INSERT INTO EmployeeMaster (EmployeeCode,FirstName, LastName, CountryId, StateId, CityId, EmailAddress, MobileNumber, PanNumber, PassportNumber, ProfileImage, Gender, DateOfBirth, DateOfJoinee, IsActive, CreatedDate, IsDeleted)
    VALUES (@EmployeeCode,@FirstName, @LastName, @CountryId, @StateId, @CityId, @EmailAddress, @MobileNumber, @PanNumber, @PassportNumber, @ProfileImage, @Gender, @DateOfBirth, @DateOfJoinee, @IsActive, GETDATE(), 0);
END
