using clinic.Models;
using clinic.Schemas.reservation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace clinic.Controllers.Reserv
{
    public class SetReservationController :Controller
    {
        private readonly DataContext _dataContext;
        public SetReservationController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpPost("/setReservation")]
        public async Task<IActionResult> SetReservation([FromBody] SetReservationRequest request)

        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(errors);
            }
            var doctor = await _dataContext.Users.FindAsync(request.DoctorId);
            if (doctor == null) return BadRequest("ექიმი არ არსებობს");
            var reservation = new Reservation
            {
                Title = request.Title,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                DoctorId = doctor.Id,
            };
            if (request.UserId != null)
            {
                var user = await _dataContext.Users.FindAsync(request.UserId);
                if (user == null) return BadRequest("მომხმარებელი არ არსებობს");
                reservation.UserId = user.Id;
            }
            await _dataContext.Reservations.AddAsync(reservation);
            await _dataContext.SaveChangesAsync();
            return Ok();

        }
    }
}
