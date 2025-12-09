using System;
using SimulationFIN31.Models.Enums;

namespace SimulationFIN31.Models
{
    public class SimulationEvent
    {
        public required EventCategory Category { get; set; }
        public required Guid EventId = new Guid();

        public SimulationEvent()
        {
            
        }
    }
} 
