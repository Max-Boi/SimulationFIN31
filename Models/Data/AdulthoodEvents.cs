using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Name = "Significant Career Advancement",
            Description = "Adult receives meaningful promotion or career advancement.",
            BaseProbability = 0.35,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = 5,
            MoodImpact = 20,
            SocialBelongingImpact = 10,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = -8,
            SocialEnergyChange = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.3),
                new InfluenceFactor("JobStatus", 1.2)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_engagement",
            Name = "Engagement or Marriage",
            Description = "Adult becomes engaged or married to partner.",
            BaseProbability = 0.40,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = 5,
            MoodImpact = 22,
            SocialBelongingImpact = 20,
            ResilienceImpact = 10,
            HealthImpact = 5,
            AnxietyChange = -10,
            SocialEnergyChange = 8,
            Prerequisites = ["emerging_serious_relationship"],
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.2)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_home_purchase",
            Name = "First Home Purchase",
            Description = "Adult purchases first home, establishing residential stability.",
            BaseProbability = 0.30,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = 12,
            MoodImpact = 18,
            SocialBelongingImpact = 12,
            ResilienceImpact = 10,
            HealthImpact = 0,
            AnxietyChange = 5,
            SocialEnergyChange = 3,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.5),
                new InfluenceFactor("JobStatus", 1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_child_born",
            Name = "Birth of Child",
            Description = "Adult becomes a parent with birth of child.",
            BaseProbability = 0.35,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = 20,
            MoodImpact = 22,
            SocialBelongingImpact = 18,
            ResilienceImpact = 12,
            HealthImpact = -5,
            AnxietyChange = 15,
            SocialEnergyChange = -8,
            Prerequisites = ["adulthood_engagement"],
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_career_pivot",
            Name = "Career Change",
            Description = "Adult makes significant career change to new field.",
            BaseProbability = 0.25,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = 18,
            MoodImpact = 8,
            SocialBelongingImpact = -5,
            ResilienceImpact = 12,
            HealthImpact = 0,
            AnxietyChange = 12,
            SocialEnergyChange = 3,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.2)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_job_loss",
            Name = "Job Loss",
            Description = "Adult experiences unexpected job loss.",
            BaseProbability = 0.18,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = 28,
            MoodImpact = -22,
            SocialBelongingImpact = -10,
            ResilienceImpact = -5,
            HealthImpact = -8,
            AnxietyChange = 25,
            SocialEnergyChange = -10,
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
            Name = "Divorce or Separation",
            Description = "Adult goes through divorce or long-term relationship separation.",
            BaseProbability = 0.25,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = 30,
            MoodImpact = -25,
            SocialBelongingImpact = -18,
            ResilienceImpact = 5,
            HealthImpact = -10,
            AnxietyChange = 25,
            SocialEnergyChange = -15,
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
            Name = "Death of Parent",
            Description = "Adult experiences death of a parent.",
            BaseProbability = 0.15,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = 28,
            MoodImpact = -25,
            SocialBelongingImpact = 5,
            ResilienceImpact = 10,
            HealthImpact = -8,
            AnxietyChange = 20,
            SocialEnergyChange = -12,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.4)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_entrepreneurship",
            Name = "Business Venture Started",
            Description = "Adult starts own business or entrepreneurial venture.",
            BaseProbability = 0.15,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = 18,
            MoodImpact = 15,
            SocialBelongingImpact = 8,
            ResilienceImpact = 15,
            HealthImpact = -5,
            AnxietyChange = 12,
            SocialEnergyChange = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.3),
                new InfluenceFactor("IncomeLevel", 1.1)
            ]
        },
        new PersonalEvent
        {
            Id = "adulthood_mental_health_support",
            Name = "Sought Mental Health Support",
            Description = "Adult proactively seeks therapy or mental health support.",
            BaseProbability = 0.30,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = -15,
            MoodImpact = 12,
            SocialBelongingImpact = 5,
            ResilienceImpact = 18,
            HealthImpact = 8,
            AnxietyChange = -15,
            SocialEnergyChange = 5,
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
            Name = "Industry Growth Period",
            Description = "Professional industry experiences growth, creating opportunities.",
            BaseProbability = 0.30,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = -8,
            MoodImpact = 12,
            SocialBelongingImpact = 8,
            ResilienceImpact = 5,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("JobStatus", 1.2)
            ]
        },
        new GenericEvent
        {
            Id = "adulthood_community_involvement",
            Name = "Community Leadership Role",
            Description = "Adult takes on leadership role in community organization.",
            BaseProbability = 0.20,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = 8,
            MoodImpact = 15,
            SocialBelongingImpact = 18,
            ResilienceImpact = 10,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 1.4),
                new InfluenceFactor("SocialEnvironmentLevel", 1.3)
            ]
        },
        new GenericEvent
        {
            Id = "adulthood_health_milestone",
            Name = "Health Improvement Achievement",
            Description = "Adult achieves significant health or fitness milestone.",
            BaseProbability = 0.30,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = -10,
            MoodImpact = 15,
            SocialBelongingImpact = 5,
            ResilienceImpact = 12,
            HealthImpact = 15,
            InfluenceFactors = []
        },
        new GenericEvent
        {
            Id = "adulthood_economic_downturn",
            Name = "Economic Downturn",
            Description = "Economic downturn affects financial security and career prospects.",
            BaseProbability = 0.18,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
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
            Name = "Meaningful Reunion",
            Description = "Adult reconnects with old friends or family members.",
            BaseProbability = 0.35,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = -8,
            MoodImpact = 15,
            SocialBelongingImpact = 15,
            ResilienceImpact = 5,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.3),
                new InfluenceFactor("SocialEnergyLevel", 1.1)
            ]
        },
        new GenericEvent
        {
            Id = "adulthood_major_purchase",
            Name = "Major Life Purchase",
            Description = "Adult makes significant life purchase (vehicle, equipment, etc.).",
            BaseProbability = 0.45,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = 8,
            MoodImpact = 12,
            SocialBelongingImpact = 5,
            ResilienceImpact = 5,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.4)
            ]
        },
        new GenericEvent
        {
            Id = "adulthood_professional_recognition",
            Name = "Professional Recognition",
            Description = "Adult receives recognition or award in professional field.",
            BaseProbability = 0.20,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = -8,
            MoodImpact = 18,
            SocialBelongingImpact = 12,
            ResilienceImpact = 10,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.3),
                new InfluenceFactor("JobStatus", 1.2)
            ]
        },
        new GenericEvent
        {
            Id = "adulthood_chronic_health",
            Name = "Chronic Health Condition Diagnosed",
            Description = "Adult diagnosed with manageable chronic health condition.",
            BaseProbability = 0.15,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = true,
            StressImpact = 22,
            MoodImpact = -15,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = -20,
            InfluenceFactors = []
        },
        new GenericEvent
        {
            Id = "adulthood_friend_loss",
            Name = "Close Friend Drifted Apart",
            Description = "Adult experiences natural drifting apart from close friend.",
            BaseProbability = 0.40,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = 8,
            MoodImpact = -10,
            SocialBelongingImpact = -12,
            ResilienceImpact = 3,
            HealthImpact = 0,
            InfluenceFactors = []
        },
        new GenericEvent
        {
            Id = "adulthood_sibling_milestone",
            Name = "Sibling Life Milestone",
            Description = "Sibling experiences major life event (marriage, child, etc.).",
            BaseProbability = 0.45,
            MinAge = ADULTHOOD_MIN,
            MaxAge = ADULTHOOD_MAX,
            IsUnique = false,
            StressImpact = 3,
            MoodImpact = 10,
            SocialBelongingImpact = 12,
            ResilienceImpact = 3,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.3)
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