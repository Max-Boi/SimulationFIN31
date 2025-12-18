namespace SimulationFIN31.Models.structs;

public readonly struct CopingTrigger
{
    public double? StressThreshold { get; }
    public double? MoodThreshold { get; }
    public double? BelongingThreshold { get; }
        
    public CopingTrigger(
        double? stressThreshold = null,
        double? moodThreshold = null,
        double? belongingThreshold = null)
    {
        StressThreshold = stressThreshold;
        MoodThreshold = moodThreshold;
        BelongingThreshold = belongingThreshold;
    }
        
    public bool HasAnyTrigger() => 
        StressThreshold.HasValue || 
        MoodThreshold.HasValue || 
        BelongingThreshold.HasValue;
}