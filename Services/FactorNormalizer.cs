using System;
using SimulationFIN31.Models;
using SimulationFIN31.Models.Enums;
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

        return Math.Clamp(normalized, MinValue, MaxValue);
    }

    private double GetRawValue(SimulationState profile, string factorName)
    {
        return factorName switch
        {
            // Enums → Konvertiere zu double
            "IncomeLevel" => MapIncomeLevel(profile.IncomeLevel),
            "ParentsEducationLevel" => MapEducationLevel(profile.ParentsEducationLevel),
            "JobStatus" => MapJobStatus(profile.JobStatus),
            "Gender" => MapGender(profile.Gender),
            "GenderMale" => MapGenderMale(profile.Gender),
            "GenderFemale" => MapGenderFemale(profile.Gender),
            "GenderNonBinary" => MapGenderNonBinary(profile.Gender),
            "SocialEnergyLevel" => MapSocialEnergyLevel(profile.SocialEnergyLevel),
            "ParentsRelationshipQuality" => MapParentsRelationshipQuality(profile.ParentsRelationshipQuality),

            // Integer 0-100 Werte (können bleiben)
            "AnxietyLevel" => profile.AnxietyLevel,
            "FamilyCloseness" => profile.FamilyCloseness,
            "SocialEnvironmentLevel" => profile.SocialEnvironmentLevel,
            "IntelligenceScore" => profile.IntelligenceScore,

            // Boolean
            "HasAdhd" => profile.HasAdhd ? 1.0 : 0.0,
            "HasAutism" => profile.HasAutism ? 1.0 : 0.0,
            "ParentsWithAddiction" => profile.ParentsWithAddiction ? 1.0 : 0.0,

            // Dynamische Zustände
            "CurrentStress" => profile.CurrentStress,
            "CurrentMood" => profile.CurrentMood,
            "SocialBelonging" => profile.SocialBelonging,
            "ResilienceScore" => profile.ResilienceScore,
            "PhysicalHealth" => profile.PhysicalHealth,

            _ => 0.5 // Default fallback
        };
    }

    // ============ ENUM MAPPINGS ============

    /// <summary>
    ///     Income: Low=0, Medium=1, High=2 → 0.0, 0.5, 1.0
    /// </summary>
    private double MapIncomeLevel(IncomeLevel level)
    {
        return level switch
        {
            IncomeLevel.ReallyLow => 0.0,
            IncomeLevel.Low => 0.15,
            IncomeLevel.LowHigh => 0.3,
            IncomeLevel.MediumLow => 0.45,
            IncomeLevel.Medium => 0.6,
            IncomeLevel.MediumHigh => 0.75,
            IncomeLevel.High => 1.0,
            _ => 0.5
        };
    }

    /// <summary>
    ///     Education: Low=0, Medium=1, High=2 → 0.0, 0.5, 1.0
    /// </summary>
    private double MapEducationLevel(ParentsEducationLevel level)
    {
        return level switch
        {
            ParentsEducationLevel.ReallyLow => 0.0,
            ParentsEducationLevel.Low => 0.15,
            ParentsEducationLevel.LowHigh => 0.3,
            ParentsEducationLevel.MediumLow => 0.45,
            ParentsEducationLevel.Medium => 0.6,
            ParentsEducationLevel.MediumHigh => 0.75,
            ParentsEducationLevel.High => 1.0,
            _ => 0.5
        };
    }

    /// <summary>
    ///     JobStatus: LowPrestige=0, MediumPrestige=1, HighPrestige=2 → 0.0, 0.5, 1.0
    /// </summary>
    private double MapJobStatus(JobStatus status)
    {
        return status switch
        {
            JobStatus.LowPrestige1 => 0.0,
            JobStatus.LowPrestige2 => 0.15,
            JobStatus.LowPrestige3 => 0.3,
            JobStatus.MediumPrestige1 => 0.45,
            JobStatus.MediumPrestige2 => 0.6,
            JobStatus.MediumPrestige3 => 0.75,
            JobStatus.HighPrestige => 1.0,
            _ => 0.5
        };
    }

    /// <summary>
    ///     Gender: Male=0, Female=1 → 0.0, 1.0
    ///     ACHTUNG: Nur relevant wenn Event geschlechtsspezifisch ist!
    /// </summary>
    private double MapGender(GenderType gender)
    {
        return gender switch
        {
            GenderType.Male => 0.0,
            GenderType.Female => 1.0,
            _ => 0.5
        };
    }

    private double MapGenderMale(GenderType gender)
    {
        return gender switch
        {
            GenderType.Male => 1.0,
            GenderType.Female => 0.0,
            GenderType.NonBinary => 0.5,
            _ => 0.5
        };
    }

    private double MapGenderFemale(GenderType gender)
    {
        return gender switch
        {
            GenderType.Male => 0.0,
            GenderType.Female => 1.0,
            GenderType.NonBinary => 0.5,
            _ => 0.5
        };
    }

    private double MapGenderNonBinary(GenderType gender)
    {
        return gender switch
        {
            GenderType.Male => 0.0,
            GenderType.Female => 0.0,
            GenderType.NonBinary => 1.0,
            _ => 0.5
        };
    }

    /// <summary>
    ///     SocialEnergy: 5 Stufen → 0.0, 0.25, 0.5, 0.75, 1.0
    /// </summary>
    private double MapSocialEnergyLevel(SocialEnergyLevel level)
    {
        return level switch
        {
            SocialEnergyLevel.StrongIntrovert => 0.0,
            SocialEnergyLevel.Introvert => 0.25,
            SocialEnergyLevel.Ambivert => 0.5,
            SocialEnergyLevel.Extravert => 0.75,
            SocialEnergyLevel.StrongExtravert => 1.0,
            _ => 0.5
        };
    }

    /// <summary>
    ///     ParentsRelationshipQuality: Conflictual=0.0, Neutral=0.5, Harmonious=1.0
    ///     Higher values represent better relationship quality (protective factor).
    /// </summary>
    private double MapParentsRelationshipQuality(ParentsRelationshipQuality quality)
    {
        return quality switch
        {
            ParentsRelationshipQuality.Conflictual => 0.0,
            ParentsRelationshipQuality.Neutral => 0.5,
            ParentsRelationshipQuality.Harmonious => 1.0,
            _ => 0.5
        };
    }

    private double NormalizeByType(double value, string factorName)
    {
        return factorName switch
        {
            // Enums sind SCHON normalisiert durch Mapping
            "IncomeLevel" or "ParentsEducationLevel" or "JobStatus"
                or "Gender" or "GenderMale" or "GenderFemale" or "GenderNonBinary"
                or "SocialEnergyLevel" or "ParentsRelationshipQuality" => value,

            // 0-100 Skalen
            "AnxietyLevel" or "FamilyCloseness" or "SocialEnvironmentLevel"
                or "CurrentStress" or "SocialBelonging" or "ResilienceScore" or "PhysicalHealth"
                => value / 100.0,

            // IQ (75-140)
            // 100 corresponds to 0.5 (Neutral)
            "IntelligenceScore" => value <= 100
                ? (value - 75) / 25.0 * 0.5
                : 0.5 + (value - 100) / 40.0 * 0.5,

            // Mood (-100 bis +100 → 0-1)
            "CurrentMood" => (value + 100) / 200.0,

            // Boolean (already 0 or 1)
            _ => value
        };
    }
}