using System;
using Avalonia.Controls;
using Avalonia.Input;
using ScottPlot;
using ScottPlot.Plottables;
using SimulationFIN31.ViewModels;

namespace SimulationFIN31.Views;


public partial class EvaluationView : UserControl
{
    private Crosshair? _crosshair;
    private Text? _tooltipText;
    private EvaluationViewModel? _viewModel;

    public EvaluationView()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }
    
    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (DataContext is EvaluationViewModel vm)
        {
            _viewModel = vm;
            ConfigureChart(vm);
            SetupInteractivity();
        }
    }

    /// <summary>
    ///     Sets up mouse interaction for showing data point values.
    /// </summary>
    private void SetupInteractivity()
    {
        HealthChart.PointerMoved += OnPointerMoved;
        HealthChart.PointerExited += OnPointerExited;
    }

    /// <summary>
    ///     Handles mouse movement to update crosshair position and show values.
    /// </summary>
    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (_crosshair == null || _tooltipText == null || _viewModel == null || _viewModel.Ages.Length == 0)
            return;

        var position = e.GetPosition(HealthChart);
        var pixel = new Pixel((float)position.X, (float)position.Y);
        var coordinates = HealthChart.Plot.GetCoordinates(pixel);

        // Find the closest age index
        var age = (int)Math.Round(coordinates.X);
        var index = Array.FindIndex(_viewModel.Ages, a => (int)a == age);

        if (index >= 0 && index < _viewModel.Ages.Length)
        {
            // Show crosshair vertical line (without label)
            _crosshair.IsVisible = true;
            _crosshair.Position = new Coordinates(_viewModel.Ages[index], coordinates.Y);

            // Get values at this age
            var stress = _viewModel.StressValues[index];
            var mood = _viewModel.MoodValues[index];
            var social = _viewModel.SocialValues[index];
            var resilience = _viewModel.ResilienceValues[index];

            // Build tooltip text
            var tooltipContent =
                $"Alter: {age}\n" +
                $"Stress: {stress:F1}\n" +
                $"Stimmung: {mood:F1}\n" +
                $"Sozial: {social:F1}\n" +
                $"Resilienz: {resilience:F1}";

            _tooltipText.LabelText = tooltipContent;
            _tooltipText.IsVisible = true;

            // Get axis limits for boundary detection
            var limits = HealthChart.Plot.Axes.GetLimits();
            var xMin = limits.Left;
            var xMax = limits.Right;
            var yMin = limits.Bottom;
            var yMax = limits.Top;
            
            var xRange = xMax - xMin;
            var yRange = yMax - yMin;
            
            var xOffset = coordinates.X > xMin + xRange * 0.6 ? -3.5 : 1.5;
            
            var yOffset = coordinates.Y > yMin + yRange * 0.5 ? 10 : -25;
            
            _tooltipText.Location = new Coordinates(
                _viewModel.Ages[index] + xOffset,
                coordinates.Y + yOffset
            );
            
            if (xOffset < 0)
                _tooltipText.LabelAlignment = Alignment.UpperRight;
            else
                _tooltipText.LabelAlignment = Alignment.UpperLeft;
        }
        else
        {
            _crosshair.IsVisible = false;
            _tooltipText.IsVisible = false;
        }

        HealthChart.Refresh();
    }
    
    private void OnPointerExited(object? sender, PointerEventArgs e)
    {
        if (_crosshair != null) _crosshair.IsVisible = false;

        if (_tooltipText != null) _tooltipText.IsVisible = false;

        HealthChart.Refresh();
    }

  
    private void ConfigureChart(EvaluationViewModel vm)
    {
        var plot = HealthChart.Plot;
        plot.Clear();
        
        if (vm.Ages.Length > 0)
        {
         
            var stressPlot = plot.Add.Scatter(vm.Ages, vm.StressValues);
            stressPlot.LegendText = "Stress";
            stressPlot.Color = new Color(231, 76, 60); // #E74C3C
            stressPlot.LineWidth = 2.5f;
            stressPlot.MarkerSize = 6;
            stressPlot.MarkerShape = MarkerShape.FilledCircle;

            // Add Mood line (Blue)
            var moodPlot = plot.Add.Scatter(vm.Ages, vm.MoodValues);
            moodPlot.LegendText = "Stimmung";
            moodPlot.Color = new Color(52, 152, 219); // #3498DB
            moodPlot.LineWidth = 2.5f;
            moodPlot.MarkerSize = 6;
            moodPlot.MarkerShape = MarkerShape.FilledCircle;

            // Add Social Belonging line (Purple)
            var socialPlot = plot.Add.Scatter(vm.Ages, vm.SocialValues);
            socialPlot.LegendText = "Soziale Zugehoerigkeit";
            socialPlot.Color = new Color(155, 89, 182); // #9B59B6
            socialPlot.LineWidth = 2.5f;
            socialPlot.MarkerSize = 6;
            socialPlot.MarkerShape = MarkerShape.FilledCircle;

            // Add Resilience line (Green)
            var resiliencePlot = plot.Add.Scatter(vm.Ages, vm.ResilienceValues);
            resiliencePlot.LegendText = "Resilienz";
            resiliencePlot.Color = new Color(46, 204, 113); // #2ECC71
            resiliencePlot.LineWidth = 1.5f;
            resiliencePlot.MarkerSize = 3;
            resiliencePlot.MarkerShape = MarkerShape.FilledCircle;

            // Add crosshair for hover interaction (vertical line only, no label)
            _crosshair = plot.Add.Crosshair(0, 0);
            _crosshair.IsVisible = false;
            _crosshair.LineColor = Colors.White.WithAlpha(0.7);
            _crosshair.LineWidth = 1;
            _crosshair.HorizontalLine.IsVisible = false; // Only show vertical line
            _crosshair.VerticalLine.IsVisible = true;

            // Add custom tooltip text annotation for better positioning control
            _tooltipText = plot.Add.Text("", 0, 0);
            _tooltipText.IsVisible = false;
            _tooltipText.LabelFontSize = 12;
            _tooltipText.LabelFontColor = Colors.White;
            _tooltipText.LabelBackgroundColor = new Color(50, 50, 50, 220); // Semi-transparent dark background
            _tooltipText.LabelBorderColor = new Color(80, 80, 80);
            _tooltipText.LabelBorderWidth = 1;
            _tooltipText.LabelPadding = 8;
            _tooltipText.LabelAlignment = Alignment.UpperLeft;
        }

        // Configure axes
        plot.Axes.Bottom.Label.Text = "Alter (Jahre)";
        plot.Axes.Left.Label.Text = "Wert";

        // Add margins to give space for tooltip labels
        plot.Axes.Margins(bottom: 0.15, top: 0.15, left: 0.05, right: 0.1);

        // Remove grid
        plot.Grid.IsVisible = false;

        // Dark theme styling to match app design
        plot.FigureBackground.Color = new Color(40, 40, 40); // #282828
        plot.DataBackground.Color = new Color(30, 30, 30); // Slightly darker

        // Axis styling
        var axisColor = new Color(200, 200, 200); // Light gray for visibility
        plot.Axes.Bottom.Label.ForeColor = axisColor;
        plot.Axes.Left.Label.ForeColor = axisColor;
        plot.Axes.Bottom.TickLabelStyle.ForeColor = axisColor;
        plot.Axes.Left.TickLabelStyle.ForeColor = axisColor;
        plot.Axes.Bottom.MajorTickStyle.Color = axisColor;
        plot.Axes.Left.MajorTickStyle.Color = axisColor;
        plot.Axes.Bottom.MinorTickStyle.Color = axisColor;
        plot.Axes.Left.MinorTickStyle.Color = axisColor;
        plot.Axes.Bottom.FrameLineStyle.Color = axisColor;
        plot.Axes.Left.FrameLineStyle.Color = axisColor;

        // Legend styling
        plot.ShowLegend();
        plot.Legend.BackgroundColor = new Color(50, 50, 50);
        plot.Legend.FontColor = Colors.White;
        plot.Legend.OutlineColor = new Color(80, 80, 80);

        // Set axis limits with some padding
        if (vm.Ages.Length > 0)
        {
            plot.Axes.SetLimitsX(0, 32); // Age 0-30 with padding
            plot.Axes.SetLimitsY(-110, 110); // Values with padding
        }

        HealthChart.Refresh();
    }
}