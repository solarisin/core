# Solarisin.Core
A collection of general .net core classes and functions used across all of my projects

## Updating the package repository

- The GITHUB_TOKEN environment variable is required to update the package repository.
- Remember to update the version

```
dotnet nuget push "./src/Solarisin.Core/bin/Release/Solarisin.Core.6.0.0-dev.1.nupkg" --api-key %GITHUB_TOKEN% --source "github"
```
