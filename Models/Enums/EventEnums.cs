namespace SimulationFIN31.Models.Enums;

public enum EventCategory
{
    Generic,
    Personal,
    Coping
}

public enum CopingType
{
    Functional,
    Dysfunctional,
    Neutral
}

/// <summary>
///     Visual categories for events, used to determine icon/image representation.
///     Each category corresponds to a specific visual theme for UI display.
/// </summary>
public enum VisualCategory
{
    /// <summary>Family relationships, bonds, siblings, parents, children.</summary>
    Family,

    /// <summary>Work, employment, jobs, promotions, entrepreneurship.</summary>
    Career,

    /// <summary>School, academic performance, learning, graduation, university.</summary>
    Education,

    /// <summary>Death of loved ones (family, friends).</summary>
    Death,

    /// <summary>Physical health, illness, medical care, checkups.</summary>
    Health,

    /// <summary>Friendships, peer relationships, social belonging, social activities.</summary>
    Social,

    /// <summary>Romantic relationships, love, dating, marriage, heartbreak, divorce.</summary>
    Romance,

    /// <summary>Arts, music, creative expression, creative talents.</summary>
    Creativity,

    /// <summary>Athletic activities, sports achievements, physical fitness.</summary>
    Sports,

    /// <summary>Money, income, economic issues, purchases, financial stability.</summary>
    Financial,

    /// <summary>Mental health struggles, therapy, psychological support.</summary>
    MentalHealth,

    /// <summary>Violence, abuse, conflict, harassment, bullying, assault.</summary>
    Trauma,

    /// <summary>Recognition, awards, success, achievements, leadership positions.</summary>
    Achievement,

    /// <summary>Vacation, travel, hobbies, recreational activities, fun experiences.</summary>
    Leisure,

    /// <summary>Community involvement, volunteering, civic engagement, neighborhood.</summary>
    Community,

    /// <summary>Housing, moving, relocation, home purchase, living situation.</summary>
    Home,

    /// <summary>Self-discovery, identity exploration, personal growth, life transitions.</summary>
    Identity,

    /// <summary>Pets, animals, pet-related experiences.</summary>
    Pet,

    /// <summary>Coping mechanisms and strategies (for CopingMechanism events).</summary>
    CopingNeutral,
    CopingFunctional,
    CopingDysfunctional,

    /// <summary>Natural disasters, environmental events.</summary>
    Nature
}

/// <summary>
///     Represents the major life phases in human development.
///     Age ranges are based on developmental psychology research.
/// </summary>
public enum LifePhase
{
    /// <summary>Ages 0-6: Early development, family-centric experiences.</summary>
    Childhood,

    /// <summary>Ages 6-12: peer relationships, learning how to work with others.</summary>
    SchoolBeginning,

    /// <summary>Ages 12-18: puberty, identity and personality development.</summary>
    Adolescence,

    /// <summary>
    ///     Ages 18-24: University or apprenticeships, romantic interests and relationships stabilize, independence from
    ///     parents, first jobs
    /// </summary>
    EmergingAdulthood,

    /// <summary>Ages 24-30: stable job, relationship and love life, family interactions and possible loss, job focus</summary>
    Adulthood
}