using System;
using SimulationFIN31.Models;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.Services;

public class FactorNormalizer : IFactorNormalizer
{
    private const double MinValue = 0.01;
    private const double MaxValue = 0.99;
        
    public double Normalize(SimulationState profile, string factorName)
    {
        var rawValue = GetRawValue(profile, factorName);
        var normalized = NormalizeByType(rawValue, factorName);
            
        // Clamp to prevent edge cases
        return Math.Clamp(normalized, MinValue, MaxValue);
    }
        
    private double GetRawValue(SimulationState profile, string factorName) => factorName switch
    {
        "SocialEnergyLevel" => profile.SocialEnergyLevel,
        "AnxietyLevel" => profile.AnxietyLevel,
        "FamilyCloseness" => profile.FamilyCloseness,
        "IntelligenceScore" => profile.IntelligenceScore,
        "IncomeLevel" => profile.IncomeLevel,
        "ParentsEducationLevel" => profile.ParentsEducationLevel,
        "JobStatus" => profile.JobStatus,
        "SocialEnvironmentLevel" => profile.SocialEnvironmentLevel,
        "ParentsRelationshipQuality" => profile.ParentsRelationshipQuality,
        "HasAdhd" => profile.HasAdhd ? 1.0 : 0.0,
        "HasAutism" => profile.HasAutism ? 1.0 : 0.0,
        "ParentsWithAddiction" => profile.ParentsWithAddiction ? 1.0 : 0.0,
        "CurrentStress" => profile.CurrentStress,
        "CurrentMood" => profile.CurrentMood,
        "SocialBelonging" => profile.SocialBelonging,
        "ResilienceScore" => profile.ResilienceScore,
        "PhysicalHealth" => profile.PhysicalHealth,
        _ => 0.5 // Default fallback
    };
        
    private double NormalizeByType(double value, string factorName) => factorName switch
    {
        // 0-100 Skalen
        "SocialEnergyLevel" or "AnxietyLevel" or "FamilyCloseness" 
            or "SocialEnvironmentLevel" or "ParentsRelationshipQuality"
            or "CurrentStress" or "SocialBelonging" or "ResilienceScore" 
            or "PhysicalHealth" => value / 100.0,
            
        // IQ (70-145 → 0-1)
        "IntelligenceScore" => (value - 70) / 75.0,
            
        // 1-3 Skalen (Income, Education, Job)
        "IncomeLevel" or "ParentsEducationLevel" or "JobStatus" 
            => (value - 1) / 2.0,
            
        // Mood (-100 bis +100 → 0-1)
        "CurrentMood" => (value + 100) / 200.0,
            
        // Boolean (already 0 or 1)
        _ => value
    };

        
}