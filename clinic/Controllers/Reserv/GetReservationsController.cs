using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Data;

namespace clinic.Controllers.Reserv
{
    public class GetReservationsController :Controller
    {
        private readonly DataContext _dataContext;
        public GetReservationsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("getReservations/Doctor/{id}")]
        public async Task<IActionResult> GetDoctorReservations(int Id) 
        {
            var reservations = await _dataContext.Reservations.Where(r=> r.DoctorId == Id).ToListAsync();
            if (reservations.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(reservations);
        }
        [HttpGet("getReservations/User/{id}")]
        public async Task<IActionResult> GetUserReservations(int Id)
        {
            var reservations = await _dataContext.Reservations.Where(r => r.UserId == Id).ToListAsync();
            if (reservations.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(reservations);
        }
    }
}
