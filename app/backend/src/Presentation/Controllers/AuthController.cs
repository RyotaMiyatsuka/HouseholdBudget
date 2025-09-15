using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // POST /api/auth/login
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        // Mock implementation
        if (loginRequest.Email == "test@example.com" && loginRequest.Password == "password")
        {
            var response = new LoginResponse
            {
                Token = "dummy-jwt-token"
            };
            return Ok(response);
        }
        return Unauthorized();
    }
}

// --- DTOs based on openapi.yml ---

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public string Token { get; set; }
}
