Feature: Reports
	As a user I should be able to get results from the report

@loginViaApi
Scenario: Run report
	When navigate to 'ReportsAndSettings' -> 'Reports'
		And find 'Project Profitability' report
	Then some results are returned after running the report