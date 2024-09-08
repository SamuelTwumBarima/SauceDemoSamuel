using OpenQA.Selenium;
using System.Globalization;

namespace SauceDemoSamuel.Pages
{
    public class ProductPagePom : BasePage
    {
        public ProductPagePom(IWebDriver driver) : base(driver) { }

        #region Locator
        // Property to locate and return a collection of price elements on the page
        public IReadOnlyCollection<IWebElement> PriceElements => _driver.FindElements(By.ClassName("inventory_item_price"));

        // Property to locate and return the username input field
        public IWebElement UsernameInput => _driver.FindElement(By.Id("user-name"));
        #endregion

        // Method to get the max price value
        public double GetHighestPrice()
        {
            // Lists to store prices and corresponding elements
            List<double> prices = new List<double>();

            // Iterate over the price elements to find the highest price
            foreach (var priceElement in PriceElements)
            {
                // Extract price text and clean it
                string priceText = priceElement.Text.Replace("$", "").Trim();
                if (double.TryParse(priceText, NumberStyles.Currency, CultureInfo.InvariantCulture, out double price))
                {
                    prices.Add(price);
                }
            }

            // Return the highest price found
            return prices.Max();
        }

        // Method to add the item with the max price to the cart
        public void AddMaxPricedItemToCart()
        {
            // Get the maximum price value
            double maxPrice = GetHighestPrice();

            // Iterate over the price elements to find the element that corresponds to the max price
            foreach (var priceElement in PriceElements)
            {
                string priceText = priceElement.Text.Replace("$", "").Trim();
                if (double.TryParse(priceText, NumberStyles.Currency, CultureInfo.InvariantCulture, out double price))
                {
                    if (price == maxPrice)
                    {
                        //  The "Add to Cart" button is in the same container as the price element
                        IWebElement addToCartButton = priceElement.FindElement(By.XPath("./following-sibling::button[contains(text(), 'Add to cart')]"));

                        // Click the "Add to Cart" button
                        addToCartButton.Click();
                        break; // Break after finding and clicking the button for the highest price
                    }
                }
            }
        }
    }
}