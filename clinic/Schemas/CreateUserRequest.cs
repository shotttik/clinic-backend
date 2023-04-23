using System.Diagnostics.CodeAnalysis;
using clinic.Models;
using clinic.Schemas.Auth;

namespace clinic.Schemas
{
    public class CreateUserRequest : UserRegisterRequest
    {
        public UserRole Role { get; set; } = UserRole.User;
    }
}
