USE [NeoSoft_Shahina]
GO
/****** Object:  StoredProcedure [dbo].[stp_Emp_GetAllEmployee]    Script Date: 25-08-2024 13:10:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER Procedure [dbo].[stp_Emp_GetAllEmployee]
@EmpId int
As
Begin
if(@EmpId=0 OR @EmpId=null)
Begin
select EM.Row_Id as EmpId,* from EmployeeMaster EM
left join Country C on C.Row_Id=EM.CountryId
left join State S on S.Row_Id=EM.StateId
left join City Ct on Ct.Row_Id=EM.CityId
--where EM.IsActive=1
End
else
select EM.Row_Id as EmpId,* from EmployeeMaster EM
left join Country C on C.Row_Id=EM.CountryId
left join State S on S.Row_Id=EM.StateId
left join City Ct on Ct.Row_Id=EM.CityId
where EM.Row_Id=@EmpId
End