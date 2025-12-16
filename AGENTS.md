# AGENTS.md

## 1. Project Context & Tech Stack
* **Framework:** Avalonia UI (Latest Stable Version).
* **Language:** C# 9.0+ (Use modern features: Records, Pattern Matching, Target-typed `new`).
* **Architecture:** MVVM (Model-View-ViewModel).
* **MVVM Framework:** CommunityToolkit.Mvvm (Source Generators).
* **Styling:** Avalonia Styles (No raw CSS, use `.axaml` Styles/ControlThemes).

## 2. Strict Guardrails (DO NOT IGNORE)
> **WARNING:** Violating these rules will result in the rejection of the generation.

1.  **Scope Restriction:** You may **ONLY** edit or create files explicitly mentioned in the prompt or directly related to the requested change. Editing unrelated configuration files (e.g., `.csproj`, `Program.cs`, `App.axaml`) is forbidden unless explicitly requested.
2.  **No Dependency Changes:** It is **STRICTLY FORBIDDEN** to install new NuGet packages, run `dotnet add package`, or modify the project file (`.csproj`) to add references. Use only existing libraries.
3.  **No Business Logic in Code-Behind:** `.axaml.cs` files must contain only UI-specific logic that cannot be handled in the ViewModel.

## 3. Project Structure & Roles

### Frontend (Views & Styles)
* **File Types:** `*.axaml`, `*.axaml.cs`
* **ROLE:** AVALONIA EXPERT: Expert in UX Design and Desktop Frontend
* **Responsibility:** Layout definition, Data-Binding, and visual design.
* **Path:** `/Views`
* **Best Practices:**
  * Use `CompiledBinding` (`x:DataType`) for type safety and performance.
  * Use `Grid` and `StackPanel` for layouts.
  * Avoid `x:Name` unless absolutely necessary for code-behind references.
  * Avoid the code-behind

### Backend (ViewModels & Models)
* **File Types:** `*.cs`
* * **ROLE:** DOTNET EXPERT: Expert in C# OOP and Architecture, you know when and how to use patterns and how to write maintainable code
* **Responsibility:** Application logic, state management, data processing.
* **Path:** `/ViewModels`, `/Models`, `/Services`
* **Best Practices:**
  * ViewModels must inherit from `ViewModelBase` (ObservableObject).
  * Use `ObservableCollection<T>` for lists bound to the UI.
  * Asynchronous operations must return `Task` (not `void`) and support `CancellationToken` where appropriate.

## 4. Coding Conventions & Patterns
* **CommunityToolkit Usage:**
  * MUST use source generators: `[ObservableProperty]` for fields, `[RelayCommand]` for methods.
  * Do NOT write manual `INotifyPropertyChanged` boilerplate.
* **Naming:**
  * Views: `MainWindow`, `HomeView`,`SettingView`, `SimulationView` 
  * ViewModels: `MainWindowViewModel`, `HomeViewModel`, `SettingViewModel`, `SimulationViewModel`
* **Nullability:** Nullable Reference Types are enabled. Avoid `null` where possible.

## 5. Reasoning & Critical Thinking (REQUIRED)
Before generating code, perform the following steps:
1.  **Recall Best Practices:** Check your internal knowledge for the latest Avalonia and C# best practices relevant to the request.
2.  **Critical Evaluation:** Question your first approach. Is there a more performant, cleaner, or more "Avalonia-native" way to solve this? (e.g., using a Converter vs. ViewModel logic).
3.  **Justification:** After the code block, provide a brief "Why" section explaining why you chose this specific implementation and how it aligns with best practices.
4. **Role:** Always name your current Role