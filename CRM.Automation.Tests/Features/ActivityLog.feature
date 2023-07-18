Feature: Activity Log
	As a user I should be able to remove activity logs

@loginViaApi
Scenario: Remove events from activity log
	When navigate to 'ReportsAndSettings' -> 'ActivityLog'
		And select first 3 items in Activity log table
		And delete selected items from Activity log table
	Then selected items are deleted from Activity log table