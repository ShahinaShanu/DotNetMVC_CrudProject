using System.Data;
using Shahina_DemoMVC.DomainModel;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections.Generic;
using System.Configuration;
using System;

namespace Shahina_DemoMVC.DLL
{
    //Add database related working here
    public class EmployeeDLL
    {
        private readonly string _connectionString;

        public EmployeeDLL()
        {
            // Retrieve connection string from config file
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        //public EmployeeDLL(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}
     public IEnumerable<Country> GetCountries()
    {
       var countries = new List<Country>();

       using (var connection = new SqlConnection(_connectionString))
       {
        using (var command = new SqlCommand("stp_Emp_GetCountries", connection))
        {
          command.CommandType = CommandType.StoredProcedure;
          connection.Open();

          using (var reader = command.ExecuteReader())
          {
           while (reader.Read())
           {
            countries.Add(new Country
             {
               CountryId = reader.GetInt32(0),
               CountryName = reader.GetString(1)
             });
           }
           }
       }
     }

     return countries;
    }

        public IEnumerable<State> GetStatesByCountryId(int countryId)
        {
            var states = new List<State>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("stp_Emp_GetStatesByCountryId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CountryId", countryId);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            states.Add(new State
                            {
                                StateId = reader.GetInt32(0),
                                StateName = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return states;
        }

        public IEnumerable<City> GetCitiesByStateId(int stateId)
        {
            var cities = new List<City>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("stp_Emp_GetCitiesByStateId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StateId", stateId);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities.Add(new City
                            {
                                CityId = reader.GetInt32(0),
                                CityName = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return cities;
        }
        public bool CheckEmailExists(string emailAddress)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT COUNT(*) FROM EmployeeMaster WHERE EmailAddress = @EmailAddress", conn);
                cmd.Parameters.AddWithValue("@EmailAddress", emailAddress);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckPanNumberExists(string panNumber)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT COUNT(*) FROM EmployeeMaster WHERE PanNumber = @PanNumber", conn);
                cmd.Parameters.AddWithValue("@PanNumber", panNumber);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckPassportNumberExists(string passportNumber)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT COUNT(*) FROM EmployeeMaster WHERE PassportNumber = @PassportNumber", conn);
                cmd.Parameters.AddWithValue("@PassportNumber", passportNumber);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
        public bool CheckMobileNumberExists(string mobileNumber)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT COUNT(*) FROM EmployeeMaster WHERE MobileNumber = @MobileNumber", conn);
                cmd.Parameters.AddWithValue("@MobileNumber", mobileNumber);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
        public bool InsertEmployee(Employee employee)
        {
            bool status=false;
            try {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand("stp_Emp_InsertEmployee", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@EmpId", employee.Id);
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@CountryId", employee.CountryId);
                    cmd.Parameters.AddWithValue("@StateId", employee.StateId);
                    cmd.Parameters.AddWithValue("@CityId", employee.CityId);
                    cmd.Parameters.AddWithValue("@EmailAddress", employee.EmailAddress);
                    cmd.Parameters.AddWithValue("@MobileNumber", employee.MobileNumber);
                    cmd.Parameters.AddWithValue("@PanNumber", employee.PanNumber);
                    cmd.Parameters.AddWithValue("@PassportNumber", employee.PassportNumber);
                    cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    cmd.Parameters.AddWithValue("@DateOfJoinee", employee.DateOfJoinee);
                    cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
                    //cmd.Parameters.AddWithValue("@CreatedDate", employee.ProfilePicture);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    status = true;
                }
            }
            catch {
            status = false;
            }
            return status;
        }
        public DataTable GetAllEmployees(int? id)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("stp_Emp_GetAllEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmpId", id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }
        public int DeleteEmployee(int id)
        {
            int statusId=0;
            try 
            { 
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("stp_Emp_DeleteEmployee", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmpId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                statusId = 1;
            }
            }
            catch (Exception ex)
            {
                statusId = 0;
            }
            return statusId;
        }
    }
}
