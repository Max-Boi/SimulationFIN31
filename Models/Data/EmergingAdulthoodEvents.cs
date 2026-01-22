using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.Data;

public static class EmergingAdulthoodEvents
{
    private const int EMERGING_ADULTHOOD_MIN = 18;
    private const int EMERGING_ADULTHOOD_MAX = 23;

    #region Emerging Adulthood Personal Events (Ages 18-24)

    /// <summary>
    ///     Personal events specific to Emerging Adulthood phase (ages 18-24).
    ///     Focus on independence, higher education, and early career.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> EmergingAdulthoodPersonalEvents { get; } =
    [
        new()
        {
            Id = "emerging_university_acceptance",
            Name = "Universitätszulassung",
            Description =
                "Der junge Erwachsene erhält die Zulassung zur gewünschten Universität oder Ausbildungsprogramm.",
            BaseProbability = 0.55,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = 20,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 20,
            SocialBelongingImpact = 10,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = -8,
            SocialEnergyChange = 5,
            VisualCategory = VisualCategory.Education,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.5),
                new InfluenceFactor("IncomeLevel", 1.4),
                new InfluenceFactor("FamilyCloseness", 1.3),
                new InfluenceFactor("ParentsWithAddiction", -1.3),
                new InfluenceFactor("GenderFemale", 1.05),
                new InfluenceFactor("HasAdhd", -1.1)
            ]
        },
        new()
        {
            Id = "emerging_serious_relationship",
            Name = "Feste romantische Beziehung",
            Description = "Der junge Erwachsene geht eine ernsthafte, feste romantische Partnerschaft ein.",
            BaseProbability = 0.50,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 18,
            SocialBelongingImpact = 20,
            ResilienceImpact = 8,
            HealthImpact = 5,
            AnxietyChange = -10,
            SocialEnergyChange = 5,
            VisualCategory = VisualCategory.Romance,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 1.4),
                new InfluenceFactor("SocialEnvironmentLevel", 1.3),
                new InfluenceFactor("FamilyCloseness", 1.2),
                new InfluenceFactor("AnxietyLevel", -1.1),
                new InfluenceFactor("GenderFemale", 1.1)
            ]
        },
        new()
        {
            Id = "emerging_first_apartment",
            Name = "Erste eigene Wohnung",
            Description = "Der junge Erwachsene zieht in die erste eigene Wohnung, weg von der Familie.",
            BaseProbability = 0.60,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 10,
            MoodImpact = 15,
            SocialBelongingImpact = -5,
            ResilienceImpact = 15,
            HealthImpact = 0,
            AnxietyChange = 8,
            SocialEnergyChange = 3,
            VisualCategory = VisualCategory.Home,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.4),
                new InfluenceFactor("JobStatus", 1.3),
                new InfluenceFactor("ParentsEducationLevel", 1.2),
                new InfluenceFactor("FamilyCloseness", 1.1)
            ]
        },
        new()
        {
            Id = "emerging_career_start",
            Name = "Erste Karriereposition",
            Description = "Der junge Erwachsene beginnt die erste professionelle oder karriereorientierte Stelle.",
            BaseProbability = 0.45,
            MinAge = 20,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 8,
            MoodImpact = 18,
            SocialBelongingImpact = 10,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = 5,
            SocialEnergyChange = 5,
            VisualCategory = VisualCategory.Career,
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsEducationLevel", 1.4),
                new InfluenceFactor("IntelligenceScore", 1.4),
                new InfluenceFactor("SocialEnvironmentLevel", 1.2),
                new InfluenceFactor("IncomeLevel", 1.2),
                new InfluenceFactor("GenderMale", 1.05)
            ]
        },
        new()
        {
            Id = "emerging_degree_completion",
            Name = "Abschluss erworben",
            Description = "Der junge Erwachsene schließt ein Hochschulstudium oder eine berufliche Zertifizierung ab.",
            BaseProbability = 0.50,
            MinAge = 21,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -15,
            MoodImpact = 22,
            SocialBelongingImpact = 12,
            ResilienceImpact = 15,
            HealthImpact = 0,
            AnxietyChange = -10,
            SocialEnergyChange = 5,
            VisualCategory = VisualCategory.Education,
            Prerequisites = ["emerging_university_acceptance"],
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.4),
                new InfluenceFactor("FamilyCloseness", 1.3),
                new InfluenceFactor("ParentsEducationLevel", 1.3),
                new InfluenceFactor("ParentsWithAddiction", -1.3)
            ]
        },
        new()
        {
            Id = "emerging_relationship_ended",
            Name = "Beziehungstrennung",
            Description = "Der junge Erwachsene erlebt das Ende einer ernsthaften romantischen Beziehung.",
            BaseProbability = 0.20,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = true,
            StressImpact = 20,
            MoodImpact = -22,
            SocialBelongingImpact = -18,
            ResilienceImpact = 3,
            HealthImpact = -5,
            AnxietyChange = 18,
            SocialEnergyChange = -12,
            VisualCategory = VisualCategory.Romance,
            Prerequisites = ["emerging_serious_relationship"],
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 1.4),
                new InfluenceFactor("FamilyCloseness", -1.3),
                new InfluenceFactor("ParentsRelationshipQuality", -1.3),
                new InfluenceFactor("SocialEnvironmentLevel", -1.1)
            ]
        },
        new()
        {
            Id = "emerging_academic_failure",
            Name = "Akademischer Rückschlag",
            Description =
                "Der junge Erwachsene erlebt ein signifikantes akademisches Versagen oder einen Studienabbruch.",
            BaseProbability = 0.20,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = true,
            StressImpact = 20,
            MoodImpact = -20,
            SocialBelongingImpact = -15,
            ResilienceImpact = -8,
            HealthImpact = -5,
            AnxietyChange = 20,
            SocialEnergyChange = -10,
            VisualCategory = VisualCategory.Education,
            Prerequisites = ["emerging_university_acceptance"],
            Exclusions = ["emerging_degree_completion"],
            InfluenceFactors =
            [
                new InfluenceFactor("HasAdhd", 1.5),
                new InfluenceFactor("AnxietyLevel", 1.5),
                new InfluenceFactor("IntelligenceScore", -1.4),
                new InfluenceFactor("ParentsWithAddiction", 1.4),
                new InfluenceFactor("FamilyCloseness", -1.4),
                new InfluenceFactor("IncomeLevel", -1.2)
            ]
        },
        new()
        {
            Id = "emerging_financial_independence",
            Name = "Finanzielle Unabhängigkeit",
            Description = "Der junge Erwachsene erreicht finanzielle Unabhängigkeit von den Eltern.",
            BaseProbability = 0.40,
            MinAge = 20,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -8,
            MoodImpact = 18,
            SocialBelongingImpact = 5,
            ResilienceImpact = 15,
            HealthImpact = 0,
            AnxietyChange = -10,
            SocialEnergyChange = 5,
            VisualCategory = VisualCategory.Financial,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.4),
                new InfluenceFactor("JobStatus", 1.4),
                new InfluenceFactor("ParentsEducationLevel", 1.3),
                new InfluenceFactor("IncomeLevel", 1.2)
            ]
        },
        new()
        {
            Id = "emerging_identity_crisis",
            Name = "Quarterlife-Crisis",
            Description = "Der junge Erwachsene durchlebt eine Phase der Verwirrung über Lebensrichtung und Identität.",
            BaseProbability = 0.35,
            MinAge = 21,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = true,
            StressImpact = 18,
            MoodImpact = -18,
            SocialBelongingImpact = -12,
            ResilienceImpact = 3,
            HealthImpact = -5,
            AnxietyChange = 18,
            SocialEnergyChange = -10,
            VisualCategory = VisualCategory.Identity,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 1.5),
                new InfluenceFactor("FamilyCloseness", -1.4),
                new InfluenceFactor("ParentsWithAddiction", 1.4),
                new InfluenceFactor("SocialEnvironmentLevel", -1.3),
                new InfluenceFactor("ParentsRelationshipQuality", -1.3),
                new InfluenceFactor("GenderNonBinary", 1.4)
            ]
        },
        new()
        {
            Id = "emerging_skill_mastery",
            Name = "Beherrschung beruflicher Fähigkeiten",
            Description =
                "Der junge Erwachsene erreicht bemerkenswerte Expertise in einer beruflichen oder kreativen Fähigkeit.",
            BaseProbability = 0.30,
            MinAge = 21,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -8,
            MoodImpact = 18,
            SocialBelongingImpact = 10,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = -10,
            SocialEnergyChange = 5,
            VisualCategory = VisualCategory.Identity,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.4),
                new InfluenceFactor("ParentsEducationLevel", 1.3),
                new InfluenceFactor("FamilyCloseness", 1.2),
                new InfluenceFactor("IncomeLevel", 1.1)
            ]
        }
    ];

    #endregion

    #region Emerging Adulthood Generic Events (Ages 18-24)

    /// <summary>
    ///     Generic events that can occur during Emerging Adulthood phase (ages 18-24).
    /// </summary>
    public static IReadOnlyList<GenericEvent> EmergingAdulthoodGenericEvents { get; } =
    [
        new()
        {
            Id = "emerging_first_vote",
            Name = "Erste Wahlteilnahme",
            Description = "Der junge Erwachsene nimmt zum ersten Mal an einer wichtigen Wahl teil.",
            BaseProbability = 0.70,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = 20,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 0,
            MoodImpact = 8,
            SocialBelongingImpact = 10,
            ResilienceImpact = 5,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Community,
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsEducationLevel", 1.4),
                new InfluenceFactor("SocialEnvironmentLevel", 1.2),
                new InfluenceFactor("IntelligenceScore", 1.1)
            ]
        },
        new()
        {
            Id = "emerging_city_move",
            Name = "Umzug in neue Stadt",
            Description = "Der junge Erwachsene zieht für neue Möglichkeiten in eine neue Stadt.",
            BaseProbability = 0.35,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 15,
            MoodImpact = 8,
            SocialBelongingImpact = -12,
            ResilienceImpact = 12,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Home,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 1.0),
                new InfluenceFactor("IncomeLevel", 1.8),
                new InfluenceFactor("ParentsEducationLevel", 1.5),
                new InfluenceFactor("AnxietyLevel", -1.3)
            ]
        },
        new()
        {
            Id = "emerging_networking_success",
            Name = "Berufliche Netzwerkerfolge",
            Description = "Der junge Erwachsene knüpft wertvolle berufliche Kontakte.",
            BaseProbability = 0.35,
            MinAge = 20,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 12,
            SocialBelongingImpact = 15,
            ResilienceImpact = 8,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Social,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 1.4),
                new InfluenceFactor("ParentsEducationLevel", 1.3),
                new InfluenceFactor("SocialEnvironmentLevel", 1.2),
                new InfluenceFactor("IntelligenceScore", 1.1)
            ]
        },
        new()
        {
            Id = "emerging_economic_recession",
            Name = "Wirtschaftskrise",
            Description = "Ein wirtschaftlicher Abschwung beeinflusst Jobaussichten und finanzielle Sicherheit.",
            BaseProbability = 0.20,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 22,
            MoodImpact = -15,
            SocialBelongingImpact = -5,
            ResilienceImpact = 8,
            HealthImpact = -5,
            VisualCategory = VisualCategory.Financial,
            InfluenceFactors =
            [
              
            ]
        },
        new()
        {
            Id = "emerging_friend_group_formed",
            Name = "Enger Freundeskreis gebildet",
            Description = "Der junge Erwachsene entwickelt einen eng verbundenen Freundeskreis.",
            BaseProbability = 0.50,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -12,
            MoodImpact = 18,
            SocialBelongingImpact = 22,
            ResilienceImpact = 10,
            HealthImpact = 5,
            VisualCategory = VisualCategory.Social,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 1.3),
                new InfluenceFactor("SocialEnvironmentLevel", 1.2),
                new InfluenceFactor("GenderFemale", 1.1)
            ]
        },
        new()
        {
            Id = "emerging_health_scare",
            Name = "Gesundheitsschock",
            Description =
                "Der junge Erwachsene erlebt ein besorgniserregendes Gesundheitsproblem, das Aufmerksamkeit erfordert.",
            BaseProbability = 0.15,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 20,
            MoodImpact = -12,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = -15,
            VisualCategory = VisualCategory.Health,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 1.4),
                new InfluenceFactor("IncomeLevel", -1.1),
                new InfluenceFactor("ParentsWithAddiction", 1.3)
            ]
        },
        new()
        {
            Id = "emerging_parent_aging",
            Name = "Alterung der Eltern bemerkt",
            Description = "Der junge Erwachsene bemerkt, dass die Gesundheit der Eltern mit dem Alter abnimmt.",
            BaseProbability = 0.25,
            MinAge = 20,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 15,
            MoodImpact = -10,
            SocialBelongingImpact = 8,
            ResilienceImpact = 8,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Family,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.4),
                new InfluenceFactor("ParentsWithAddiction", 1.3),
                new InfluenceFactor("IncomeLevel", -1.1),
                new InfluenceFactor("GenderFemale", 1.1)
            ]
        },
        new()
        {
            Id = "emerging_travel_abroad",
            Name = "Internationale Reiseerfahrung",
            Description = "Der junge Erwachsene reist international und gewinnt neue Perspektiven.",
            BaseProbability = 0.35,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -8,
            MoodImpact = 18,
            SocialBelongingImpact = 8,
            ResilienceImpact = 12,
            HealthImpact = 5,
            VisualCategory = VisualCategory.Leisure,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.2)
            ]
        },
        new()
        {
            Id = "emerging_housing_crisis",
            Name = "Wohnungskrise",
            Description = "Der junge Erwachsene kämpft mit Wohnkosten und Verfügbarkeit.",
            BaseProbability = 0.30,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 18,
            MoodImpact = -12,
            SocialBelongingImpact = -5,
            ResilienceImpact = 5,
            HealthImpact = -5,
            VisualCategory = VisualCategory.Home,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -1.5),
                new InfluenceFactor("JobStatus", -1.4),
                new InfluenceFactor("ParentsWithAddiction", 1.3)
            ]
        },
        new()
        {
            Id = "emerging_debt_management",
            Name = "Studienschuldenmanagement",
            Description = "Der junge Erwachsene beginnt, bedeutende Studienkredite zu verwalten.",
            BaseProbability = 0.45,
            MinAge = 21,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 15,
            MoodImpact = -8,
            SocialBelongingImpact = 0,
            ResilienceImpact = 8,
            HealthImpact = -3,
            VisualCategory = VisualCategory.Financial,
            Prerequisites = ["emerging_university_acceptance"],
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -1.2)
            ]
        }
    ];

    #endregion

    #region Aggregated Event Collections

    /// <summary>
    ///     All generic events for the Emerging Adulthood phase.
    /// </summary>
    public static IReadOnlyList<GenericEvent> AllGenericEvents { get; } = new ReadOnlyCollection<GenericEvent>(
    [
        ..EmergingAdulthoodGenericEvents
    ]);

    /// <summary>
    ///     All personal events for the Emerging Adulthood phase.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> AllPersonalEvents { get; } = new ReadOnlyCollection<PersonalEvent>(
    [
        ..EmergingAdulthoodPersonalEvents
    ]);

    #endregion
}