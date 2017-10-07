# .NET Coding School

## Qualco Project (Team 2)

Project specifications:

### City Residents Collection System

The purpose of the project is to provide the residents of a Municipality a Portal from within they will be able to overview, make payments towards their city bills and request Settlements for their outstanding amounts.
In more detail, each resident of the Municipality will be provided with an initial username and a temporary password. Using these credentials, they will be able to login into the Portal and manage their account. After the first login, they will be asked to update the password to a permanent one.

### Front End Specification

The Portal will contain the following screens:

1. User Management. View the Name, Address and telephone info. Also, change password.
2. Manage Bills. View the unpaid bills, their due date and amount. Also, button link to pay each bill.
3. Pay Bill. Select one of the unpaid bills, enter an amount and credit card info and submit the payment (dummy mechanism).
4. Request Settlement. Suggest a down payment amount and number of installments. The mechanism calculates the exact amounts and, after verification, submits the Request for authorization.

### Back Office Operations

The portal works each day with a snapshot of the City’s main system. Specifically:

<ol>
  <li>Every day at 04:00, the City’s Main System creates and saves onto the Hard Drive a text file containing (in a specified format) a list of customers and their unpaid bills. The Portal fetches that file and processes it as follows:
    <ol type="a">
      <li>A list of unique citizens is extracted. If a citizen does not exist in the portal, a new user is created with username his Tax Number (AFM) and a dummy password. An email is sent to the customer, inviting him to login into the portal. If the citizen exists, no operation is performed.</li>
      <li>The list of bills is deleted and the new list is inserted.</li>
    </ol>
  </li>
  <li>Every night at midnight, two text files are extracted and saved onto the Hard Drive.
    <ol type="a">
      <li>List of payments performed.</li>
      <li>List of Settlement requests.</li>
    </ol>
  </li>
