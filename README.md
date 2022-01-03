# Hahn-Application

**Prerequisite:**
1. Download the source code and unzip it in root folder (eg: d:). due to long path, it might give error.

**Steps to setup Backend:**
1. Install Visual Studio 2019
2. Install .NET 5

**Steps to setup Frontend:**
1. Install latest Node.js: https://nodejs.org/en/
2. Install latest typescript : npm install -g typescript
3. Navigate to project folder
3. Install aurelia-cli globally: npm install aurelia-cli -g
4. Restore the dependent packages: npm install
5. Command to Build: au build --env dev 
6. Command to Run: au run --env run --watch 
7. Ensure the API's post number in Aurelia project. We can find the custom config file at location ./resources/config. This config file contains API path and locale value.

**Concepts used in API:**
1. Used In Memory database
2. Used .NET 5 API project
3. Used FluentValidator to validate the model
4. Swagger to describe the API
5. Seri log to log the information
6. The Repository and Unit of Work Patterns - The repository and unit of work patterns are intended to create an abstraction layer between the data access layer and the business logic layer of an application. Implementing these patterns can help insulate your application from changes in the data store and can facilitate automated unit testing or test-driven development (TDD).
7. NUnit to test the APIs end point with different input values
8. Used InMemory Cache to cache the output values

**Concepts used in UI:**
1. Aurelia UI framework
2. Used I18N for localization
3. Bootstrap for UI design
4. Typescript for backend programming language
5. Webpack to bundle the executables
6. aurelia-validation & Bootstrap FormRenderer to validate the from entries
7. aurelia-dialog to show the messages



**Note:** To run the WEB Api service in docker compose `docker-compose up`
