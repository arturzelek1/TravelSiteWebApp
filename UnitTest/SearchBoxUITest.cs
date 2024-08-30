using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace UiTests
{
    public class FormTests : IDisposable
    {
        private readonly IWebDriver driver;

        public FormTests()
        {
            driver = new FirefoxDriver(@"E:\GitHub\TravelSiteWebApp\UnitTest\webDriver");
        }

        [Fact]
        public void FormElements_ShouldBePresent()
        {
            driver.Navigate().GoToUrl("https://localhost:7132");

            var whereField = driver.FindElement(By.Id("where"));
            var fromField = driver.FindElement(By.Id("from"));
            var peopleField = driver.FindElement(By.Id("people"));
            var searchButton = driver.FindElement(By.XPath("//input[@value='Szukaj']"));

            Assert.True(whereField.Displayed, "The 'where' input field should be visible.");
            Assert.True(fromField.Displayed, "The 'from' input field should be visible.");
            Assert.True(peopleField.Displayed, "The 'people' input field should be visible.");
            Assert.True(searchButton.Displayed, "The 'search' button should be visible.");
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
