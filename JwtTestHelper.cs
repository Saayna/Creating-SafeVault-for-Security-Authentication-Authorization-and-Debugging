public static class JwtTestHelper
{
    public static string GenerateTestToken(string username, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("UnitTestKey1234567890!"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: "TestIssuer",
            audience: "TestAudience",
            expires: DateTime.UtcNow.AddMinutes(15),
            claims: new[] { new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, role) },
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
