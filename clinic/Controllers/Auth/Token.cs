using clinic.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace clinic.Controllers.Auth
{
    public class Token
    {

        public static string CreateToken(
            int Id, string Email, string Pid,
            UserRole Role, string FirstName, 
            string LastName, string Category, 
            string Image, string rCount)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", Id.ToString()),
                new Claim("FirstName", FirstName),
                new Claim("LastName", LastName),
                new Claim("Email", Email),
                new Claim("Pid", Pid),
                new Claim("Role", Role.ToString()),
                new Claim("Category", Category),
                new Claim("Image", Image),
                new Claim("MyReservationsCount", rCount),

            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                ConfigurationHelper.config.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
