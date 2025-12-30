using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;
    private readonly JwtService _jwt;
    private readonly RefreshTokenService _refresh;

    public AuthController(AuthService auth, JwtService jwt, RefreshTokenService refresh)
    {
        _auth = auth; _jwt = jwt; _refresh = refresh;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        var user = _auth.Authenticate(req.Username, req.Password);
        if (user == null) return Unauthorized(new { error = "Invalid username or password." });

        var access = _jwt.GenerateAccessToken(user.Username, user.Role);
        var refresh = _refresh.Issue(user.Username);

        return Ok(new { accessToken = access, refreshToken = refresh.Token, expiresIn = 900 });
    }

    [HttpPost("refresh")]
    public IActionResult Refresh([FromBody] RefreshRequest req)
    {
        var (ok, access, newRefresh) = _refresh.Refresh(req.RefreshToken, _auth.GetRole, _jwt);
        if (!ok) return Unauthorized(new { error = "Invalid refresh token." });

        return Ok(new { accessToken = access, refreshToken = newRefresh!.Token, expiresIn = 900 });
    }
}
