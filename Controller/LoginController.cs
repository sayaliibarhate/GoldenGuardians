using GoldenGuardians.Models;
using Google.Protobuf.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mysqlx.Session;
using Org.BouncyCastle.Asn1.Ess;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Dilithium;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoldenGuardians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]

        public IActionResult Login([FromBody] Login UserLogin)
        {
            var email = Authenticate(UserLogin);

            if (email != null)
            {
                var token = Generate(email);
                return Ok(token);
            }

            return NotFound("email not found");

        }

        private string Generate(Register User)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, User.Name),
                new Claim(ClaimTypes.Gender, User.Gender),
                new Claim(ClaimTypes.DateOfBirth, User.Birthdate),
                new Claim(ClaimTypes.MobilePhone, User.Phoneno),
                new Claim(ClaimTypes.Email, User.Email),
                new Claim(ClaimTypes.UserData, User.Pass),
                new Claim(ClaimTypes.StreetAddress, User.City)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],//api will validate if issuer or audience has from that we have defined in the startup class 
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);//call the handler and pass the token
        }

        private Register? Authenticate(Login UserLogin)
        {

            var currentUser = Userconstants.Users.FirstOrDefault(o => o.Email.ToLower() ==
             UserLogin.email.ToLower() && o.Pass == UserLogin.password);

            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
    }
}