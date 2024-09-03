Create Procedure stp_Emp_GetCountries
As
Begin
select * from Country

end

Alter Procedure stp_Emp_GetStatesByCountryId
@CountryId int
As
Begin
select row_id as StateId,StateName from State where Countryid=@CountryId

end

Alter Procedure stp_Emp_GetCitiesByStateId
@StateId int
As
Begin
select row_id as CityId,CityName from city where StateId=@StateId 

end