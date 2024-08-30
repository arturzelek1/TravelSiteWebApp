using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace UiTests
{
    public class LoginBannerTests : IDisposable
    {
        private readonly IWebDriver driver;

        public LoginBannerTests()
        {
            driver = new FirefoxDriver(@"E:\GitHub\TravelSiteWebApp\UnitTest\webDriver");
        }

        [Fact]
        public void LoginBanner_ShouldBeVisible()
        {
            driver.Navigate().GoToUrl("https://localhost:7132/");

            var loginBanner = driver.FindElement(By.ClassName("banner"));
            Assert.True(loginBanner.Displayed);

            var loginButton = driver.FindElement(By.ClassName("login-button"));
            Assert.True(loginButton.Displayed);
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
