[TestFixture]
public class AuthorizationPolicyTests
{
    [Test]
    public async Task AdminEndpoint_ShouldRequireAdminRole()
    {
        var client = new WebApplicationFactory<Program>().CreateClient();
        var token = await Tokens.LoginAndGetTokenAsync(client, "adminUser", "AdminPass!");
        var req = new HttpRequestMessage(HttpMethod.Get, "/api/secure/admin");
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var res = await client.SendAsync(req);
        Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
    }
}
