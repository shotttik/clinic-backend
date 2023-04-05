using AutoMapper;
using clinic.Models;
using clinic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers.Auth
{
    public class RegistrationController :Controller
    {
        private readonly IMailService mailService;
        public RegistrationController(IMailService mailService)
        {
            this.mailService = mailService;
        }
       
        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] MailRequest mailRequest)
        {
            try
            {
                await mailService.SendEmailAsync(mailRequest);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
