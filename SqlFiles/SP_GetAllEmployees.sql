
Alter Procedure [dbo].[stp_Emp_GetAllEmployee]
As
Begin
select EM.Row_Id as EmpId,* from EmployeeMaster EM
left join Country C on C.Row_Id=EM.CountryId
left join State S on S.Row_Id=EM.StateId
left join City Ct on Ct.Row_Id=EM.CityId
End