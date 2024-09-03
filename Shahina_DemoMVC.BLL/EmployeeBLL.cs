using Shahina_DemoMVC.DLL;
using Shahina_DemoMVC.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
namespace Shahina_DemoMVC.BLL
{
    //Add Business rules and logics here
    public class EmployeeBLL
    {
        private readonly EmployeeDLL _employeeDLL;

        public EmployeeBLL()
        {
            _employeeDLL = new EmployeeDLL();
        }

        public IEnumerable<Country> GetCountries()
        {
            return _employeeDLL.GetCountries();
        }

        public IEnumerable<State> GetStatesByCountryId(int countryId)
        {
            return _employeeDLL.GetStatesByCountryId(countryId);
        }

        public IEnumerable<City> GetCitiesByStateId(int stateId)
        {
            return _employeeDLL.GetCitiesByStateId(stateId);
        }
        public bool CheckEmailExists(string emailAddress)
        {
            return _employeeDLL.CheckEmailExists(emailAddress);
        }
        public bool CheckPanNumberExists(string emailAddress)
        {
            return _employeeDLL.CheckPanNumberExists(emailAddress);
        }
        public bool CheckPassportNumberExists(string emailAddress)
        {
            return _employeeDLL.CheckPassportNumberExists(emailAddress);
        }
        public bool CheckMobileNumberExists(string mobileNumber)
        {
            return _employeeDLL.CheckMobileNumberExists(mobileNumber);
        }
        public bool InsertEmployee(Employee employee)
        {
            employee.PanNumber = employee.PanNumber.ToUpper();
            employee.PassportNumber = employee.PassportNumber.ToUpper();

            bool isInserted=_employeeDLL.InsertEmployee(employee);
            return isInserted;
        }
        public List<Employee> GetAllEmployees()
        {
            DataTable dataTable = _employeeDLL.GetAllEmployees(0);
            List<Employee> employees = new List<Employee>();

            foreach (DataRow row in dataTable.Rows)
            {
                Employee employee = new Employee
                {
                    Id = Convert.ToInt32(row["EmpId"]),
                    EmpCode = row["EmployeeCode"].ToString(),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    EmailAddress = row["EmailAddress"].ToString(),
                    MobileNumber = row["MobileNumber"].ToString(),
                    PanNumber = row["PanNumber"].ToString(),
                    PassportNumber = row["PassportNumber"].ToString(),
                    DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]),
                    DateOfJoinee = row["DateOfJoinee"] != DBNull.Value ? Convert.ToDateTime(row["DateOfJoinee"]) : (DateTime?)null,
                    CountryId = Convert.ToInt32(row["CountryId"]),
                    CountryName = row["CountryName"].ToString(),
                    StateId = Convert.ToInt32(row["StateId"]),
                    StateName = row["StateName"].ToString(),
                    CityId = Convert.ToInt32(row["CityId"]) ,
                    CityName = row["CityName"].ToString(),
                    Gender = row["Gender"] != DBNull.Value ? (byte?)row["Gender"] : (byte?)null,
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    ProfileImage = row["ProfileImage"].ToString()
                };
                employees.Add(employee);
            }

            return employees;
        }
        public Employee GetEmployeeById(int id)
        {
            DataTable dataTable = _employeeDLL.GetAllEmployees(id);
            Employee employee = new Employee();
            foreach (DataRow row in dataTable.Rows)
            {
                employee = new Employee
                {
                    Id = Convert.ToInt32(row["EmpId"]),
                    EmpCode = row["EmployeeCode"].ToString(),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    EmailAddress = row["EmailAddress"].ToString(),
                    MobileNumber = row["MobileNumber"].ToString(),
                    PanNumber = row["PanNumber"].ToString(),
                    PassportNumber = row["PassportNumber"].ToString(),
                    DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]),
                    DateOfJoinee = row["DateOfJoinee"] != DBNull.Value ? Convert.ToDateTime(row["DateOfJoinee"]) : (DateTime?)null,
                    CountryId = Convert.ToInt32(row["CountryId"]),
                    CountryName = row["CountryName"].ToString(),
                    StateId = Convert.ToInt32(row["StateId"]),
                    StateName = row["StateName"].ToString(),
                    CityId = Convert.ToInt32(row["CityId"]),
                    CityName = row["CityName"].ToString(),
                    Gender = row["Gender"] != DBNull.Value ? (byte?)row["Gender"] : (byte?)null,
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    ProfileImage = row["ProfileImage"].ToString()
                };
             
            }

            return employee;

        }
        public int DeleteEmployee(int id)
        {
            int statusId = _employeeDLL.DeleteEmployee(id);
            return statusId;
        }
    }
}
