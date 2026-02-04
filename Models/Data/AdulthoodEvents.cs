using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.Data;

public static class AdulthoodEvents
{
    private const int ADULTHOOD_MIN = 24;
    private const int ADULTHOOD_MAX = 67;
    private const int ADULTHOOD_SENIOR_WORK = 50;


    #region Adulthood Personal Events (Ages 24-80)

    /// <summary>
    ///     Personal events specific to Adulthood phase (ages 24-80).
    ///     Focus on career establishment, family formation, and life stability.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> AdulthoodPersonalEvents { get; } =
    [
        new()
        {
            Id = "adulthood_career_promotion",
            Name = "Bedeutende Karrierebeförderung",
            Description = "Der Erwachsene erhält eine bedeutsame Beförderung oder beruflichen Aufstieg.",
            BaseProbability = 0.25,
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
                new InfluenceFactor("IntelligenceScore", 1.4),
                new InfluenceFactor("JobStatus", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3),
                new InfluenceFactor("SocialEnergyLevel", 1.2),
                new InfluenceFactor("GenderMale", 1.05)
            ]
        },
        new()
        {
            Id = "adulthood_engagement",
            Name = "Verlobung oder Heirat",
            Description = "Der Erwachsene verlobt sich oder heiratet den Partner.",
            BaseProbability = 0.30,
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
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("ParentsRelationshipQuality", 1.4),
                new InfluenceFactor("SocialEnvironmentLevel", 1.2),
                new InfluenceFactor("IncomeLevel", 1.1)
            ]
        },
        new()
        {
            Id = "adulthood_home_purchase",
            Name = "Erster Eigenheimkauf",
            Description = "Der Erwachsene erwirbt sein erstes Eigenheim und schafft Wohnstabilität.",
            BaseProbability = 0.20,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 25,
            MoodImpact = 18,
            SocialBelongingImpact = 12,
            ResilienceImpact = 0,
            HealthImpact = 0,
            AnxietyChange = 5,
            SocialEnergyChange = 3,
            VisualCategory = VisualCategory.Home,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.6),
                new InfluenceFactor("JobStatus", 1.4),
                new InfluenceFactor("ParentsEducationLevel", 1.2),
                new InfluenceFactor("FamilyCloseness", 1.1)
            ]
        },
        new()
        {
            Id = "adulthood_child_born",
            Name = "Geburt eines Kindes",
            Description = "Der Erwachsene wird mit der Geburt eines Kindes Elternteil.",
            BaseProbability = 0.15,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_SENIOR_WORK,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 20,
            MoodImpact = 15,
            SocialBelongingImpact = 18,
            ResilienceImpact = -5,
            HealthImpact = -5,
            AnxietyChange = 0,
            SocialEnergyChange = -8,
            VisualCategory = VisualCategory.Family,
            Prerequisites = ["adulthood_engagement"],
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 0.9),
                new InfluenceFactor("IncomeLevel", 0.9),
                new InfluenceFactor("SocialEnvironmentLevel", 0.9)
            ]
        },
        new()
        {
            Id = "adulthood_career_pivot",
            Name = "Beruflicher Neustart",
            Description = "Der Erwachsene wechselt in ein völlig neues Berufsfeld.",
            BaseProbability = 0.18,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_SENIOR_WORK,
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
                new InfluenceFactor("IntelligenceScore", 1.4),
                new InfluenceFactor("IncomeLevel", 1.2),
                new InfluenceFactor("ParentsEducationLevel", 1.2),
                new InfluenceFactor("AnxietyLevel", -0.9),
                new InfluenceFactor("HasAdhd", 1.3)
            ]
        },
        new()
        {
            Id = "adulthood_job_loss",
            Name = "Jobverlust",
            Description = "Der Erwachsene erlebt einen unerwarteten Arbeitsplatzverlust.",
            BaseProbability = 0.06,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 22,
            MoodImpact = -22,
            SocialBelongingImpact = -15,
            ResilienceImpact = -7,
            HealthImpact = -8,
            AnxietyChange = 22,
            SocialEnergyChange = -12,
            VisualCategory = VisualCategory.Career,
            InfluenceFactors =
            [
                new InfluenceFactor("JobStatus", -0.5),
                new InfluenceFactor("IncomeLevel", -0.8),
                new InfluenceFactor("ParentsEducationLevel", -0.6),
                new InfluenceFactor("ParentsWithAddiction", 1.3),
                new InfluenceFactor("HasAdhd", 1.2)
            ]
        },
        new()
        {
            Id = "adulthood_divorce",
            Name = "Scheidung oder Trennung",
            Description = "Der Erwachsene durchlebt eine Scheidung oder die Trennung einer langjährigen Beziehung.",
            BaseProbability = 0.08,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 28,
            MoodImpact = -28,
            SocialBelongingImpact = -22,
            ResilienceImpact = -3,
            HealthImpact = -10,
            AnxietyChange = 25,
            SocialEnergyChange = -18,
            Prerequisites = ["adulthood_engagement"],
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsRelationshipQuality", -1.4),
                new InfluenceFactor("FamilyCloseness", -1.2),
                new InfluenceFactor("ParentsWithAddiction", 1.4),
                new InfluenceFactor("IncomeLevel", -1.0),
                new InfluenceFactor("AnxietyLevel", 1.3),
                new InfluenceFactor("GenderFemale", 1.05)
            ]
        },
        new()
        {
            Id = "adulthood_parent_death",
            Name = "Tod eines Elternteils",
            Description = "Der Erwachsene erlebt den Tod eines Elternteils.",
            VisualCategory = VisualCategory.Death,
            BaseProbability = 0.12,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = true,
            StressImpact = 25,
            MoodImpact = -30,
            SocialBelongingImpact = -10,
            ResilienceImpact = 5,
            HealthImpact = -8,
            AnxietyChange = 22,
            SocialEnergyChange = -15,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.3),
                new InfluenceFactor("ParentsWithAddiction", 1.4),
                new InfluenceFactor("IncomeLevel", -0.9)
            ]
        },
        new()
        {
            Id = "adulthood_entrepreneurship",
            Name = "Gründung eines Unternehmens",
            Description = "Der Erwachsene startet ein eigenes Unternehmen oder unternehmerisches Projekt.",
            BaseProbability = 0.10,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_SENIOR_WORK,
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
                new InfluenceFactor("IntelligenceScore", 1.4),
                new InfluenceFactor("IncomeLevel", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3),
                new InfluenceFactor("SocialEnergyLevel", 1.2),
                new InfluenceFactor("GenderMale", 1.1)
            ]
        },
        new()
        {
            Id = "adulthood_mental_health_support",
            Name = "Psychologische Unterstützung gesucht",
            Description = "Der Erwachsene nimmt professionelle psychologische Unterstützung in Anspruch.",
            BaseProbability = 0.20,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -20,
            MoodImpact = 15,
            SocialBelongingImpact = 10,
            ResilienceImpact = 5,
            HealthImpact = 0,
            AnxietyChange = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 1.3),
                new InfluenceFactor("ParentsEducationLevel", 1.1),
                new InfluenceFactor("IncomeLevel", 1.1)
            ]
        },
        new()
        {
            Id = "adulthood_burnout",
            Name = "Burnout-Krise",
            Description = "Der Erwachsene erlebt eine ernsthafte Erschöpfungskrise durch berufliche und private Überlastung.",
            BaseProbability = 0.12,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 30,
            MoodImpact = -25,
            SocialBelongingImpact = -15,
            ResilienceImpact = -10,
            HealthImpact = -18,
            AnxietyChange = 25,
            SocialEnergyChange = -20,
            VisualCategory = VisualCategory.Health,
            InfluenceFactors =
            [
                new InfluenceFactor("CurrentStress", 1.6),
                new InfluenceFactor("AnxietyLevel", 1.4),
                new InfluenceFactor("JobStatus", 1.3),
                new InfluenceFactor("SocialEnergyLevel", -1.2),
                new InfluenceFactor("ResilienceScore", -1.3),
                new InfluenceFactor("FamilyCloseness", -1.1),
                new InfluenceFactor("HasAdhd", 1.2)
            ]
        },
        new()
        {
            Id = "adulthood_volunteer_work",
            Name = "Ehrenamtliches Engagement",
            Description = "Der Erwachsene beginnt ein regelmäßiges ehrenamtliches Engagement in einer sozialen Organisation.",
            BaseProbability = 0.18,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 5,
            MoodImpact = 18,
            SocialBelongingImpact = 22,
            ResilienceImpact = 10,
            HealthImpact = 5,
            AnxietyChange = -8,
            SocialEnergyChange = 10,
            VisualCategory = VisualCategory.Community,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 1.4),
                new InfluenceFactor("SocialBelonging", 1.3),
                new InfluenceFactor("FamilyCloseness", 1.2),
                new InfluenceFactor("ParentsEducationLevel", 1.2),
                new InfluenceFactor("CurrentMood", 1.1),
                new InfluenceFactor("AnxietyLevel", -1.1),
                new InfluenceFactor("GenderFemale", 1.08)
            ]
        },
        new()
        {
            Id = "adulthood_relocation",
            Name = "Umzug in eine neue Stadt",
            Description = "Der Erwachsene zieht aus beruflichen oder persönlichen Gründen in eine neue Stadt.",
            BaseProbability = 0.15,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_SENIOR_WORK,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 20,
            MoodImpact = 5,
            SocialBelongingImpact = -18,
            ResilienceImpact = 8,
            HealthImpact = 0,
            AnxietyChange = 15,
            SocialEnergyChange = -5,
            VisualCategory = VisualCategory.Home,
            InfluenceFactors =
            [
                new InfluenceFactor("JobStatus", 1.4),
                new InfluenceFactor("IntelligenceScore", 1.2),
                new InfluenceFactor("IncomeLevel", 1.3),
                new InfluenceFactor("SocialEnvironmentLevel", -1.2),
                new InfluenceFactor("FamilyCloseness", -1.1),
                new InfluenceFactor("HasAdhd", 1.15),
                new InfluenceFactor("ParentsRelationshipQuality", -0.9)
            ]
        }
    ];

    #endregion

    #region Adulthood Generic Events (Ages 24-30)

    /// <summary>
    ///     Generic events that can occur during Adulthood phase (ages 24-30).
    /// </summary>
    public static IReadOnlyList<GenericEvent> AdulthoodGenericEvents { get; } =
    [
        new()
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
            VisualCategory = VisualCategory.Career,
            InfluenceFactors = []
        },
        new()
        {
            Id = "adulthood_community_involvement",
            Name = "Führungsrolle in der Gemeinschaft",
            Description = "Der Erwachsene übernimmt eine Führungsrolle in einer Gemeinschaftsorganisation.",
            BaseProbability = 0.15,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_SENIOR_WORK,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 8,
            MoodImpact = 15,
            SocialBelongingImpact = 18,
            ResilienceImpact = 10,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Community,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 1.4),
                new InfluenceFactor("SocialEnvironmentLevel", 1.4),
                new InfluenceFactor("FamilyCloseness", 1.2),
                new InfluenceFactor("ParentsEducationLevel", 1.1),
                new InfluenceFactor("GenderFemale", 1.05)
            ]
        },
        new()
        {
            Id = "adulthood_health_milestone",
            Name = "Gesundheitlicher Meilenstein",
            Description = "Der Erwachsene erreicht ein bedeutendes Gesundheits- oder Fitnessziel.",
            BaseProbability = 0.20,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -10,
            MoodImpact = 15,
            SocialBelongingImpact = 5,
            ResilienceImpact = 12,
            HealthImpact = 15,
            VisualCategory = VisualCategory.Health,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.3),
                new InfluenceFactor("SocialEnergyLevel", 1.2),
                new InfluenceFactor("AnxietyLevel", -1.1)
            ]
        },
        new()
        {
            Id = "adulthood_economic_downturn",
            Name = "Wirtschaftlicher Abschwung",
            Description =
                "Ein wirtschaftlicher Abschwung beeinträchtigt finanzielle Sicherheit und Karriereaussichten.",
            BaseProbability = 0.25,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 22,
            MoodImpact = -15,
            SocialBelongingImpact = -5,
            ResilienceImpact = 8,
            HealthImpact = -5,
            VisualCategory = VisualCategory.Financial,
            InfluenceFactors = []
        },
        new()
        {
            Id = "adulthood_reunion",
            Name = "Bedeutsames Wiedersehen",
            Description = "Der Erwachsene trifft alte Freunde oder Familienangehörige wieder.",
            BaseProbability = 0.22,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -12,
            MoodImpact = 25,
            SocialBelongingImpact = 20,
            ResilienceImpact = 5,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Social,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.4),
                new InfluenceFactor("SocialEnergyLevel", 1.3),
                new InfluenceFactor("SocialEnvironmentLevel", 1.2)
            ]
        },
        new()
        {
            Id = "adulthood_major_purchase",
            Name = "Große Anschaffung",
            Description = "Der Erwachsene tätigt eine bedeutende Anschaffung (Fahrzeug, Ausstattung, etc.).",
            BaseProbability = 0.30,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 8,
            MoodImpact = 15,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Financial,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.5),
                new InfluenceFactor("JobStatus", 1.3)
            ]
        },
        new()
        {
            Id = "adulthood_professional_recognition",
            Name = "Berufliche Anerkennung",
            Description = "Der Erwachsene erhält Anerkennung oder eine Auszeichnung im beruflichen Bereich.",
            BaseProbability = 0.15,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -8,
            MoodImpact = 25,
            SocialBelongingImpact = 12,
            ResilienceImpact = 10,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Achievement,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.4),
                new InfluenceFactor("JobStatus", 1.4),
                new InfluenceFactor("SocialEnergyLevel", 1.2),
                new InfluenceFactor("ParentsEducationLevel", 1.1),
                new InfluenceFactor("GenderMale", 1.05)
            ]
        },
        new()
        {
            Id = "adulthood_chronic_health",
            Name = "Chronische Erkrankung diagnostiziert",
            Description = "Beim Erwachsenen wird eine behandelbare chronische Erkrankung diagnostiziert.",
            BaseProbability = 0.08,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 22,
            MoodImpact = -15,
            SocialBelongingImpact = 5,
            ResilienceImpact = -12,
            HealthImpact = -20,
            VisualCategory = VisualCategory.Health,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -1.1),
                new InfluenceFactor("GenderFemale", 1.1)
            ]
        },
        new()
        {
            Id = "adulthood_friend_loss",
            Name = "Entfremdung von Freund",
            Description = "Der Erwachsene entfremdet sich natürlich von einem engen Freund.",
            BaseProbability = 0.25,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 8,
            MoodImpact = -10,
            SocialBelongingImpact = -12,
            ResilienceImpact = 3,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Social,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", -1.2),
                new InfluenceFactor("AnxietyLevel", 1.2),
                new InfluenceFactor("SocialEnvironmentLevel", -1.0)
            ]
        },
        new()
        {
            Id = "adulthood_sibling_milestone",
            Name = "Meilenstein im Leben des Geschwisters",
            Description = "Das Geschwister erlebt ein bedeutendes Lebensereignis (Hochzeit, Kind, etc.).",
            BaseProbability = 0.30,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 5,
            MoodImpact = 15,
            SocialBelongingImpact = 18,
            ResilienceImpact = 3,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Family,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.4),
                new InfluenceFactor("SocialEnvironmentLevel", 1.2)
            ]
        }
    ];

    #endregion

    #region Aggregated Event Collections

    /// <summary>
    ///     All generic events for the Adulthood phase.
    /// </summary>
    public static IReadOnlyList<GenericEvent> AllGenericEvents { get; } = new ReadOnlyCollection<GenericEvent>(
    [
        ..AdulthoodGenericEvents
    ]);

    /// <summary>
    ///     All personal events for the Adulthood phase.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> AllPersonalEvents { get; } = new ReadOnlyCollection<PersonalEvent>(
    [
        ..AdulthoodPersonalEvents
    ]);

    #endregion
}