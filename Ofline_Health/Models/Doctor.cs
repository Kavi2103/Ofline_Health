using System.ComponentModel.DataAnnotations;

namespace Ofline_Health.Models
{
    public class Doctor
    {
        [Key]
        public int DocotorId { get; set; }
        public string? Doctor_Name { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Specialty { get; set; }

        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email  is required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }


        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? Address { get; set; }


    }
}
