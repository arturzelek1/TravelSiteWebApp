using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace UiTests
{
    public class HomePageTests : IDisposable
    {
        private readonly IWebDriver driver;

        public HomePageTests()
        {
            driver = new FirefoxDriver(@"E:\GitHub\TravelSiteWebApp\UnitTest\webDriver");
        }

        [Fact]
        public void PageTitle_ShouldBeHomePage()
        {
            driver.Navigate().GoToUrl("https://localhost:7132/"); 
            Assert.Equal("Home Page - TravelSiteManagement", driver.Title);
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
