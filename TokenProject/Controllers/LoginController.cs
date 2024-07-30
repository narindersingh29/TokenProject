using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokenProject.Entities;

namespace TokenProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public LoginController(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {

            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var (accessToken, refreshToken) = await _tokenService.GenerateTokenAsync(user);

            return Ok(new { accessToken, refreshToken });

        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenModel model)
        {
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == model.RefreshToken);

            if (refreshToken == null)
            {
                return Unauthorized();
            }

            var user = await _context.Users.FindAsync(refreshToken.UserId);

            if (user == null)
            {
                return Unauthorized();
            }

            var (newAccessToken, newRefreshToken) = await _tokenService.GenerateTokenAsync(user);

            // revoke the old refresh token
            _context.RefreshTokens.Remove(refreshToken);

            return Ok(new { newAccessToken, newRefreshToken });
        }
        
    }
}
