using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using exam.Models;
using exam.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exam.Controllers
{
    
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private IUserRepository _iUserRepository;
        private string secrectKey = "needtogetthisfromenvironment";

        public AuthController(IUserRepository iUserRepository)
        {
            _iUserRepository = iUserRepository;
        }

        // API Auth sign-up

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp(string username, string email, string name,
                                     string password = "", string password_confirm = "")
        {
            if (!password.Equals(password_confirm))
            {
                return StatusCode(500, "Password Comfirm does not match! ");
            }
            var tmp1 = await _iUserRepository.FindByEmail(email);
            var tmp2 = await _iUserRepository.FindByUsername(username);
            if (tmp1 != null || tmp2 != null)
            {
                return StatusCode(500, "Email or username does exist! ");
            }

            var user = new User
            {
                name = name,
                username = username,
                email = email,
                password = password
            };

            await _iUserRepository.Create(user);
            return Ok(new { id = user.id, msg = "Successful!" });
        }

        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var user = await _iUserRepository.FindByUsername(username);

            if (user == null || !user.password.Equals(password))
            {
                return BadRequest();
            }

            var token = genToken(user);
            return Ok(new { token = genToken(user) });
        }

        private string genToken(User u)
        {

            var key = new SymmetricSecurityKey(Encoding.Default.GetBytes(this.secrectKey));
            var claims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.NameId, u.email),
                new Claim(JwtRegisteredClaimNames.Jti,u.email),

                new Claim(ClaimTypes.Role,u.role+""),
                new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
            };
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenStr;
        }

        [Authorize]
        [HttpGet]
        public IActionResult UserInfo()
        {
            var dict = new Dictionary<string, string>();
            var caller = User.Claims.ToList();
            return Ok();
        }
    }
}