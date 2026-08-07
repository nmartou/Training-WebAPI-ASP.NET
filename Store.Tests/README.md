# Tests folder

## Commands

### Installation

Creation of the folder which is a new project next to the target one.

```shell
dotnet new xunit -n <projectName>.Tests
```

Add reference to the targeted project

```shell
dotnet add <projectName>.Tests reference <projectName>/<projectName>.csproj
```

Add the solution to the solution file (which is usefull to have one file that manage all projects at the time)

```shell
dotnet new sln # If no solution file exist
dotnet sln add Store.Tests/Store.Tests.csproj
```

### Test commands

Run all tests commands together

```shell
dotnet test Store.Tests
```

Run all tests with a hot reload

```shell
dotnet watch test --project Store.Tests
```

Run with a filter to specify a class or a method

```shell
dotnet test --filter "FullyQualifiedName~<class>"
dotnet test --filter "DisplayName~<method>"
```

Run test with a category type

```shell
dotnet test --filter "Category=Unit"
```

Run tests with detailed logs

```shell
dotnet test --logger "console;verbosity=detailed"
```

Run this command to know the code coverage

```shell
dotnet test --collect:"XPlat Code Coverage"
```
