
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TokenProject.Entities;
#nullable disable
namespace TokenProject
{
    public class TokenService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public TokenService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //public async Task<string> GenerateTokenAsync(User user)
        //{
        //   var roles = await _context.UserRoles
        //        .Where(ur => ur.UserId == user.Id)
        //        .Select(ur => ur.Role.Name)
        //        .ToListAsync();

        //    var claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.Name, user.Email),
        //   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //};

        //    foreach (var role in roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    }

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        _configuration["Jwt:Issuer"],
        //        _configuration["Jwt:Audience"],
        //        claims,
        //        expires: DateTime.UtcNow.AddMinutes(30),
        //        signingCredentials: creds
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        public async Task<(string AccessToken, string RefreshToken)> GenerateTokenAsync(User user)
        {
            var roles = await _context.UserRoles
             .Where(ur => ur.UserId == user.Id)
               .Select(ur => ur.Role.Name)
               .ToListAsync();

            var claims = new List<Claim>
        {
           new Claim(ClaimTypes.Name, user.Email),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                   _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                     expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds
                );


             var refreshToken = Guid.NewGuid().ToString();

            // store the refresh token in the database or a secure storage
            await _context.RefreshTokens.AddAsync(new RefreshToken { UserId = user.Id, Token = refreshToken });
            await _context.SaveChangesAsync();

            return (new JwtSecurityTokenHandler().WriteToken(token), refreshToken);
        }
    }
}
