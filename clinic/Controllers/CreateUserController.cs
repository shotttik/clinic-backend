using System.Security.Cryptography;
using clinic.Models;
using clinic.Schemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace clinic.Controllers
{
    [Authorize(Policy = "Admin")]
    public class CreateUserController : Controller
    {
        private readonly DataContext _dataContext;
        public CreateUserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
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
            if (await _dataContext.Users.AnyAsync(u => u.Email == request.Email || u.Pid == request.Pid))
            {
                return BadRequest("მომხმარებელი უკვე რეგისტრირებულია.");
            }
            CreatePasswordHash(request.Password, out byte [] passwordHash, out byte [] passwordSalt);
            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Pid = request.Pid,
                VerifiedAt = DateTime.Now,
                IsAdmin = request.IsAdmin,
            };

            _dataContext.Users.Add(user);
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
