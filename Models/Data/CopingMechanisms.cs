using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.Data;

/// <summary>
/// Static catalog containing all predefined life events, organized by life phase.
/// Events are based on developmental psychology research including Attachment Theory (Bowlby),
/// Life-Span Development (Erikson), and the Stress-Diathesis Model.
/// </summary>
public static class CopingMechanism
{
    #region Coping Mechanisms

    private static readonly List<EventTypes.CopingMechanism> CopingMechanismsList =
    [
        // Functional Coping Mechanisms
        new EventTypes.CopingMechanism
        {
            Id = "coping_exercise",
            Name = "Physical Exercise",
            Description = "Engaging in regular physical activity to manage stress.",
            Category = EventCategory.Coping,
            BaseProbability = 0.40,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(stressThreshold: 50),
            ShortTermRelief = 0.6,
            LongTermImpactMultiplier = 1.3,
            IsHabitForming = true,
            StressImpact = -25,
            MoodImpact = 15,
            SocialBelongingImpact = 12,
            ResilienceImpact = 15,
            HealthImpact = 30,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 2.5),
                new InfluenceFactor("IncomeLevel", 1.5)
            ]
        },
        new EventTypes.CopingMechanism
        {
            Id = "coping_social_support",
            Name = "Seeking Social Support",
            Description = "Reaching out to friends or family for emotional support.",
            Category = EventCategory.Coping,
            BaseProbability = 0.45,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(moodThreshold: -20, belongingThreshold: 40),
            ShortTermRelief = 0.7,
            LongTermImpactMultiplier = 1.25,
            IsHabitForming = true,
            StressImpact = -30,
            MoodImpact = 25,
            SocialBelongingImpact = 25,
            ResilienceImpact = 15,
            HealthImpact = 3,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 3.0),
                new InfluenceFactor("FamilyCloseness", 2.5)
            ]
        },
        new EventTypes.CopingMechanism
        {
            Id = "coping_mindfulness",
            Name = "Mindfulness Practice",
            Description = "Practicing meditation or mindfulness techniques.",
            Category = EventCategory.Coping,
            BaseProbability = 0.25,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(stressThreshold: 60),
            ShortTermRelief = 0.5,
            LongTermImpactMultiplier = 1.5,
            IsHabitForming = true,
            StressImpact = -18,
            MoodImpact = 15,
            SocialBelongingImpact = 0,
            ResilienceImpact = 25,
            HealthImpact = 15,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 2.5)
            ]
        },
        new EventTypes.CopingMechanism
        {
            Id = "coping_creative_expression",
            Name = "Creative Expression",
            Description = "Using art, music, or writing to process emotions.",
            Category = EventCategory.Coping,
            BaseProbability = 0.2,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(moodThreshold: -30),
            ShortTermRelief = 0.55,
            LongTermImpactMultiplier = 1.2,
            IsHabitForming = true,
            StressImpact = -15,
            MoodImpact = 20,
            SocialBelongingImpact = 5,
            ResilienceImpact = 10,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.1),
                new InfluenceFactor("ParentsEducationLevel", 2.5)
            ]
        },
        new EventTypes.CopingMechanism
        {
            Id = "coping_problem_solving",
            Name = "Active Problem Solving",
            Description = "Systematically addressing the source of stress.",
            Category = EventCategory.Coping,
            BaseProbability = 0.40,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(stressThreshold: 45),
            ShortTermRelief = 0.45,
            LongTermImpactMultiplier = 1.4,
            IsHabitForming = true,
            StressImpact = -25,
            MoodImpact = 15,
            SocialBelongingImpact = 3,
            ResilienceImpact = 8,
            HealthImpact = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.8),
                new InfluenceFactor("IncomeLevel", 2.0)
            ]
        },

        // Dysfunctional Coping Mechanisms
        new EventTypes.CopingMechanism
        {
            Id = "coping_avoidance",
            Name = "Avoidance and Withdrawal",
            Description = "Avoiding stressors and withdrawing from social situations.",
            Category = EventCategory.Coping,
            BaseProbability = 0.45,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Dysfunctional,
            Trigger = new CopingTrigger(stressThreshold: 55),
            ShortTermRelief = 0.6,
            LongTermImpactMultiplier = 0.7,
            IsHabitForming = true,
            StressImpact = -8,
            MoodImpact = -5,
            SocialBelongingImpact = -12,
            ResilienceImpact = -8,
            HealthImpact = -3,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 1.4),
                new InfluenceFactor("SocialEnergyLevel", -2.5)
            ]
        },
        new EventTypes.CopingMechanism
        {
            Id = "coping_substance_use",
            Name = "Substance Use",
            Description = "Using alcohol or other substances to cope with stress.",
            Category = EventCategory.Coping,
            BaseProbability = 0.3,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Dysfunctional,
            Trigger = new CopingTrigger(stressThreshold: 70, moodThreshold: -40),
            ShortTermRelief = 0.75,
            LongTermImpactMultiplier = 0.5,
            IsHabitForming = true,
            StressImpact = -20,
            MoodImpact = 8,
            SocialBelongingImpact = -5,
            ResilienceImpact = -15,
            HealthImpact = -15,
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsWithAddiction", 4.0),
                new InfluenceFactor("AnxietyLevel", 1.7),
                new InfluenceFactor("FamilyCloseness", -0.8)
            ]
        },
        new EventTypes.CopingMechanism
        {
            Id = "coping_rumination",
            Name = "Rumination",
            Description = "Repetitive negative thinking about problems and feelings.",
            Category = EventCategory.Coping,
            BaseProbability = 0.50,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Dysfunctional,
            Trigger = new CopingTrigger(moodThreshold: -25),
            ShortTermRelief = 0.2,
            LongTermImpactMultiplier = 0.65,
            IsHabitForming = true,
            StressImpact = 10,
            MoodImpact = -15,
            SocialBelongingImpact = -5,
            ResilienceImpact = -10,
            HealthImpact = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 1.5),
                new InfluenceFactor("IntelligenceScore", 1.1)
            ]
        },
        new EventTypes.CopingMechanism
        {
            Id = "coping_emotional_eating",
            Name = "Emotional Eating",
            Description = "Using food to cope with negative emotions.",
            Category = EventCategory.Coping,
            BaseProbability = 0.40,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Dysfunctional,
            Trigger = new CopingTrigger(stressThreshold: 50, moodThreshold: -20),
            ShortTermRelief = 0.65,
            LongTermImpactMultiplier = 0.75,
            IsHabitForming = true,
            StressImpact = -10,
            MoodImpact = 5,
            SocialBelongingImpact = -3,
            ResilienceImpact = -5,
            HealthImpact = -10,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 1.2)
            ]
        },
        new EventTypes.CopingMechanism
        {
            Id = "coping_aggression",
            Name = "Aggressive Behavior",
            Description = "Expressing frustration through verbal or physical aggression.",
            Category = EventCategory.Coping,
            BaseProbability = 0.30,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Dysfunctional,
            Trigger = new CopingTrigger(stressThreshold: 75),
            ShortTermRelief = 0.5,
            LongTermImpactMultiplier = 0.6,
            IsHabitForming = true,
            StressImpact = -12,
            MoodImpact = -8,
            SocialBelongingImpact = -18,
            ResilienceImpact = -8,
            HealthImpact = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsWithAddiction", 1.3),
                new InfluenceFactor("FamilyCloseness", -0.8)
            ]
        },

        // Neutral Coping Mechanisms
        new EventTypes.CopingMechanism
        {
            Id = "coping_distraction",
            Name = "Distraction Activities",
            Description = "Engaging in entertainment or activities to take mind off stress.",
            Category = EventCategory.Coping,
            BaseProbability = 0.60,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Neutral,
            Trigger = new CopingTrigger(stressThreshold: 40),
            ShortTermRelief = 0.55,
            LongTermImpactMultiplier = 1.0,
            IsHabitForming = false,
            StressImpact = -10,
            MoodImpact = 8,
            SocialBelongingImpact = 0,
            ResilienceImpact = 0,
            HealthImpact = -2,
            InfluenceFactors = []
        },
        new EventTypes.CopingMechanism
        {
            Id = "coping_sleep",
            Name = "Rest and Sleep",
            Description = "Using sleep as a way to escape or recover from stress.",
            Category = EventCategory.Coping,
            BaseProbability = 0.55,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Neutral,
            Trigger = new CopingTrigger(stressThreshold: 55),
            ShortTermRelief = 0.5,
            LongTermImpactMultiplier = 1.0,
            IsHabitForming = false,
            StressImpact = -12,
            MoodImpact = 5,
            SocialBelongingImpact = -3,
            ResilienceImpact = 3,
            HealthImpact = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", -0.5)
            ]
        },
        new EventTypes.CopingMechanism
        {
            Id = "coping_humor",
            Name = "Humor and Laughter",
            Description = "Using humor to lighten mood and perspective.",
            Category = EventCategory.Coping,
            BaseProbability = 0.45,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Neutral,
            Trigger = new CopingTrigger(moodThreshold: -15),
            ShortTermRelief = 0.6,
            LongTermImpactMultiplier = 1.1,
            IsHabitForming = false,
            StressImpact = -8,
            MoodImpact = 12,
            SocialBelongingImpact = 8,
            ResilienceImpact = 5,
            HealthImpact = 2,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 1.2)
            ]
        },
        new EventTypes.CopingMechanism
        {
            Id = "coping_nature",
            Name = "Nature and Outdoor Time",
            Description = "Spending time in nature to decompress.",
            Category = EventCategory.Coping,
            BaseProbability = 0.35,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(stressThreshold: 45),
            ShortTermRelief = 0.5,
            LongTermImpactMultiplier = 1.15,
            IsHabitForming = true,
            StressImpact = -12,
            MoodImpact = 10,
            SocialBelongingImpact = 3,
            ResilienceImpact = 8,
            HealthImpact = 8,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 1.1)
            ]
        },
        new EventTypes.CopingMechanism
        {
            Id = "coping_journaling",
            Name = "Journaling",
            Description = "Writing about thoughts and feelings to process emotions.",
            Category = EventCategory.Coping,
            BaseProbability = 0.30,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(moodThreshold: -25, stressThreshold: 50),
            ShortTermRelief = 0.45,
            LongTermImpactMultiplier = 1.25,
            IsHabitForming = true,
            StressImpact = -10,
            MoodImpact = 8,
            SocialBelongingImpact = 0,
            ResilienceImpact = 12,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.2)
            ]
        }
    ];

    #endregion

    #region Aggregated Collections

    /// <summary>
    /// All coping mechanisms available in the simulation.
    /// </summary>
    public static IReadOnlyList<EventTypes.CopingMechanism> AllcopingMechanisms { get; } = new ReadOnlyCollection<EventTypes.CopingMechanism>(
    [
        ..CopingMechanismsList
    ]);

    #endregion
}