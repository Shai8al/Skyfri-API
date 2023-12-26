# Skyfri-API

## Description
This project is a .NET 6 API built with Entity Framework using a Code First approach. It provides endpoints for managing tenants,portfolio and plant.

## Prerequisites
 - [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
 - [MS SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)


## Installation
 1. Clone the repository: 
    ```bash
     git clone https://github.com/Shai8al/Skyfri-API.git
    ```
 2. Navigate to the project repository:
    ```bash
     cd Skyfri
    ```
 3. Restore dependencies:
    ```bash
     dotnet restore
    ```
 4. Update AppConfig:
    -   Open the file /Skyfri/appsettings.json
    -   Update the config setting "SkyfriConnection" with the connection string to the database that will be used.
    
 5. Setup database migration and update
    - Navigate to data_access directory
      ```bash
         cd .\data_access
      ```
    - Add migrations:
        ```bash
            #Open Package Manager Console in Visual Studio and run the following code
            add-migration initialmigration
        ```
    - Update database:
        ```bash
            #Open Package Manager Console in Visual Studio and run the following code
            update-database
        ```

## Usage
The API has the following endpoints based on the entities
   ### Tenants
   - Get all tenants listed
    This endpoint fetches all the endpoints listed in the application.
   - Add a tenant
    This endpoint is to add a tenant
   - Get a specific tenant data
    This endpoint is to fetch a specific tenant based on the tenant id.
   - Edit an existing tenant data
    This endpoint is to update a specific tenant's data.
   - Delete a tenant
    This endpoint is to delete a specific tenant's data.
   
   ### Portfolio
   - Get all the portfolios for a tenant
    This endpoint is to get all the portfolios for a tenant.
   - Add a new portfolio for a tenant
    This endpoint is to add a new portfolio for a tenant.
   - Delete a portfolio for a tenant
    This endpoint is to delete a portfolio for a tenant.

   ### Plant
   - Get all the plants for a portfolio
    This endpoint is to get all the plants for a portfolio.
   - Add a new plant for a portfolio
    This endpoint is to add a new plant for a portfolio.
   - Delete a plant for a portfolio
    This endpoint is to delete a plant for a portfolio.


