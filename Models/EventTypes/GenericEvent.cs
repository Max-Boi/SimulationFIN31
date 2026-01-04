using System.Collections.Generic;
using SimulationFIN31.Models.Enums;

namespace SimulationFIN31.Models.EventTypes;

public class GenericEvent : LifeEvent
{
    public GenericEvent()
    {
        Category = EventCategory.Generic;
    }

    public List<string> TriggersFollowUpEvents { get; set; } = new();
}