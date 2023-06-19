using System.ComponentModel.DataAnnotations;

namespace Ofline_Health.Models
{
    public class Reception
    {
        [Key]
        public int Reception_Id { get; set; }

        [Required(ErrorMessage = "Username  is required")]
        [DataType(DataType.Password)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }



    }
}
