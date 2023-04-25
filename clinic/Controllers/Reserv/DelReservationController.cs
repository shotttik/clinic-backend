using System.IdentityModel.Tokens.Jwt;
using clinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace clinic.Controllers.Reserv
{

    [Authorize]
    public class DelReservationController :Controller
    {
        private readonly DataContext _dataContext;
        public DelReservationController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpPost("delReservation/{id}")]
        public async Task<IActionResult> DelReservation(int id)
        {
            var reservation = await _dataContext.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            Request.Headers.TryGetValue("Authorization", out var authHeader);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(authHeader.ToString().Split(' ') [1]);
            token.Payload.TryGetValue("Id", out var userId);
            var user = await _dataContext.Users.FindAsync(int.Parse((string)userId!));
            if (user == null)
            {
                return Unauthorized();
            }

            bool delete = false;
            if (user.Id == reservation.UserId ||
                user.Id == reservation.DoctorId ||
                user.Role == UserRole.Admin)
            {
                delete = true;
            }


            if (!delete) return BadRequest("წაშლა შეუძლებელია.");

            _dataContext.Reservations.Remove(reservation);
            await _dataContext.SaveChangesAsync();
            return Ok();



        }
    }
}
