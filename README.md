# Point Tracker Coding Challenge

This project is a simple collection of HTTP endpoints that allow transactions to be added to a user's account, and calculates user point spend. A Swagger UI is used for the frontend, which contains descriptions of the endpoints as well. The transaction service, which maintains the list of transactions, is created as a singleton each time the application is started, and transactions are not persisted between runs. This application is not thread-safe.

## Dependencies

.NET SDK 5.0.x (can be downloaded from here: https://dotnet.microsoft.com/download/dotnet/5.0)

## Running the application

In a terminal, ensure dotnet is installed with the following command: `dotnet --version`. 

If so, navigate to the points-api/src/PointTracker.Web folder and run `dotnet run` to start the backend.

Navigate to `http://localhost:<port>/swagger` to interact with the endpoints. Under each request in Swagger, you can click the "Try it out" button to interact with the API.