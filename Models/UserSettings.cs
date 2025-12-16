using System.Text.Json.Serialization;

namespace SimulationFIN31.Models;

public record class UserSettings
{
    [JsonPropertyName("incomeLevel")]
    public int IncomeLevel { get; init; }

    [JsonPropertyName("parentsEducationLevel")]
    public int ParentsEducationLevel { get; init; }

    [JsonPropertyName("culturalPracticeLevel")]
    public int CulturalPracticeLevel { get; init; }

    [JsonPropertyName("socialEnvironmentLevel")]
    public int SocialEnvironmentLevel { get; init; }

    [JsonPropertyName("hasAdhd")]
    public bool HasAdhd { get; init; }

    [JsonPropertyName("hasAutism")]
    public bool HasAutism { get; init; }

    [JsonPropertyName("parentsWithAddiction")]
    public bool ParentsWithAddiction { get; init; }

    [JsonPropertyName("intelligenceScore")]
    public int IntelligenceScore { get; init; }

    [JsonPropertyName("anxietyLevel")]
    public int AnxietyLevel { get; init; }

    [JsonPropertyName("parentsRelationshipQuality")]
    public required string ParentsRelationshipQuality { get; init; }

    [JsonPropertyName("familyCloseness")]
    public int FamilyCloseness { get; init; }

    [JsonPropertyName("socialEnergyLevel")]
    public required string SocialEnergyLevel { get; init; }
}
