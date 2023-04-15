using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace clinic.Controllers.Auth
{
    [ApiController]
    public class VerificationController :Controller
    {
        private readonly DataContext _dataContext;
        public VerificationController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("verify")]
        public async Task<IActionResult> VerifyUser(string token)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if (user == null)
            {
                return BadRequest("Invalid token."); 
            }
            if (tokenExpired(token))
            {
                return StatusCode(408);

            }
            user.VerifiedAt = DateTime.Now;
            await _dataContext.SaveChangesAsync();

            return Ok("მომხმარებელი გააქტიურდა წარმატებით.");
        }

        private bool tokenExpired(string token)
        {
            byte [] data = Convert.FromBase64String(token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            if (when < DateTime.UtcNow.AddMinutes(-30))
            {
                return true;
            }
            return false;
        }
    }
}
