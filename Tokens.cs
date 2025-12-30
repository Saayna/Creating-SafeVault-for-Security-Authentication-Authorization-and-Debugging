public static class Tokens
{
    public static async Task<string> LoginAndGetTokenAsync(HttpClient client, string username, string password)
    {
        var res = await client.PostAsJsonAsync("/api/auth/login", new { Username = username, Password = password });
        res.EnsureSuccessStatusCode();
        var payload = await res.Content.ReadFromJsonAsync<TokenResponse>();
        return payload!.AccessToken;
    }

    private class TokenResponse { public string AccessToken { get; set; } = string.Empty; }
}
