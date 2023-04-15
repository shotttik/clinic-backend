using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using clinic.Models;
namespace clinic.Schemas
{
    public class CreateDoctorRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "არა სწორი მეილის ფორმატი")]
        public string Email { get; set; }
        public int Views { get; set; } = 0;
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "უნდა შეიცავდეს 11 რიცხვს")]
        [RegularExpression(@"^\d+$")]
        public string Pid { get; set; }
        public string? Image { get; set; } = null;
        public string? Document { get; set; } = null;
        public int CategoryId { get; set; }
        
    }

  
}
