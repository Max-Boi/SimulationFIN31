using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.Data;

public static class SchoolBeginningEvents
{
    private const int SCHOOL_BEGINNING_MIN = 6;
    private const int SCHOOL_BEGINNING_MAX = 11;

    #region School Beginning Personal Events (Ages 6-12)

    /// <summary>
    /// Personal events specific to School Beginning phase (ages 6-12).
    /// Focus on peer relationships, academic development, and social skills.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> SchoolBeginningPersonalEvents { get; } =
    [
        new PersonalEvent
        {
            Id = "school_best_friend",
            Name = "Best Friend Bond Formed",
            Description = "Child forms a close best friend relationship with mutual trust and support.",
            BaseProbability = 0.60,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            StressImpact = -10,
            MoodImpact = 15,
            SocialBelongingImpact = 18,
            ResilienceImpact = 8,
            HealthImpact = 0,
            AnxietyChange = -8,
            SocialEnergyChange = 10,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 2.5),
                new InfluenceFactor("SocialEnergyLevel", 1.8),
                new InfluenceFactor("FamilyCloseness", 1.5)
            ]
        },
        new PersonalEvent
        {
            Id = "school_academic_achievement",
            Name = "Academic Achievement",
            Description = "Child achieves notable academic success, building confidence in learning.",
            BaseProbability = 0.45,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            StressImpact = -8,
            MoodImpact = 12,
            SocialBelongingImpact = 5,
            ResilienceImpact = 10,
            HealthImpact = 0,
            AnxietyChange = -5,
            SocialEnergyChange = 3,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 2.8),
                new InfluenceFactor("ParentsEducationLevel", 2.5),
                new InfluenceFactor("FamilyCloseness", 2.0),
                new InfluenceFactor("IncomeLevel", 1.5)
            ]
        },
        new PersonalEvent
        {
            Id = "school_sports_talent",
            Name = "Athletic Talent Discovered",
            Description = "Child shows natural athletic ability and enjoys physical activities.",
            BaseProbability = 0.35,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            StressImpact = -5,
            MoodImpact = 12,
            SocialBelongingImpact = 10,
            ResilienceImpact = 10,
            HealthImpact = 10,
            AnxietyChange = -5,
            SocialEnergyChange = 8,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 1.2)
            ]
        },
        new PersonalEvent
        {
            Id = "school_team_membership",
            Name = "Team or Group Membership",
            Description = "Child joins a team or group activity, learning cooperation and belonging.",
            BaseProbability = 0.50,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            StressImpact = -3,
            MoodImpact = 10,
            SocialBelongingImpact = 15,
            ResilienceImpact = 5,
            HealthImpact = 5,
            AnxietyChange = -5,
            SocialEnergyChange = 8,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 1.2),
                new InfluenceFactor("SocialEnergyLevel", 1.1)
            ]
        },
        new PersonalEvent
        {
            Id = "school_leadership_role",
            Name = "Leadership Role Taken",
            Description = "Child takes on a leadership role among peers (class representative, team captain).",
            BaseProbability = 0.20,
            MinAge = 8,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            StressImpact = 5,
            MoodImpact = 12,
            SocialBelongingImpact = 12,
            ResilienceImpact = 10,
            HealthImpact = 0,
            AnxietyChange = -3,
            SocialEnergyChange = 10,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.1),
                new InfluenceFactor("SocialEnergyLevel", 1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "school_bullying_victim",
            Name = "Bullying Experience",
            Description = "Child experiences bullying from peers, causing emotional distress.",
            BaseProbability = 0.30,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            StressImpact = 25,
            MoodImpact = -20,
            SocialBelongingImpact = -15,
            ResilienceImpact = -8,
            HealthImpact = -5,
            AnxietyChange = 20,
            SocialEnergyChange = -15,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", -2.5),
                new InfluenceFactor("HasAutism", 3.0),
                new InfluenceFactor("HasAdhd", 2.5),
                new InfluenceFactor("FamilyCloseness", -2.0),
                new InfluenceFactor("IncomeLevel", -2.0),
                new InfluenceFactor("SocialEnvironmentLevel", -2.0)
            ]
        },
        new PersonalEvent
        {
            Id = "school_learning_difficulty",
            Name = "Learning Difficulty Identified",
            Description = "A learning difference is identified, requiring additional support.",
            BaseProbability = 0.15,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = 9,
            IsUnique = true,
            StressImpact = 15,
            MoodImpact = -10,
            SocialBelongingImpact = -5,
            ResilienceImpact = 5,
            HealthImpact = 0,
            AnxietyChange = 12,
            SocialEnergyChange = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("HasAdhd", 4.0),
                new InfluenceFactor("HasAutism", 3.5),
                new InfluenceFactor("IntelligenceScore", -2.0),
                new InfluenceFactor("ParentsEducationLevel", -1.5)
            ]
        },
        new PersonalEvent
        {
            Id = "school_social_rejection",
            Name = "Social Rejection Experience",
            Description = "Child experiences exclusion or rejection from peer group.",
            BaseProbability = 0.35,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            StressImpact = 18,
            MoodImpact = -15,
            SocialBelongingImpact = -18,
            ResilienceImpact = -5,
            HealthImpact = 0,
            AnxietyChange = 15,
            SocialEnergyChange = -12,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", -2.5),
                new InfluenceFactor("AnxietyLevel", 2.5),
                new InfluenceFactor("HasAutism", 2.5),
                new InfluenceFactor("HasAdhd", 2.0),
                new InfluenceFactor("IncomeLevel", -1.8),
                new InfluenceFactor("FamilyCloseness", -1.5)
            ]
        },
        new PersonalEvent
        {
            Id = "school_hobby_discovery",
            Name = "Meaningful Hobby Discovered",
            Description = "Child discovers a hobby that provides joy and a sense of accomplishment.",
            BaseProbability = 0.55,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            StressImpact = -8,
            MoodImpact = 12,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = 3,
            AnxietyChange = -8,
            SocialEnergyChange = 3,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.1)
            ]
        },
        new PersonalEvent
        {
            Id = "school_custody_adjustment",
            Name = "Custody Arrangement Adjustment",
            Description = "Child adjusts to living arrangements following parents' separation.",
            BaseProbability = 0.80,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            StressImpact = 20,
            MoodImpact = -12,
            SocialBelongingImpact = -8,
            ResilienceImpact = 5,
            HealthImpact = -3,
            AnxietyChange = 15,
            SocialEnergyChange = -15,
            Prerequisites = ["childhood_parents_divorce"],
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.2)
            ]
        }
    ];

    #endregion

    #region School Beginning Generic Events (Ages 6-12)

    /// <summary>
    /// Generic events that can occur during School Beginning phase (ages 6-12).
    /// </summary>
    public static IReadOnlyList<GenericEvent> SchoolBeginningGenericEvents { get; } =
    [
        new GenericEvent
        {
            Id = "school_field_trip",
            Name = "School Field Trip",
            Description = "Class participates in an educational field trip.",
            BaseProbability = 0.65,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            StressImpact = -5,
            MoodImpact = 12,
            SocialBelongingImpact = 8,
            ResilienceImpact = 2,
            HealthImpact = 2,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.1)
            ]
        },
        new GenericEvent
        {
            Id = "school_first_day",
            Name = "First Day of Primary School",
            Description = "Child experiences the milestone of starting primary school.",
            BaseProbability = 0.95,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = 7,
            IsUnique = true,
            StressImpact = 12,
            MoodImpact = 5,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 0.8)
            ]
        },
        new GenericEvent
        {
            Id = "school_teacher_positive",
            Name = "Positive Teacher Relationship",
            Description = "Child develops a positive relationship with an encouraging teacher.",
            BaseProbability = 0.50,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            StressImpact = -8,
            MoodImpact = 12,
            SocialBelongingImpact = 10,
            ResilienceImpact = 8,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 1.2)
            ]
        },
        new GenericEvent
        {
            Id = "school_extracurricular_success",
            Name = "Extracurricular Achievement",
            Description = "Child achieves recognition in music, art, or other extracurricular activity.",
            BaseProbability = 0.30,
            MinAge = 7,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            StressImpact = -5,
            MoodImpact = 15,
            SocialBelongingImpact = 10,
            ResilienceImpact = 10,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.2),
                new InfluenceFactor("IncomeLevel", 1.1)
            ]
        },
        new GenericEvent
        {
            Id = "school_family_financial_stress",
            Name = "Family Financial Stress",
            Description = "Family experiences period of financial difficulty affecting daily life.",
            BaseProbability = 0.25,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            StressImpact = 15,
            MoodImpact = -10,
            SocialBelongingImpact = -5,
            ResilienceImpact = 5,
            HealthImpact = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -4.0),
                new InfluenceFactor("JobStatus", -3.5),
                new InfluenceFactor("ParentsEducationLevel", -2.0),
                new InfluenceFactor("ParentsWithAddiction", 2.5)
            ]
        },
        new GenericEvent
        {
            Id = "school_summer_camp",
            Name = "Summer Camp Experience",
            Description = "Child attends summer camp, developing independence and social skills.",
            BaseProbability = 0.35,
            MinAge = 7,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            StressImpact = 3,
            MoodImpact = 12,
            SocialBelongingImpact = 12,
            ResilienceImpact = 10,
            HealthImpact = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.3),
                new InfluenceFactor("SocialEnergyLevel", 1.1)
            ]
        },
        new GenericEvent
        {
            Id = "school_natural_disaster",
            Name = "Natural Disaster Impact",
            Description = "Community experiences a natural disaster affecting daily life.",
            BaseProbability = 0.08,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            StressImpact = 25,
            MoodImpact = -15,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = -10,
            InfluenceFactors = []
        },
        new GenericEvent
        {
            Id = "school_technology_access",
            Name = "Technology Access Gained",
            Description = "Child gains access to personal technology (tablet, computer) for learning.",
            BaseProbability = 0.60,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            StressImpact = -3,
            MoodImpact = 8,
            SocialBelongingImpact = 5,
            ResilienceImpact = 0,
            HealthImpact = -2,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.4),
                new InfluenceFactor("ParentsEducationLevel", 1.2)
            ]
        },
        new GenericEvent
        {
            Id = "school_grandparent_loss",
            Name = "Loss of Grandparent",
            Description = "Child experiences the death of a grandparent, confronting mortality.",
            BaseProbability = 0.20,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            StressImpact = 20,
            MoodImpact = -18,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = -3,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.3)
            ]
        },
        new GenericEvent
        {
            Id = "school_birthday_celebration",
            Name = "Memorable Birthday Celebration",
            Description = "Child has a particularly memorable and joyful birthday celebration.",
            BaseProbability = 0.40,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            StressImpact = -8,
            MoodImpact = 15,
            SocialBelongingImpact = 12,
            ResilienceImpact = 3,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.2),
                new InfluenceFactor("IncomeLevel", 1.1)
            ]
        }
    ];

    #endregion

    #region Aggregated Event Collections

    /// <summary>
    /// All generic events for the School Beginning phase.
    /// </summary>
    public static IReadOnlyList<GenericEvent> AllGenericEvents { get; } = new ReadOnlyCollection<GenericEvent>(
    [
        ..SchoolBeginningGenericEvents
    ]);

    /// <summary>
    /// All personal events for the School Beginning phase.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> AllPersonalEvents { get; } = new ReadOnlyCollection<PersonalEvent>(
    [
        ..SchoolBeginningPersonalEvents
    ]);

    #endregion
}