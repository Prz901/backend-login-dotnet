using authLogin.Entities;
using authLogin.Models;

namespace authLogin.Services
{
  public interface IAuthService
  {
    Task<User?> RegisterAsync(UserDto request);
    Task<string?> LoginAsync(UserDto request);

  }
}


