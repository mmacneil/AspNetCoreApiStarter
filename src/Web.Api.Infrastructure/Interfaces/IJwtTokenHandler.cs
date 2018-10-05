using System.IdentityModel.Tokens.Jwt;
 

namespace Web.Api.Infrastructure.Interfaces
{
        public interface IJwtTokenHandler
        {
            string WriteToken(JwtSecurityToken jwt);
        }
}
