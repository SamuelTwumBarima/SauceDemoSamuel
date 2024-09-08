using OpenQA.Selenium;
using System.Globalization;

namespace SauceDemoSamuel.Pages
{
    public class ShoppingCartPagePom : BasePage
    {
        public ShoppingCartPagePom(IWebDriver driver) : base(driver) { }

        #region Locators
        // Property to get the shopping cart icon element
        public IWebElement CartIcon => _driver.FindElement(By.Id("shopping_cart_container"));
        // Property to get all items in the cart
        public IReadOnlyCollection<IWebElement> CartItems => _driver.FindElements(By.ClassName("cart_item"));

        // Property to get prices of the items in the cart
        public IReadOnlyCollection<IWebElement> CartPrices => _driver.FindElements(By.ClassName("inventory_item_price"));
        #endregion

        public double GetHighestPriceInCart()
        {
            // Click on the cart icon to view the cart contents
            CartIcon.Click();

            // Dictionary to store item prices
            List<double> itemPrices = new List<double>();

            foreach (var item in CartItems)
            {
                // Extract price text and clean it
                var priceText = item.FindElement(By.ClassName("inventory_item_price")).Text.Replace("$", "").Trim();
                if (double.TryParse(priceText, NumberStyles.Currency, CultureInfo.InvariantCulture, out double price))
                {
                    itemPrices.Add(price);
                }
            }

            // Return the highest price found, or 0 if no items are found
            return itemPrices.Count > 0 ? itemPrices.Max() : 0;
        }
    }


}


