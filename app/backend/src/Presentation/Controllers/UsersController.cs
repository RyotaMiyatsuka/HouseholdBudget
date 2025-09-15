using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    // POST /api/users
    [HttpPost]
    public IActionResult CreateUser([FromBody] NewUser newUser)
    {
        // Mock implementation
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = newUser.Name,
            Email = newUser.Email
        };
        return CreatedAtAction(nameof(CreateUser), new { userId = user.Id }, user);
    }

    // PUT /api/users/{userId}
    [HttpPut("{userId}")]
    public IActionResult UpdateUser(string userId, [FromBody] UpdateUser updateUser)
    {
        // Mock implementation
        var user = new User
        {
            Id = userId,
            Name = updateUser.Name,
            Email = updateUser.Email
        };
        return Ok(user);
    }
}

// --- DTOs based on openapi.yml ---

public class NewUser
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UpdateUser
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
