using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.Data;

public static class SchoolBeginningEvents
{
    private const int SCHOOL_BEGINNING_MIN = 6;
    private const int SCHOOL_BEGINNING_MAX = 11;

    #region School Beginning Personal Events (Ages 6-12)

    /// <summary>
    ///     Personal events specific to School Beginning phase (ages 6-12).
    ///     Focus on peer relationships, academic development, and social skills.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> SchoolBeginningPersonalEvents { get; } =
    [
        new()
        {
            Id = "school_best_friend",
            Name = "Beste Freundschaft geknüpft",
            Description = "Das Kind entwickelt eine enge Freundschaft mit gegenseitigem Vertrauen und Unterstützung.",
            VisualCategory = VisualCategory.Social,
            BaseProbability = 0.60,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            IsTraumatic = false,
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
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("HasAutism", -1.0)
            ]
        },
        new()
        {
            Id = "school_academic_achievement",
            Name = "Schulischer Erfolg",
            Description =
                "Das Kind erzielt bemerkenswerte schulische Leistungen und gewinnt Selbstvertrauen beim Lernen.",
            VisualCategory = VisualCategory.Achievement,
            BaseProbability = 0.45,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            IsTraumatic = false,
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
                new InfluenceFactor("IncomeLevel", 1.5),
                new InfluenceFactor("HasAdhd", -1.5),
                new InfluenceFactor("GenderFemale", 1.2)
            ]
        },
        new()
        {
            Id = "school_sports_talent",
            Name = "Sportliches Talent entdeckt",
            Description =
                "Das Kind zeigt natürliche sportliche Fähigkeiten und findet Freude an körperlichen Aktivitäten.",
            VisualCategory = VisualCategory.Sports,
            BaseProbability = 0.35,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 12,
            SocialBelongingImpact = 10,
            ResilienceImpact = 10,
            HealthImpact = 10,
            AnxietyChange = -5,
            SocialEnergyChange = 8,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 2.0),
                new InfluenceFactor("SocialEnvironmentLevel", 1.8),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("IncomeLevel", 1.3),
                new InfluenceFactor("GenderMale", 1.2),
                new InfluenceFactor("HasAdhd", 1.3)
            ]
        },
        new()
        {
            Id = "school_team_membership",
            Name = "Team- oder Gruppenmitgliedschaft",
            Description = "Das Kind tritt einem Team oder einer Gruppe bei und lernt Zusammenarbeit und Zugehörigkeit.",
            VisualCategory = VisualCategory.Social,
            BaseProbability = 0.50,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -3,
            MoodImpact = 10,
            SocialBelongingImpact = 15,
            ResilienceImpact = 5,
            HealthImpact = 5,
            AnxietyChange = -5,
            SocialEnergyChange = 8,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 2.0),
                new InfluenceFactor("SocialEnergyLevel", 1.8),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("IncomeLevel", 1.3)
            ]
        },
        new()
        {
            Id = "school_leadership_role",
            Name = "Führungsrolle übernommen",
            Description =
                "Das Kind übernimmt eine Führungsrolle unter Gleichaltrigen (Klassensprecher, Mannschaftskapitän).",
            VisualCategory = VisualCategory.Achievement,
            BaseProbability = 0.20,
            MinAge = 8,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 5,
            MoodImpact = 12,
            SocialBelongingImpact = 12,
            ResilienceImpact = 10,
            HealthImpact = 0,
            AnxietyChange = -3,
            SocialEnergyChange = 10,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 2.5),
                new InfluenceFactor("IntelligenceScore", 1.8),
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("SocialEnvironmentLevel", 1.5),
                new InfluenceFactor("AnxietyLevel", -1.5)
            ]
        },
        new()
        {
            Id = "school_bullying_victim",
            Name = "Mobbing-Erfahrung",
            Description = "Das Kind wird von Mitschülern gemobbt, was zu emotionaler Belastung führt.",
            VisualCategory = VisualCategory.Trauma,
            BaseProbability = 0.30,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            IsTraumatic = true,
            StressImpact = 30,
            MoodImpact = -30,
            SocialBelongingImpact = -28,
            ResilienceImpact = -12,
            HealthImpact = -8,
            AnxietyChange = 28,
            SocialEnergyChange = -22,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", -2.5),
                new InfluenceFactor("HasAutism", 3.0),
                new InfluenceFactor("HasAdhd", 2.5),
                new InfluenceFactor("FamilyCloseness", -2.0),
                new InfluenceFactor("IncomeLevel", -2.0),
                new InfluenceFactor("SocialEnvironmentLevel", -2.0),
                new InfluenceFactor("GenderMale", 1.4)
            ]
        },
        new()
        {
            Id = "school_learning_difficulty",
            Name = "Lernschwierigkeit festgestellt",
            Description = "Eine Lernbesonderheit wird identifiziert und erfordert zusätzliche Unterstützung.",
            VisualCategory = VisualCategory.Education,
            BaseProbability = 0.15,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = 9,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 20,
            MoodImpact = -18,
            SocialBelongingImpact = -15,
            ResilienceImpact = 3,
            HealthImpact = 0,
            AnxietyChange = 18,
            SocialEnergyChange = -10,
            InfluenceFactors =
            [
                new InfluenceFactor("HasAdhd", 4.0),
                new InfluenceFactor("HasAutism", 3.5),
                new InfluenceFactor("IntelligenceScore", -2.0),
                new InfluenceFactor("ParentsEducationLevel", -1.5),
                new InfluenceFactor("GenderMale", 1.6)
            ]
        },
        new()
        {
            Id = "school_social_rejection",
            Name = "Soziale Ablehnung erlebt",
            Description = "Das Kind erfährt Ausgrenzung oder Ablehnung durch die Gruppe der Gleichaltrigen.",
            VisualCategory = VisualCategory.Social,
            BaseProbability = 0.35,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 22,
            MoodImpact = -22,
            SocialBelongingImpact = -25,
            ResilienceImpact = -8,
            HealthImpact = -3,
            AnxietyChange = 20,
            SocialEnergyChange = -18,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", -2.5),
                new InfluenceFactor("AnxietyLevel", 2.5),
                new InfluenceFactor("HasAutism", 2.5),
                new InfluenceFactor("HasAdhd", 2.0),
                new InfluenceFactor("IncomeLevel", -1.8),
                new InfluenceFactor("FamilyCloseness", -1.5),
                new InfluenceFactor("GenderFemale", 1.4)
            ]
        },
        new()
        {
            Id = "school_hobby_discovery",
            Name = "Sinnvolles Hobby entdeckt",
            Description = "Das Kind entdeckt ein Hobby, das Freude und Erfolgserlebnisse vermittelt.",
            VisualCategory = VisualCategory.Leisure,
            BaseProbability = 0.55,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -8,
            MoodImpact = 12,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = 3,
            AnxietyChange = -8,
            SocialEnergyChange = 3,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.8),
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("IncomeLevel", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.5)
            ]
        },
        new()
        {
            Id = "school_custody_adjustment",
            Name = "Umgang mit Sorgerechtsregelung",
            Description = "Das Kind passt sich nach der Trennung der Eltern an die neuen Lebensumstände an.",
            VisualCategory = VisualCategory.Trauma,
            BaseProbability = 0.80,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            IsTraumatic = true,
            StressImpact = 25,
            MoodImpact = -22,
            SocialBelongingImpact = -20,
            ResilienceImpact = 3,
            HealthImpact = -5,
            AnxietyChange = 22,
            SocialEnergyChange = -18,
            Prerequisites = ["childhood_parents_divorce"],
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", -2.5),
                new InfluenceFactor("AnxietyLevel", 2.5),
                new InfluenceFactor("SocialEnvironmentLevel", -2.0),
                new InfluenceFactor("ParentsWithAddiction", 2.0)
            ]
        },
        new()
        {
            Id = "school_relational_aggression",
            Name = "Beziehungsaggression erlebt",
            Description = "Kind erlebt subtile soziale Ausgrenzung, Gerüchte oder Manipulation durch Gleichaltrige.",
            VisualCategory = VisualCategory.Social,
            BaseProbability = 0.30,
            MinAge = 7,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            IsTraumatic = true,
            StressImpact = 25,
            MoodImpact = -25,
            SocialBelongingImpact = -28,
            ResilienceImpact = -8,
            HealthImpact = -5,
            AnxietyChange = 22,
            SocialEnergyChange = -18,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 2.0),
                new InfluenceFactor("SocialEnvironmentLevel", -2.0),
                new InfluenceFactor("FamilyCloseness", -1.8),
                new InfluenceFactor("GenderFemale", 1.8)
            ]
        },
        new()
        {
            Id = "school_emotional_suppression",
            Name = "Druck zur emotionalen Unterdrückung",
            Description = "Kind lernt, Emotionen zu unterdrücken aufgrund sozialer Erwartungen an sein Geschlecht.",
            VisualCategory = VisualCategory.MentalHealth,
            BaseProbability = 0.35,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 15,
            MoodImpact = -12,
            SocialBelongingImpact = -8,
            ResilienceImpact = -10,
            HealthImpact = -5,
            AnxietyChange = 15,
            SocialEnergyChange = -10,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", -2.0),
                new InfluenceFactor("ParentsEducationLevel", -1.5),
                new InfluenceFactor("FamilyCloseness", -1.5),
                new InfluenceFactor("GenderMale", 1.8)
            ]
        },
        new()
        {
            Id = "school_reading_excellence",
            Name = "Herausragende Lesekompetenz",
            Description = "Kind zeigt überdurchschnittliche Lesefähigkeiten und Freude an Literatur.",
            VisualCategory = VisualCategory.Education,
            BaseProbability = 0.25,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = 9,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 12,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = 0,
            AnxietyChange = -5,
            SocialEnergyChange = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 2.5),
                new InfluenceFactor("ParentsEducationLevel", 2.2),
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("GenderFemale", 1.4)
            ]
        },
        new()
        {
            Id = "school_stem_encouragement",
            Name = "MINT-Förderung erhalten",
            Description =
                "Kind erhält besondere Ermutigung in Mathematik, Informatik, Naturwissenschaften oder Technik.",
            VisualCategory = VisualCategory.Education,
            BaseProbability = 0.30,
            MinAge = 7,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -3,
            MoodImpact = 10,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = 0,
            AnxietyChange = -3,
            SocialEnergyChange = 3,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 2.0),
                new InfluenceFactor("ParentsEducationLevel", 2.0),
                new InfluenceFactor("IncomeLevel", 1.5),
                new InfluenceFactor("GenderMale", 1.3)
            ]
        },
        new()
        {
            Id = "school_gender_nonconformity_challenge",
            Name = "Geschlechtsuntypische Interessen",
            Description =
                "Kind zeigt Interessen oder Verhaltensweisen, die nicht den Geschlechternormen entsprechen, und erfährt gemischte Reaktionen.",
            VisualCategory = VisualCategory.Identity,
            BaseProbability = 0.25,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 15,
            MoodImpact = -5,
            SocialBelongingImpact = -10,
            ResilienceImpact = 8,
            HealthImpact = 0,
            AnxietyChange = 10,
            SocialEnergyChange = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", -2.0),
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("GenderNonBinary", 2.5)
            ]
        },
        new()
        {
            Id = "school_sports_exclusion",
            Name = "Sportliche Ausgrenzung",
            Description =
                "Kind wird von sportlichen Aktivitäten ausgeschlossen oder entmutigt aufgrund von Geschlechtererwartungen.",
            VisualCategory = VisualCategory.Sports,
            BaseProbability = 0.20,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 12,
            MoodImpact = -15,
            SocialBelongingImpact = -12,
            ResilienceImpact = -5,
            HealthImpact = -5,
            AnxietyChange = 10,
            SocialEnergyChange = -8,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", -2.0),
                new InfluenceFactor("SocialEnergyLevel", -1.5),
                new InfluenceFactor("GenderFemale", 1.5)
            ]
        }
    ];

    #endregion

    #region School Beginning Generic Events (Ages 6-12)

    /// <summary>
    ///     Generic events that can occur during School Beginning phase (ages 6-12).
    /// </summary>
    public static IReadOnlyList<GenericEvent> SchoolBeginningGenericEvents { get; } =
    [
        new()
        {
            Id = "school_field_trip",
            Name = "Schulausflug",
            Description = "Die Klasse nimmt an einem lehrreichen Ausflug teil.",
            BaseProbability = 0.65,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 12,
            SocialBelongingImpact = 8,
            ResilienceImpact = 2,
            HealthImpact = 2,
            VisualCategory = VisualCategory.Leisure,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.8),
                new InfluenceFactor("SocialEnvironmentLevel", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3)
            ]
        },
        new()
        {
            Id = "school_first_day",
            Name = "Erster Schultag",
            Description = "Das Kind erlebt den Meilenstein des ersten Grundschultags.",
            BaseProbability = 0.95,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = 7,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 12,
            MoodImpact = 5,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Education,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("AnxietyLevel", -1.5),
                new InfluenceFactor("SocialEnvironmentLevel", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3)
            ]
        },
        new()
        {
            Id = "school_teacher_positive",
            Name = "Positive Lehrerbeziehung",
            Description = "Das Kind entwickelt eine positive Beziehung zu einer ermutigenden Lehrkraft.",
            BaseProbability = 0.50,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -8,
            MoodImpact = 12,
            SocialBelongingImpact = 10,
            ResilienceImpact = 8,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Education,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 2.0),
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("SocialEnergyLevel", 1.5),
                new InfluenceFactor("AnxietyLevel", -1.3),
                new InfluenceFactor("GenderFemale", 1.2)
            ]
        },
        new()
        {
            Id = "school_extracurricular_success",
            Name = "Außerschulischer Erfolg",
            Description = "Das Kind erhält Anerkennung in Musik, Kunst oder anderen außerschulischen Aktivitäten.",
            BaseProbability = 0.30,
            MinAge = 7,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 15,
            SocialBelongingImpact = 10,
            ResilienceImpact = 10,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Achievement,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 2.0),
                new InfluenceFactor("IncomeLevel", 2.0),
                new InfluenceFactor("ParentsEducationLevel", 1.8),
                new InfluenceFactor("FamilyCloseness", 1.5)
            ]
        },
        new()
        {
            Id = "school_family_financial_stress",
            Name = "Finanzielle Belastung Familie",
            Description =
                "Die Familie durchlebt eine Phase finanzieller Schwierigkeiten, die den Alltag beeinträchtigen.",
            BaseProbability = 0.25,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 15,
            MoodImpact = -10,
            SocialBelongingImpact = -5,
            ResilienceImpact = 5,
            HealthImpact = -5,
            VisualCategory = VisualCategory.Financial,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -4.0),
                new InfluenceFactor("JobStatus", -3.5),
                new InfluenceFactor("ParentsEducationLevel", -2.0),
                new InfluenceFactor("ParentsWithAddiction", 2.5)
            ]
        },
        new()
        {
            Id = "school_summer_camp",
            Name = "Ferienlager-Erlebnis",
            Description =
                "Das Kind besucht ein Ferienlager und entwickelt Selbstständigkeit sowie soziale Kompetenzen.",
            BaseProbability = 0.35,
            MinAge = 7,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 3,
            MoodImpact = 12,
            SocialBelongingImpact = 12,
            ResilienceImpact = 10,
            HealthImpact = 5,
            VisualCategory = VisualCategory.Leisure,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 2.5),
                new InfluenceFactor("SocialEnergyLevel", 2.0),
                new InfluenceFactor("FamilyCloseness", 1.8),
                new InfluenceFactor("AnxietyLevel", -1.5)
            ]
        },
        new()
        {
            Id = "school_natural_disaster",
            Name = "Naturkatastrophe erlebt",
            Description = "Die Gemeinschaft erlebt eine Naturkatastrophe, die den Alltag beeinträchtigt.",
            BaseProbability = 0.08,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            IsTraumatic = true,
            StressImpact = 25,
            MoodImpact = -15,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = -10,
            VisualCategory = VisualCategory.Nature,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", -2.0),
                new InfluenceFactor("IncomeLevel", -1.8)
            ]
        },
        new()
        {
            Id = "school_technology_access",
            Name = "Zugang zu Technologie",
            Description = "Das Kind erhält Zugang zu persönlicher Technologie (Tablet, Computer) zum Lernen.",
            BaseProbability = 0.60,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -3,
            MoodImpact = 8,
            SocialBelongingImpact = 5,
            ResilienceImpact = 0,
            HealthImpact = -2,
            VisualCategory = VisualCategory.Education,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 2.5),
                new InfluenceFactor("ParentsEducationLevel", 2.0)
            ]
        },
        new()
        {
            Id = "school_grandparent_loss",
            Name = "Verlust eines Großelternteils",
            Description = "Das Kind erlebt den Tod eines Großelternteils und wird mit Sterblichkeit konfrontiert.",
            BaseProbability = 0.20,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = true,
            IsTraumatic = true,
            StressImpact = 20,
            MoodImpact = -18,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = -3,
            VisualCategory = VisualCategory.Death,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 2.0)
            ]
        },
        new()
        {
            Id = "school_birthday_celebration",
            Name = "Unvergessliche Geburtstagsfeier",
            Description = "Das Kind erlebt eine besonders schöne und freudige Geburtstagsfeier.",
            BaseProbability = 0.40,
            MinAge = SCHOOL_BEGINNING_MIN,
            MaxAge = SCHOOL_BEGINNING_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -8,
            MoodImpact = 15,
            SocialBelongingImpact = 12,
            ResilienceImpact = 3,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Leisure,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 2.0),
                new InfluenceFactor("IncomeLevel", 1.8),
                new InfluenceFactor("SocialEnvironmentLevel", 1.5)
            ]
        }
    ];

    #endregion

    #region Aggregated Event Collections

    /// <summary>
    ///     All generic events for the School Beginning phase.
    /// </summary>
    public static IReadOnlyList<GenericEvent> AllGenericEvents { get; } = new ReadOnlyCollection<GenericEvent>(
    [
        ..SchoolBeginningGenericEvents
    ]);

    /// <summary>
    ///     All personal events for the School Beginning phase.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> AllPersonalEvents { get; } = new ReadOnlyCollection<PersonalEvent>(
    [
        ..SchoolBeginningPersonalEvents
    ]);

    #endregion
}