using CollegeProjectManagment.Core.DTO;
using CollegeProjectManagment.Core.Identity;
using CollegeProjectManagment.Core.ServiceContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CollegeProjectManagment.Core.Services
{
    public class JwtService : IJwtService
    {
        //private readonly IConfiguration _configuration;

        //public JwtService(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        //public AuthenticationResponse CreateJwtToken(ApplicationUser user)
        //{
        //    DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATIN_MINUTES"]));
        //    Claim[] claims = new Claim[] {
        //        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),

        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT ID  

        //        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()), // Issued at ( date and time of token generation)

        //        new Claim(ClaimTypes.NameIdentifier, user.Email), // unique name identifier of the user (Email)
        //        new Claim(ClaimTypes.Name, user.PersonName), // unique name identifier of the user (Email)

        //    };

        //    SymmetricSecurityKey securityKey = new SymmetricSecurityKey
        //        SymmetricSecurityKey(
        //           Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        public AuthenticationResponse CreateJwtToken(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}

