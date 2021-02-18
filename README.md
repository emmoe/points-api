# Point Tracker Coding Challenge

This project is a simple collection of HTTP endpoints that allow transactions to be added to a user's account, and calculates user point spend. A Swagger UI is used for the frontend, which contains descriptions of the endpoints as well. The transaction service, which maintains the list of transactions, is created as a singleton each time the application is started, and transactions are not persisted between runs. This application is not thread-safe.

## Dependencies

.NET SDK 5.0.x

## Running the application

In a terminal in the points-api folder, run `dotnet run` to start the backend.

Navigate to `localhost:<port>/swagger` to interact with the endpoints.