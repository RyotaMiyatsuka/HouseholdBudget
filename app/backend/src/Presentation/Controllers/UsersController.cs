using Microsoft.AspNetCore.Mvc;

namespace HouseholdBudget.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
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
