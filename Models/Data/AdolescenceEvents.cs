using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.Data;

public static class AdolescenceEvents
{
    private const int ADOLESCENCE_MIN = 12;
    private const int ADOLESCENCE_MAX = 17;

    #region Adolescence Personal Events (Ages 12-18)

    /// <summary>
    /// Personal events specific to Adolescence phase (ages 12-18).
    /// Focus on identity formation, puberty, and peer relationships.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> AdolescencePersonalEvents { get; } =
    [
        new PersonalEvent
        {
            Id = "adolescence_first_romance",
            Name = "Erste romantische Beziehung",
            Description = "Teenager erlebt erste romantische Beziehung und erkundet emotionale Intimität.",
            BaseProbability = 0.65,
            MinAge = 13,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 5,
            MoodImpact = 15,
            SocialBelongingImpact = 12,
            ResilienceImpact = 5,
            HealthImpact = 0,
            AnxietyChange = 5,
            SocialEnergyChange = 8,
            VisualCategory = VisualCategory.Romance,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 2.0),
                new InfluenceFactor("SocialEnvironmentLevel", 1.8),
                new InfluenceFactor("AnxietyLevel", -1.5),
                new InfluenceFactor("FamilyCloseness", 1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "adolescence_strong_academics",
            Name = "Starke schulische Leistungen",
            Description = "Teenager erzielt durchgehend starke schulische Ergebnisse und schafft Zukunftschancen.",
            BaseProbability = 0.35,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 12,
            SocialBelongingImpact = 5,
            ResilienceImpact = 10,
            HealthImpact = 0,
            AnxietyChange = -5,
            SocialEnergyChange = 0,
            VisualCategory = VisualCategory.Education,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 3.0),
                new InfluenceFactor("ParentsEducationLevel", 2.8),
                new InfluenceFactor("FamilyCloseness", 2.0),
                new InfluenceFactor("IncomeLevel", 1.8),
                new InfluenceFactor("ParentsWithAddiction", -2.0)
            ]
        },
        new PersonalEvent
        {
            Id = "adolescence_identity_exploration",
            Name = "Identitätsfindungsphase",
            Description = "Teenager erforscht aktiv die eigene Identität, Werte und Überzeugungen.",
            BaseProbability = 0.75,
            MinAge = 13,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 8,
            MoodImpact = 5,
            SocialBelongingImpact = 5,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = 8,
            SocialEnergyChange = 5,
            VisualCategory = VisualCategory.Identity,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.8),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.5),
                new InfluenceFactor("SocialEnvironmentLevel", 1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "adolescence_leadership_school",
            Name = "Führungsposition in der Schule",
            Description = "Teenager wird in bedeutende schulische Führungsrolle gewählt oder ernannt.",
            BaseProbability = 0.15,
            MinAge = 14,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 8,
            MoodImpact = 15,
            SocialBelongingImpact = 15,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = 5,
            SocialEnergyChange = 10,
            VisualCategory = VisualCategory.Achievement,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 2.5),
                new InfluenceFactor("IntelligenceScore", 2.0),
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("SocialEnvironmentLevel", 1.5),
                new InfluenceFactor("AnxietyLevel", -1.5)
            ]
        },
        new PersonalEvent
        {
            Id = "adolescence_creative_expression",
            Name = "Kreative Entfaltung",
            Description = "Teenager entwickelt starkes kreatives Ventil (Musik, Kunst, Schreiben) zur Selbstentfaltung.",
            BaseProbability = 0.40,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -10,
            MoodImpact = 15,
            SocialBelongingImpact = 8,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = -8,
            SocialEnergyChange = 5,
            VisualCategory = VisualCategory.Creativity,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.8),
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("ParentsEducationLevel", 1.5),
                new InfluenceFactor("IncomeLevel", 1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "adolescence_heartbreak",
            Name = "Erster Liebeskummer",
            Description = "Teenager erlebt das Ende einer romantischen Beziehung und lernt mit Verlust umzugehen.",
            BaseProbability = 0.70,
            MinAge = 14,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            IsTraumatic = true,
            StressImpact = 25,
            MoodImpact = -28,
            SocialBelongingImpact = -18,
            ResilienceImpact = 5,
            HealthImpact = -5,
            AnxietyChange = 28,
            SocialEnergyChange = -20,
            VisualCategory = VisualCategory.Romance,
            Prerequisites = ["adolescence_first_romance"],
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 2.5),
                new InfluenceFactor("FamilyCloseness", -2.0),
                new InfluenceFactor("SocialEnvironmentLevel", -1.5)
            ]
        },
        new PersonalEvent
        {
            Id = "adolescence_peer_pressure",
            Name = "Starker Gruppenzwang",
            Description = "Teenager erlebt intensiven Gruppenzwang bezüglich riskanter Verhaltensweisen.",
            BaseProbability = 0.55,
            MinAge = 13,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 22,
            MoodImpact = -15,
            SocialBelongingImpact = -8,
            ResilienceImpact = -8,
            HealthImpact = -8,
            AnxietyChange = 18,
            SocialEnergyChange = -5,
            VisualCategory = VisualCategory.Social,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 2.0),
                new InfluenceFactor("ParentsWithAddiction", 3.0),
                new InfluenceFactor("FamilyCloseness", -2.5),
                new InfluenceFactor("SocialEnvironmentLevel", -2.0),
                new InfluenceFactor("ParentsEducationLevel", -1.5)
            ]
        },
        new PersonalEvent
        {
            Id = "adolescence_mental_health_struggle",
            Name = "Psychische Belastungen",
            Description = "Teenager erlebt erhebliche Angst- oder Depressionssymptome.",
            BaseProbability = 0.3,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            IsTraumatic = true,
            StressImpact = 32,
            MoodImpact = -38,
            SocialBelongingImpact = -28,
            ResilienceImpact = -15,
            HealthImpact = -15,
            AnxietyChange = 35,
            SocialEnergyChange = -25,
            VisualCategory = VisualCategory.MentalHealth,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 4.0),
                new InfluenceFactor("FamilyCloseness", -3.5),
                new InfluenceFactor("ParentsWithAddiction", 4.0),
                new InfluenceFactor("ParentsRelationshipQuality", -3.0),
                new InfluenceFactor("SocialEnvironmentLevel", -2.5),
                new InfluenceFactor("IncomeLevel", -2.0)
            ]
        },
        new PersonalEvent
        {
            Id = "adolescence_sports_achievement",
            Name = "Bedeutender sportlicher Erfolg",
            Description = "Teenager erzielt bemerkenswerte Erfolge im Wettkampfsport.",
            BaseProbability = 0.20,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 18,
            SocialBelongingImpact = 15,
            ResilienceImpact = 12,
            HealthImpact = 10,
            AnxietyChange = -8,
            SocialEnergyChange = 10,
            VisualCategory = VisualCategory.Sports,
            Prerequisites = ["school_sports_talent"],
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 2.0),
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("SocialEnvironmentLevel", 1.8),
                new InfluenceFactor("IncomeLevel", 1.5)
            ]
        },
        new PersonalEvent
        {
            Id = "adolescence_online_harassment",
            Name = "Online-Belästigung",
            Description = "Teenager erlebt Cybermobbing oder Online-Belästigung.",
            BaseProbability = 0.35,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            IsTraumatic = true,
            StressImpact = 28,
            MoodImpact = -28,
            SocialBelongingImpact = -25,
            ResilienceImpact = -10,
            HealthImpact = -8,
            AnxietyChange = 28,
            SocialEnergyChange = -20,
            VisualCategory = VisualCategory.Trauma,
            Prerequisites = ["school_technology_access"],
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", -2.5),
                new InfluenceFactor("AnxietyLevel", 2.5),
                new InfluenceFactor("HasAutism", 2.5),
                new InfluenceFactor("HasAdhd", 2.0),
                new InfluenceFactor("FamilyCloseness", -2.0),
                new InfluenceFactor("SocialEnvironmentLevel", -1.8)
            ]
        }
    ];

    #endregion

    #region Adolescence Generic Events (Ages 12-18)

    /// <summary>
    /// Generic events that can occur during Adolescence phase (ages 12-18).
    /// </summary>
    public static IReadOnlyList<GenericEvent> AdolescenceGenericEvents { get; } =
    [
        new GenericEvent
        {
            Id = "adolescence_high_school_start",
            Name = "Übergang zur weiterführenden Schule",
            Description = "Teenager wechselt zur weiterführenden Schule und meistert neue soziale Dynamiken.",
            BaseProbability = 0.95,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = 14,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 15,
            MoodImpact = 5,
            SocialBelongingImpact = -5,
            ResilienceImpact = 8,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("SocialEnvironmentLevel", 1.5),
                new InfluenceFactor("AnxietyLevel", -1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3)
            ]
        },
        new GenericEvent
        {
            Id = "adolescence_first_job",
            Name = "Erster Nebenjob",
            Description = "Teenager bekommt ersten Nebenjob und lernt Verantwortung sowie Unabhängigkeit.",
            BaseProbability = 0.45,
            MinAge = 15,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 8,
            MoodImpact = 10,
            SocialBelongingImpact = 8,
            ResilienceImpact = 12,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -2.0),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("SocialEnergyLevel", 1.5),
                new InfluenceFactor("SocialEnvironmentLevel", 1.3)
            ]
        },
        new GenericEvent
        {
            Id = "adolescence_drivers_license",
            Name = "Führerschein erworben",
            Description = "Teenager macht Führerschein und gewinnt erhebliche Unabhängigkeit.",
            BaseProbability = 0.60,
            MinAge = 16,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 15,
            SocialBelongingImpact = 10,
            ResilienceImpact = 8,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 2.0),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("SocialEnvironmentLevel", 1.3)
            ]
        },
        new GenericEvent
        {
            Id = "adolescence_graduation",
            Name = "Schulabschluss",
            Description = "Teenager schließt die Schule ab und erreicht einen wichtigen Lebensübergang.",
            BaseProbability = 0.85,
            MinAge = 17,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -10,
            MoodImpact = 20,
            SocialBelongingImpact = 15,
            ResilienceImpact = 15,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 2.0),
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("ParentsEducationLevel", 1.8),
                new InfluenceFactor("ParentsWithAddiction", -2.0)
            ]
        },
        new GenericEvent
        {
            Id = "adolescence_family_illness",
            Name = "Schwere Erkrankung in Familie",
            Description = "Nahes Familienmitglied erleidet schwere Erkrankung.",
            BaseProbability = 0.23,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 22,
            MoodImpact = -15,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("IncomeLevel", -1.5),
                new InfluenceFactor("ParentsWithAddiction", 2.0)
            ]
        },
        new GenericEvent
        {
            Id = "adolescence_volunteer_work",
            Name = "Freiwilligenarbeit",
            Description = "Teenager engagiert sich in bedeutungsvoller Freiwilligenarbeit.",
            BaseProbability = 0.35,
            MinAge = 14,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 12,
            SocialBelongingImpact = 15,
            ResilienceImpact = 10,
            HealthImpact = 3,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 2.0),
                new InfluenceFactor("SocialEnergyLevel", 1.8),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3)
            ]
        },
        new GenericEvent
        {
            Id = "adolescence_travel_opportunity",
            Name = "Bedeutende Reiseerfahrung",
            Description = "Teenager erhält Gelegenheit zu reisen und erweitert seinen Horizont.",
            BaseProbability = 0.30,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -8,
            MoodImpact = 15,
            SocialBelongingImpact = 8,
            ResilienceImpact = 10,
            HealthImpact = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 2.5),
                new InfluenceFactor("ParentsEducationLevel", 1.8),
                new InfluenceFactor("FamilyCloseness", 1.5)
            ]
        },
        new GenericEvent
        {
            Id = "adolescence_economic_hardship",
            Name = "Finanzielle Familienprobleme",
            Description = "Familie kämpft mit erheblichen finanziellen Schwierigkeiten.",
            BaseProbability = 0.20,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 20,
            MoodImpact = -12,
            SocialBelongingImpact = -8,
            ResilienceImpact = 8,
            HealthImpact = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -4.0),
                new InfluenceFactor("JobStatus", -3.5),
                new InfluenceFactor("ParentsEducationLevel", -2.5),
                new InfluenceFactor("ParentsWithAddiction", 3.0)
            ]
        },
        new GenericEvent
        {
            Id = "adolescence_mentorship",
            Name = "Bedeutsame Mentorschaft",
            Description = "Teenager entwickelt Beziehung zu unterstützendem Mentor (Lehrer, Trainer, Verwandter).",
            BaseProbability = 0.30,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -10,
            MoodImpact = 15,
            SocialBelongingImpact = 12,
            ResilienceImpact = 15,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 2.0),
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("SocialEnergyLevel", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3)
            ]
        },
        new GenericEvent
        {
            Id = "adolescence_community_recognition",
            Name = "Anerkennung durch Gemeinschaft",
            Description = "Teenager erhält Anerkennung für Beitrag zur Gemeinschaft.",
            BaseProbability = 0.15,
            MinAge = 14,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -8,
            MoodImpact = 18,
            SocialBelongingImpact = 15,
            ResilienceImpact = 10,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 2.0),
                new InfluenceFactor("SocialEnergyLevel", 2.0),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("IntelligenceScore", 1.3)
            ]
        }
    ];

    #endregion

    #region Aggregated Event Collections

    /// <summary>
    /// All generic events for the Adolescence phase.
    /// </summary>
    public static IReadOnlyList<GenericEvent> AllGenericEvents { get; } = new ReadOnlyCollection<GenericEvent>(
    [
        ..AdolescenceGenericEvents
    ]);

    /// <summary>
    /// All personal events for the Adolescence phase.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> AllPersonalEvents { get; } = new ReadOnlyCollection<PersonalEvent>(
    [
        ..AdolescencePersonalEvents
    ]);

    #endregion
}