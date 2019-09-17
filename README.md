# Instructions

## Running the simulation server

1) Ensure .NET Core 2.2 SDK is installed (https://dotnet.microsoft.com/download)
    - Note if using Visual Studio 2017, ensure the "Compatible with Visual 2017" version is installed
2) Open, build and run the `ZooSimulator.sln` in Visual Studio 2017 or higher
    - Note there is no output in the simulator browser window (possibly 404), this is expected

## Running the UI

1) Ensure npm and node are installed (tested with Node 10.16.3)
2) Install angular-cli (npm install -g @angular/cli)
3) In a terminal window, update to the "UI" directory
4) Run `npm install` to fetch the javascript dependencies
5) Run `ng serve` to deploy a local server instance
6) Open the browser at http://localhost:4200/