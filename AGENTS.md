# AGENTS.md

## 1. Projekt Kontext & Tech Stack
* **Framework:** Avalonia UI (Neueste Stable Version)
* **Sprache:** C# 9.0+ (Nutzung moderner Features wie Records, Pattern Matching, Target-typed `new`)
* **Architektur:** MVVM (Model-View-ViewModel)
* **Styling:** Avalonia Styles (kein reines CSS, Nutzung von `.axaml` Styles)

## 2. Strikte Guardrails (NICHT IGNORIEREN)
> **WARNUNG:** Verstöße gegen diese Regeln führen zum Abbruch der Generierung.

1.  **Scope-Beschränkung:** Du darfst **NUR** Dateien bearbeiten oder erstellen, die im Prompt explizit erwähnt wurden oder direkt mit der angeforderten Änderung in Verbindung stehen. Das Bearbeiten von unbeteiligten Konfigurationsdateien (z.B. `.csproj`, `Program.cs`, `App.axaml`) ist ohne Aufforderung verboten.
2.  **Dependency-Verbot:** Es ist **STRIKT VERBOTEN**, neue NuGet-Pakete zu installieren, `dotnet add package` auszuführen oder die Projektdatei (`.csproj`) zu ändern, um Referenzen hinzuzufügen. Nutze nur vorhandene Bibliotheken.
3.  **Keine Business-Logik im Code-Behind:** `.axaml.cs` Dateien dürfen nur UI-spezifische Logik enthalten, die nicht im ViewModel abbildbar ist.

## 3. Projektstruktur & Rollen

### Frontend (Views & Styles)
* **Dateitypen:** `*.axaml`, `*.axaml.cs`
* **Verantwortung:** Definition des Layouts, Data-Binding und visuelles Design.
* **Pfad:** `/Views`
* **Best Practices:**
    * Nutze `CompiledBinding` (`x:DataType`) für Typsicherheit und Performance.
    * Verwende `Grid` und `StackPanel` für Layouts.
    * Control-Namen (`x:Name`) nur verwenden, wenn absolut notwendig für Code-Behind Referenzen.

### Backend (ViewModels & Models)
* **Dateitypen:** `*.cs`
* **Verantwortung:** Anwendungslogik, State-Management, Datenverarbeitung.
* **Pfad:** `/ViewModels`, `/Models`, `/Services`
* **Best Practices:**
    * ViewModels erben von `ViewModelBase` (implementiert `INotifyPropertyChanged` / `ObservableObject`).
    * Verwende `ObservableCollection<T>` für Listen, die an die UI gebunden sind.
    * Asynchrone Operationen müssen `Task` (nicht `void`) zurückgeben und CancellationToken unterstützen, wo sinnvoll.

## 4. Coding Conventions & Patterns
* **ReactiveUI / CommunityToolkit:** Wenn im Projekt vorhanden, nutze die entsprechenden Attribute (`[ObservableProperty]`, `[RelayCommand]`) statt manuellem Boilerplate-Code.
* **Naming:**
    * Views: `MainWindow`, `HomeView`
    * ViewModels: `MainWindowViewModel`, `HomeViewModel`
* **Nullability:** Nullable Reference Types sind aktiviert. Vermeide `null` wo möglich.
