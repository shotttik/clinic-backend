using System.Security.Cryptography;
using clinic.Models;
using clinic.Schemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace clinic.Controllers.Auth
{
    [ApiController]
    public class LoginController :Controller
    {
        private readonly DataContext _dataContext;
        public LoginController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginRequest request)
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
            if (user.VerifiedAt == null)
            {
                return BadRequest("არ არის აქტიური!");
            }
            if (!VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("პაროლი არასწორია.");
            }
            string categoryName = "";
            if(user.CategoryId != null)
            {
            var category = await _dataContext.Categories.FindAsync(user.CategoryId);
            if(category != null) categoryName = category.Name;
            }
            string token = Token.CreateToken(user.Email, user.Pid, user.Role, user.FirstName, user.LastName, categoryName);
            return Ok(new { token = token });
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
