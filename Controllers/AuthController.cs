using Microsoft.AspNetCore.Mvc;
using AttendanceBackend.Models;

namespace AttendanceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Pour le test, on accepte juste "admin" / "1234"
            if (request.Username == "admin" && request.Password == "1234")
            {
                return Ok(new { Token = "demo-token" });
            }
            return Unauthorized(new { Message = "Nom d'utilisateur ou mot de passe incorrect" });
        }
    }
}