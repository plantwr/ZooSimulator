# Instructions

## Running the simulation server

1) Ensure .NET Core 2.2 SDK is installed (https://dotnet.microsoft.com/download)
    - Note if using Visual Studio 2017, ensure the "Compatible with Visual 2017" version is installed
2) Open, build and run the `ZooSimulator.sln` in Visual Studio 2017 or higher

## Building the UI independently

1) Ensure npm and node are installed (tested with Node 10.16.3)
2) Install angular-cli (npm install -g @angular/cli)
3) In a terminal window, update to the "UI" directory
4) Run `npm install` to fetch the javascript dependencies
5) Update `environment.ts` to point at server
6) Run `ng serve` to deploy a local server instance
7) Open the browser at http://localhost:4200/
