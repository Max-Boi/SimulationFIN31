using System.Collections.Generic;
using SimulationFIN31.Models.Enums;

namespace SimulationFIN31.Models;

/// <summary>
///     Converts between German UI strings and domain enums.
///     Provides bidirectional mapping for ViewModel ↔ Model conversion.
/// </summary>
public static class EnumConverter
{
    private static readonly IReadOnlyDictionary<string, ParentsRelationshipQuality> ParentsRelationshipMap =
        new Dictionary<string, ParentsRelationshipQuality>
        {
            ["harmonisch"] = ParentsRelationshipQuality.Harmonious,
            ["neutral"] = ParentsRelationshipQuality.Neutral,
            ["konfliktgeladen"] = ParentsRelationshipQuality.Conflictual
        };

    private static readonly IReadOnlyDictionary<string, SocialEnergyLevel> SocialEnergyMap =
        new Dictionary<string, SocialEnergyLevel>
        {
            ["Stark introvertiert"] = SocialEnergyLevel.StrongIntrovert,
            ["Eher introvertiert"] = SocialEnergyLevel.Introvert,
            ["Ambivertiert"] = SocialEnergyLevel.Ambivert,
            ["Eher extrovertiert"] = SocialEnergyLevel.Extravert,
            ["Stark extrovertiert"] = SocialEnergyLevel.StrongExtravert
        };

    private static readonly IReadOnlyDictionary<string, GenderType> GenderMap = new Dictionary<string, GenderType>
    {
        ["Männlich"] = GenderType.Male,
        ["Weiblich"] = GenderType.Female,
        ["Non-Binär"] = GenderType.NonBinary
    };

    /// <summary>
    ///     Converts German string to ParentsRelationshipQuality enum.
    /// </summary>
    /// <param name="germanValue">German display string</param>
    /// <returns>Corresponding enum value, or Neutral as default</returns>
    public static ParentsRelationshipQuality ToParentsRelationshipQuality(string germanValue)
    {
        if (string.IsNullOrWhiteSpace(germanValue))
            return ParentsRelationshipQuality.Neutral;

        if (ParentsRelationshipMap.TryGetValue(germanValue, out var result))
            return result;

        return ParentsRelationshipQuality.Neutral;
    }

    /// <summary>
    ///     Converts German string to SocialEnergyLevel enum.
    /// </summary>
    /// <param name="germanValue">German display string</param>
    /// <returns>Corresponding enum value, or Ambivert as default</returns>
    public static SocialEnergyLevel ToSocialEnergyLevel(string germanValue)
    {
        if (string.IsNullOrWhiteSpace(germanValue))
            return SocialEnergyLevel.Ambivert;

        if (SocialEnergyMap.TryGetValue(germanValue, out var result))
            return result;

        return SocialEnergyLevel.Ambivert;
    }

    /// <summary>
    ///     Converts German string to GenderType enum.
    /// </summary>
    /// <param name="germanValue">German display string</param>
    /// <returns>Corresponding enum value, or Female as default</returns>
    public static GenderType ToGenderType(string germanValue)
    {
        if (string.IsNullOrWhiteSpace(germanValue))
            return GenderType.Female;

        if (GenderMap.TryGetValue(germanValue, out var result))
            return result;

        return GenderType.Female;
    }

    /// <summary>
    ///     Converts ParentsRelationshipQuality enum to German display string.
    /// </summary>
    public static string ToGermanString(this ParentsRelationshipQuality value)
    {
        return value switch
        {
            ParentsRelationshipQuality.Harmonious => "harmonisch",
            ParentsRelationshipQuality.Neutral => "neutral",
            ParentsRelationshipQuality.Conflictual => "konfliktgeladen",
            _ => "neutral"
        };
    }

    /// <summary>
    ///     Converts SocialEnergyLevel enum to German display string.
    /// </summary>
    public static string ToGermanString(this SocialEnergyLevel value)
    {
        return value switch
        {
            SocialEnergyLevel.StrongIntrovert => "Stark introvertiert",
            SocialEnergyLevel.Introvert => "Eher introvertiert",
            SocialEnergyLevel.Ambivert => "Ambivertiert",
            SocialEnergyLevel.Extravert => "Eher extrovertiert",
            SocialEnergyLevel.StrongExtravert => "Stark extrovertiert",
            _ => "Ambivertiert"
        };
    }

    /// <summary>
    ///     Converts GenderType enum to German display string.
    /// </summary>
    public static string ToGermanString(this GenderType value)
    {
        return value switch
        {
            GenderType.Male => "Männlich",
            GenderType.Female => "Weiblich",
            GenderType.NonBinary => "Non-Binär",
            _ => "Weiblich"
        };
    }

    /// <summary>
    ///     Maps slider value (1-7) to IncomeLevel enum.
    /// </summary>
    public static IncomeLevel MapIncomeLevel(int value)
    {
        return value switch
        {
            1 => IncomeLevel.ReallyLow,
            2 => IncomeLevel.Low,
            3 => IncomeLevel.LowHigh,
            4 => IncomeLevel.MediumLow,
            5 => IncomeLevel.Medium,
            6 => IncomeLevel.MediumHigh,
            7 => IncomeLevel.High,
            _ => IncomeLevel.Medium
        };
    }

    /// <summary>
    ///     Maps slider value (1-7) to ParentsEducationLevel enum.
    /// </summary>
    public static ParentsEducationLevel MapParentsEducationLevel(int value)
    {
        return value switch
        {
            1 => ParentsEducationLevel.ReallyLow,
            2 => ParentsEducationLevel.Low,
            3 => ParentsEducationLevel.LowHigh,
            4 => ParentsEducationLevel.MediumLow,
            5 => ParentsEducationLevel.Medium,
            6 => ParentsEducationLevel.MediumHigh,
            7 => ParentsEducationLevel.High
        };
    }

    /// <summary>
    ///     Maps slider value (1-7) to JobStatus enum.
    /// </summary>
    public static JobStatus MapJobStatus(int value)
    {
        return value switch
        {
            1 => JobStatus.LowPrestige1,
            2 => JobStatus.LowPrestige2,
            3 => JobStatus.LowPrestige3,
            4 => JobStatus.MediumPrestige1,
            5 => JobStatus.MediumPrestige2,
            6 => JobStatus.MediumPrestige3,
            7 => JobStatus.HighPrestige,
            _ => JobStatus.MediumPrestige2
        };
    }
}