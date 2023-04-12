using clinic.Models;
using clinic.Schemas;
using clinic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace clinic.Controllers.Auth
{
    [Route("/[controller]")]
    [ApiController]
    public class CreateCodeController :Controller
    {   
        private readonly IMailService _mailService;
        private readonly DataContext _dataContext;
        public CreateCodeController(DataContext dataContext, IMailService mailService) 
        {
            _dataContext = dataContext;
            _mailService = mailService;
        }
        [HttpPost("createRestoreCode")]
        public async Task<IActionResult> CreateRestoreCode([FromBody] CheckEmailRequest request)
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
            var random_code = "";
            for(int i = 0; i <1000; i++)
            {
                random_code = Utils.GenerateRandomString(10);
                var code_exists = await _dataContext.CodeVerifications.FirstOrDefaultAsync(c => c.Code == random_code);
                if(code_exists == null)
                {
                    break;
                }
            }

            var code = new CodeVerification
            {
                Code = random_code,
                Email = user.Email,
                ExpirationDate = DateTime.Now.AddMinutes(5)
            };
            _dataContext.CodeVerifications.Add(code);
            await _dataContext.SaveChangesAsync();

            var mailRequest = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Restore Password",
                Body = $"You have 5 minutes. \n Use this code: {random_code}"

            };
            await _mailService.SendEmailAsync(mailRequest);

            return Ok();
        }
    }
}
