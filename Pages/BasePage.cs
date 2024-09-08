using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemoSamuel.Pages
{
    public class BasePage
    {
        // Property to hold the WebDriver instance
        public IWebDriver _driver { get; set; }

        // Constructor to initialize the WebDriver instance
        public BasePage(IWebDriver driver)
        {
            // Assign the provided WebDriver instance to the class property
            this._driver = driver;
        }

        // Method to refresh the current page
        public void Refresh() => _driver.Navigate().Refresh();

        // Method to navigate to a specified URL
        public void OpenURl(string url) => _driver.Navigate().GoToUrl(url);
    }
}