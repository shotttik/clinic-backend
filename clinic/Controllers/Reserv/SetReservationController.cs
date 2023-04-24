using System.Globalization;
using can.Migrations;
using clinic.Models;
using clinic.Schemas.reservation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            DateTime startDate = DateTime.ParseExact(request.Start, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(request.End, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
            int searchId = (int)(request.UserId == null ? request.DoctorId : request.UserId);
            var exists = await _dataContext.Reservations.Where(r =>
            r.UserId == searchId && (
            startDate > r.StartDate &&
            startDate < r.EndDate ||
            endDate > r.StartDate &&
            endDate < r.EndDate)).ToListAsync();
            if (!exists.IsNullOrEmpty()) return BadRequest("ამ დროს ჯავშანი უკვე გაკეთებულია.");
            var reservation = new Reservation
            {
                Title = request.Title,
                StartDate = startDate,
                EndDate = endDate,
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
