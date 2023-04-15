using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace clinic.Controllers.Auth
{
    public class Token
    {

        public static string CreateToken(string Email, string Pid,bool IsAdmin, string FirstName, string LastName )
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim("FirstName", FirstName),
                new Claim("LastName", LastName),
                new Claim("Email", Email),
                new Claim("Pid", Pid),
                new Claim("IsAdmin", IsAdmin.ToString()),


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
