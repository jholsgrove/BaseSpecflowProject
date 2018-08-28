Feature: Test
	In order to make sure this framework is working
	As a dev
	I want to go to Google and check the title of the window

@mytag
Scenario: GoToGoogle
	Given I have navigated to 'http://www.Google.com'
	Then the window title is 'Google'