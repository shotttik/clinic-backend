using clinic.Schemas;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using System.Net.Http.Headers;
using clinic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

namespace clinic.Controllers.Doctor
{

    /*[Authorize(Policy = "Admin")]*/
    public class CreateDoctorController : Controller
    {
        private readonly DataContext _dataContext;
        public CreateDoctorController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpPost("createDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorRequest request)
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
            if (await _dataContext.Users.AnyAsync(d => d.Email == request.Email || d.Pid == request.Pid))
            {
                return BadRequest("ექიმი უკვე რეგისტრირებულია.");
            }

            var category = await _dataContext.Categories.FindAsync(request.CategoryId);
            if (category == null)
            {
                return NotFound("კატეგორია ვერ მოიძებნა");
            }
            CreatePasswordHash(request.Password, out byte [] passwordHash, out byte [] passwordSalt);

            var doctor = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Pid = request.Pid,
                Category = category,
                Image = request.Image,
                Document = request.Document,
                Role = UserRole.Doctor,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerifiedAt = DateTime.Now

            };
            await _dataContext.Users.AddAsync(doctor);
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

