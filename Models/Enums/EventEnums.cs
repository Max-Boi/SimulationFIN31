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
/// Represents the major life phases in human development.
/// Age ranges are based on developmental psychology research.
/// </summary>
public enum LifePhase
{
    /// <summary>Ages 0-6: Early development, family-centric experiences.</summary>
    Childhood,

    /// <summary>Ages 6-12: peer relationships, learning how to work with others.</summary>
    SchoolBeginning,

    /// <summary>Ages 12-18: puberty, identity and personality development.</summary>
    Adolescence,

    /// <summary>Ages 18-24: University or apprenticeships, romantic interests and relationships stabilize, independence from parents, first jobs</summary>
    EmergingAdulthood,

    /// <summary>Ages 24-30: stable job, relationship and love life, family interactions and possible loss, job focus</summary>
    Adulthood,
}