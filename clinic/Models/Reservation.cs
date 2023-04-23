namespace clinic.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? UserId { get; set; }
        public int DoctorId { get; set; }
    }
}
