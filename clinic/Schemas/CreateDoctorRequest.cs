using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using clinic.Models;

namespace clinic.Schemas
{
    public class CreateDoctorRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "არა სწორი მეილის ფორმატი")]
        public string Email { get; set; }
        public int Views { get; set; } = 0;
        [StringLength(11, MinimumLength = 11, ErrorMessage = "უნდა შეიცავდეს 11 რიცხვს")]
        [RegularExpression(@"^\d+$")]
        public string Pid { get; set; }
        public string Image { get; set; }
        public string Document { get; set; }
        [AllowNull]
        public int CategoryId { get; set; }
        
    }
}
