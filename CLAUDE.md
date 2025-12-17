# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

```bash
# Build the project
dotnet build

# Run the application
dotnet run

# Build release version
dotnet build -c Release
```

## Architecture Overview

This is an Avalonia UI desktop application (.NET 9) simulating life outcomes based on socioeconomic and psychological factors (FIN31 project). It uses MVVM architecture with CommunityToolkit.Mvvm source generators.

### Key Architectural Patterns

**Dependency Injection:** Services and ViewModels are registered in `ServiceCollectionExtensions.cs` and resolved via `Microsoft.Extensions.DependencyInjection`. The service provider is built in `App.axaml.cs`.

**Navigation:** Uses a custom `NavigationService` that holds the current `ViewModelBase`. `MainWindowViewModel` subscribes to `PropertyChanged` on the navigation service to update the displayed view. Navigation is type-safe via `NavigateTo<T>()`.

**ViewLocator:** Convention-based view resolution replaces "ViewModel" with "View" in the type name (e.g., `HomeViewModel` → `HomeView`). Views must follow this naming convention.

### Project Structure

- `/ViewModels` - ViewModels inheriting from `ViewModelBase` (which extends `ObservableObject`)
- `/Views` - Avalonia XAML views (`.axaml`) with minimal code-behind
- `/Models` - Data models including `SimulationSettings` (record with simulation parameters) and `SimulationEvent`
- `/Services` - Services including `NavigationService` with interfaces in `/Services/Interfaces`

### MVVM Conventions

- Use `[ObservableProperty]` attribute for bindable fields (generates public property with change notification)
- Use `[RelayCommand]` attribute for command methods
- ViewModels must inherit from `ViewModelBase`
- Use `ObservableCollection<T>` for UI-bound lists
- Views use `CompiledBinding` (`x:DataType`) for type-safe bindings

### Important Constraints (from AGENTS.md)

- Do NOT add new NuGet packages or modify `.csproj`
- Keep code-behind (`.axaml.cs`) minimal - logic belongs in ViewModels
- Only edit files directly related to the requested change
