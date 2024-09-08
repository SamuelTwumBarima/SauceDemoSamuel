Feature: AddToCart

Adding to cart

Scenario: Add Item to cart
	Given A user is on the login Page
	When A 'standard' user logs in
	And Adds the highest price item to the cart
	Then The item should be added to the cart
