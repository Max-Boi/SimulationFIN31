using SimulationFIN31.Models.EventTypes;

namespace SimulationFIN31.Models.structs;

public readonly struct WeightedEvent
{
    public LifeEvent Event { get; }
    public double Weight { get; }
    public double NormalizedProbability { get; }
        
    public WeightedEvent(LifeEvent lifeEvent, double weight, double normalizedProbability)
    {
        Event = lifeEvent;
        Weight = weight;
        NormalizedProbability = normalizedProbability;
    }
}