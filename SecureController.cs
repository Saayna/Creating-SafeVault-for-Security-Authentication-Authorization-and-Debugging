using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/secure")]
public class SecureController : ControllerBase
{
    private readonly ILogger<SecureController> _logger;
    public SecureController(ILogger<SecureController> logger) => _logger = logger;

    [HttpGet("user")]
    [Authorize]
    public IActionResult GetUserData()
    {
        _logger.LogInformation("User {User} accessed /api/secure/user", User.Identity!.Name);
        return Ok(new { message = "Secure user data" });
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAdminData()
    {
        _logger.LogInformation("Admin {User} accessed /api/secure/admin", User.Identity!.Name);
        return Ok(new { message = "Secure admin data" });
    }
}
