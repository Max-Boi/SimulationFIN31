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

    public static IReadOnlyList<PersonalEvent> AdolescencePersonalEvents { get; } =
    [
        new()
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
        new()
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
        new()
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
        new()
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
        new()
        {
            Id = "adolescence_creative_expression",
            Name = "Kreative Entfaltung",
            Description =
                "Teenager entwickelt starkes kreatives Ventil (Musik, Kunst, Schreiben) zur Selbstentfaltung.",
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
        new()
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
                new InfluenceFactor("SocialEnvironmentLevel", -1.5),
                new InfluenceFactor("GenderFemale", 1.4)
            ]
        },
        new()
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
                new InfluenceFactor("ParentsEducationLevel", -1.5),
                new InfluenceFactor("GenderMale", 1.4)
            ]
        },
        new()
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
                new InfluenceFactor("IncomeLevel", -2.0),
                new InfluenceFactor("GenderFemale", 1.8)
            ]
        },
        new()
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
                new InfluenceFactor("IncomeLevel", 1.5),
                new InfluenceFactor("GenderMale", 1.3)
            ]
        },
        new()
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
                new InfluenceFactor("SocialEnvironmentLevel", -1.8),
                new InfluenceFactor("GenderFemale", 1.5)
            ]
        },
        new()
        {
            Id = "adolescence_menarche",
            Name = "Erste Menstruation",
            Description = "Teenager erlebt die Menarche und den Übergang in die körperliche Reife.",
            BaseProbability = 0.95,
            MinAge = 10,
            MaxAge = 15,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 12,
            MoodImpact = -5,
            SocialBelongingImpact = 0,
            ResilienceImpact = 5,
            HealthImpact = -3,
            AnxietyChange = 10,
            SocialEnergyChange = -5,
            VisualCategory = VisualCategory.Health,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("GenderFemale", 10.0)
            ]
        },
        new()
        {
            Id = "adolescence_voice_change",
            Name = "Stimmbruch",
            Description = "Teenager durchlebt den Stimmbruch als Teil der pubertären Entwicklung.",
            BaseProbability = 0.95,
            MinAge = 11,
            MaxAge = 15,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 8,
            MoodImpact = -3,
            SocialBelongingImpact = -5,
            ResilienceImpact = 3,
            HealthImpact = 0,
            AnxietyChange = 8,
            SocialEnergyChange = -5,
            VisualCategory = VisualCategory.Health,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 1.3),
                new InfluenceFactor("GenderMale", 10.0)
            ]
        },
        new()
        {
            Id = "adolescence_body_image_concerns",
            Name = "Körperbildprobleme",
            Description = "Teenager entwickelt negative Einstellung zum eigenen Körper und Aussehen.",
            BaseProbability = 0.45,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 22,
            MoodImpact = -25,
            SocialBelongingImpact = -15,
            ResilienceImpact = -10,
            HealthImpact = -8,
            AnxietyChange = 25,
            SocialEnergyChange = -15,
            VisualCategory = VisualCategory.MentalHealth,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 3.0),
                new InfluenceFactor("SocialEnvironmentLevel", -2.0),
                new InfluenceFactor("FamilyCloseness", -2.0),
                new InfluenceFactor("GenderFemale", 2.0)
            ]
        },
        new()
        {
            Id = "adolescence_eating_disorder_onset",
            Name = "Essstörung entwickelt",
            Description = "Teenager entwickelt Symptome einer Essstörung als Reaktion auf Körperbildprobleme.",
            BaseProbability = 0.08,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            IsTraumatic = true,
            StressImpact = 35,
            MoodImpact = -40,
            SocialBelongingImpact = -25,
            ResilienceImpact = -20,
            HealthImpact = -30,
            AnxietyChange = 35,
            SocialEnergyChange = -25,
            VisualCategory = VisualCategory.MentalHealth,
            Prerequisites = ["adolescence_body_image_concerns"],
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 4.0),
                new InfluenceFactor("FamilyCloseness", -3.5),
                new InfluenceFactor("ParentsWithAddiction", 3.0),
                new InfluenceFactor("GenderFemale", 2.5)
            ]
        },
        new()
        {
            Id = "adolescence_sexual_harassment",
            Name = "Sexuelle Belästigung erlebt",
            Description = "Teenager erlebt ungewollte sexuelle Aufmerksamkeit, Kommentare oder Berührungen.",
            BaseProbability = 0.25,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            IsTraumatic = true,
            StressImpact = 35,
            MoodImpact = -35,
            SocialBelongingImpact = -25,
            ResilienceImpact = -15,
            HealthImpact = -10,
            AnxietyChange = 35,
            SocialEnergyChange = -25,
            VisualCategory = VisualCategory.Trauma,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", -2.5),
                new InfluenceFactor("FamilyCloseness", -2.0),
                new InfluenceFactor("GenderFemale", 2.2)
            ]
        },
        new()
        {
            Id = "adolescence_risky_behavior",
            Name = "Riskantes Verhalten",
            Description =
                "Teenager engagiert sich in riskantem Verhalten wie gefährliche Mutproben oder illegale Aktivitäten.",
            BaseProbability = 0.30,
            MinAge = 13,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 15,
            MoodImpact = 5,
            SocialBelongingImpact = 10,
            ResilienceImpact = -5,
            HealthImpact = -15,
            AnxietyChange = -5,
            SocialEnergyChange = 8,
            VisualCategory = VisualCategory.Social,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 2.0),
                new InfluenceFactor("ParentsWithAddiction", 3.0),
                new InfluenceFactor("FamilyCloseness", -2.5),
                new InfluenceFactor("SocialEnvironmentLevel", -2.0),
                new InfluenceFactor("GenderMale", 1.8)
            ]
        },
        new()
        {
            Id = "adolescence_early_puberty_distress",
            Name = "Frühe Pubertät belastend",
            Description = "Teenager erlebt sehr frühe körperliche Entwicklung und fühlt sich dadurch unwohl.",
            BaseProbability = 0.20,
            MinAge = 10,
            MaxAge = 13,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 22,
            MoodImpact = -20,
            SocialBelongingImpact = -18,
            ResilienceImpact = -8,
            HealthImpact = -5,
            AnxietyChange = 22,
            SocialEnergyChange = -15,
            VisualCategory = VisualCategory.Health,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 2.5),
                new InfluenceFactor("SocialEnvironmentLevel", -2.0),
                new InfluenceFactor("FamilyCloseness", -1.8),
                new InfluenceFactor("GenderFemale", 1.8)
            ]
        },
        new()
        {
            Id = "adolescence_gender_identity_exploration",
            Name = "Geschlechtsidentität hinterfragen",
            Description = "Teenager erkundet und hinterfragt die eigene Geschlechtsidentität.",
            BaseProbability = 0.15,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 18,
            MoodImpact = -10,
            SocialBelongingImpact = -12,
            ResilienceImpact = 8,
            HealthImpact = 0,
            AnxietyChange = 18,
            SocialEnergyChange = -8,
            VisualCategory = VisualCategory.Identity,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.5),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("SocialEnvironmentLevel", 1.3),
                new InfluenceFactor("GenderNonBinary", 3.0)
            ]
        },
        new()
        {
            Id = "adolescence_gender_discrimination",
            Name = "Geschlechterdiskriminierung erlebt",
            Description = "Teenager erlebt Benachteiligung oder Einschränkungen aufgrund des Geschlechts.",
            BaseProbability = 0.35,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 18,
            MoodImpact = -18,
            SocialBelongingImpact = -12,
            ResilienceImpact = -5,
            HealthImpact = -3,
            AnxietyChange = 15,
            SocialEnergyChange = -8,
            VisualCategory = VisualCategory.Social,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", -2.5),
                new InfluenceFactor("ParentsEducationLevel", -1.5),
                new InfluenceFactor("GenderFemale", 1.5),
                new InfluenceFactor("GenderNonBinary", 2.0)
            ]
        },
        new()
        {
            Id = "adolescence_male_emotional_support",
            Name = "Emotionale Unterstützung erhalten",
            Description =
                "Männlicher Teenager erfährt ungewöhnlich gute emotionale Unterstützung und kann Gefühle zeigen.",
            BaseProbability = 0.25,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -15,
            MoodImpact = 18,
            SocialBelongingImpact = 15,
            ResilienceImpact = 15,
            HealthImpact = 5,
            AnxietyChange = -15,
            SocialEnergyChange = 10,
            VisualCategory = VisualCategory.MentalHealth,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 3.0),
                new InfluenceFactor("ParentsEducationLevel", 2.0),
                new InfluenceFactor("SocialEnvironmentLevel", 1.8),
                new InfluenceFactor("GenderMale", 1.5)
            ]
        }
    ];

    #endregion

    #region Adolescence Generic Events (Ages 12-18)

    /// <summary>
    ///     Generic events that can occur during Adolescence phase (ages 12-18).
    /// </summary>
    public static IReadOnlyList<GenericEvent> AdolescenceGenericEvents { get; } =
    [
        new()
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
            VisualCategory = VisualCategory.Education,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("SocialEnvironmentLevel", 1.5),
                new InfluenceFactor("AnxietyLevel", -1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3)
            ]
        },
        new()
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
            VisualCategory = VisualCategory.Career,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -2.0),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("SocialEnergyLevel", 1.5),
                new InfluenceFactor("SocialEnvironmentLevel", 1.3)
            ]
        },
        new()
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
            VisualCategory = VisualCategory.Achievement,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 2.0),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("SocialEnvironmentLevel", 1.3)
            ]
        },
        new()
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
            VisualCategory = VisualCategory.Education,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 2.0),
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("ParentsEducationLevel", 1.8),
                new InfluenceFactor("ParentsWithAddiction", -2.0)
            ]
        },
        new()
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
            VisualCategory = VisualCategory.Health,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("IncomeLevel", -1.5),
                new InfluenceFactor("ParentsWithAddiction", 2.0)
            ]
        },
        new()
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
            VisualCategory = VisualCategory.Community,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 2.0),
                new InfluenceFactor("SocialEnergyLevel", 1.8),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3)
            ]
        },
        new()
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
            VisualCategory = VisualCategory.Leisure,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 2.5),
                new InfluenceFactor("ParentsEducationLevel", 1.8),
                new InfluenceFactor("FamilyCloseness", 1.5)
            ]
        },
        new()
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
            VisualCategory = VisualCategory.Financial,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -4.0),
                new InfluenceFactor("JobStatus", -3.5),
                new InfluenceFactor("ParentsEducationLevel", -2.5),
                new InfluenceFactor("ParentsWithAddiction", 3.0)
            ]
        },
        new()
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
            VisualCategory = VisualCategory.Social,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 2.0),
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("SocialEnergyLevel", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3)
            ]
        },
        new()
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
            VisualCategory = VisualCategory.Community,
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
    ///     All generic events for the Adolescence phase.
    /// </summary>
    public static IReadOnlyList<GenericEvent> AllGenericEvents { get; } = new ReadOnlyCollection<GenericEvent>(
    [
        ..AdolescenceGenericEvents
    ]);

    /// <summary>
    ///     All personal events for the Adolescence phase.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> AllPersonalEvents { get; } = new ReadOnlyCollection<PersonalEvent>(
    [
        ..AdolescencePersonalEvents
    ]);

    #endregion
}