using JobTrackerApi.Data;
using JobTrackerApi.DTOs;
using JobTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JobTrackerApi.Services;

namespace JobTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JobDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;  
        private readonly JwtService _jwt;

        public AuthController(JobDbContext context, JwtService jwt, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _jwt = jwt;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (userExists)
                return BadRequest("User already exists");

            var user = new User
            {
                Email = dto.Email
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
                return Unauthorized("Invalid email or password");

            // ⭐ Use PasswordHasher.VerifyHashedPassword instead of BCrypt
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid email or password");

            var token = _jwt.GenerateToken(user.Id, user.Email);
            return Ok(new { token });
        }
    }
}