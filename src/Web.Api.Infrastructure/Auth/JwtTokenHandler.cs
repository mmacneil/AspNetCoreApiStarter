using System.IdentityModel.Tokens.Jwt;
using Web.Api.Infrastructure.Interfaces;

namespace Web.Api.Infrastructure.Auth
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JwtTokenHandler()
        {
            if(_jwtSecurityTokenHandler==null)
                _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public string WriteToken(JwtSecurityToken jwt)
        {
            return _jwtSecurityTokenHandler.WriteToken(jwt);
        }
    }
}
