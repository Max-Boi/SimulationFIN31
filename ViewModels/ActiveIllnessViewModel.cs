using System;
using Avalonia;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using SimulationFIN31.Models.Enums;

namespace SimulationFIN31.ViewModels;

/// <summary>
///     ViewModel representing a currently active mental illness for display in the UI.
///     Handles visual representation of severity and fluctuation status.
/// </summary>
public sealed partial class ActiveIllnessViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _name = string.Empty;
    [ObservableProperty] 
    private EpisodeSeverity _severity;
    [ObservableProperty] 
    private double _fluctuation;
    [ObservableProperty]
    private IBrush _backgroundBrush;
    [ObservableProperty] 
    private string _statusMessage = string.Empty;
    [ObservableProperty] 
    private IBrush _statusColor;

    public ActiveIllnessViewModel(string name, EpisodeSeverity severity, double fluctuation)
    {
        Name = name;
        Severity = severity;
        Fluctuation = fluctuation;
        
        UpdateVisuals();
    }

    public void Update(double fluctuation)
    {
        if (Math.Abs(Fluctuation - fluctuation) < 0.001) return;
        
        Fluctuation = fluctuation;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        BackgroundBrush = GetSeverityColor(Severity);

        if (Fluctuation > 0.7)
        {
            StatusMessage = "Symptome stark ausgeprägt";
            // StatusColor is strictly for the status text if needed, 
            // but we'll use a standard dark color for readability on pastel
            StatusColor = Brushes.DarkRed; 
        }
        else if (Fluctuation < 0.3)
        {
            StatusMessage = "Symptome heute mild";
            StatusColor = Brushes.DarkGreen;
        }
        else
        {
            StatusMessage = "Symptome stabil";
            StatusColor = Brushes.DarkSlateGray;
        }
    }

    private static SolidColorBrush GetSeverityColor(EpisodeSeverity severity)
    {
        // Pastel colors for better readability
        var color = severity switch
        {
            EpisodeSeverity.Mild => Color.Parse("#FFF59D"),     // Pastel Yellow (Yellow 200)
            EpisodeSeverity.Moderate => Color.Parse("#FFCC80"), // Pastel Orange (Orange 200)
            EpisodeSeverity.Severe => Color.Parse("#EF9A9A"),   // Pastel Red (Red 200)
            _ => Colors.LightGray
        };

        return new SolidColorBrush(color);
    }
}
