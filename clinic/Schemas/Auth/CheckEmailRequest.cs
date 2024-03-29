﻿using System.ComponentModel.DataAnnotations;

namespace clinic.Schemas.Auth
{
    public class CheckEmailRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "არა სწორი მეილის ფორმატი")]
        public string Email { get; set; }
    }
}
