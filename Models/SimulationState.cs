using System.Collections.Generic;
using SimulationFIN31.Models.Enums;

namespace SimulationFIN31.Models;

public class SimulationState
{
    // Sozioökonomische Faktoren
    public IncomeLevel IncomeLevel { get; set; }
    public ParentsEducationLevel ParentsEducationLevel { get; set; } 
    public JobStatus JobStatus { get; set; } 
    public int SocialEnvironmentLevel { get; set; } 
        
    // Familiäres Milieu
    public int FamilyCloseness { get; set; } 
    public int ParentsRelationshipQuality { get; set; } 
    public bool ParentsWithAddiction { get; set; }
        
    // Individuelle Neurobiologie
    public bool HasAdhd { get; set; }
    public bool HasAutism { get; set; }
    public int IntelligenceScore { get; set; } 
    public int AnxietyLevel { get; set; } 
    public SocialEnergyLevel SocialEnergyLevel { get; set; } 
        
    public double CurrentStress { get; set; }
    public double CurrentMood { get; set; }
    public double SocialBelonging { get; set; }
    public double ResilienceScore { get; set; }
    public double PhysicalHealth { get; set; } = 100;
    
    public int CurrentAge { get; set; }
    public GenderType Gender { get; set; }
    
    public List<string> EventHistory { get; set; } = new();
    public LifePhase LifePhase { get; set; } = LifePhase.Childhood; 
  
    public Dictionary<string, double> CopingPreferences { get; set; } = new();
}