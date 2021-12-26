using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Devjobs.Models;
using Devjobs.Dtos;
using System.Text;
using System.Security.Cryptography;
using System.Security.Claims;
using Devjobs.Repositories;

namespace Devjobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {        
        public IConfiguration configuration;
        public IUsersRepository users;

        public AuthController(IConfiguration config, IUsersRepository repo)
        {            
            this.configuration = config;
            this.users = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto form)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }
            
            var userInDb = await users.GetUserByEmailAsync(form.Email);
            if (userInDb is not null)
            {
                return BadRequest("The email has been registered by another account!");
            }
            var user = new User
            {
                Email = form.Email,         
                Password = GetMD5(form.Password),
                Role = form.IsCorporate ? "corporate" : "candidate"
            };
            await users.AddUserAsync(user);            

            // tra token ve client
            return Ok(new JwtSecurityTokenHandler().WriteToken(CreateAccessToken(user)));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto userData)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }
            userData.Password = GetMD5(userData.Password);
            var user = await users.GetUserByEmailAsync(userData.Email);
                //FirstOrDefaultAsync(u => u.Email == userData.Email && u.Password == userData.Password);
            if (user.Password == userData.Password)
            {
                return Unauthorized("Email or Password is not correct!");
            }
            
            // tra token ve client
            return Ok(new JwtSecurityTokenHandler().WriteToken(CreateAccessToken(user)));            
        }

        //[HttpPost("me")]
        //public async Task<ActionResult<dynamic>> GetMe()
        //{
        //    return 
        //}


        private JwtSecurityToken CreateAccessToken(User user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()),
                new Claim("Id",user.Id.ToString()),                
                new Claim("Email",user.Email),
                new Claim("Role",user.Role),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddDays(1), signingCredentials: signature);

            return token;
        }
        private static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] frData = Encoding.UTF8.GetBytes(str);
            byte[] toData = md5.ComputeHash(frData);
            string hashString = "";
            for (int i = 0; i < toData.Length; i++)
            {
                hashString += toData[i].ToString("x2");
            }
            return hashString;
        }

    }
}
