using System.Security.Cryptography;
using AutoMapper;
using clinic.Models;
using clinic.Schemas;
using clinic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace clinic.Controllers.Auth
{
    public class RegistrationController :Controller
    {
        private readonly IMailService _mailService;
        private readonly DataContext _dataContext;
        public RegistrationController(DataContext dataContext, IMailService mailService) 
        {
           _dataContext = dataContext;
           _mailService = mailService;
        }
       
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserRegisterRequest request)
        {
            if(_dataContext.Users.Any(u=>u.Email == request.Email || u.Pid == request.Pid))
            {
                return BadRequest("User already exists.");
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
                VerificationToken = CreateRandomToken()
            };
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            var mailRequest = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Email verification",
                Body = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/verify?token={user.VerificationToken}"

             };
            await _mailService.SendEmailAsync(mailRequest);
            return Ok("გთხოვთ შეამოწმოთ მეილი, და გადახვიდეთ აქტივაციის ბმულზე!");
        }

        private void CreatePasswordHash(string password, out byte [] passwordHash, out byte [] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public string CreateRandomToken()
        {
            byte [] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte [] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
    }
}
}
