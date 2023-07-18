# CRM.Automation

The solution consists of 2 Projects - CRM.Automation.Framework and CRM.Automation.Tests.

CRM.Automation.Framework contains handlers for Browser, wrappers for different Element typs, Utils.

CRM.Automation.Tests is where all features, steps and hooks live.

## Run scenario/s
In order to run test/s build the solution:
```
dotnet build
```

To run all test:
```
dotnet test
```

To run tests by tag:
```
dotnet test --filter Category={TagName}
```

To run test by scenario name:
```
dotnet test --filter DisplayName="{ScenarioName}"
```

Scenarios marked with @loginViaApi perform login via api.

## Report
Allure is used as a report. 
By Default you can find report files at `CRM.Automation.Tests/bin/{configuration}/net6.0/allure-results`. 
In order to generate a report one should install Allure cli. Detailed instructions depending on your OS can be found [here](https://docs.qameta.io/allure/#_installing_a_commandline)

To generate report run:
```
allure generate <allure-results-directory> --clean
```