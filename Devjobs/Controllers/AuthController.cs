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

namespace Devjobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext context;
        public IConfiguration configuration;

        public AuthController(IConfiguration config, DatabaseContext context)
        {
            this.context = context;
            this.configuration = config;
        }

        [HttpPost("/api/register")]
        public async Task<IActionResult> Register(RegisterDto form)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }
            
            var userInDb = await context.Users.FirstOrDefaultAsync(u => u.Email == form.Email);
            if (userInDb is not null)
            {
                return BadRequest("The email has been registered by another account!");
            }
            var user = new User
            {
                Email = form.Email,
                Username = form.Email,
                Phone = form.Phone,
                FirstName = form.FirstName,
                LastName = form.LastName,
                Password = GetMD5(form.Password),
                Role = "User"
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            // tra token ve client
            return Ok(new JwtSecurityTokenHandler().WriteToken(CreateAccessToken(user)));
        }

        [HttpPost("/api/login")]
        public async Task<IActionResult> Login(LoginDto userData)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }
            userData.Password = GetMD5(userData.Password);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userData.Email && u.Password == userData.Password);
            if (user is null)
            {
                return Unauthorized("Email or Password is not correct!");
            }
            
            // tra token ve client
            return Ok(new JwtSecurityTokenHandler().WriteToken(CreateAccessToken(user)));            
        }


        private JwtSecurityToken CreateAccessToken(User user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()),
                new Claim("Id",user.Id.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("Email",user.Email),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddDays(1), signingCredentials: signature);

            return token;
        }
        private string GetMD5(string str)
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
