using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using server.Data;
using server.Models;

namespace server.Controllers
{
   [ApiController]
   [EnableCors("AllowClient")]
   public class UserController : ControllerBase
   {
#pragma warning disable CS8604
      private readonly TaskManagerContext _taskmanagercontext;
      private readonly ILogger<User> _logger;
      private readonly IConfiguration _configuration;
      public UserController(TaskManagerContext taskmanagercontext, ILogger<User> logger, IConfiguration configuration)
      {
         _taskmanagercontext = taskmanagercontext;
         _logger = logger;
         _configuration = configuration;
      }
      [HttpGet("All")]
      public async Task<IActionResult> All()
      {
         return Ok(await _taskmanagercontext.Users.ToListAsync());
      }

      [HttpPost("Register")]
      public async Task<IActionResult> Register([FromBody] User user)
      {
         var existingUser = await _taskmanagercontext.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
         if (existingUser != null)
         {
            return BadRequest("UserName Already Existing");
         }

         user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
         _taskmanagercontext.Users.Add(user);

         await _taskmanagercontext.SaveChangesAsync();
         return Ok();
      }

      [HttpPost("Login")]
      public async Task<IActionResult> Login([FromBody] User user)
      {
         var existingUser = await _taskmanagercontext.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
         if (existingUser == null || !BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password))
         {
            return Unauthorized("Invalid username or password");
         }


         var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));


         var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

         var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, existingUser.UserName),
               new Claim("IDUser", existingUser.IDUser.ToString())
            };

         var tokeOptions = new JwtSecurityToken(
             issuer: _configuration["Jwt:Issuer"],
             audience: _configuration["Jwt:Audience"],
             claims: claims,
             expires: DateTime.Now.AddMinutes(60),
             signingCredentials: signinCredentials
         );

         var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
         return Ok(new { Token = tokenString });
      }

      [HttpGet("Profile")]
      public async Task<IActionResult> Profile()
      {
         var iduser = User.Claims.FirstOrDefault(c => c.Type == "IDUser")?.Value;

         if (iduser == null)
         {
            return NotFound();
         }

         Guid IDUSER = Guid.Parse(iduser);
         var user = await _taskmanagercontext.Users.FirstOrDefaultAsync(m => m.IDUser == IDUSER);

         if (user == null)
         {
            return NotFound();
         }

         return Ok(user);
      }

      [HttpPut("EditProfile")] //Need Review
      public async Task<IActionResult> EditProfile(User user)
      {
         var iduser = User.Claims.FirstOrDefault(c => c.Type == "IDUser")?.Value;
         Guid IDUSER = Guid.Parse(iduser);

         if (ModelState.IsValid)
         {
            try
            {
               _taskmanagercontext.Update(user);
               await _taskmanagercontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!UserExists(IDUSER))
               {
                  return NotFound();
               }
               else
               {
                  throw;
               }
            }
            return Ok();
         }
         return BadRequest("ModelState InValid");
      }

      private bool UserExists(Guid IDUSER)
      {
         return _taskmanagercontext.Users.Any(e => e.IDUser == IDUSER);
      }

      [HttpPost("Logout")]
      public async Task<IActionResult> Logout()
      {
         await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);

         return Ok(new { Message = "Logged out successfully" });
      }

   }

}