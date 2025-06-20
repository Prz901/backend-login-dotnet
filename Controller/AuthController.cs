using System.Threading.Tasks;
using authLogin.Entities;
using authLogin.Models;
using authLogin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace authLogin.Controllers
{
  // Quando passado esse API/controller ele cria uma rota com o 'api / [nome da classe como o nome é authController ele tira o controller e deixa apenas o auth]
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController(IAuthService authService) : ControllerBase
  {
    public static User user = new();

    [HttpPost("usuarios")]
    public async Task<ActionResult<User>> Register(UserDto request)
    {
      var user = await authService.RegisterAsync(request);

      if (user is null)
      {
        return BadRequest("Username already exists.");
      }
      return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDto request)
    {
      var token = await authService.LoginAsync(request);

      if (token is null)
      {
        return BadRequest("Invalid username or password.");
      }

      return Ok(token);

    }

    [Authorize]
    [HttpGet]
    public IActionResult AuthenticatedOnlyEndpoint()
    {
      return Ok("You are authenticated!");
    }
  }
}




