using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.Data;

/// <summary>
///     Static catalog containing all predefined life events, organized by life phase.
///     Events are based on developmental psychology research including Attachment Theory (Bowlby),
///     Life-Span Development (Erikson), and the Stress-Diathesis Model.
/// </summary>
public static class CopingMechanism
{
    #region Coping Mechanisms

    private static readonly List<EventTypes.CopingMechanism> CopingMechanismsList =
    [
        // Functional Coping Mechanisms
        new()
        {
            Id = "coping_exercise",
            Name = "Körperliche Bewegung",
            Description =
                "Regelmäßige körperliche Aktivität zur Stressbewältigung. Sport fördert die Ausschüttung von Endorphinen und verbessert das allgemeine Wohlbefinden.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingFunctional,
            BaseProbability = 0.40,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(50),
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
        new()
        {
            Id = "coping_social_support",
            Name = "Soziale Unterstützung suchen",
            Description =
                "Sich an Freunde oder Familie wenden, um emotionale Unterstützung zu erhalten. Der Austausch mit nahestehenden Personen kann Stress reduzieren und das Wohlbefinden steigern.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingFunctional,
            BaseProbability = 0.45,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(moodThreshold: -20, belongingThreshold: 40),
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
        new()
        {
            Id = "coping_mindfulness",
            Name = "Achtsamkeitspraxis",
            Description =
                "Meditation oder Achtsamkeitstechniken praktizieren, um innere Ruhe zu finden. Diese Methoden fördern die emotionale Regulation und reduzieren Stressreaktionen.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingFunctional,
            BaseProbability = 0.25,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(60),
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
        new()
        {
            Id = "coping_creative_expression",
            Name = "Kreativer Ausdruck",
            Description =
                "Kunst, Musik oder Schreiben nutzen, um Emotionen zu verarbeiten. Kreativität bietet einen gesunden Kanal für emotionale Ausdrucksformen.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingFunctional,
            BaseProbability = 0.2,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(moodThreshold: -30),
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
        new()
        {
            Id = "coping_problem_solving",
            Name = "Aktive Problemlösung",
            Description =
                "Die Stressquelle systematisch angehen und lösen. Diese proaktive Strategie stärkt das Gefühl von Kontrolle und Selbstwirksamkeit.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingFunctional,
            BaseProbability = 0.40,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(45),
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
        new()
        {
            Id = "coping_avoidance",
            Name = "Vermeidung und Rückzug",
            Description =
                "Stressauslöser meiden und sich aus sozialen Situationen zurückziehen. Dies bringt kurzfristige Erleichterung, verstärkt jedoch langfristig Ängste und Isolation.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingDysfunctional,
            BaseProbability = 0.45,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Dysfunctional,
            Trigger = new CopingTrigger(55),
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
        new()
        {
            Id = "coping_substance_use",
            Name = "Substanzkonsum",
            Description =
                "Alkohol oder andere Substanzen zur Stressbewältigung nutzen. Dies führt zu kurzfristiger Betäubung, birgt jedoch erhebliche Risiken für Abhängigkeit und Gesundheit.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingDysfunctional,
            BaseProbability = 0.4,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Dysfunctional,
            Trigger = new CopingTrigger(70, -40),
            IsHabitForming = true,
            StressImpact = -20,
            MoodImpact = 8,
            SocialBelongingImpact = -5,
            ResilienceImpact = -15,
            HealthImpact = -15,
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsWithAddiction", 3.5),
                new InfluenceFactor("AnxietyLevel", 1.7),
                new InfluenceFactor("FamilyCloseness", -1.8)
            ]
        },
        new()
        {
            Id = "coping_rumination",
            Name = "Grübeln",
            Description =
                "Wiederkehrendes negatives Denken über Probleme und Gefühle. Diese Gedankenschleifen verstärken negative Emotionen und beeinträchtigen die Problemlösefähigkeit.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingDysfunctional,
            BaseProbability = 0.50,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Dysfunctional,
            Trigger = new CopingTrigger(moodThreshold: -25),
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
        new()
        {
            Id = "coping_emotional_eating",
            Name = "Emotionales Essen",
            Description =
                "Nahrung zur Bewältigung negativer Emotionen nutzen. Diese Strategie bietet kurzfristige Beruhigung, kann jedoch zu Essstörungen und gesundheitlichen Problemen führen.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingDysfunctional,
            BaseProbability = 0.40,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Dysfunctional,
            Trigger = new CopingTrigger(50, -20),
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
        new()
        {
            Id = "coping_aggression",
            Name = "Aggressives Verhalten",
            Description =
                "Frustration durch verbale oder körperliche Aggression ausdrücken. Dies schädigt zwischenmenschliche Beziehungen und verschlimmert langfristig das emotionale Befinden.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingDysfunctional,
            BaseProbability = 0.30,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Dysfunctional,
            Trigger = new CopingTrigger(75),
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
        new()
        {
            Id = "coping_distraction",
            Name = "Ablenkungsaktivitäten",
            Description =
                "Unterhaltung oder Aktivitäten nutzen, um sich vom Stress abzulenken. Diese Strategie bietet zeitweilige Entlastung ohne langfristige positive oder negative Effekte.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingNeutral,
            BaseProbability = 0.60,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Neutral,
            Trigger = new CopingTrigger(40),
            IsHabitForming = false,
            StressImpact = -10,
            MoodImpact = 8,
            SocialBelongingImpact = 0,
            ResilienceImpact = 0,
            HealthImpact = -2,
            InfluenceFactors = []
        },
        new()
        {
            Id = "coping_sleep",
            Name = "Ruhe und Schlaf",
            Description =
                "Schlaf als Flucht oder zur Erholung von Stress nutzen. Ausreichender Schlaf unterstützt die Regeneration, exzessiver Schlaf kann jedoch zur Vermeidung werden.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingNeutral,
            BaseProbability = 0.55,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Neutral,
            Trigger = new CopingTrigger(55),
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
        new()
        {
            Id = "coping_humor",
            Name = "Humor und Lachen",
            Description =
                "Humor nutzen, um die Stimmung und Perspektive aufzuhellen. Lachen kann Spannungen lösen und das soziale Miteinander stärken.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingNeutral,
            BaseProbability = 0.45,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Neutral,
            Trigger = new CopingTrigger(moodThreshold: -15),
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
        new()
        {
            Id = "coping_nature",
            Name = "Natur und Draußensein",
            Description =
                "Zeit in der Natur verbringen, um sich zu entspannen. Naturerlebnisse fördern das psychische Wohlbefinden und reduzieren Stresshormone.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingFunctional,
            BaseProbability = 0.35,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(45),
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
        new()
        {
            Id = "coping_journaling",
            Name = "Tagebuch schreiben",
            Description =
                "Gedanken und Gefühle niederschreiben, um Emotionen zu verarbeiten. Journaling fördert Selbstreflexion und hilft, Gedankenmuster zu erkennen.",
            Category = EventCategory.Coping,
            VisualCategory = VisualCategory.CopingFunctional,
            BaseProbability = 0.30,
            MaxAge = 100,
            IsUnique = false,
            IsTraumatic = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(moodThreshold: -25, stressThreshold: 50),
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
    ///     All coping mechanisms available in the simulation.
    /// </summary>
    public static IReadOnlyList<EventTypes.CopingMechanism> AllcopingMechanisms { get; } =
        new ReadOnlyCollection<EventTypes.CopingMechanism>(
        [
            ..CopingMechanismsList
        ]);

    #endregion
}