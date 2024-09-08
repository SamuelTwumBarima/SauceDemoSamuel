using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemoSamuel.Pages;
using SauceDemoSamuel.Support;
using System;
using TechTalk.SpecFlow;

namespace SauceDemoSamuel.StepDefinitions
{
    [Binding]
    public class AddToCartStepDefinitions
    {
        private readonly LoginPagePom _loginPagePom;
        private readonly IWebDriver _driver;
        private readonly ProductPagePom _productPagePom;
        private readonly ShoppingCartPagePom _shoppingCartPagePom;
        private readonly ScenarioContext _scenarioContext;

        public AddToCartStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext)
        {
            // Initialize the web driver for interacting with the browser
            _driver = driver;

            // Create an instance of LoginPagePom to interact with the login page
            _loginPagePom = new LoginPagePom(_driver);

            // Create an instance of ProductPagePom to interact with the product page
            _productPagePom = new ProductPagePom(_driver);

            // Create an instance of ShoppingCartPagePom to interact with the shopping cart page
            _shoppingCartPagePom = new ShoppingCartPagePom(_driver);

            // Store the scenario context to share information between steps in the same scenario
            _scenarioContext = scenarioContext;
        }

        [Given(@"A user is on the login Page")]
        public void GivenAUserIsOnTheLoginPage()
        {
            // Open the login page URL using the URL configured in ConfigurationReader
            _loginPagePom.OpenURl(ConfigurationReader.SauceDemoURL);
        }

        [When(@"A '([^']*)' user logs in")]
        public void WhenAUserLogsIn(string userType)
        {
            // Declare variables to hold the username and password
            string username;
            string password;

            // Choose credentials based on user type
            switch (userType.ToLower())
            {
                case "standard":
                    username = ConfigurationReader.StandardUserUsername;
                    password = ConfigurationReader.StandardUserPassword;
                    break;

                case "locked":
                    username = ConfigurationReader.LockedUserUsername;
                    password = ConfigurationReader.LockedUserPassword;
                    break;

                default:
                    throw new ArgumentException($"Unknown user type: {userType}");
            }

            // Call the Login method on the LoginPagePom instance to perform the login operatio
            _loginPagePom.Login(username, password);
        }

        [When(@"Adds the highest price item to the cart")]
        public void WhenAddsTheHighestPriceItemToTheCart()
        {
            // Call the method to sort items by price and add the highest-priced item to the cart
            _productPagePom.AddMaxPricedItemToCart();

            // Call GetMaxPrice() to get the highest price and store it in the ScenarioContext
            double highestPrice = _productPagePom.GetHighestPrice();

            // Store the highest price in the ScenarioContext
            _scenarioContext.Add("HighestPriceItemAmount", highestPrice);
        }

        [Then(@"The item should be added to the cart")]
        public void ThenTheItemShouldBeAddedToTheCart()
        {
            // Retrieve the highest price of the item that was stored in the scenario context
            double highestPriceItemAmount = _scenarioContext.Get<double>("HighestPriceItemAmount");

            // Get the highest price of items currently in the cart
            double priceOfItemInBasket = _shoppingCartPagePom.GetHighestPriceInCart();

            // Assert that the highest price in the cart matches the highest price of the item that was added
            priceOfItemInBasket.Should().Be(highestPriceItemAmount, "The highest priced item should be added to the cart");

        }
    }
}
