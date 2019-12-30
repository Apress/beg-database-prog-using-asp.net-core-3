using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.Angular.Models;
using EmployeeManager.Angular.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace EmployeeManager.Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private IConfiguration config;

        private AppDbContext db;

        public SecurityController(IConfiguration config, AppDbContext db)
        {
            this.config = config;
            this.db = db;
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public IActionResult SignIn([FromBody]SignIn loginDetails)
        {
            var query = from u in db.Users
                        where u.UserName == loginDetails.UserName && u.Password == loginDetails.Password
                        select u;

            if (query.Count() > 0)
            {
                var tokenString = GenerateJWT(loginDetails.UserName);
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public IActionResult Register([FromBody]Register userDetails)
        {
            var usr = from u in db.Users
                      where u.UserName == userDetails.UserName
                      select u;

            if (usr.Count() <= 0)
            {
                var user = new User();
                user.UserName = userDetails.UserName;
                user.Password = userDetails.Password;
                user.Email = userDetails.Email;
                user.FullName = userDetails.FullName;
                user.BirthDate = userDetails.BirthDate;
                user.Role = "Manager";

                db.Users.Add(user);
                db.SaveChanges();
                return Ok("User created successfully.");
            }
            else
            {
                return BadRequest("User Name already exists.");
            }
        }



        private string GenerateJWT(string userName)
        {
            var usr = (from u in db.Users
                       where u.UserName == userName
                       select u).SingleOrDefault();

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, usr.UserName));
            claims.Add(new Claim(ClaimTypes.Role, usr.Role));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                expires: DateTime.Now.AddHours(12),
                signingCredentials: credentials,
                claims: claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
