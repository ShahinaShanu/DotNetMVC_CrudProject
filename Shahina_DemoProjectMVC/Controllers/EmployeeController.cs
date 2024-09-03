using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shahina_DemoProjectMVC.Models;
using Shahina_DemoMVC.BLL;
using Shahina_DemoMVC.DLL;
using Shahina_DemoMVC.DomainModel;
using Newtonsoft.Json;
using System.Web.Optimization;
using System.Threading.Tasks;

namespace Shahina_DemoProjectMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeBLL _employeeBLL;

        public EmployeeController()
        {
            _employeeBLL = new EmployeeBLL();
        }
        // GET: Employee
        public ActionResult Index()
        {
            var model = new EmployeeVM
            {
                Countries = _employeeBLL.GetCountries().Select(c => new SelectListItem
                {
                    Value = c.CountryId.ToString(),
                    Text = c.CountryName
                }).ToList(),
                States = new List<SelectListItem>(), // Initialize with an empty list
                Cities = new List<SelectListItem>()
            };
            model.Gender = 1;
            ViewBag.Title = "Employee Registration";
            return View(model);
            //return View();
        }
        [HttpGet]
        
        public JsonResult GetStates(int countryId)
        {
            var states = _employeeBLL.GetStatesByCountryId(countryId);

            var stateSelectList = states.Select(s => new
            {
                Value = s.StateId.ToString(),
                Text = s.StateName
            });

            return Json(stateSelectList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetCities(int stateId)
        {
            var cities = _employeeBLL.GetCitiesByStateId(stateId);

            var citySelectList = cities.Select(s => new
            {
                Value = s.CityId.ToString(),
                Text = s.CityName
            });

            return Json(citySelectList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult CheckEmailExists(string emailAddress)
        {
            bool exists = _employeeBLL.CheckEmailExists(emailAddress);
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult CheckPanNumberExists(string panNumber)
        {
            bool exists = _employeeBLL.CheckPanNumberExists(panNumber);
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CheckPassportNumberExists(string passportNumber)
        {
            bool exists = _employeeBLL.CheckPassportNumberExists(passportNumber);
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult CheckMobileNumberExists(string mobileNumber)
        {
            bool exists = _employeeBLL.CheckMobileNumberExists(mobileNumber);
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEmployee(EmployeeVM model, HttpPostedFileBase ProfileImage)
        {
            try 
            {
                if (ProfileImage != null)
                {
                    if (ProfileImage.ContentLength > 200 * 1024 || !ProfileImage.ContentType.Contains("image/jpeg"))
                    {
                        ModelState.AddModelError("ProfileImage", "Profile picture must be a jpg or png and no larger than 200 KB.");
                    }
                }
                
                if (ModelState.IsValid)
                {
                   if (ProfileImage != null && ProfileImage.ContentLength > 0)
                    {
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ProfileImage.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Uploads/Employee"), uniqueFileName);
                        ProfileImage.SaveAs(filePath);
                        model.ProfileImage = uniqueFileName; 
                    }
                    else
                    {
                        if (model.Row_Id != 0) 
                        { 
                        model.ProfileImage = _employeeBLL.GetEmployeeById(model.Row_Id)?.ProfileImage;
                        }
                        else
                        {
                            model.ProfileImage = "";
                        }

                    }
                    Employee employeeDomainModel = new Employee
                    {
                        Id= model.Row_Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmailAddress = model.EmailAddress,
                        MobileNumber = model.MobileNumber,
                        PanNumber = model.PanNumber.ToUpper(),
                        PassportNumber = model.PassportNumber.ToUpper(),
                        DateOfBirth = model.DateOfBirth,
                        DateOfJoinee = model.DateOfJoinee,
                        CountryId = model.CountryId,
                        StateId = model.StateId,
                        CityId = model.CityId,
                        Gender=model.Gender,
                        ProfileImage = model.ProfileImage,
                        IsActive = model.IsActive,
                        
                    };
                    bool isInserted=_employeeBLL.InsertEmployee(employeeDomainModel);
                    if (isInserted!=false)
                    { 
                    if(model.Row_Id!=0)
                            TempData["SuccessMessage"] = "Employee Updated Successfully";
                    else
                            TempData["SuccessMessage"] = "Employee Registered Successfully";
                    }
                   
                    return RedirectToAction("EmployeeList");
                    
                }
                
                if (!ModelState.IsValid)
                {
                    if (model.Row_Id != 0)
                    {
                        ViewBag.Title = "Edit/Update Form";
                    }
                    else { ViewBag.Title = "Employee Registration"; }
                        
                    model.Countries = _employeeBLL.GetCountries().Select(c => new SelectListItem
                    {
                        Value = c.CountryId.ToString(),
                        Text = c.CountryName
                    }).ToList();

                    model.States = _employeeBLL.GetStatesByCountryId(model.CountryId).Select(s => new SelectListItem
                    {
                        Value = s.StateId.ToString(),
                        Text = s.StateName
                    }).ToList();

                    model.Cities = _employeeBLL.GetCitiesByStateId(model.StateId).Select(c => new SelectListItem
                    {
                        Value = c.CityId.ToString(),
                        Text = c.CityName
                    }).ToList();
                }
            }
            catch(Exception ex) 
            {
                return View("index", model);
                //throw new Exception(ex.Message);

            }
           
            return View("index", model);
        }

        public ActionResult EmployeeList()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetEmployeeList()
        {
            List<Employee> employees = _employeeBLL.GetAllEmployees();
            
            var data = employees.Select(e => new
            {
                e.Id,
                e.EmpCode,
                e.FirstName,
                e.LastName,
                e.EmailAddress,
                e.MobileNumber,
                e.PassportNumber,
                e.PanNumber,
                e.CountryName,
                e.StateName,
                e.CityName,
                Gender = e.Gender == 1 ? "Male" : "Female",
                e.ProfileImage,
                IsActive=e.IsActive == true ? "Yes" : "NO"
            }).ToList();
            
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EmployeeDetailsById(int id)
        {
            var employee = _employeeBLL.GetEmployeeById(id) ;
            var countries = _employeeBLL.GetCountries(); 
            var states = _employeeBLL.GetStatesByCountryId(employee.CountryId); 
            var cities = _employeeBLL.GetCitiesByStateId(employee.StateId);
            if (employee == null)
            {
                return HttpNotFound();
            }
           
            var employeeVM = new EmployeeVM
            {
                Row_Id = employee.Id,
                EmployeeCode = employee.EmpCode,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                CountryId = employee.CountryId,
                StateId = employee.StateId,
                CityId = employee.CityId,
                CountryName = employee.CountryName,
                StateName = employee.StateName,
                CityName = employee.CityName,
                EmailAddress = employee.EmailAddress,
                MobileNumber = employee.MobileNumber,
                PanNumber = employee.PanNumber,
                PassportNumber = employee.PassportNumber,
                ProfileImage = employee.ProfileImage,
                Gender = employee.Gender,
                DateOfBirth = employee.DateOfBirth,
                DateOfJoinee = employee.DateOfJoinee,
                IsActive = employee.IsActive,
                //CreatedDate = employee.CreatedDate,
                //UpdatedDate = employee.UpdatedDate,
                IsDeleted = employee.IsDeleted,
                DeletedDate = employee.DeletedDate
            };
            employeeVM.Countries = countries.Select(c => new SelectListItem
            {
                Value = c.CountryId.ToString(),
                Text = c.CountryName,
                Selected = c.CountryId == employee.CountryId
            }).ToList();

            employeeVM.States = states.Select(s => new SelectListItem
            {
                Value = s.StateId.ToString(),
                Text = s.StateName,
                Selected = s.StateId == employee.StateId
            }).ToList();

            employeeVM.Cities = cities.Select(c => new SelectListItem
            {
                Value = c.CityId.ToString(),
                Text = c.CityName,
                Selected = c.CityId == employee.CountryId
            }).ToList();
          
            ViewBag.Title = "Edit/Update Employee";
            return View("index",employeeVM);
        }

        [HttpPost]
        public JsonResult DeleteEmployee(int id)
        {
            try
            {
                int statusId=_employeeBLL.DeleteEmployee(id);
                if (statusId == 1)
                {
                    return Json(new { success = true, message = "Employee deleted successfully." });
                }
                else {
                    return Json(new { success = false, message = "Error occurred while deleting employee." });
                }
            }
            catch (Exception ex)
            {
               return Json(new { success = false, message = "Error occurred while deleting employee." });
            }
        }

    }
}