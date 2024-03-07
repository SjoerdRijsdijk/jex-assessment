## Requirements

- .NET Core 8 SDK (latest version)
- Node JS (20.11.1 LTS was used during development)

## Running the application

To run this application, you need to first start the backend API. Without this the frontend app won't function correctly.
You can run the backend API by opening the solution file in Visual Studio and pressing either (Ctrl + F5 or F5).
The database with the necessary migrations are automatically created/executed. The Sqlite database is placed in the `CurrentDirectory`.

To get the frontend up and running, you need to first navigate to the `frontend` directory using the terminal and then run the command `npm ci` to install all necessary dependencies. After that is done, you can start the application by running the command `npm run start` in the same location.

## Just so you know

This was my first time working with angular, so don't expect top quality code when looking at the frontend part. I've tried my best to set it up as good as possible with my current knowledge.
