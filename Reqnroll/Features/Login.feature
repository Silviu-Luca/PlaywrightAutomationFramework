Feature: Tests for login page
		Basic tests for login page

@smoke
Scenario: VerifyLogin
	Given I go to SauceDemo login page
	When I fill the user 
	And I fill the pass
	And I click the login button
	Then I am redirected to inventory page
