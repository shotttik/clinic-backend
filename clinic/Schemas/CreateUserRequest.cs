using System.Diagnostics.CodeAnalysis;

namespace clinic.Schemas
{
    public class CreateUserRequest : UserRegisterRequest
    {
        public bool IsAdmin { get; set; } = false;
    }
}
