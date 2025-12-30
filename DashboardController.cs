using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminDashboard() => Ok("Admin dashboard");

    [HttpGet("user")]
    [Authorize(Roles = "User")]
    public IActionResult UserDashboard() => Ok("User dashboard");

    [HttpGet("guest")]
    [Authorize(Roles = "Guest")]
    public IActionResult GuestDashboard() => Ok("Guest dashboard");
}
