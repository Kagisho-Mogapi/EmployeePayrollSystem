using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePayrollSystem.Models
{
    public class AddEmployee
    {
        public int ID { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Pincode")]
        public int Pincode { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Qualification")]
        public string Qualification { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Role Name")]
        [ForeignKey("RoleName")]
        public string RoleName { get; set; }

        [Display(Name = "Salary")]
        public string Salary { get; set; }

        [Display(Name = "Percentage Increase")]
        public string PercentageIncrease { get; set; }

        [Display(Name = "Bank Account")]
        public string BankAccount { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", 
            ErrorMessage = "Password should have at least one: lower case, upper case, number and special character" )]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$",
            ErrorMessage = "Password should have at least one: lower case, upper case, number and special character")]
        public string ConfirmPassword { get; set; }


    }
}
