using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Reflection;
using TechTalk.SpecFlow;
using SauceDemoSamuel.Support;
using BoDi;

namespace SauceDemoSamuel.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        // Private readonly field to hold the object container instance
        private readonly IObjectContainer _objectContainer;

        // Private field to hold the WebDriver instance
        private IWebDriver _driver;

        // Constructor that initializes the Hooks class with an object container
        public Hooks(IObjectContainer objectContainer)
        {
            // Assign the provided object container to the class field
            _objectContainer = objectContainer;
        }

        // Method to be executed before each scenario
        [BeforeScenario]
        public void BeforeScenario()
        {
            // Initialize the ChromeDriver instance
            _driver = new ChromeDriver();

            // Maximize the browser window to ensure elements are visible
            _driver.Manage().Window.Maximize();

            // Register the WebDriver instance in the object container
            // This allows other classes, such as step definitions and page objects, to access the WebDriver instance
            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
        }

        // Method to be executed after each scenario
        [AfterScenario]
        public void AfterScenario()
        {
            // Check if the WebDriver instance is not null before attempting to quit and dispose
            if (_driver != null)
            {
                // Quit the WebDriver instance, closing all browser windows and ending the WebDriver session
                _driver.Quit();

                // Dispose of the WebDriver instance to release resources
                _driver.Dispose();
            }

        }
    }
}