namespace clinic.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? UserId { get; set; }
        public string? UserFullName { get; set; }
        public int DoctorId { get; set; }
        public string? DoctorFullName { get; set; }

    }
}
