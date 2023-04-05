using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace clinic.Models
{
    [Index(nameof(email), IsUnique = true)]
    [Index(nameof(pid), IsUnique = true)]
    public class User
    {
        public int id { get; set; }
        [Required]
        [StringLength(255,MinimumLength = 5)]
        public string firstName { get; set; }
        public string lastName { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public byte [] password { get; set; }
        [StringLength(11, MinimumLength =11, ErrorMessage = "Personal ID must be 11 characters.")]
        public string pid { get; set; }
        public bool isAdmin { get; set; } = false;
        public bool isActived { get; set; } = false;

    }
}
