using ApiToken.Dtos;
using ApiToken.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiToken.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService _token;

        public AuthenticationController(ITokenService token)
        {
            _token = token;
        }

        [HttpPost("authentication")]
        public IActionResult Login([FromForm] LoginDto login)
        {
            try
            {
                var token = _token.GenerateToken(login);

                if (token == "")
                {
                    return Unauthorized();
                }

                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro de criação por parte do servidor(Controller): {ex.Message}");
            }

        }
    }
}
