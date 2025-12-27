using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.Data;

public static class EmergingAdulthoodEvents
{
    private const int EMERGING_ADULTHOOD_MIN = 18;
    private const int EMERGING_ADULTHOOD_MAX = 23;

    #region Emerging Adulthood Personal Events (Ages 18-24)

    /// <summary>
    /// Personal events specific to Emerging Adulthood phase (ages 18-24).
    /// Focus on independence, higher education, and early career.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> EmergingAdulthoodPersonalEvents { get; } =
    [
        
        new PersonalEvent
        {
            Id = "emerging_university_acceptance",
            Name = "University Acceptance",
            Description = "Young adult gains acceptance to desired university or training program.",
            BaseProbability = 0.55,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = 20,
            IsUnique = true,
            StressImpact = -5,
            MoodImpact = 20,
            SocialBelongingImpact = 10,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = -8,
            SocialEnergyChange = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 3.0),
                new InfluenceFactor("ParentsEducationLevel", 3.0),
                new InfluenceFactor("IncomeLevel", 2.5),
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("ParentsWithAddiction", -2.5)
            ]
        },
        new PersonalEvent
        {
            Id = "emerging_serious_relationship",
            Name = "Committed Romantic Relationship",
            Description = "Young adult forms a serious, committed romantic partnership.",
            BaseProbability = 0.50,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = -5,
            MoodImpact = 18,
            SocialBelongingImpact = 20,
            ResilienceImpact = 8,
            HealthImpact = 5,
            AnxietyChange = -10,
            SocialEnergyChange = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 2.0),
                new InfluenceFactor("SocialEnvironmentLevel", 1.8),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("AnxietyLevel", -1.5)
            ]
        },
        new PersonalEvent
        {
            Id = "emerging_first_apartment",
            Name = "First Independent Living",
            Description = "Young adult moves into first apartment away from family.",
            BaseProbability = 0.60,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = 10,
            MoodImpact = 15,
            SocialBelongingImpact = -5,
            ResilienceImpact = 15,
            HealthImpact = 0,
            AnxietyChange = 8,
            SocialEnergyChange = 3,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 2.0),
                new InfluenceFactor("JobStatus", 1.8),
                new InfluenceFactor("ParentsEducationLevel", 1.5),
                new InfluenceFactor("FamilyCloseness", 1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "emerging_career_start",
            Name = "First Career-Track Position",
            Description = "Young adult begins first professional or career-track job.",
            BaseProbability = 0.45,
            MinAge = 20,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = 8,
            MoodImpact = 18,
            SocialBelongingImpact = 10,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = 5,
            SocialEnergyChange = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsEducationLevel", 2.0),
                new InfluenceFactor("IntelligenceScore", 2.0),
                new InfluenceFactor("SocialEnvironmentLevel", 1.5),
                new InfluenceFactor("IncomeLevel", 1.5)
            ]
        },
        new PersonalEvent
        {
            Id = "emerging_degree_completion",
            Name = "Degree or Certification Completed",
            Description = "Young adult completes higher education degree or professional certification.",
            BaseProbability = 0.50,
            MinAge = 21,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = -15,
            MoodImpact = 22,
            SocialBelongingImpact = 12,
            ResilienceImpact = 15,
            HealthImpact = 0,
            AnxietyChange = -10,
            SocialEnergyChange = 5,
            Prerequisites = ["emerging_university_acceptance"],
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 2.0),
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("ParentsEducationLevel", 1.8),
                new InfluenceFactor("ParentsWithAddiction", -2.0)
            ]
        },
        new PersonalEvent
        {
            Id = "emerging_relationship_ended",
            Name = "Significant Relationship Ended",
            Description = "Young adult experiences end of serious romantic relationship.",
            BaseProbability = 0.40,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = 28,
            MoodImpact = -30,
            SocialBelongingImpact = -25,
            ResilienceImpact = 3,
            HealthImpact = -8,
            AnxietyChange = 25,
            SocialEnergyChange = -18,
            Prerequisites = ["emerging_serious_relationship"],
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 2.5),
                new InfluenceFactor("FamilyCloseness", -2.0),
                new InfluenceFactor("ParentsRelationshipQuality", -2.0),
                new InfluenceFactor("SocialEnvironmentLevel", -1.5)
            ]
        },
        new PersonalEvent
        {
            Id = "emerging_academic_failure",
            Name = "Academic Setback",
            Description = "Young adult experiences significant academic failure or dropout.",
            BaseProbability = 0.20,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = 28,
            MoodImpact = -28,
            SocialBelongingImpact = -22,
            ResilienceImpact = -12,
            HealthImpact = -8,
            AnxietyChange = 28,
            SocialEnergyChange = -15,
            Prerequisites = ["emerging_university_acceptance"],
            Exclusions = ["emerging_degree_completion"],
            InfluenceFactors =
            [
                new InfluenceFactor("HasAdhd", 3.5),
                new InfluenceFactor("AnxietyLevel", 3.0),
                new InfluenceFactor("IntelligenceScore", -2.5),
                new InfluenceFactor("ParentsWithAddiction", 3.0),
                new InfluenceFactor("FamilyCloseness", -2.5),
                new InfluenceFactor("IncomeLevel", -2.0)
            ]
        },
        new PersonalEvent
        {
            Id = "emerging_financial_independence",
            Name = "Financial Independence Achieved",
            Description = "Young adult achieves financial independence from parents.",
            BaseProbability = 0.40,
            MinAge = 20,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = -8,
            MoodImpact = 18,
            SocialBelongingImpact = 5,
            ResilienceImpact = 15,
            HealthImpact = 0,
            AnxietyChange = -10,
            SocialEnergyChange = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 2.0),
                new InfluenceFactor("JobStatus", 2.0),
                new InfluenceFactor("ParentsEducationLevel", 1.8),
                new InfluenceFactor("IncomeLevel", 1.5)
            ]
        },
        new PersonalEvent
        {
            Id = "emerging_identity_crisis",
            Name = "Quarter-Life Crisis",
            Description = "Young adult experiences period of confusion about life direction and identity.",
            BaseProbability = 0.35,
            MinAge = 21,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = 25,
            MoodImpact = -25,
            SocialBelongingImpact = -18,
            ResilienceImpact = 3,
            HealthImpact = -8,
            AnxietyChange = 25,
            SocialEnergyChange = -15,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 3.0),
                new InfluenceFactor("FamilyCloseness", -2.5),
                new InfluenceFactor("ParentsWithAddiction", 2.5),
                new InfluenceFactor("SocialEnvironmentLevel", -2.0),
                new InfluenceFactor("ParentsRelationshipQuality", -2.0)
            ]
        },
        new PersonalEvent
        {
            Id = "emerging_skill_mastery",
            Name = "Professional Skill Mastery",
            Description = "Young adult achieves notable expertise in professional or creative skill.",
            BaseProbability = 0.30,
            MinAge = 21,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = -8,
            MoodImpact = 18,
            SocialBelongingImpact = 10,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = -10,
            SocialEnergyChange = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 2.0),
                new InfluenceFactor("ParentsEducationLevel", 1.8),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("IncomeLevel", 1.3)
            ]
        }
    ];

    #endregion

    #region Emerging Adulthood Generic Events (Ages 18-24)

    /// <summary>
    /// Generic events that can occur during Emerging Adulthood phase (ages 18-24).
    /// </summary>
    public static IReadOnlyList<GenericEvent> EmergingAdulthoodGenericEvents { get; } =
    [
        new GenericEvent
        {
            Id = "emerging_first_vote",
            Name = "First Electoral Vote",
            Description = "Young adult participates in first major election.",
            BaseProbability = 0.70,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = 20,
            IsUnique = true,
            StressImpact = 0,
            MoodImpact = 8,
            SocialBelongingImpact = 10,
            ResilienceImpact = 5,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsEducationLevel", 2.0),
                new InfluenceFactor("SocialEnvironmentLevel", 1.5),
                new InfluenceFactor("IntelligenceScore", 1.3)
            ]
        },
        new GenericEvent
        {
            Id = "emerging_city_move",
            Name = "Move to New City",
            Description = "Young adult relocates to new city for opportunity.",
            BaseProbability = 0.35,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = 15,
            MoodImpact = 8,
            SocialBelongingImpact = -12,
            ResilienceImpact = 12,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 2.0),
                new InfluenceFactor("IncomeLevel", 1.8),
                new InfluenceFactor("ParentsEducationLevel", 1.5),
                new InfluenceFactor("AnxietyLevel", -1.3)
            ]
        },
        new GenericEvent
        {
            Id = "emerging_networking_success",
            Name = "Professional Networking Success",
            Description = "Young adult makes valuable professional connections.",
            BaseProbability = 0.35,
            MinAge = 20,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = -5,
            MoodImpact = 12,
            SocialBelongingImpact = 15,
            ResilienceImpact = 8,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 2.0),
                new InfluenceFactor("ParentsEducationLevel", 1.8),
                new InfluenceFactor("SocialEnvironmentLevel", 1.5),
                new InfluenceFactor("IntelligenceScore", 1.3)
            ]
        },
        new GenericEvent
        {
            Id = "emerging_economic_recession",
            Name = "Economic Recession Impact",
            Description = "Economic downturn affects job prospects and financial security.",
            BaseProbability = 0.20,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = 22,
            MoodImpact = -15,
            SocialBelongingImpact = -5,
            ResilienceImpact = 8,
            HealthImpact = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -3.5),
                new InfluenceFactor("JobStatus", -3.0),
                new InfluenceFactor("ParentsEducationLevel", -2.0)
            ]
        },
        new GenericEvent
        {
            Id = "emerging_friend_group_formed",
            Name = "Close Friend Group Formed",
            Description = "Young adult develops tight-knit friend group.",
            BaseProbability = 0.50,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = -12,
            MoodImpact = 18,
            SocialBelongingImpact = 22,
            ResilienceImpact = 10,
            HealthImpact = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 1.3),
                new InfluenceFactor("SocialEnvironmentLevel", 1.2)
            ]
        },
        new GenericEvent
        {
            Id = "emerging_health_scare",
            Name = "Personal Health Scare",
            Description = "Young adult experiences concerning health issue requiring attention.",
            BaseProbability = 0.15,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = 20,
            MoodImpact = -12,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = -15,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 2.0),
                new InfluenceFactor("IncomeLevel", -1.5),
                new InfluenceFactor("ParentsWithAddiction", 2.0)
            ]
        },
        new GenericEvent
        {
            Id = "emerging_parent_aging",
            Name = "Parent Health Decline Noticed",
            Description = "Young adult notices parent's health declining with age.",
            BaseProbability = 0.25,
            MinAge = 20,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = 15,
            MoodImpact = -10,
            SocialBelongingImpact = 8,
            ResilienceImpact = 8,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 2.0),
                new InfluenceFactor("ParentsWithAddiction", 2.0),
                new InfluenceFactor("IncomeLevel", -1.3)
            ]
        },
        new GenericEvent
        {
            Id = "emerging_travel_abroad",
            Name = "International Travel Experience",
            Description = "Young adult travels internationally, gaining new perspectives.",
            BaseProbability = 0.35,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = -8,
            MoodImpact = 18,
            SocialBelongingImpact = 8,
            ResilienceImpact = 12,
            HealthImpact = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.2)
            ]
        },
        new GenericEvent
        {
            Id = "emerging_housing_crisis",
            Name = "Housing Affordability Crisis",
            Description = "Young adult struggles with housing costs and availability.",
            BaseProbability = 0.30,
            MinAge = EMERGING_ADULTHOOD_MIN,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = 18,
            MoodImpact = -12,
            SocialBelongingImpact = -5,
            ResilienceImpact = 5,
            HealthImpact = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -3.5),
                new InfluenceFactor("JobStatus", -2.5),
                new InfluenceFactor("ParentsWithAddiction", 2.0)
            ]
        },
        new GenericEvent
        {
            Id = "emerging_debt_management",
            Name = "Student Debt Management",
            Description = "Young adult begins managing significant student loan debt.",
            BaseProbability = 0.45,
            MinAge = 21,
            MaxAge = EMERGING_ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = 15,
            MoodImpact = -8,
            SocialBelongingImpact = 0,
            ResilienceImpact = 8,
            HealthImpact = -3,
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
    /// All generic events for the Emerging Adulthood phase.
    /// </summary>
    public static IReadOnlyList<GenericEvent> AllGenericEvents { get; } = new ReadOnlyCollection<GenericEvent>(
    [
        ..EmergingAdulthoodGenericEvents
    ]);

    /// <summary>
    /// All personal events for the Emerging Adulthood phase.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> AllPersonalEvents { get; } = new ReadOnlyCollection<PersonalEvent>(
    [
        ..EmergingAdulthoodPersonalEvents
    ]);

    #endregion
}