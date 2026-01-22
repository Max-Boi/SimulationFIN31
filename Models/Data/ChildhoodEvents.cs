using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.Data;

public static class ChildhoodEvents
{
    private const int CHILDHOOD_MIN = 0;
    private const int CHILDHOOD_MAX = 5;

    #region Childhood Personal Events (Ages 0-6)

    /// <summary>
    ///     Personal events specific to the Childhood phase (ages 0-6).
    ///     Based on Attachment Theory and early developmental milestones.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> ChildhoodPersonalEvents { get; } =
    [
        new()
        {
            Id = "childhood_secure_attachment",
            Name = "Sichere Bindung entwickelt",
            Description =
                "Kind entwickelt durch konsequente, einfühlsame Betreuung eine sichere Bindung zur primären Bezugsperson.",
            BaseProbability = 0.65,
            MinAge = CHILDHOOD_MIN,
            MaxAge = 3,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -10,
            MoodImpact = 15,
            SocialBelongingImpact = 20,
            ResilienceImpact = 15,
            HealthImpact = 5,
            AnxietyChange = -10,
            SocialEnergyChange = 5,
            VisualCategory = VisualCategory.Family,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.6),
                new InfluenceFactor("ParentsRelationshipQuality", 1.5),
                new InfluenceFactor("ParentsWithAddiction", -1.4),
                new InfluenceFactor("ParentsEducationLevel", 1.1)
            ]
        },
        new()
        {
            Id = "childhood_first_friendship",
            Name = "Erste wahre Freundschaft",
            Description =
                "Kind schließt seine erste bedeutsame Freundschaft mit einem Gleichaltrigen und lernt Gegenseitigkeit und Teilen.",
            BaseProbability = 0.70,
            MinAge = 3,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 12,
            SocialBelongingImpact = 15,
            ResilienceImpact = 5,
            HealthImpact = 0,
            AnxietyChange = -5,
            SocialEnergyChange = 8,
            VisualCategory = VisualCategory.Social,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 1.5),
                new InfluenceFactor("FamilyCloseness", 1.3),
                new InfluenceFactor("SocialEnergyLevel", 1.2),
                new InfluenceFactor("HasAutism", -0.9)
            ]
        },
        new()
        {
            Id = "childhood_parental_praise",
            Name = "Bedeutsame elterliche Anerkennung",
            Description =
                "Kind erhält echtes Lob und Anerkennung für seine Bemühungen, was das Selbstwertgefühl stärkt.",
            BaseProbability = 0.60,
            MinAge = 2,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -8,
            MoodImpact = 15,
            SocialBelongingImpact = 10,
            ResilienceImpact = 8,
            HealthImpact = 0,
            AnxietyChange = -8,
            SocialEnergyChange = 3,
            VisualCategory = VisualCategory.Achievement,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.4),
                new InfluenceFactor("ParentsWithAddiction", -1.3)
            ]
        },
        new()
        {
            Id = "childhood_sibling_bond",
            Name = "Geschwisterbindung gestärkt",
            Description = "Kind entwickelt eine positive, unterstützende Beziehung zu seinen Geschwistern.",
            BaseProbability = 0.50,
            MinAge = 2,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 10,
            SocialBelongingImpact = 12,
            ResilienceImpact = 5,
            HealthImpact = 0,
            AnxietyChange = -3,
            SocialEnergyChange = 5,
            VisualCategory = VisualCategory.Family,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("ParentsRelationshipQuality", 1.4)
            ]
        },
        new()
        {
            Id = "childhood_creative_talent",
            Name = "Kreatives Talent entdeckt",
            Description =
                "Kind zeigt natürliche Begabung für kreative Ausdrucksformen wie Kunst, Musik oder Geschichtenerzählen.",
            BaseProbability = 0.30,
            MinAge = 3,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -3,
            MoodImpact = 12,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = 0,
            AnxietyChange = -5,
            SocialEnergyChange = 0,
            VisualCategory = VisualCategory.Creativity,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.4),
                new InfluenceFactor("ParentsEducationLevel", 1.3),
                new InfluenceFactor("IncomeLevel", 1.2),
                new InfluenceFactor("HasAutism", 1.1)
            ]
        },
        new()
        {
            Id = "childhood_separation_anxiety",
            Name = "Trennungsangst-Episode",
            Description = "Kind erlebt erheblichen Kummer bei Trennung von der primären Bezugsperson.",
            BaseProbability = 0.40,
            MinAge = 1,
            MaxAge = 4,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 12,
            MoodImpact = -12,
            SocialBelongingImpact = -5,
            ResilienceImpact = -2,
            HealthImpact = -1,
            AnxietyChange = 12,
            SocialEnergyChange = -5,
            VisualCategory = VisualCategory.MentalHealth,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 1.4),
                new InfluenceFactor("FamilyCloseness", -1.4),
                new InfluenceFactor("ParentsWithAddiction", 1.3),
                new InfluenceFactor("ParentsRelationshipQuality", -1.2),
                new InfluenceFactor("GenderFemale", 1.05)
            ],
            Exclusions = ["childhood_secure_attachment"]
        },
        new()
        {
            Id = "childhood_witnessed_conflict",
            Name = "Elterlichen Konflikt miterlebt",
            Description = "Kind erlebt einen schwerwiegenden Konflikt zwischen den Eltern oder Betreuungspersonen mit.",
            BaseProbability = 0.35,
            MinAge = 2,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            IsTraumatic = true,
            StressImpact = 18,
            MoodImpact = -15,
            SocialBelongingImpact = -12,
            ResilienceImpact = -5,
            HealthImpact = -3,
            AnxietyChange = 15,
            SocialEnergyChange = -8,
            VisualCategory = VisualCategory.Trauma,
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsRelationshipQuality", -1.5),
                new InfluenceFactor("ParentsWithAddiction", 1.4),
                new InfluenceFactor("IncomeLevel", -1.1)
            ]
        },
        new()
        {
            Id = "childhood_early_illness",
            Name = "Schwere Kinderkrankheit",
            Description =
                "Kind erlebt eine bedeutende Krankheit, die längere Pflege oder Krankenhausaufenthalt erfordert.",
            BaseProbability = 0.15,
            MinAge = CHILDHOOD_MIN,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 18,
            MoodImpact = -12,
            SocialBelongingImpact = -5,
            ResilienceImpact = 5,
            HealthImpact = -15,
            AnxietyChange = 10,
            SocialEnergyChange = -8,
            VisualCategory = VisualCategory.Health,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -1.4),
                new InfluenceFactor("ParentsWithAddiction", 1.3),
                new InfluenceFactor("SocialEnvironmentLevel", -1.1)
            ]
        },
        new()
        {
            Id = "childhood_kindergarten_success",
            Name = "Erfolgreicher Kindergarteneinstieg",
            Description =
                "Kind gewöhnt sich gut an den Kindergarten, schließt Freundschaften und zeigt Freude am Lernen.",
            BaseProbability = 0.55,
            MinAge = 4,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 10,
            SocialBelongingImpact = 12,
            ResilienceImpact = 8,
            HealthImpact = 0,
            AnxietyChange = -8,
            SocialEnergyChange = 6,
            VisualCategory = VisualCategory.Education,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 1.5),
                new InfluenceFactor("AnxietyLevel", -1.3),
                new InfluenceFactor("FamilyCloseness", 1.4),
                new InfluenceFactor("ParentsEducationLevel", 1.3)
            ]
        },
        new()
        {
            Id = "childhood_neglect_experience",
            Name = "Emotionale Vernachlässigung",
            Description = "Kind erlebt eine Phase emotionaler Unerreichbarkeit der Betreuungspersonen.",
            BaseProbability = 0.20,
            MinAge = CHILDHOOD_MIN,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            IsTraumatic = true,
            StressImpact = 20,
            MoodImpact = -25,
            SocialBelongingImpact = -20,
            ResilienceImpact = -10,
            HealthImpact = -5,
            AnxietyChange = 18,
            SocialEnergyChange = -12,
            VisualCategory = VisualCategory.Trauma,
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsWithAddiction", 1.6),
                new InfluenceFactor("FamilyCloseness", -1.6),
                new InfluenceFactor("ParentsRelationshipQuality", -1.4),
                new InfluenceFactor("IncomeLevel", -1.2),
                new InfluenceFactor("ParentsEducationLevel", -1.1),
                new InfluenceFactor("GenderMale", 1.1),
                new InfluenceFactor("SocialEnvironmentLevel", -1.1)
            ],
            Exclusions = ["childhood_secure_attachment"]
        },
        new()
        {
            Id = "childhood_early_language_development",
            Name = "Frühe Sprachentwicklung",
            Description =
                "Kind zeigt überdurchschnittlich frühe Sprachentwicklung und verbale Kommunikationsfähigkeiten.",
            BaseProbability = 0.30,
            MinAge = 1,
            MaxAge = 3,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 10,
            SocialBelongingImpact = 8,
            ResilienceImpact = 5,
            HealthImpact = 0,
            AnxietyChange = -3,
            SocialEnergyChange = 5,
            VisualCategory = VisualCategory.Education,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.5),
                new InfluenceFactor("FamilyCloseness", 1.4),
                new InfluenceFactor("ParentsEducationLevel", 1.3),
                new InfluenceFactor("GenderFemale", 1.2),
                new InfluenceFactor("HasAutism", -0.8)
            ]
        },
        new()
        {
            Id = "childhood_rough_play_injury",
            Name = "Verletzung beim Toben",
            Description = "Kind verletzt sich bei wildem körperlichem Spiel, lernt aber auch Grenzen kennen.",
            BaseProbability = 0.35,
            MinAge = 2,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 10,
            MoodImpact = -5,
            SocialBelongingImpact = 0,
            ResilienceImpact = 5,
            HealthImpact = -8,
            AnxietyChange = 5,
            SocialEnergyChange = 0,
            VisualCategory = VisualCategory.Health,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 1.4),
                new InfluenceFactor("SocialEnvironmentLevel", 1.2),
                new InfluenceFactor("GenderMale", 1.3),
                new InfluenceFactor("HasAdhd", 1.4)
            ]
        },
        new()
        {
            Id = "childhood_emotional_intelligence",
            Name = "Emotionale Sensibilität entwickelt",
            Description = "Kind zeigt frühe Fähigkeit, Emotionen bei sich und anderen zu erkennen und zu benennen.",
            BaseProbability = 0.35,
            MinAge = 2,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 10,
            SocialBelongingImpact = 12,
            ResilienceImpact = 8,
            HealthImpact = 0,
            AnxietyChange = -5,
            SocialEnergyChange = 5,
            VisualCategory = VisualCategory.Social,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3),
                new InfluenceFactor("IntelligenceScore", 1.2),
                new InfluenceFactor("GenderFemale", 1.2)
            ]
        },
        new()
        {
            Id = "childhood_gender_stereotyping",
            Name = "Geschlechterstereotype erlebt",
            Description = "Kind erlebt einschränkende Geschlechtererwartungen von Erwachsenen oder Gleichaltrigen.",
            BaseProbability = 0.40,
            MinAge = 3,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 8,
            MoodImpact = -8,
            SocialBelongingImpact = -5,
            ResilienceImpact = -3,
            HealthImpact = 0,
            AnxietyChange = 5,
            SocialEnergyChange = -3,
            VisualCategory = VisualCategory.Social,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", -1.3),
                new InfluenceFactor("ParentsEducationLevel", -1.1),
                new InfluenceFactor("GenderNonBinary", 1.4)
            ]
        }
    ];

    #endregion

    #region Childhood Generic Events (Ages 0-6)

    /// <summary>
    ///     Generic events that can occur during Childhood phase (ages 0-6).
    /// </summary>
    public static IReadOnlyList<GenericEvent> ChildhoodGenericEvents { get; } =
    [
        new()
        {
            Id = "childhood_family_vacation",
            Name = "Familienurlaub",
            Description =
                "Familie unternimmt gemeinsamen, unvergesslichen Urlaub und schafft positive gemeinsame Erlebnisse.",
            BaseProbability = 0.45,
            MinAge = 2,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -10,
            MoodImpact = 15,
            SocialBelongingImpact = 10,
            ResilienceImpact = 3,
            HealthImpact = 2,
            VisualCategory = VisualCategory.Leisure,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.5),
                new InfluenceFactor("FamilyCloseness", 1.4),
                new InfluenceFactor("ParentsRelationshipQuality", 1.3)
            ]
        },
        new()
        {
            Id = "childhood_pet_acquired",
            Name = "Haustier angeschafft",
            Description = "Familie bekommt ein Haustier, das Verantwortung lehrt und Gesellschaft bietet.",
            BaseProbability = 0.35,
            MinAge = 3,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 12,
            SocialBelongingImpact = 8,
            ResilienceImpact = 5,
            HealthImpact = 3,
            VisualCategory = VisualCategory.Pet,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.5),
                new InfluenceFactor("FamilyCloseness", 1.4)
            ],
            TriggersFollowUpEvents = ["childhood_pet_loss"]
        },
        new()
        {
            Id = "childhood_grandparent_bond",
            Name = "Bindung zu Großeltern",
            Description = "Kind entwickelt durch gemeinsame Zeit eine bedeutsame Verbindung zu Großeltern.",
            BaseProbability = 0.50,
            MinAge = CHILDHOOD_MIN,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -8,
            MoodImpact = 10,
            SocialBelongingImpact = 12,
            ResilienceImpact = 5,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Family,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.5)
            ]
        },
        new()
        {
            Id = "childhood_new_sibling",
            Name = "Neues Geschwisterchen geboren",
            Description = "Ein neues Geschwisterkind kommt zur Familie und verändert die Familiendynamik.",
            BaseProbability = 0.40,
            MinAge = 1,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 8,
            MoodImpact = 3,
            SocialBelongingImpact = 5,
            ResilienceImpact = 3,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Family,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.4),
                new InfluenceFactor("ParentsRelationshipQuality", 1.3)
            ]
        },
        new()
        {
            Id = "childhood_family_relocation",
            Name = "Familienumzug",
            Description = "Familie zieht in ein neues Zuhause oder eine neue Nachbarschaft.",
            BaseProbability = 0.25,
            MinAge = CHILDHOOD_MIN,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 12,
            MoodImpact = -5,
            SocialBelongingImpact = -8,
            ResilienceImpact = 5,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Home,
            InfluenceFactors =
            [
                new InfluenceFactor("JobStatus", 1.2)
            ]
        },
        new()
        {
            Id = "childhood_pet_loss",
            Name = "Verlust des Haustiers",
            Description = "Familienhaustier stirbt und führt das Konzept von Verlust ein.",
            BaseProbability = 0.20,
            MinAge = 3,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            IsTraumatic = false,
            StressImpact = 15,
            MoodImpact = -18,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = 0,
            VisualCategory = VisualCategory.Pet,
            Prerequisites = ["childhood_pet_acquired"],
            InfluenceFactors = []
        },
        new()
        {
            Id = "childhood_parent_job_loss",
            Name = "Elternteil verliert Arbeit",
            Description =
                "Ein Elternteil verliert den Arbeitsplatz, was die finanzielle Stabilität der Familie beeinträchtigt.",
            BaseProbability = 0.18,
            MinAge = CHILDHOOD_MIN,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 15,
            MoodImpact = -8,
            SocialBelongingImpact = -5,
            ResilienceImpact = 0,
            HealthImpact = -3,
            VisualCategory = VisualCategory.Financial,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -1.4),
                new InfluenceFactor("ParentsEducationLevel", -1.3),
                new InfluenceFactor("JobStatus", -1.3),
                new InfluenceFactor("ParentsWithAddiction", 1.3)
            ]
        },
        new()
        {
            Id = "childhood_community_event",
            Name = "Positives Gemeinschaftsereignis",
            Description = "Kind nimmt an einer Gemeinschaftsfeier oder -versammlung teil.",
            BaseProbability = 0.40,
            MinAge = 2,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = -5,
            MoodImpact = 10,
            SocialBelongingImpact = 12,
            ResilienceImpact = 3,
            HealthImpact = 2,
            VisualCategory = VisualCategory.Community,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 1.5),
                new InfluenceFactor("IncomeLevel", 1.3)
            ]
        },
        new()
        {
            Id = "childhood_health_checkup",
            Name = "Routineuntersuchung",
            Description = "Kind nimmt an regulärer ärztlicher Untersuchung mit Impfungen teil.",
            BaseProbability = 0.70,
            MinAge = CHILDHOOD_MIN,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            IsTraumatic = false,
            StressImpact = 5,
            MoodImpact = -3,
            SocialBelongingImpact = 0,
            ResilienceImpact = 2,
            HealthImpact = 5,
            VisualCategory = VisualCategory.Health,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.4)
            ]
        },
        new()
        {
            Id = "childhood_parents_divorce",
            Name = "Elterntrennung oder Scheidung",
            Description =
                "Eltern trennen sich oder lassen sich scheiden, was die Familienstruktur grundlegend verändert.",
            BaseProbability = 0.25,
            MinAge = CHILDHOOD_MIN,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            IsTraumatic = true,
            StressImpact = 22,
            MoodImpact = -22,
            SocialBelongingImpact = -18,
            ResilienceImpact = -7,
            HealthImpact = -5,
            VisualCategory = VisualCategory.Family,
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsRelationshipQuality", -1.5),
                new InfluenceFactor("ParentsWithAddiction", 1.5),
                new InfluenceFactor("IncomeLevel", -1.2),
                new InfluenceFactor("FamilyCloseness", -1.4)
            ],
            TriggersFollowUpEvents = ["school_custody_adjustment"]
        }
    ];

    #endregion

    #region Aggregated Event Collections

    /// <summary>
    ///     All generic events for the Childhood phase.
    /// </summary>
    public static IReadOnlyList<GenericEvent> AllGenericEvents { get; } = new ReadOnlyCollection<GenericEvent>(
    [
        ..ChildhoodGenericEvents
    ]);

    /// <summary>
    ///     All personal events for the Childhood phase.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> AllPersonalEvents { get; } = new ReadOnlyCollection<PersonalEvent>(
    [
        ..ChildhoodPersonalEvents
    ]);

    #endregion
}