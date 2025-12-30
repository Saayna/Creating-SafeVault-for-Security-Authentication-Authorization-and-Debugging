[TestFixture]
public class TestInputValidation
{
    [Test]
    public void ValidUsername_ShouldPass()
    {
        Assert.DoesNotThrow(() => InputSanitizer.SanitizeUsername("validUser123"));
    }

    [Test]
    public void InvalidUsername_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() => InputSanitizer.SanitizeUsername("DROP TABLE Users;"));
    }
}
