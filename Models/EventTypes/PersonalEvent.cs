using SimulationFIN31.Models.Enums;

namespace SimulationFIN31.Models.EventTypes;

public class PersonalEvent : LifeEvent
{
    public PersonalEvent()
    {
        Category = EventCategory.Personal;
    }
        
    // Persönliche Events können die Persönlichkeit leicht verändern
    public double AnxietyChange { get; set; }
    public double SocialEnergyChange { get; set; }


}