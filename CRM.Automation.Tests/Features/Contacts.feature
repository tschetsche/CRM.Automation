Feature: Contacts
	As a user I should be able to create contact

@removeContact
Scenario: Create contact
	When I login to the CRM
		And navigate to 'SalesAndMarketing' -> 'Contacts'
		And create new contact with the following data:
		  | FieldName  | Value               |
		  | FirstName  | Test                |
		  | LastName   | User                |
		  | Role       | Sales               |
		  | Categories | Customers,Suppliers |
	Then created contact data matches entered on the previous step