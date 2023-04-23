using System.ComponentModel.DataAnnotations;

namespace clinic.Schemas.reservation
{
    public class SetReservationRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }
        public int? UserId { get; set; }
        [Required]
        public int DoctorId { get; set; }
    }
}
