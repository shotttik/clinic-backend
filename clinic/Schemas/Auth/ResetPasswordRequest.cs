using System.ComponentModel.DataAnnotations;

namespace clinic.Schemas.Auth
{
    public class ResetPasswordRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "არა სწორი მეილის ფორმატი")]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
        [Required, StringLength(255, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[0-9])(?=.*[A-Z])(?=.*\W)(?=.{8,}).+$", ErrorMessage = "(მინიმუმ 8 სიმბოლო რომელიც უნდა შეიცავდეს რიცხვს, სიმბოლოს, დიდ/პატარა ასო).")]
        public string Password { get; set; }
        [Required, StringLength(255, MinimumLength = 8), Compare("Password", ErrorMessage = "პაროლები არ ემთხვევა")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[0-9])(?=.*[A-Z])(?=.*\W)(?=.{8,}).+$", ErrorMessage = "(მინიმუმ 8 სიმბოლო რომელიც უნდა შეიცავდეს რიცხვს, სიმბოლოს, დიდ/პატარა ასო).")]
        public string ConfirmPassword { get; set; }
    }
}
