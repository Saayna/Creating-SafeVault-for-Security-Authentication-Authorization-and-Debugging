[TestFixture]
public class SecurityIntegrationTests
{
    private HttpClient _client = null!;

    [SetUp]
    public void Setup()
    {
        _client = new WebApplicationFactory<Program>().CreateClient();
    }

    [Test]
    public async Task XssPayload_ShouldBeEncoded()
    {
        var payload = new { Username = "bob", Email = "<script>alert('XSS')</script>", Password = "Pass123!" };
        var res = await _client.PostAsJsonAsync("/api/users", payload);
        var body = await res.Content.ReadAsStringAsync();
        Assert.IsFalse(body.Contains("<script>"));
        Assert.IsTrue(body.Contains("&lt;script&gt;"));
    }
}
