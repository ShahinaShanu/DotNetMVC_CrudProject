USE [NeoSoft_Shahina]
GO
/****** Object:  StoredProcedure [dbo].[stp_Emp_InsertEmployee]    Script Date: 25-08-2024 19:34:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[stp_Emp_InsertEmployee]
    @EmpId int,
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

	if (@EmpId=0 or @EmpId is null)
    INSERT INTO EmployeeMaster (EmployeeCode,FirstName, LastName, CountryId, StateId, CityId, EmailAddress, MobileNumber, PanNumber, PassportNumber, ProfileImage, Gender, DateOfBirth, DateOfJoinee, IsActive, CreatedDate, IsDeleted)
    VALUES (@EmployeeCode,@FirstName, @LastName, @CountryId, @StateId, @CityId, @EmailAddress, @MobileNumber, @PanNumber, @PassportNumber, @ProfileImage, @Gender, @DateOfBirth, @DateOfJoinee, @IsActive, GETDATE(), 0);

	else if(@EmpId>0)
	update EmployeeMaster set FirstName=@FirstName, LastName=@LastName,CountryId=@CountryId,StateId=@StateId,CityId=@CityId,EmailAddress=@EmailAddress,MobileNumber=@MobileNumber,PanNumber=@PanNumber,PassportNumber=@PassportNumber
	,ProfileImage=@ProfileImage,Gender=@Gender,DateOfBirth=@DateOfBirth,DateOfJoinee=@DateOfJoinee,IsActive=@IsActive,IsDeleted=0,UpdatedDate=GETDATE() where Row_id=@EmpId
END
