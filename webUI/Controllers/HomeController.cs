using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using webUI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace webUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IConfiguration _configuration;
        private readonly UsersDbContext _context;

        public HomeController(ILogger<HomeController> logger, UsersDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserInfo _userData)
        {
            if (_userData != null && _userData.UserName != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.UserName, _userData.Password);

                if (user != null)
                {
                    ViewBag.UserName = user.UserName;
                    ViewBag.Id = user.UserId.ToString();
                    ViewBag.Password = user.Password;
                    return View();

                    //create claims details based on the user information
                    var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim("Id", user.UserId.ToString()),
                            new Claim("UserName", user.UserName)
                            //new Claim("Email", user.Email)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    //ModelState.AddModelError("Name1", "Invalid credentials1");
                    ViewBag.NotValidUser = "Invalid credentials!";
                    ViewBag.Failedcount  = 1;
                    //return Error();// "Invalid credentials");
                    //return BadRequest("Invalid credentials");
                }
            }
            else
            {
                //ModelState.AddModelError("Name2", "Invalid credentials2");
                ViewBag.NotValidUser = "Invalid credentials!";
                ViewBag.Failedcount = 1;
                //return Error(); 
                //BadRequest();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<UserInfo> GetUser(string username, string password)
        {
            return await _context.UserInfo.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
    }
}
