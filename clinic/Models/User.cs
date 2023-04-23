using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace clinic.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Pid), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte [] PasswordHash { get; set; }
        public byte [] PasswordSalt { get; set; }
        public string Pid { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public int Views { get; set; } = 0;
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? Image { get; set; }
        public string? Document { get; set; }


    }
    public enum UserRole
    {
        User,
        Admin,
        Doctor
    }

}
