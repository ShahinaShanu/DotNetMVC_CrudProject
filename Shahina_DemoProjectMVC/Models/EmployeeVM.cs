using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;


namespace Shahina_DemoProjectMVC.Models
{
    public class EmployeeVM
    {
        public int Row_Id { get; set; }
        public string EmployeeCode { get; set; }
        [Display(Name="First Name")]
        [Required(ErrorMessage="first Name is required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is Required")]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "State is Required")]
        public int StateId { get; set; }
        public string StateName { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "City is Required")]
        public int CityId { get; set; }
        public string CityName { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is Required")]
        //[Required, EmailAddress, Remote("IsEmailUnique", "Employee")]
        public string EmailAddress { get; set; }
        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Please enter your mobile number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please Enter your valid Mobile Number")]
        public string MobileNumber { get; set; }
        [Display(Name = "Pan Number")]
        [Required(ErrorMessage = "Pan Number is required.")]
        [RegularExpression(@"^([A-Za-z]){5}([0-9]){4}([A-Za-z]){1}$", ErrorMessage = "Please Enter your valid Pan Number.")]
        public string PanNumber { get; set; }
        [Display(Name = "Passport Number")]
        [Required(ErrorMessage = "Passport Number is Required")]
        [RegularExpression(@"^[A-PR-WY][1-9]\d\s?\d{4}[1-9]$", ErrorMessage = "Please Enter your valid Passport Number")]
        public string PassportNumber { get; set; }
        [Display(Name = "Profile Image")]
        //[Required(ErrorMessage = "Please upload your image")]
        public string ProfileImage { get; set; }
        [Required(ErrorMessage = "Please enter your Gender")]
        public byte? Gender { get; set; }
        [Display(Name = "Is Active?")]
        //[Required(ErrorMessage = "Please check if Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Please Enter your date of birth")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        //[Remote("ValidateDOB", "Employee")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Date of Joinee")]
        [Required(ErrorMessage = "Please Enter your date of joining")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfJoinee { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        
    }


    


}