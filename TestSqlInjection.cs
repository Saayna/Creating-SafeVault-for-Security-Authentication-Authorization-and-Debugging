[TestFixture]
public class TestSqlInjection
{
    [Test]
    public void SqlInjectionAttempt_ShouldNotReturnUser()
    {
        var repo = new UserRepository("connectionString");
        var malicious = "alice' OR '1'='1";
        var user = repo.GetByUsername(malicious);
        Assert.IsNull(user);
    }
}
