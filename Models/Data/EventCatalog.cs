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
public static class EventCatalog
{
    
    private const int CHILDHOOD_MIN = 0;
    private const int CHILDHOOD_MAX = 5;
    private const int SCHOOL_BEGINNING_MIN = 6;
    private const int SCHOOL_BEGINNING_MAX = 11;
    private const int ADOLESCENCE_MIN = 12;
    private const int ADOLESCENCE_MAX = 17;
    private const int EMERGING_ADULTHOOD_MIN = 18;
    private const int EMERGING_ADULTHOOD_MAX = 23;
    private const int ADULTHOOD_MIN = 24;
    private const int ADULTHOOD_MAX = 30;

    #region All Events Collection

    /// <summary>
    /// Read-only collection of all personal events across all life phases.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> AllPersonalEvents { get; } = new ReadOnlyCollection<PersonalEvent>(
    [
        ..ChildhoodPersonalEvents,
        ..SchoolBeginningPersonalEvents,
        ..AdolescencePersonalEvents,
        ..EmergingAdulthoodPersonalEvents,
        ..AdulthoodPersonalEvents
    ]);

    /// <summary>
    /// Read-only collection of all generic events across all life phases.
    /// </summary>
    public static IReadOnlyList<GenericEvent> AllGenericEvents { get; } = new ReadOnlyCollection<GenericEvent>(
    [
        ..ChildhoodGenericEvents,
        ..SchoolBeginningGenericEvents,
        ..AdolescenceGenericEvents,
        ..EmergingAdulthoodGenericEvents,
        ..AdulthoodGenericEvents
    ]);

    /// <summary>
    /// Read-only collection of all coping mechanisms.
    /// </summary>
    public static IReadOnlyList<CopingMechanism> AllCopingMechanisms { get; } = new ReadOnlyCollection<CopingMechanism>(CopingMechanismsList);

    #endregion

    #region Childhood Personal Events (Ages 0-6)

    /// <summary>
    /// Personal events specific to the Childhood phase (ages 0-6).
    /// Based on Attachment Theory and early developmental milestones.
    /// </summary>
    public static IReadOnlyList<PersonalEvent> ChildhoodPersonalEvents { get; } =
    [
        new PersonalEvent
        {
            Id = "childhood_secure_attachment",
            Name = "Secure Attachment Formed",
            Description = "Child develops secure attachment with primary caregiver through consistent, responsive care.",
            BaseProbability = 0.65,
            MinAge = CHILDHOOD_MIN,
            MaxAge = 3,
            IsUnique = true,
            StressImpact = -10,
            MoodImpact = 15,
            SocialBelongingImpact = 20,
            ResilienceImpact = 15,
            HealthImpact = 5,
            AnxietyChange = -10,
            SocialEnergyChange = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.5),
                new InfluenceFactor("ParentsRelationshipQuality", 1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "childhood_first_friendship",
            Name = "First True Friendship",
            Description = "Child forms their first meaningful friendship with a peer, learning reciprocity and sharing.",
            BaseProbability = 0.70,
            MinAge = 3,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            StressImpact = -5,
            MoodImpact = 12,
            SocialBelongingImpact = 15,
            ResilienceImpact = 5,
            HealthImpact = 0,
            AnxietyChange = -5,
            SocialEnergyChange = 8,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 1.2)
            ]
        },
        new PersonalEvent
        {
            Id = "childhood_parental_praise",
            Name = "Meaningful Parental Recognition",
            Description = "Child receives genuine praise and recognition for effort, building self-esteem.",
            BaseProbability = 0.60,
            MinAge = 2,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            StressImpact = -8,
            MoodImpact = 15,
            SocialBelongingImpact = 10,
            ResilienceImpact = 8,
            HealthImpact = 0,
            AnxietyChange = -8,
            SocialEnergyChange = 3,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.4),
                new InfluenceFactor("ParentsEducationLevel", 1.1)
            ]
        },
        new PersonalEvent
        {
            Id = "childhood_sibling_bond",
            Name = "Sibling Bond Strengthened",
            Description = "Child develops a positive, supportive relationship with sibling(s).",
            BaseProbability = 0.50,
            MinAge = 2,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            StressImpact = -5,
            MoodImpact = 10,
            SocialBelongingImpact = 12,
            ResilienceImpact = 5,
            HealthImpact = 0,
            AnxietyChange = -3,
            SocialEnergyChange = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "childhood_creative_talent",
            Name = "Creative Talent Discovered",
            Description = "Child shows natural aptitude for creative expression (art, music, storytelling).",
            BaseProbability = 0.30,
            MinAge = 3,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            StressImpact = -3,
            MoodImpact = 12,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = 0,
            AnxietyChange = -5,
            SocialEnergyChange = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.2)
            ]
        },
        new PersonalEvent
        {
            Id = "childhood_separation_anxiety",
            Name = "Separation Anxiety Episode",
            Description = "Child experiences significant distress when separated from primary caregiver.",
            BaseProbability = 0.40,
            MinAge = 1,
            MaxAge = 4,
            IsUnique = false,
            StressImpact = 15,
            MoodImpact = -10,
            SocialBelongingImpact = -5,
            ResilienceImpact = -3,
            HealthImpact = 0,
            AnxietyChange = 12,
            SocialEnergyChange = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 1.4),
                new InfluenceFactor("FamilyCloseness", -0.3)
            ],
            Exclusions = ["childhood_secure_attachment"]
        },
        new PersonalEvent
        {
            Id = "childhood_witnessed_conflict",
            Name = "Witnessed Parental Conflict",
            Description = "Child witnesses significant conflict between parents or caregivers.",
            BaseProbability = 0.35,
            MinAge = 2,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            StressImpact = 20,
            MoodImpact = -15,
            SocialBelongingImpact = -8,
            ResilienceImpact = -5,
            HealthImpact = -2,
            AnxietyChange = 15,
            SocialEnergyChange = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsRelationshipQuality", -1.5),
                new InfluenceFactor("ParentsWithAddiction", 1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "childhood_early_illness",
            Name = "Significant Childhood Illness",
            Description = "Child experiences a notable illness requiring extended care or hospitalization.",
            BaseProbability = 0.15,
            MinAge = CHILDHOOD_MIN,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            StressImpact = 18,
            MoodImpact = -12,
            SocialBelongingImpact = -5,
            ResilienceImpact = 5,
            HealthImpact = -15,
            AnxietyChange = 10,
            SocialEnergyChange = -8,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -0.8)
            ]
        },
        new PersonalEvent
        {
            Id = "childhood_kindergarten_success",
            Name = "Successful Kindergarten Transition",
            Description = "Child adapts well to kindergarten, making friends and engaging with learning.",
            BaseProbability = 0.55,
            MinAge = 4,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            StressImpact = -5,
            MoodImpact = 10,
            SocialBelongingImpact = 12,
            ResilienceImpact = 8,
            HealthImpact = 0,
            AnxietyChange = -8,
            SocialEnergyChange = 6,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 1.3),
                new InfluenceFactor("AnxietyLevel", -0.5)
            ]
        },
        new PersonalEvent
        {
            Id = "childhood_neglect_experience",
            Name = "Emotional Neglect Experience",
            Description = "Child experiences period of emotional unavailability from caregivers.",
            BaseProbability = 0.20,
            MinAge = CHILDHOOD_MIN,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            StressImpact = 25,
            MoodImpact = -20,
            SocialBelongingImpact = -15,
            ResilienceImpact = -10,
            HealthImpact = -5,
            AnxietyChange = 18,
            SocialEnergyChange = -10,
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsWithAddiction", 1.5),
                new InfluenceFactor("FamilyCloseness", -1.5),
                new InfluenceFactor("IncomeLevel", -0.5)
            ],
            Exclusions = ["childhood_secure_attachment"]
        }
    ];

    #endregion

    #region Childhood Generic Events (Ages 0-6)

    /// <summary>
    /// Generic events that can occur during Childhood phase (ages 0-6).
    /// </summary>
    public static IReadOnlyList<GenericEvent> ChildhoodGenericEvents { get; } =
    [
        new GenericEvent
        {
            Id = "childhood_family_vacation",
            Name = "Family Vacation",
            Description = "Family takes a memorable vacation together, creating positive shared experiences.",
            BaseProbability = 0.45,
            MinAge = 2,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            StressImpact = -10,
            MoodImpact = 15,
            SocialBelongingImpact = 10,
            ResilienceImpact = 3,
            HealthImpact = 2,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.3),
                new InfluenceFactor("FamilyCloseness", 1.2)
            ]
        },
        new GenericEvent
        {
            Id = "childhood_pet_acquired",
            Name = "Family Pet Acquired",
            Description = "Family welcomes a pet, teaching responsibility and providing companionship.",
            BaseProbability = 0.35,
            MinAge = 3,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            StressImpact = -5,
            MoodImpact = 12,
            SocialBelongingImpact = 8,
            ResilienceImpact = 5,
            HealthImpact = 3,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.1)
            ],
            TriggersFollowUpEvents = ["childhood_pet_loss"]
        },
        new GenericEvent
        {
            Id = "childhood_grandparent_bond",
            Name = "Grandparent Bonding Experience",
            Description = "Child develops meaningful connection with grandparent through quality time.",
            BaseProbability = 0.50,
            MinAge = CHILDHOOD_MIN,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            StressImpact = -8,
            MoodImpact = 10,
            SocialBelongingImpact = 12,
            ResilienceImpact = 5,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.4)
            ]
        },
        new GenericEvent
        {
            Id = "childhood_new_sibling",
            Name = "New Sibling Born",
            Description = "A new sibling joins the family, changing family dynamics.",
            BaseProbability = 0.40,
            MinAge = 1,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            StressImpact = 8,
            MoodImpact = 3,
            SocialBelongingImpact = 5,
            ResilienceImpact = 3,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("FamilyCloseness", 1.2)
            ]
        },
        new GenericEvent
        {
            Id = "childhood_family_relocation",
            Name = "Family Relocation",
            Description = "Family moves to a new home or neighborhood.",
            BaseProbability = 0.25,
            MinAge = CHILDHOOD_MIN,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            StressImpact = 12,
            MoodImpact = -5,
            SocialBelongingImpact = -8,
            ResilienceImpact = 5,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("JobStatus", 1.2)
            ]
        },
        new GenericEvent
        {
            Id = "childhood_pet_loss",
            Name = "Loss of Family Pet",
            Description = "Family pet passes away, introducing the concept of loss.",
            BaseProbability = 0.20,
            MinAge = 3,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            StressImpact = 15,
            MoodImpact = -18,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = 0,
            Prerequisites = ["childhood_pet_acquired"],
            InfluenceFactors = []
        },
        new GenericEvent
        {
            Id = "childhood_parent_job_loss",
            Name = "Parent Job Loss",
            Description = "A parent loses their job, affecting family financial stability.",
            BaseProbability = 0.18,
            MinAge = CHILDHOOD_MIN,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            StressImpact = 15,
            MoodImpact = -8,
            SocialBelongingImpact = -5,
            ResilienceImpact = 0,
            HealthImpact = -3,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", -0.8),
                new InfluenceFactor("ParentsEducationLevel", -0.5)
            ]
        },
        new GenericEvent
        {
            Id = "childhood_community_event",
            Name = "Positive Community Event",
            Description = "Child participates in community celebration or gathering.",
            BaseProbability = 0.40,
            MinAge = 2,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            StressImpact = -5,
            MoodImpact = 10,
            SocialBelongingImpact = 12,
            ResilienceImpact = 3,
            HealthImpact = 2,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnvironmentLevel", 1.3)
            ]
        },
        new GenericEvent
        {
            Id = "childhood_health_checkup",
            Name = "Routine Health Checkup",
            Description = "Child attends regular medical checkup with vaccinations.",
            BaseProbability = 0.70,
            MinAge = CHILDHOOD_MIN,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = false,
            StressImpact = 5,
            MoodImpact = -3,
            SocialBelongingImpact = 0,
            ResilienceImpact = 2,
            HealthImpact = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("IncomeLevel", 1.1)
            ]
        },
        new GenericEvent
        {
            Id = "childhood_parents_divorce",
            Name = "Parents Separate or Divorce",
            Description = "Parents decide to separate or divorce, fundamentally changing family structure.",
            BaseProbability = 0.25,
            MinAge = CHILDHOOD_MIN,
            MaxAge = CHILDHOOD_MAX,
            IsUnique = true,
            StressImpact = 25,
            MoodImpact = -20,
            SocialBelongingImpact = -10,
            ResilienceImpact = -5,
            HealthImpact = -5,
            InfluenceFactors =
            [
                new InfluenceFactor("ParentsRelationshipQuality", -1.8),
                new InfluenceFactor("ParentsWithAddiction", 1.4)
            ],
            TriggersFollowUpEvents = ["school_custody_adjustment"]
        }
    ];

    #endregion

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
                new InfluenceFactor("SocialEnvironmentLevel", 1.3),
                new InfluenceFactor("SocialEnergyLevel", 0.8)
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
                new InfluenceFactor("IntelligenceScore", 1.4),
                new InfluenceFactor("ParentsEducationLevel", 1.2)
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
                new InfluenceFactor("SocialEnergyLevel", -0.8),
                new InfluenceFactor("HasAutism", 1.3),
                new InfluenceFactor("HasAdhd", 1.2)
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
                new InfluenceFactor("HasAdhd", 1.5),
                new InfluenceFactor("HasAutism", 1.4)
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
                new InfluenceFactor("SocialEnergyLevel", -0.6),
                new InfluenceFactor("AnxietyLevel", 1.2)
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
            SocialEnergyChange = -8,
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
                new InfluenceFactor("IncomeLevel", -1.5),
                new InfluenceFactor("JobStatus", -1.2)
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
                new InfluenceFactor("SocialEnergyLevel", 1.2)
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
                new InfluenceFactor("IntelligenceScore", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3)
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
                new InfluenceFactor("IntelligenceScore", 1.1)
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
                new InfluenceFactor("SocialEnergyLevel", 1.4),
                new InfluenceFactor("IntelligenceScore", 1.2)
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
                new InfluenceFactor("AnxietyLevel", 1.2)
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
            StressImpact = 20,
            MoodImpact = -20,
            SocialBelongingImpact = -5,
            ResilienceImpact = 8,
            HealthImpact = -3,
            AnxietyChange = 15,
            SocialEnergyChange = -10,
            Prerequisites = ["adolescence_first_romance"],
            InfluenceFactors = []
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
            StressImpact = 18,
            MoodImpact = -8,
            SocialBelongingImpact = 5,
            ResilienceImpact = -5,
            HealthImpact = -5,
            AnxietyChange = 12,
            SocialEnergyChange = 3,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 1.2),
                new InfluenceFactor("ParentsWithAddiction", 1.3)
            ]
        },
        new PersonalEvent
        {
            Id = "adolescence_mental_health_struggle",
            Name = "Mental Health Difficulties",
            Description = "Teen experiences significant anxiety or depression symptoms.",
            BaseProbability = 0.25,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            StressImpact = 25,
            MoodImpact = -25,
            SocialBelongingImpact = -15,
            ResilienceImpact = -10,
            HealthImpact = -10,
            AnxietyChange = 25,
            SocialEnergyChange = -15,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 1.6),
                new InfluenceFactor("FamilyCloseness", -0.8),
                new InfluenceFactor("ParentsWithAddiction", 1.4)
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
                new InfluenceFactor("SocialEnergyLevel", 1.2)
            ]
        },
        new PersonalEvent
        {
            Id = "adolescence_online_harassment",
            Name = "Online Harassment Experience",
            Description = "Teen experiences cyberbullying or online harassment.",
            BaseProbability = 0.30,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            StressImpact = 22,
            MoodImpact = -18,
            SocialBelongingImpact = -12,
            ResilienceImpact = -5,
            HealthImpact = -5,
            AnxietyChange = 20,
            SocialEnergyChange = -12,
            Prerequisites = ["school_technology_access"],
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", -0.5),
                new InfluenceFactor("AnxietyLevel", 1.2)
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
                new InfluenceFactor("AnxietyLevel", 0.8)
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
                new InfluenceFactor("IncomeLevel", -0.5)
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
                new InfluenceFactor("IncomeLevel", 1.2)
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
                new InfluenceFactor("IntelligenceScore", 1.1)
            ]
        },
        new GenericEvent
        {
            Id = "adolescence_family_illness",
            Name = "Family Member Serious Illness",
            Description = "Close family member experiences serious illness.",
            BaseProbability = 0.18,
            MinAge = ADOLESCENCE_MIN,
            MaxAge = ADOLESCENCE_MAX,
            IsUnique = false,
            StressImpact = 22,
            MoodImpact = -15,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = -5,
            InfluenceFactors = []
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
                new InfluenceFactor("SocialEnvironmentLevel", 1.2),
                new InfluenceFactor("SocialEnergyLevel", 1.1)
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
                new InfluenceFactor("IncomeLevel", 1.5)
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
                new InfluenceFactor("IncomeLevel", -1.6),
                new InfluenceFactor("JobStatus", -1.3)
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
                new InfluenceFactor("SocialEnvironmentLevel", 1.3)
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
                new InfluenceFactor("SocialEnvironmentLevel", 1.4),
                new InfluenceFactor("SocialEnergyLevel", 1.2)
            ]
        }
    ];

    #endregion

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
                new InfluenceFactor("IntelligenceScore", 1.5),
                new InfluenceFactor("ParentsEducationLevel", 1.3),
                new InfluenceFactor("IncomeLevel", 1.2)
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
                new InfluenceFactor("SocialEnergyLevel", 1.1)
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
                new InfluenceFactor("IncomeLevel", 1.2)
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
                new InfluenceFactor("ParentsEducationLevel", 1.2),
                new InfluenceFactor("IntelligenceScore", 1.2)
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
                new InfluenceFactor("IntelligenceScore", 1.3)
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
            StressImpact = 25,
            MoodImpact = -22,
            SocialBelongingImpact = -15,
            ResilienceImpact = 5,
            HealthImpact = -5,
            AnxietyChange = 18,
            SocialEnergyChange = -12,
            Prerequisites = ["emerging_serious_relationship"],
            InfluenceFactors = []
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
            StressImpact = 25,
            MoodImpact = -20,
            SocialBelongingImpact = -10,
            ResilienceImpact = -8,
            HealthImpact = -5,
            AnxietyChange = 20,
            SocialEnergyChange = -8,
            Prerequisites = ["emerging_university_acceptance"],
            Exclusions = ["emerging_degree_completion"],
            InfluenceFactors =
            [
                new InfluenceFactor("HasAdhd", 1.4),
                new InfluenceFactor("AnxietyLevel", 1.3),
                new InfluenceFactor("IntelligenceScore", -0.8)
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
                new InfluenceFactor("IntelligenceScore", 1.1),
                new InfluenceFactor("JobStatus", 1.3)
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
            StressImpact = 20,
            MoodImpact = -15,
            SocialBelongingImpact = -8,
            ResilienceImpact = 5,
            HealthImpact = -5,
            AnxietyChange = 18,
            SocialEnergyChange = -8,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 1.3),
                new InfluenceFactor("IntelligenceScore", 1.1)
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
                new InfluenceFactor("IntelligenceScore", 1.3)
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
                new InfluenceFactor("ParentsEducationLevel", 1.1)
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
                new InfluenceFactor("SocialEnergyLevel", 1.2)
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
                new InfluenceFactor("SocialEnergyLevel", 1.4),
                new InfluenceFactor("ParentsEducationLevel", 1.2)
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
                new InfluenceFactor("IncomeLevel", -1.3)
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
            InfluenceFactors = []
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
                new InfluenceFactor("FamilyCloseness", 1.3)
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
                new InfluenceFactor("IncomeLevel", -1.4)
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
                new InfluenceFactor("JobStatus", -1.3),
                new InfluenceFactor("IncomeLevel", -1.2)
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
                new InfluenceFactor("ParentsRelationshipQuality", -1.3),
                new InfluenceFactor("FamilyCloseness", -0.8)
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
                new InfluenceFactor("IncomeLevel", -1.3),
                new InfluenceFactor("JobStatus", -1.2)
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

    #region Coping Mechanisms

    private static readonly List<CopingMechanism> CopingMechanismsList =
    [
        // Functional Coping Mechanisms
        new CopingMechanism
        {
            Id = "coping_exercise",
            Name = "Physical Exercise",
            Description = "Engaging in regular physical activity to manage stress.",
            Category = EventCategory.Coping,
            BaseProbability = 0.40,
            MinAge = 6,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(stressThreshold: 50),
            ShortTermRelief = 0.6,
            LongTermImpactMultiplier = 1.3,
            IsHabitForming = true,
            StressImpact = -15,
            MoodImpact = 12,
            SocialBelongingImpact = 5,
            ResilienceImpact = 8,
            HealthImpact = 10,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 1.1)
            ]
        },
        new CopingMechanism
        {
            Id = "coping_social_support",
            Name = "Seeking Social Support",
            Description = "Reaching out to friends or family for emotional support.",
            Category = EventCategory.Coping,
            BaseProbability = 0.45,
            MinAge = 10,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(moodThreshold: -20, belongingThreshold: 40),
            ShortTermRelief = 0.7,
            LongTermImpactMultiplier = 1.25,
            IsHabitForming = true,
            StressImpact = -12,
            MoodImpact = 15,
            SocialBelongingImpact = 18,
            ResilienceImpact = 10,
            HealthImpact = 3,
            InfluenceFactors =
            [
                new InfluenceFactor("SocialEnergyLevel", 1.3),
                new InfluenceFactor("FamilyCloseness", 1.2)
            ]
        },
        new CopingMechanism
        {
            Id = "coping_mindfulness",
            Name = "Mindfulness Practice",
            Description = "Practicing meditation or mindfulness techniques.",
            Category = EventCategory.Coping,
            BaseProbability = 0.25,
            MinAge = 12,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(stressThreshold: 60),
            ShortTermRelief = 0.5,
            LongTermImpactMultiplier = 1.35,
            IsHabitForming = true,
            StressImpact = -18,
            MoodImpact = 10,
            SocialBelongingImpact = 3,
            ResilienceImpact = 15,
            HealthImpact = 5,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.1),
                new InfluenceFactor("ParentsEducationLevel", 1.1)
            ]
        },
        new CopingMechanism
        {
            Id = "coping_creative_expression",
            Name = "Creative Expression",
            Description = "Using art, music, or writing to process emotions.",
            Category = EventCategory.Coping,
            BaseProbability = 0.35,
            MinAge = 6,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(moodThreshold: -30),
            ShortTermRelief = 0.55,
            LongTermImpactMultiplier = 1.2,
            IsHabitForming = true,
            StressImpact = -12,
            MoodImpact = 14,
            SocialBelongingImpact = 5,
            ResilienceImpact = 10,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.1)
            ]
        },
        new CopingMechanism
        {
            Id = "coping_problem_solving",
            Name = "Active Problem Solving",
            Description = "Systematically addressing the source of stress.",
            Category = EventCategory.Coping,
            BaseProbability = 0.40,
            MinAge = 12,
            MaxAge = 100,
            IsUnique = false,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(stressThreshold: 45),
            ShortTermRelief = 0.45,
            LongTermImpactMultiplier = 1.4,
            IsHabitForming = true,
            StressImpact = -15,
            MoodImpact = 10,
            SocialBelongingImpact = 3,
            ResilienceImpact = 18,
            HealthImpact = 0,
            InfluenceFactors =
            [
                new InfluenceFactor("IntelligenceScore", 1.3)
            ]
        },

        // Dysfunctional Coping Mechanisms
        new CopingMechanism
        {
            Id = "coping_avoidance",
            Name = "Avoidance and Withdrawal",
            Description = "Avoiding stressors and withdrawing from social situations.",
            Category = EventCategory.Coping,
            BaseProbability = 0.45,
            MinAge = 8,
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
                new InfluenceFactor("SocialEnergyLevel", -1.2)
            ]
        },
        new CopingMechanism
        {
            Id = "coping_substance_use",
            Name = "Substance Use",
            Description = "Using alcohol or other substances to cope with stress.",
            Category = EventCategory.Coping,
            BaseProbability = 0.25,
            MinAge = 14,
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
                new InfluenceFactor("ParentsWithAddiction", 1.5),
                new InfluenceFactor("AnxietyLevel", 1.3),
                new InfluenceFactor("FamilyCloseness", -0.8)
            ]
        },
        new CopingMechanism
        {
            Id = "coping_rumination",
            Name = "Rumination",
            Description = "Repetitive negative thinking about problems and feelings.",
            Category = EventCategory.Coping,
            BaseProbability = 0.50,
            MinAge = 10,
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
        new CopingMechanism
        {
            Id = "coping_emotional_eating",
            Name = "Emotional Eating",
            Description = "Using food to cope with negative emotions.",
            Category = EventCategory.Coping,
            BaseProbability = 0.40,
            MinAge = 8,
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
        new CopingMechanism
        {
            Id = "coping_aggression",
            Name = "Aggressive Behavior",
            Description = "Expressing frustration through verbal or physical aggression.",
            Category = EventCategory.Coping,
            BaseProbability = 0.30,
            MinAge = 6,
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
        new CopingMechanism
        {
            Id = "coping_distraction",
            Name = "Distraction Activities",
            Description = "Engaging in entertainment or activities to take mind off stress.",
            Category = EventCategory.Coping,
            BaseProbability = 0.60,
            MinAge = 6,
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
        new CopingMechanism
        {
            Id = "coping_sleep",
            Name = "Rest and Sleep",
            Description = "Using sleep as a way to escape or recover from stress.",
            Category = EventCategory.Coping,
            BaseProbability = 0.55,
            MinAge = 6,
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
        new CopingMechanism
        {
            Id = "coping_humor",
            Name = "Humor and Laughter",
            Description = "Using humor to lighten mood and perspective.",
            Category = EventCategory.Coping,
            BaseProbability = 0.45,
            MinAge = 8,
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
        new CopingMechanism
        {
            Id = "coping_nature",
            Name = "Nature and Outdoor Time",
            Description = "Spending time in nature to decompress.",
            Category = EventCategory.Coping,
            BaseProbability = 0.35,
            MinAge = 6,
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
        new CopingMechanism
        {
            Id = "coping_journaling",
            Name = "Journaling",
            Description = "Writing about thoughts and feelings to process emotions.",
            Category = EventCategory.Coping,
            BaseProbability = 0.30,
            MinAge = 10,
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
}