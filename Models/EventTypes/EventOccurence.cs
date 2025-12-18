using System;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.EventTypes;

public class EventOccurrence
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string EventName { get; set; } = string.Empty;
    public EventCategory Category { get; set; }
    public DateTime OccurredAt { get; set; }
    public int AvatarAgeAtOccurrence { get; set; }
    public EventEffects AppliedEffects { get; set; }
}