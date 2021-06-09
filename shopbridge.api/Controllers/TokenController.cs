using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using shopbridge.api.Models;
using shopbridge.api.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace shopbridge.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IUser _userRepo;
        private readonly JwtSettings _jwtSettings;
        public TokenController(IUser user, IOptions<JwtSettings> jwtSettings)
        {
            _userRepo = user;
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Generate Token
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostAsync(LoginCredential credential)
        {
            try
            {
                var user = _userRepo.GetByUsernamePasswordAsync(credential.Username, credential.Password);
                if (user == null)
                    return BadRequest("Username or Password is incorrect");

                var tokenHandler = new JwtSecurityTokenHandler();
                var secret = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.Version, "v1")
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                
                return Ok(user.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Some error has occured!");
            }
        }
    }
}
