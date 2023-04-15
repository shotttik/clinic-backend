using System.Security.Cryptography;
using clinic.Schemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace clinic.Controllers.Auth
{
    [ApiController]
    public class ResetPasswordController :Controller
    {
        private readonly DataContext _dataContext;
        public ResetPasswordController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassowrd([FromBody] ResetPasswordRequest request)
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
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest("მომხმარებელი ვერ მოიძებნა.");
            }
            var code = await _dataContext.CodeVerifications.FirstOrDefaultAsync(c => c.Code == request.Code && c.Email == request.Email);
            if (code == null)
            {
                return BadRequest("კოდი არასწორია.");

            }
            DateTime currentDate = DateTime.Now;
            if (code.ExpirationDate < currentDate)
            {
                return BadRequest("კოდის მოქმედების ვადა ამოიწურა.");
        
            }

            CreatePasswordHash(request.Password, out byte [] passwordHash, out byte [] passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            _dataContext.CodeVerifications.Remove(code);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        private void CreatePasswordHash(string password, out byte [] passwordHash, out byte [] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
