using System.Diagnostics.CodeAnalysis;

namespace clinic.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Views { get; set; } = 0;
        public string Pid { get; set; }
        public string Image { get; set; }
        public string Document { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
