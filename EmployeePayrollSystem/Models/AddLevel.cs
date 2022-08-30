using System.ComponentModel.DataAnnotations;

namespace EmployeePayrollSystem.Models
{
    public class AddLevel
    {
        public int ID { get; set; }

        [Display(Name = "Level Name")]
        public string LevelName { get; set; }

        public double Salary { get; set; }

        [Display(Name = "Percentage Increase")]
        public double YearlySalaryIncreasePercentage { get; set; }

        [Display(Name = "T_Allowance")]
        public double TravelAllowance { get; set; }


        [Display(Name = "M_Allowance")]
        public double MedicalAllowance { get; set; }


        [Display(Name = "I_Allowance")]
        public double InternetAllowance { get; set; }

    }
}
