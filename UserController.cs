using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repo;

    public UserController(IUserRepository repo) => _repo = repo;

    [HttpPost]
    public IActionResult Create([FromBody] UserInput input)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var (hash, salt) = PasswordHasher.HashPassword(input.Password);
        var user = new User { Username = input.Username, Email = input.Email, PasswordHash = hash, PasswordSalt = salt, Role = "User" };
        _repo.InsertUser(user);

        return Ok(new { message = "User registered successfully." });
    }
}
