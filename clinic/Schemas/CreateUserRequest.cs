using System.Diagnostics.CodeAnalysis;
using clinic.Models;

namespace clinic.Schemas
{
    public class CreateUserRequest : UserRegisterRequest
    {
        public UserRole Role { get; set; } = UserRole.User;
    }
}
