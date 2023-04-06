using System.ComponentModel.DataAnnotations;

namespace clinic.Schemas
{
    public class UserRegisterRequest
    {
        [Required]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "მინიმუმ 5 სიმბოლო")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="არა სწორი მეილის ფორმატი")]
        public string Email { get; set; }

        [Required, StringLength(255, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[0-9])(?=.*[A-Z])(?=.*\W)(?=.{8,}).+$", ErrorMessage = "(მინიმუმ 8 სიმბოლო რომელიც უნდა შეიცავდეს რიცხვს, სიმბოლოს, დიდ/პატარა ასო).")]
        public string Password { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "უნდა შეიცავდეს 11 რიცხვს")]
        [RegularExpression(@"^\d+$")]
        public string Pid { get; set; }

    }
}
