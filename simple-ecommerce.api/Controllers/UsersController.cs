using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using simple_ecommerce.api.Data;
using simple_ecommerce.api.Model;
using simple_ecommerce.api.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace simple_ecommerce.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IConfiguration _configuration;
        public readonly ApplicationDBContext _context;

        public UsersController(IConfiguration configuration, ApplicationDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(AddUserRequest addUserRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(s => s.Email == addUserRequest.Email);
            if(user == null)
            {
                //CreatePasswordHash(addUserRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);
                //var newUser = new User()
                //{
                //    Id = Guid.NewGuid(),
                //    Username = addUserRequest.Username,
                //    Email = addUserRequest.Email,
                //    PasswordHash = passwordHash,
                //    PasswordSalt = passwordSalt,
                //};
                //await _context.Users.AddAsync(newUser);
                //await _context.SaveChangesAsync();

                // send confirmation email
                string emailSubject = "Contact confirmation";
                string userName = addUserRequest.Username;
                string emailMessage = "Dear" + userName + "\n" +
                    "We received your message. Thank you for contacting us \n" +
                    "Our team contact you very soon \n" +
                    "Best regards \n \n";

                EmailSender emailSender = new EmailSender();
                emailSender.SendEmail(emailSubject, addUserRequest.Email, userName, emailMessage).Wait();

                return Ok(emailSender);
            }
            else
            {
                return BadRequest("this user are register before");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(AddUserRequest addUserRequest)
        {
            IActionResult response = Unauthorized();
            var user = await _context.Users.FirstOrDefaultAsync(s => s.Email == addUserRequest.Email);
            if (user != null)
            {
                if (user.Email != addUserRequest.Email)
                {
                    return BadRequest("User not found.");
                }

                if (!VerifyPasswordHash(addUserRequest.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return BadRequest("Wrong password.");
                }

                var token = GenerateToken(user);
                response = Ok(new { token = token, id = user.Id, username = user.Username, email = user.Email });

                return Ok(response);
            }
            return NotFound();

        }
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], null,
                    expires: DateTime.Now.AddDays(3),
                    signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
