using OpenQA.Selenium;

namespace SauceDemoSamuel.Pages
{
    public class LoginPagePom : BasePage
    {

        // Constructor that initializes the LoginPagePom class
        public LoginPagePom(IWebDriver driver) : base(driver) {}

        #region Locators
        // Property to locate and return the username input field
        public IWebElement UsernameInput => _driver.FindElement(By.Id("user-name"));

        // Property to locate and return the password input field
        public IWebElement PasswordInput => _driver.FindElement(By.Id("password"));

        // Property to locate and return the login button
        public IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));
        #endregion

        // Method to open the login page URL
        public void OpenUrl(string sauceDemoURL)
        {
            // Navigate to the specified URL
            _driver.Navigate().GoToUrl(sauceDemoURL);
        }

        // Method to perform login action using the provided username and password
        public void Login(string username, string password)
        {
            // Clear any existing text in the username input field
            UsernameInput.Clear();

            // Enter the username into the username input field
            UsernameInput.SendKeys(username.Trim());

            // Clear any existing text in the password input field
            PasswordInput.Clear();

            // Enter the password into the password input field
            PasswordInput.SendKeys(password);

            // Click the login button to submit the login form
            LoginButton.Click();
        }
    }
}
