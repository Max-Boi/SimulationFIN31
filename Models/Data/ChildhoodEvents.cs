using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.Data;

public static  class ChildhoodEvents 
{
    private const int CHILDHOOD_MIN = 0;
    private const int CHILDHOOD_MAX = 5;
    
    public static IReadOnlyList<GenericEvent> AllGenericEvents { get; } = new ReadOnlyCollection<GenericEvent>(
    [
        ..ChildhoodGenericEvents
    ]);
    
    public static IReadOnlyList<PersonalEvent> AllPersonalEvents { get; } = new ReadOnlyCollection<PersonalEvent>(
    [
        ..ChildhoodPersonalEvents,
    ]);
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
}