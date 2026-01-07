namespace SimulationFIN31.Models;

public record SimulationSettings
{
    public int IncomeLevel { get; init; }
    public int ParentsEducationLevel { get; init; }
    public int JobStatus { get; init; }
    public int SocialEnvironmentLevel { get; init; }
    public bool HasAdhd { get; init; }
    public bool HasAutism { get; init; }
    public bool ParentsWithAddiction { get; init; }
    public int IntelligenceScore { get; init; }
    public int AnxietyLevel { get; init; }
    public string ParentsRelationshipQuality { get; init; } = string.Empty;
    public int FamilyCloseness { get; init; }
    public string SocialEnergyLevel { get; init; } = string.Empty;
    public string Gender { get; init; } = string.Empty;
    
    /// <summary>
    /// The maximum age until which the simulation runs (30-80).
    /// </summary>
    public int MaxAge { get; init; } = 30;
    
    /// <summary>
    /// Whether to use two events per age cycle instead of one.
    /// </summary>
    public bool UseDoubleEvents { get; init; } = true;
}