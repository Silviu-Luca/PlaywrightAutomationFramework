Feature: CompleteBookingFlow
        Complete booking flows of a product

@regression
Scenario: BookAnItem
	Given I go to SauceDemo login page
	When I login with valid credentials
    And I add a Sauce Labs Bike Light to cart
    And I open cart 
    And I go to check out
    And I fill the billing details
    And I click on finish
	Then I am redirect to thank you page