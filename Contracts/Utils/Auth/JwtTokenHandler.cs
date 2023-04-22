using Contracts.Dto;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Utils.Auth
{

    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "ydf2345sdfgh2345olkjioklm456jkgsdf456JD";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly List<UserAccount> _userAcountList;


        public JwtTokenHandler()
        {
            _userAcountList = new List<UserAccount>
            {
                new UserAccount{UserName = "admin", Password ="admin123", Role ="Administrator"},
                new UserAccount{UserName = "user01", Password ="user01", Role ="User"}
            };
        }


        public AuthenticateResponse? GeneraJwtToken(AuthenticateRequest authenticateRequest)
        {
            if (string.IsNullOrWhiteSpace(authenticateRequest.Username) || string.IsNullOrWhiteSpace(authenticateRequest.Password))
            {
                return null;
            }

            //validate
            var userAccount = _userAcountList.Where(x => x.UserName == authenticateRequest.Username && x.Password == authenticateRequest.Password).FirstOrDefault();
            if (userAccount == null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenkey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, authenticateRequest.Username),
                new Claim(ClaimTypes.Role, userAccount.Role),

            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenkey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticateResponse
            {
                UserName = authenticateRequest.Username,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
                JwtToken = token,
            };


        }
    }
}
