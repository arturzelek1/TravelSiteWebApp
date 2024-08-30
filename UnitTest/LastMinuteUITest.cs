using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace UiTests
{
    public class LastMinuteOffersTests : IDisposable
    {
        private readonly IWebDriver driver;

        public LastMinuteOffersTests()
        {
            driver = new FirefoxDriver(@"E:\GitHub\TravelSiteWebApp\UnitTest\webDriver");
        }

        [Fact]
        public void LastMinuteOffers_SectionShouldBeVisible()
        {
            driver.Navigate().GoToUrl("https://localhost:7132/");

            var lastMinuteSection = driver.FindElement(By.ClassName("last-minute-offers"));
            Assert.True(lastMinuteSection.Displayed);
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
