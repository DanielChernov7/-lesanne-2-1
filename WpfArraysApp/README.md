# WpfArraysApp

Car inventory manager built with WPF (.NET 8) using MVVM. The app displays a list of cars, supports add/edit/delete via dialogs, and includes sorting plus search/filter options.

## Requirements
- Windows 10/11
- .NET 8 SDK
- Visual Studio 2022 (optional)

## Run
Option 1: Visual Studio
1. Open `WpfArraysApp.sln`.
2. Build and run the `WpfArraysApp` project.

Option 2: CLI
```
dotnet build

dotnet run --project .\WpfArraysApp\WpfArraysApp.csproj
```

## Features
- DataGrid list with sortable columns
- Add and edit dialogs with validation
- Delete confirmation
- Search field selection and status filter
- Clear filters

## Known Bugs
See `docs/KNOWN_BUGS.md`.

## Screenshots
Add screenshots to `docs/screenshots` (referenced by `docs/KNOWN_BUGS.md` and the issue template).
