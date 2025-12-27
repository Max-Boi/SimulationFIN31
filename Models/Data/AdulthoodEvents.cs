using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.Data;

public static class AdulthoodEvents
{
    private const int ADULTHOOD_MIN = 24;
    private const int ADULTHOOD_MAX = 30;

    #region Adulthood Personal Events (Ages 24-30)

    /// <summary>
    /// Personal events specific to Adulthood phase (ages 24-30).
    /// Focus on career establishment, family formation, and life stability.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> AdulthoodPersonalEvents { get; } =
    [
        new PersonalEvent
        {
            Id = "adulthood_career_promotion",
            Name = "Bedeutende Karrierebeförderung",
            Description = "Der Erwachsene erhält eine bedeutsame Beförderung oder beruflichen Aufstieg.",
            BaseProbability = 0.35,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 5,
            MoodImpact = 20,
            SocialBelongingImpact = 10,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = -8,
            SocialEnergyChange = 5,
            VisualCategory = VisualCategory.Career,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 2.0),
                new InfluenceFactor("JobStatus", 2.0),
                new InfluenceFactor("ParentsEducationLevel", 1.8),
                new InfluenceFactor("SocialEnergyLevel", 1.5)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_engagement",
            Name = "Verlobung oder Heirat",
            Description = "Der Erwachsene verlobt sich oder heiratet den Partner.",
            BaseProbability = 0.40,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 5,
            MoodImpact = 22,
            SocialBelongingImpact = 20,
            ResilienceImpact = 10,
            HealthImpact = 5,
            AnxietyChange = -10,
            SocialEnergyChange = 8,
            VisualCategory = VisualCategory.Romance,
            Prerequisites = ["emerging_serious_relationship"],
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 2.0),
                new InfluenceFactor("ParentsRelationshipQuality", 1.8),
                new InfluenceFactor("SocialEnvironmentLevel", 1.5),
                new InfluenceFactor("IncomeLevel", 1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_home_purchase",
            Name = "Erster Eigenheimkauf",
            Description = "Der Erwachsene erwirbt sein erstes Eigenheim und schafft Wohnstabilität.",
            BaseProbability = 0.30,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 12,
            MoodImpact = 18,
            SocialBelongingImpact = 12,
            ResilienceImpact = 10,
            HealthImpact = 0,
            AnxietyChange = 5,
            SocialEnergyChange = 3,
            VisualCategory = VisualCategory.Home,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 2.5),
                new InfluenceFactor("JobStatus", 2.0),
                new InfluenceFactor("ParentsEducationLevel", 1.5),
                new InfluenceFactor("FamilyCloseness", 1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_child_born",
            Name = "Geburt eines Kindes",
            Description = "Der Erwachsene wird mit der Geburt eines Kindes Elternteil.",
            BaseProbability = 0.35,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 20,
            MoodImpact = 22,
            SocialBelongingImpact = 18,
            ResilienceImpact = 12,
            HealthImpact = -5,
            AnxietyChange = 15,
            SocialEnergyChange = -8,
            VisualCategory = VisualCategory.Family,
            Prerequisites = ["adulthood_engagement"],
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 2.0),
                new InfluenceFactor("IncomeLevel", 1.8),
                new InfluenceFactor("ParentsRelationshipQuality", 1.5),
                new InfluenceFactor("SocialEnvironmentLevel", 1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_career_pivot",
            Name = "Beruflicher Neustart",
            Description = "Der Erwachsene wechselt in ein völlig neues Berufsfeld.",
            BaseProbability = 0.25,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 18,
            MoodImpact = 8,
            SocialBelongingImpact = -5,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = 12,
            SocialEnergyChange = 3,
            VisualCategory = VisualCategory.Career,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 2.0),
                new InfluenceFactor("IncomeLevel", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.5),
                new InfluenceFactor("AnxietyLevel", -1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_job_loss",
            Name = "Jobverlust",
            Description = "Der Erwachsene erlebt einen unerwarteten Arbeitsplatzverlust.",
            BaseProbability = 0.18,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = true,
            StressImpact = 32,
            MoodImpact = -32,
            SocialBelongingImpact = -22,
            ResilienceImpact = -10,
            HealthImpact = -12,
            AnxietyChange = 30,
            SocialEnergyChange = -18,
            VisualCategory = VisualCategory.Career,
            InfluenceFactors =
            [
                new InfluenceFactor("JobStatus", -3.5),
                new InfluenceFactor("IncomeLevel", -3.0),
                new InfluenceFactor("ParentsEducationLevel", -2.5),
                new InfluenceFactor("ParentsWithAddiction", 2.5),
                new InfluenceFactor("HasAdhd", 2.0)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_divorce",
            Name = "Scheidung oder Trennung",
            Description = "Der Erwachsene durchlebt eine Scheidung oder die Trennung einer langjährigen Beziehung.",
            BaseProbability = 0.25,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = true,
            StressImpact = 38,
            MoodImpact = -38,
            SocialBelongingImpact = -32,
            ResilienceImpact = -5,
            HealthImpact = -15,
            AnxietyChange = 35,
            SocialEnergyChange = -25,
            Prerequisites = ["adulthood_engagement"],
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsRelationshipQuality", -4.0),
                new InfluenceFactor("FamilyCloseness", -3.0),
                new InfluenceFactor("ParentsWithAddiction", 3.5),
                new InfluenceFactor("IncomeLevel", -2.0),
                new InfluenceFactor("AnxietyLevel", 2.5)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_parent_death",
            Name = "Tod eines Elternteils",
            Description = "Der Erwachsene erlebt den Tod eines Elternteils.",
            BaseProbability = 0.2,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = true,
            StressImpact = 35,
            MoodImpact = -40,
            SocialBelongingImpact = -15,
            ResilienceImpact = 5,
            HealthImpact = -12,
            AnxietyChange = 30,
            SocialEnergyChange = -20,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 2.0),
                new InfluenceFactor("ParentsWithAddiction", 2.0),
                new InfluenceFactor("IncomeLevel", -1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_entrepreneurship",
            Name = "Gründung eines Unternehmens",
            Description = "Der Erwachsene startet ein eigenes Unternehmen oder unternehmerisches Projekt.",
            BaseProbability = 0.15,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 18,
            MoodImpact = 15,
            SocialBelongingImpact = 8,
            ResilienceImpact = 15,
            HealthImpact = -5,
            AnxietyChange = 12,
            SocialEnergyChange = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 2.0),
                new InfluenceFactor("IncomeLevel", 2.0),
                new InfluenceFactor("ParentsEducationLevel", 1.8),
                new InfluenceFactor("SocialEnergyLevel", 1.5)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_mental_health_support",
            Name = "Psychologische Unterstützung gesucht",
            Description = "Der Erwachsene nimmt professionelle psychologische Unterstützung in Anspruch.",
            BaseProbability = 0.30,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -10,
            MoodImpact = 15,
            SocialBelongingImpact = 10,
            ResilienceImpact = 5,
            HealthImpact = 0,
            AnxietyChange = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 1.4),
                new InfluenceFactor("ParentsEducationLevel", 1.2),
                new InfluenceFactor("IncomeLevel", 1.1)
            ]
        }
    ];

    #endregion

    #region Adulthood Generic Events (Ages 24-30)

    /// <summary>
    /// Generic events that can occur during Adulthood phase (ages 24-30).
    /// </summary>
    public static IReadOnlyList<GenericEvent> AdulthoodGenericEvents { get; } =
    [
        new GenericEvent
        {
            Id = "adulthood_industry_growth",
            Name = "Branchenwachstum",
            Description = "Die berufliche Branche erlebt ein Wachstum und schafft neue Möglichkeiten.",
            BaseProbability = 0.30,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -8,
            MoodImpact = 12,
            SocialBelongingImpact = 8,
            ResilienceImpact = 5,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("JobStatus", 2.0),
                new InfluenceFactor("ParentsEducationLevel", 1.8),
                new InfluenceFactor("IntelligenceScore", 1.5)
            ]
        },
        new GenericEvent
        {
            Id = "adulthood_community_involvement",
            Name = "Führungsrolle in der Gemeinschaft",
            Description = "Der Erwachsene übernimmt eine Führungsrolle in einer Gemeinschaftsorganisation.",
            BaseProbability = 0.20,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 8,
            MoodImpact = 15,
            SocialBelongingImpact = 18,
            ResilienceImpact = 10,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 2.0),
                new InfluenceFactor("SocialEnvironmentLevel", 2.0),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3)
            ]
        },
        new GenericEvent
        {
            Id = "adulthood_health_milestone",
            Name = "Gesundheitlicher Meilenstein",
            Description = "Der Erwachsene erreicht ein bedeutendes Gesundheits- oder Fitnessziel.",
            BaseProbability = 0.25,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -10,
            MoodImpact = 15,
            SocialBelongingImpact = 5,
            ResilienceImpact = 12,
            HealthImpact = 15,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.8),
                new InfluenceFactor("SocialEnergyLevel", 1.5),
                new InfluenceFactor("AnxietyLevel", -1.5)
            ]
        },
        new GenericEvent
        {
            Id = "adulthood_economic_downturn",
            Name = "Wirtschaftlicher Abschwung",
            Description = "Ein wirtschaftlicher Abschwung beeinträchtigt finanzielle Sicherheit und Karriereaussichten.",
            BaseProbability = 0.18,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 22,
            MoodImpact = -15,
            SocialBelongingImpact = -5,
            ResilienceImpact = 8,
            HealthImpact = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -4.0),
                new InfluenceFactor("JobStatus", -3.5),
                new InfluenceFactor("ParentsEducationLevel", -2.5),
                new InfluenceFactor("ParentsWithAddiction", 2.0)
            ]
        },
        new GenericEvent
        {
            Id = "adulthood_reunion",
            Name = "Bedeutsames Wiedersehen",
            Description = "Der Erwachsene trifft alte Freunde oder Familienangehörige wieder.",
            BaseProbability = 0.30,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -12,
            MoodImpact = 25,
            SocialBelongingImpact = 20,
            ResilienceImpact = 5,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 2.0),
                new InfluenceFactor("SocialEnergyLevel", 1.8),
                new InfluenceFactor("SocialEnvironmentLevel", 1.5)
            ]
        },
        new GenericEvent
        {
            Id = "adulthood_major_purchase",
            Name = "Große Anschaffung",
            Description = "Der Erwachsene tätigt eine bedeutende Anschaffung (Fahrzeug, Ausstattung, etc.).",
            BaseProbability = 0.45,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 8,
            MoodImpact = 15,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 2.5),
                new InfluenceFactor("JobStatus", 1.8)
            ]
        },
        new GenericEvent
        {
            Id = "adulthood_professional_recognition",
            Name = "Berufliche Anerkennung",
            Description = "Der Erwachsene erhält Anerkennung oder eine Auszeichnung im beruflichen Bereich.",
            BaseProbability = 0.20,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -8,
            MoodImpact = 25,
            SocialBelongingImpact = 12,
            ResilienceImpact = 10,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 2.0),
                new InfluenceFactor("JobStatus", 2.0),
                new InfluenceFactor("SocialEnergyLevel", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3)
            ]
        },
        new GenericEvent
        {
            Id = "adulthood_chronic_health",
            Name = "Chronische Erkrankung diagnostiziert",
            Description = "Beim Erwachsenen wird eine behandelbare chronische Erkrankung diagnostiziert.",
            BaseProbability = 0.15,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 22,
            MoodImpact = -15,
            SocialBelongingImpact = 5,
            ResilienceImpact = -12,
            HealthImpact = -20,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 2.0),
                new InfluenceFactor("ParentsWithAddiction", 2.0),
                new InfluenceFactor("IncomeLevel", -1.5)
            ]
        },
        new GenericEvent
        {
            Id = "adulthood_friend_loss",
            Name = "Entfremdung von Freund",
            Description = "Der Erwachsene entfremdet sich natürlich von einem engen Freund.",
            BaseProbability = 0.40,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 8,
            MoodImpact = -10,
            SocialBelongingImpact = -12,
            ResilienceImpact = 3,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", -1.8),
                new InfluenceFactor("AnxietyLevel", 1.5),
                new InfluenceFactor("SocialEnvironmentLevel", -1.3)
            ]
        },
        new GenericEvent
        {
            Id = "adulthood_sibling_milestone",
            Name = "Meilenstein im Leben des Geschwisters",
            Description = "Das Geschwister erlebt ein bedeutendes Lebensereignis (Hochzeit, Kind, etc.).",
            BaseProbability = 0.45,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 5,
            MoodImpact = 15,
            SocialBelongingImpact = 18,
            ResilienceImpact = 3,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 2.0),
                new InfluenceFactor("SocialEnvironmentLevel", 1.5)
            ]
        }
    ];

    #endregion

    #region Aggregated Event Collections

    /// <summary>
    /// All generic events for the Adulthood phase.
    /// </summary>
    public static IReadOnlyList<GenericEvent> AllGenericEvents { get; } = new ReadOnlyCollection<GenericEvent>(
    [
        ..AdulthoodGenericEvents
    ]);

    /// <summary>
    /// All personal events for the Adulthood phase.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> AllPersonalEvents { get; } = new ReadOnlyCollection<PersonalEvent>(
    [
        ..AdulthoodPersonalEvents
    ]);

    #endregion
}