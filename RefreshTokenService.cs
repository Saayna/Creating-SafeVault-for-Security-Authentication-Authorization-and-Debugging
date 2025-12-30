using System.Security.Cryptography;

public class RefreshTokenService
{
    private readonly IRefreshTokenRepository _repo;
    public RefreshTokenService(IRefreshTokenRepository repo) => _repo = repo;

    public RefreshToken Issue(string username, int days = 7)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var rt = new RefreshToken { Token = token, Username = username, Expires = DateTime.UtcNow.AddDays(days) };
        _repo.Save(rt);
        return rt;
    }

    public (bool ok, string? newAccessToken, RefreshToken? newRefresh) Refresh(
        string token, Func<string, string> roleResolver, JwtService jwt)
    {
        var rt = _repo.Get(token);
        if (rt == null || rt.Revoked || rt.Expires < DateTime.UtcNow) return (false, null, null);

        var role = roleResolver(rt.Username);
        var access = jwt.GenerateAccessToken(rt.Username, role);

        rt.Revoked = true; _repo.Update(rt);
        var newRt = Issue(rt.Username);
        return (true, access, newRt);
    }
}
