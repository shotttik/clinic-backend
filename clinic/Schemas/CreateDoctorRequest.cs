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
        [AllowNull]
        public IFormFile Image { get; set; }
        [AllowNull]
        public IFormFile Document { get; set; }
        [AllowNull]
        public int CategoryId { get; set; }
        
    }
}
