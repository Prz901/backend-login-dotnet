using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using authLogin.Data;
using authLogin.Entities;
using authLogin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace authLogin.Services
{
  public class AuthService(UserDbContext context, IConfiguration configuration) : IAuthService
  {
    public async Task<string?> LoginAsync(UserDto request)
    {
      // verica usuario no banco
      var user = await context.Users.FirstOrDefaultAsync(user => user.Username == request.Username);

      if (user is null)
      {
        return null;
      }

      if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
      {
        return null;
      }

      return CreateToken(user);

    }

    public async Task<User?> RegisterAsync(UserDto request)
    {
      if (await context.Users.AnyAsync(user => user.Username == request.Username))
      {
        return null;
      }

      var user = new User();
      // cria a hash de senha
      var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);

      user.Username = request.Username;
      user.PasswordHash = hashedPassword;

      // adiciona no contexto que vai adicionar no banco -> context -> DB
      context.Users.Add(user);
      // Aqui ele adiciona no banco realmente ele comita a informacao no banco
      await context.SaveChangesAsync();
      return user;
    }

    private string CreateToken(User user)
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
      };

      var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!)
      );

      // signin credential
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

      var tokenDescriptor = new JwtSecurityToken(
        issuer: configuration.GetValue<string>("AppSettings:Issuer"),
        audience: configuration.GetValue<string>("AppSettings:Audience"),
        claims: claims,
        expires: DateTime.UtcNow.AddDays(1),
        signingCredentials: creds
      );

      return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
  }
}


