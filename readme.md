# Car Advert Assignment

Used Libraries and Technologies

  - Asp.NET Core WebAPI 2.2.0
  - [Microsoft.EntityFrameworkCore.SqlServer] 2.2.1 for localdb
  - Microsoft.EntityFrameworkCore.InMemory 2.2.1 for testing purposes
  - Microsoft.Extensions for logging and dependency injection
  - FluentValidation.AspNetCore
  - Swagger UI 
  - xUnit
  - Onion Architecture
  - Unit Tests, Integration Tests, Functional Tests

# To Run

  -  Requires LocalDB which can be installed with SQL Server Express 2016
     https://www.microsoft.com/en-us/download/details.aspx?id=54284

  - Set CarAdverts.WebAPI project as startup project and run
  - Go to  https://localhost:5001/swagger to see WebAPI Endpoints in a pretty way
  - ```sh
        https://localhost:5001/swagger
    ```
  - You can also make webservice calls to the endpoint:
  - ```sh
        https://localhost:5001/api/advert/
    ```


License
----

MIT

