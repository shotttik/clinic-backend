using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace clinic.Models
{
    [Index(nameof(code), IsUnique = true)]
    public class CodeVerification
    {
        public int id { get; set; }
        public string code { get; set; }
        public string Email { get; set; }
        public DateTime createdDate { get; set; } = DateTime.Now;
        public DateTime expirationDate { get; set; }

    }
}
