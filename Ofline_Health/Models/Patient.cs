using System.ComponentModel.DataAnnotations;

namespace Ofline_Health.Models
{
    public class Patient
    {

        [Key]
        public int PatientId { get; set; }
        public string? Patient_Name { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }
        public string? Gender { get; set; }

        public string? Problem { get; set; }

        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email  is required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? Emergency_Phone { get; set; }
   
 

    }
}

