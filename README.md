# .NET Coding School [![Build Status](https://travis-ci.org/akritikos/akritikos.github.io.svg?branch=master)](https://travis-ci.com/akritikos/qualco)

## Qualco Project (Team #2)

Guidelines for pull requests:

* Use StyleCop for consistent code styling
* Provide full documentation on classes & methods
* Your PR should be able to be build cross-platform (Current CI implementation is Travis CI, on Ubuntu Trusty using dotnet 2.0.0)
* Upon adding a new project, you should [add as link](https://tinyurl.com/yc5rbzhl) the file stylecop.json from the root of this repository and [enable it](https://tinyurl.com/yczleafl). Make sure to close and reload your project afterwards.

Project specifications:

### City Residents Collection System

The purpose of the project is to provide the residents of a Municipality a Portal from within they will be able to overview, make payments towards their city bills and request Settlements for their outstanding amounts.
In more detail, each resident of the Municipality will be provided with an initial username and a temporary password. Using these credentials, they will be able to login into the Portal and manage their account. After the first login, they will be asked to update the password to a permanent one.

### Front End Specification

The Portal will contain the following screens:

1. User Management. View the Name, Address and telephone info. Also, change password.
1. Manage Bills. View the unpaid bills, their due date and amount. Also, button link to pay each bill.
1. Pay Bill. Select one of the unpaid bills, enter an amount and credit card info and submit the payment (dummy mechanism).
1. Request Settlement. Suggest a down payment amount and number of installments. The mechanism calculates the exact amounts and, after verification, submits the Request for authorization.

### Back Office Operations

The portal works each day with a snapshot of the City's main system. Specifically:

1. Every day at 04:00, the City's Main System creates and saves onto the Hard Drive a text file containing (in a specified format) a list of customers and their unpaid bills. The Portal fetches that file and processes it as follows:
    * A list of unique citizens is extracted. If a citizen does not exist in the portal, a new user is created with username his Tax Number (AFM) and a dummy password. An email is sent to the customer, inviting him to login into the portal. If the citizen exists, no operation is performed.
    * The list of bills is deleted and the new list is inserted.
1. Every night at midnight, two text files are extracted and saved onto the Hard Drive.
    * List of payments performed.
    * List of Settlement requests.
