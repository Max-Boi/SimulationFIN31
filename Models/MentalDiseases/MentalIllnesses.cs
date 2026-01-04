using System.Collections.Generic;
using SimulationFIN31.Models.Enums;

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
            MinAge = 10, // Kann ab später Kindheit auftreten
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Female, 1.8 }, // Women have ~1.8x higher risk of depression
                { GenderType.Male, 1.0 },
                { GenderType.NonBinary, 1.4 }
            }
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
            MinAge = 8, // Kann bereits in Kindheit beginnen
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Female, 2.0 }, // Women have ~2x higher risk for generalized anxiety
                { GenderType.Male, 1.0 },
                { GenderType.NonBinary, 1.5 }
            }
        },

        ["sozialePhobie"] = new DiseaseConfig
        {
            Name = "soziale Phobie",
            StressDebuff = 1.10,
            MoodDebuff = 0.90,
            SocialProximityDebuff = 0.75,
            TriggerChance = 15,
            HealingTime = 8,
            MinAge = 10, // Typischerweise ab Schulbeginn
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Female, 1.5 }, // Women have higher risk for social phobia
                { GenderType.Male, 1.0 },
                { GenderType.NonBinary, 1.25 }
            }
        },

        ["PanicDisorder"] = new DiseaseConfig
        {
            Name = "Panic Disorder",
            StressDebuff = 1.22,
            MoodDebuff = 0.92,
            SocialProximityDebuff = 0.90,
            TriggerChance = 16,
            HealingTime = 7,
            MinAge = 15, // Meist späte Adoleszenz
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Female, 2.0 }, // Women have ~2x higher risk for panic disorder
                { GenderType.Male, 1.0 },
                { GenderType.NonBinary, 1.5 }
            }
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
            MinAge = 3 // Kann nach Trauma in jedem Alter auftreten
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
            MinAge = 16, // Erfordert Zugang zu Alkohol
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Male, 2.5 }, // Men have ~2.5x higher risk for alcohol use disorder
                { GenderType.Female, 1.0 },
                { GenderType.NonBinary, 1.75 }
            }
        },

        ["SubstanceAbuse"] = new DiseaseConfig
        {
            Name = "Substance Use Disorder",
            StressDebuff = 1.15,
            MoodDebuff = 0.80,
            SocialProximityDebuff = 0.78,
            TriggerChance = 10,
            HealingTime = 9,
            MinAge = 16, // Typischerweise ab Adoleszenz
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Male, 2.0 }, // Men have ~2x higher risk for substance abuse
                { GenderType.Female, 1.0 },
                { GenderType.NonBinary, 1.5 }
            }
        },

        // === ESSSTÖRUNGEN ===

        ["Magersucht"] = new DiseaseConfig
        {
            Name = "Magersucht",
            StressDebuff = 1.20,
            MoodDebuff = 0.80,
            SocialProximityDebuff = 0.82,
            TriggerChance = 25,
            HealingTime = 10,
            MinAge = 12, // Typischerweise ab Pubertät
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Female, 3.0 }, // Women have ~3x higher risk for anorexia
                { GenderType.Male, 1.0 },
                { GenderType.NonBinary, 2.0 }
            }
        },

        ["Bulimie"] = new DiseaseConfig
        {
            Name = "Bulimie",
            StressDebuff = 1.10,
            MoodDebuff = 0.90,
            SocialProximityDebuff = 0.85,
            TriggerChance = 20,
            HealingTime = 10,
            MinAge = 14, // Meist späte Adoleszenz
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Female, 2.5 }, // Women have ~2.5x higher risk for bulimia
                { GenderType.Male, 1.0 },
                { GenderType.NonBinary, 1.75 }
            }
        },

        ["BingeEatingStörung"] = new DiseaseConfig
        {
            Name = "Binge Eating Störung",
            StressDebuff = 1.12,
            MoodDebuff = 0.88,
            SocialProximityDebuff = 0.90,
            TriggerChance = 12,
            HealingTime = 8,
            MinAge = 10, // Kann früher auftreten als andere Essstörungen
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Female, 1.75 }, // Women have higher risk for binge eating disorder
                { GenderType.Male, 1.0 },
                { GenderType.NonBinary, 1.4 }
            }
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
            MinAge = 8 // Kann bereits in Kindheit beginnen
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
            MinAge = 16 // Diagnose erst ab später Adoleszenz
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
            MinAge = 5 // Kann nach frühem Trauma auftreten
        }
    };
}