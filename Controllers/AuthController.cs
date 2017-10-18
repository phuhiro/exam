using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using exam.Models;
using exam.Repository;
using Microsoft.AspNetCore.Http;
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
        public IActionResult SignUp(string username, string email, string name,
                                     string password, string password_confirm)
        {
            if (!password.Equals(password_confirm))
            {
                return StatusCode(500, "Password Comfirm does not match! ");
            }
            var tmp1 = _iUserRepository.FindByEmail(email);
            var tmp2 = _iUserRepository.FindByUsername(username);
            if (_iUserRepository.FindByUsername(username) != null
                || _iUserRepository.FindByEmail(email) != null)
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

            var nUser = _iUserRepository.Create(user);
            return Ok(new { id = nUser.id, msg = "Successful!" });
        }

        [HttpPost]
        [Route("sign-in")]
        public IActionResult SignIn(string username, string password)
        {
            var user = _iUserRepository.FindByUsername(username);
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
                new Claim(JwtRegisteredClaimNames.Sub, u.email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

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
    }
}