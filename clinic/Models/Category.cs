using System.Diagnostics.CodeAnalysis;

namespace clinic.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Doctor> Doctors { get; set; }

    }
}
