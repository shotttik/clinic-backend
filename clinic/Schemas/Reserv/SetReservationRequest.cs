using System.ComponentModel.DataAnnotations;

namespace clinic.Schemas.reservation
{
    public class SetReservationRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Start { get; set; }
        [Required]
        public string End { get; set; }
        public int? UserId { get; set; }
        [Required]
        public int DoctorId { get; set; }
    }
}
