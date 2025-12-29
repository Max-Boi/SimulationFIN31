using System.Collections.Generic;

namespace SimulationFIN31.Models.MentalDiseases;

public static class MentalIllnesses
{
    public static readonly Dictionary<string, DiseaseConfig> Illnesses = new()
    {
        // === DEPRESSIVE STÖRUNGEN ===

        ["MildDepression"] = new DiseaseConfig
        {
            Name = "Depressive Episode",
            StressDebuff = 1.05,
            MoodDebuff = 0.92,
            SocialProximityDebuff = 0.95,
            TriggerChance = 8,
            HealingTime = 2,
            MinAge = 10               // Kann ab später Kindheit auftreten
        },

        // === ANGSTSTÖRUNGEN ===

        ["generalisierteAngststörung"] = new DiseaseConfig
        {
            Name = "generalisierte Angststörung",
            StressDebuff = 1.15,
            MoodDebuff = 0.88,
            SocialProximityDebuff = 0.92,
            TriggerChance = 15,
            HealingTime = 3,
            MinAge = 8                // Kann bereits in Kindheit beginnen
        },

        ["sozialePhobie"] = new DiseaseConfig
        {
            Name = "soziale Phobie",
            StressDebuff = 1.10,
            MoodDebuff = 0.90,
            SocialProximityDebuff = 0.75,
            TriggerChance = 10,
            HealingTime = 8,
            MinAge = 10               // Typischerweise ab Schulbeginn
        },

        ["PanicDisorder"] = new DiseaseConfig
        {
            Name = "Panic Disorder",
            StressDebuff = 1.22,
            MoodDebuff = 0.92,
            SocialProximityDebuff = 0.90,
            TriggerChance = 16,
            HealingTime = 7,
            MinAge = 15               // Meist späte Adoleszenz
        },

        // === TRAUMA-BEZOGENE STÖRUNGEN ===

        ["PTSD"] = new DiseaseConfig
        {
            Name = "Post-Traumatic Stress Disorder",
            StressDebuff = 1.25,
            MoodDebuff = 0.85,
            SocialProximityDebuff = 0.85,
            TriggerChance = 15,
            HealingTime = 10,
            MinAge = 3                // Kann nach Trauma in jedem Alter auftreten
        },

        // === SUCHTERKRANKUNGEN ===

        ["Alcoholism"] = new DiseaseConfig
        {
            Name = "Alcohol Use Disorder",
            StressDebuff = 1.05,
            MoodDebuff = 0.82,
            SocialProximityDebuff = 0.80,
            TriggerChance = 5,
            HealingTime = 8,
            MinAge = 16               // Erfordert Zugang zu Alkohol
        },

        ["SubstanceAbuse"] = new DiseaseConfig
        {
            Name = "Substance Use Disorder",
            StressDebuff = 1.15,
            MoodDebuff = 0.80,
            SocialProximityDebuff = 0.78,
            TriggerChance = 10,
            HealingTime = 9,
            MinAge = 16               // Typischerweise ab Adoleszenz
        },

        // === ESSSTÖRUNGEN ===

        ["Magersucht"] = new DiseaseConfig
        {
            Name = "Magersucht",
            StressDebuff = 1.20,
            MoodDebuff = 0.80,
            SocialProximityDebuff = 0.82,
            TriggerChance = 8,
            HealingTime = 10,
            MinAge = 12               // Typischerweise ab Pubertät
        },

        ["Bulimie"] = new DiseaseConfig
        {
            Name = "Bulimie",
            StressDebuff = 1.10,
            MoodDebuff = 0.90,
            SocialProximityDebuff = 0.85,
            TriggerChance = 12,
            HealingTime = 10,
            MinAge = 14               // Meist späte Adoleszenz
        },

        ["BingeEatingStörung"] = new DiseaseConfig
        {
            Name = "Binge Eating Störung",
            StressDebuff = 1.12,
            MoodDebuff = 0.88,
            SocialProximityDebuff = 0.90,
            TriggerChance = 12,
            HealingTime = 8,
            MinAge = 10               // Kann früher auftreten als andere Essstörungen
        },

        // === ZWANGSSTÖRUNGEN ===

        ["OCD"] = new DiseaseConfig
        {
            Name = "Obsessive-Compulsive Disorder",
            StressDebuff = 1.22,
            MoodDebuff = 0.85,
            SocialProximityDebuff = 0.88,
            TriggerChance = 10,
            HealingTime = 12,
            MinAge = 8                // Kann bereits in Kindheit beginnen
        },

        // === PERSÖNLICHKEITSSTÖRUNGEN ===

        ["BorderlinePersonality"] = new DiseaseConfig
        {
            Name = "Borderline Personality Disorder",
            StressDebuff = 1.30,
            MoodDebuff = 0.75,
            SocialProximityDebuff = 0.80,
            TriggerChance = 15,
            HealingTime = 20,
            MinAge = 16               // Diagnose erst ab später Adoleszenz
        },

        // === DISSOZIATIVE STÖRUNGEN ===

        ["DissociativeDisorder"] = new DiseaseConfig
        {
            Name = "Dissociative Disorder",
            StressDebuff = 1.25,
            MoodDebuff = 0.80,
            SocialProximityDebuff = 0.82,
            TriggerChance = 16,
            HealingTime = 16,
            MinAge = 5                // Kann nach frühem Trauma auftreten
        }
    };
}