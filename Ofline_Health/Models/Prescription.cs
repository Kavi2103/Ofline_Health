using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ofline_Health.Models
{
   
        public class Prescription
        {
            [Key]
            public int Prescription_Id { get; set; }

            [Required]
            public int PatientId { get; set; }

            [Required]
            [ForeignKey("PatientId")]
            public Patient Patient { get; set; }

            [Required]
            public int DoctorId { get; set; }

            [Required]
            [ForeignKey("DoctorId")]
            public Doctor Doctor { get; set; }


            [Required]
            public string Description{ get; set; }


        }
    }

