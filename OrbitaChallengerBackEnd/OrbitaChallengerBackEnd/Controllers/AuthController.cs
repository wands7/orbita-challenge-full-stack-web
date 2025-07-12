using Microsoft.AspNetCore.Mvc;
using OrbitaChallengerBackEnd.Models;
using OrbitaChallengerBackEnd.Services;

namespace OrbitaChallengerBackEnd.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Simulação de autenticação. Troque por validação no banco!
            if (model.Username == "admin" && model.Password == "123")
            {
                var token = _tokenService.GenerateToken(model.Username, "Admin");
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}
