using System.ComponentModel.DataAnnotations;

namespace EmployeePayrollSystem.Models
{
    public class ApplyForLeave
    {
        public int ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime TillDate { get; set; }
        public string Reason { get; set; }

        [Display(Name="Applicant Name")]
        public string ApplicantID { get; set; }
        public string Status { get; set; }

        public ApplyForLeave()
        {
            Status = "Pending";
            Reason = "";
            ApplicantID = "";
        }
    }
}
