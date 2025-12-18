using System;
using System.Collections.Generic;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Models.EventTypes
{
    public class LifeEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double BaseProbability { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        
        public EventCategory Category { get; set; }
        public List<InfluenceFactor> InfluenceFactors { get; set; }
        
        // Direkte Effekte
        public double StressImpact { get; set; }
        public double MoodImpact { get; set; }
        public double SocialBelongingImpact { get; set; }
        public double ResilienceImpact { get; set; }
        public double HealthImpact { get; set; }
        
        public LifeEvent()
        {
            InfluenceFactors = new List<InfluenceFactor>();
            MaxAge = 100;
        }
        
        public virtual void ApplyEffects(SimulationState profile)
        {
            profile.CurrentStress = Clamp(profile.CurrentStress + StressImpact, 0, 100);
            profile.CurrentMood = Clamp(profile.CurrentMood + MoodImpact, -100, 100);
            profile.SocialBelonging = Clamp(profile.SocialBelonging + SocialBelongingImpact, 0, 100);
            profile.ResilienceScore = Clamp(profile.ResilienceScore + ResilienceImpact, 0, 100);
            profile.PhysicalHealth = Clamp(profile.PhysicalHealth + HealthImpact, 0, 100);
        }
        
        protected double Clamp(double value, double min, double max)
        {
            return Math.Max(min, Math.Min(max, value));
        }
    }
} 
