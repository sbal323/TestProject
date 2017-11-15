using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace TestSuite.Tests
{
    class oAuthTest : ITest
    {
        string ITest.Title
        {
            get
            {
                return "oAuth learning test";
            }
        }

        void ITest.Run()
        {
            var token = new JwtSecurityToken(
                issuer: "http://myappp.lanteriaonline.com/",
                audience: "http://myappp.lanteriaonline.com/powerbi",
                claims: GetClaims(),
                signingCredentials: GetKey(),
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(3)
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine(string.Format("JWT token string = {0}", tokenString));
        }

        private IEnumerable<Claim> GetClaims()
        {
            List<Claim> result = new List<Claim>();
            result.Add(new Claim("username", "bal323"));
            result.Add(new Claim("role", "administrator"));
            return result;
        }

        private SigningCredentials GetKey()
        {
            throw new NotImplementedException();
        }
    }
}
