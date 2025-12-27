using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Name = "First Romantic Relationship",
            Description = "Teen experiences first romantic relationship, exploring emotional intimacy.",
            BaseProbability = 0.65,
            MinAge = 13,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            StressImpact = 5,
            MoodImpact = 15,
            SocialBelongingImpact = 12,
            ResilienceImpact = 5,
            HealthImpact = 0,
            AnxietyChange = 5,
            SocialEnergyChange = 8,
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
            Name = "Strong Academic Performance",
            Description = "Teen achieves consistently strong academic results, building future opportunities.",
            BaseProbability = 0.35,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            StressImpact = -5,
            MoodImpact = 12,
            SocialBelongingImpact = 5,
            ResilienceImpact = 10,
            HealthImpact = 0,
            AnxietyChange = -5,
            SocialEnergyChange = 0,
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
            Name = "Identity Exploration Phase",
            Description = "Teen actively explores personal identity, values, and beliefs.",
            BaseProbability = 0.75,
            MinAge = 13,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            StressImpact = 8,
            MoodImpact = 5,
            SocialBelongingImpact = 5,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = 8,
            SocialEnergyChange = 5,
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
            Name = "School Leadership Position",
            Description = "Teen elected or appointed to significant school leadership role.",
            BaseProbability = 0.15,
            MinAge = 14,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            StressImpact = 8,
            MoodImpact = 15,
            SocialBelongingImpact = 15,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = 5,
            SocialEnergyChange = 10,
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
            Name = "Creative Expression Flourishes",
            Description = "Teen develops strong creative outlet (music, art, writing) for self-expression.",
            BaseProbability = 0.40,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            StressImpact = -10,
            MoodImpact = 15,
            SocialBelongingImpact = 8,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = -8,
            SocialEnergyChange = 5,
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
            Name = "First Heartbreak",
            Description = "Teen experiences end of romantic relationship, learning about loss.",
            BaseProbability = 0.70,
            MinAge = 14,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            StressImpact = 25,
            MoodImpact = -28,
            SocialBelongingImpact = -18,
            ResilienceImpact = 5,
            HealthImpact = -5,
            AnxietyChange = 28,
            SocialEnergyChange = -20,
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
            Name = "Significant Peer Pressure Experience",
            Description = "Teen faces intense peer pressure regarding risk behaviors.",
            BaseProbability = 0.55,
            MinAge = 13,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            StressImpact = 22,
            MoodImpact = -15,
            SocialBelongingImpact = -8,
            ResilienceImpact = -8,
            HealthImpact = -8,
            AnxietyChange = 18,
            SocialEnergyChange = -5,
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
            Name = "Mental Health Difficulties",
            Description = "Teen experiences significant anxiety or depression symptoms.",
            BaseProbability = 0.3,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            StressImpact = 32,
            MoodImpact = -38,
            SocialBelongingImpact = -28,
            ResilienceImpact = -15,
            HealthImpact = -15,
            AnxietyChange = 35,
            SocialEnergyChange = -25,
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
            Name = "Significant Sports Achievement",
            Description = "Teen achieves notable success in competitive sports.",
            BaseProbability = 0.20,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            StressImpact = -5,
            MoodImpact = 18,
            SocialBelongingImpact = 15,
            ResilienceImpact = 12,
            HealthImpact = 10,
            AnxietyChange = -8,
            SocialEnergyChange = 10,
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
            Name = "Online Harassment Experience",
            Description = "Teen experiences cyberbullying or online harassment.",
            BaseProbability = 0.35,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            StressImpact = 28,
            MoodImpact = -28,
            SocialBelongingImpact = -25,
            ResilienceImpact = -10,
            HealthImpact = -8,
            AnxietyChange = 28,
            SocialEnergyChange = -20,
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
            Name = "High School Transition",
            Description = "Teen transitions to high school, navigating new social dynamics.",
            BaseProbability = 0.95,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = 14,
            IsUnique = true,
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
            Name = "First Part-Time Job",
            Description = "Teen gets first part-time job, learning responsibility and earning independence.",
            BaseProbability = 0.45,
            MinAge = 15,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
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
            Name = "Driver's License Obtained",
            Description = "Teen earns driver's license, gaining significant independence.",
            BaseProbability = 0.60,
            MinAge = 16,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
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
            Name = "High School Graduation",
            Description = "Teen graduates from high school, marking major life transition.",
            BaseProbability = 0.85,
            MinAge = 17,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
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
            Name = "Family Member Serious Illness",
            Description = "Close family member experiences serious illness.",
            BaseProbability = 0.23,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
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
            Name = "Volunteer Work Experience",
            Description = "Teen participates in meaningful volunteer work.",
            BaseProbability = 0.35,
            MinAge = 14,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
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
            Name = "Significant Travel Experience",
            Description = "Teen has opportunity to travel, broadening perspective.",
            BaseProbability = 0.30,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
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
            Name = "Family Economic Hardship",
            Description = "Family faces significant financial difficulties.",
            BaseProbability = 0.20,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
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
            Name = "Meaningful Mentorship",
            Description = "Teen develops relationship with supportive mentor (teacher, coach, relative).",
            BaseProbability = 0.30,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
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
            Name = "Community Recognition",
            Description = "Teen receives recognition for contribution to community.",
            BaseProbability = 0.15,
            MinAge = 14,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
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