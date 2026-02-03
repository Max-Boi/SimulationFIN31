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
            // Dynamic ranges (±15% variance)
            StressDebuffMin = 1.0,
            StressDebuffMax = 1.15,
            MoodDebuffMin = 0.78,
            MoodDebuffMax = 0.98,
            SocialDebuffMin = 0.85,
            SocialDebuffMax = 1.0,
            Volatility = 0.6, // High volatility - good days/bad days
            TriggerChance = 8,
            HealingTime = 2,
            MinAge = 10,
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Female, 1.8 },
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
            // Dynamic ranges (low variance - consistently elevated)
            StressDebuffMin = 1.10,
            StressDebuffMax = 1.20,
            MoodDebuffMin = 0.85,
            MoodDebuffMax = 0.92,
            SocialDebuffMin = 0.88,
            SocialDebuffMax = 0.95,
            Volatility = 0.2, // Low volatility - persistent anxiety
            TriggerChance = 15,
            HealingTime = 3,
            MinAge = 8,
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Female, 2.0 },
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
            // Dynamic ranges (moderate variance, severe social impact)
            StressDebuffMin = 1.05,
            StressDebuffMax = 1.18,
            MoodDebuffMin = 0.82,
            MoodDebuffMax = 0.95,
            SocialDebuffMin = 0.65,
            SocialDebuffMax = 0.85,
            Volatility = 0.4, // Moderate - depends on social situations
            TriggerChance = 15,
            HealingTime = 8,
            MinAge = 10,
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Female, 1.5 },
                { GenderType.Male, 1.0 },
                { GenderType.NonBinary, 1.25 }
            }
        },

        ["Panikstörung"] = new DiseaseConfig
        {
            Name = "Panikstörung",
            StressDebuff = 1.22,
            MoodDebuff = 0.92,
            SocialProximityDebuff = 0.90,
            // Dynamic ranges (spike-based - normal with sudden intense spikes)
            StressDebuffMin = 1.0,
            StressDebuffMax = 1.45,
            MoodDebuffMin = 0.75,
            MoodDebuffMax = 0.98,
            SocialDebuffMin = 0.78,
            SocialDebuffMax = 0.98,
            Volatility = 0.8, // High volatility - panic attacks are sudden
            TriggerChance = 16,
            HealingTime = 7,
            MinAge = 15,
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Female, 2.0 },
                { GenderType.Male, 1.0 },
                { GenderType.NonBinary, 1.5 }
            }
        },

        // === TRAUMA-BEZOGENE STÖRUNGEN ===

        ["PTBS"] = new DiseaseConfig
        {
            Name = "Posttraumatische Belastungsstörung",
            StressDebuff = 1.25,
            MoodDebuff = 0.85,
            SocialProximityDebuff = 0.85,
            // Dynamic ranges (trigger-based - can spike severely)
            StressDebuffMin = 1.0,
            StressDebuffMax = 1.50,
            MoodDebuffMin = 0.70,
            MoodDebuffMax = 0.95,
            SocialDebuffMin = 0.70,
            SocialDebuffMax = 0.95,
            Volatility = 0.7, // High volatility - trauma triggers
            TriggerChance = 15,
            HealingTime = 10,
            MinAge = 3
        },

        // === SUCHTERKRANKUNGEN ===

        ["Alkoholismus"] = new DiseaseConfig
        {
            Name = "Alkoholismus",
            StressDebuff = 1.05,
            MoodDebuff = 0.82,
            SocialProximityDebuff = 0.80,
            // Dynamic ranges (moderate variance)
            StressDebuffMin = 1.0,
            StressDebuffMax = 1.15,
            MoodDebuffMin = 0.70,
            MoodDebuffMax = 0.90,
            SocialDebuffMin = 0.70,
            SocialDebuffMax = 0.90,
            Volatility = 0.5, // Moderate - depends on drinking patterns
            TriggerChance = 5,
            HealingTime = 8,
            MinAge = 16,
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Male, 2.5 },
                { GenderType.Female, 1.0 },
                { GenderType.NonBinary, 1.75 }
            }
        },

        ["Substanzmissbrauch"] = new DiseaseConfig
        {
            Name = "Substanzmissbrauch",
            StressDebuff = 1.15,
            MoodDebuff = 0.80,
            SocialProximityDebuff = 0.78,
            // Dynamic ranges (high variance due to substance effects)
            StressDebuffMin = 1.0,
            StressDebuffMax = 1.35,
            MoodDebuffMin = 0.60,
            MoodDebuffMax = 0.90,
            SocialDebuffMin = 0.65,
            SocialDebuffMax = 0.88,
            Volatility = 0.6, // High variance - withdrawal cycles
            TriggerChance = 10,
            HealingTime = 9,
            MinAge = 16,
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Male, 2.0 },
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
            // Dynamic ranges (persistent but fluctuating)
            StressDebuffMin = 1.10,
            StressDebuffMax = 1.30,
            MoodDebuffMin = 0.70,
            MoodDebuffMax = 0.88,
            SocialDebuffMin = 0.75,
            SocialDebuffMax = 0.90,
            Volatility = 0.4, // Moderate - obsessive but consistent
            TriggerChance = 25,
            HealingTime = 10,
            MinAge = 12,
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Female, 3.0 },
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
            // Dynamic ranges (cycle-based)
            StressDebuffMin = 1.0,
            StressDebuffMax = 1.22,
            MoodDebuffMin = 0.78,
            MoodDebuffMax = 0.95,
            SocialDebuffMin = 0.78,
            SocialDebuffMax = 0.92,
            Volatility = 0.5, // Moderate-high - binge/purge cycles
            TriggerChance = 20,
            HealingTime = 10,
            MinAge = 14,
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Female, 2.5 },
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
            // Dynamic ranges (episode-based)
            StressDebuffMin = 1.0,
            StressDebuffMax = 1.25,
            MoodDebuffMin = 0.75,
            MoodDebuffMax = 0.95,
            SocialDebuffMin = 0.82,
            SocialDebuffMax = 0.98,
            Volatility = 0.55, // Moderate-high - binge episodes
            TriggerChance = 12,
            HealingTime = 8,
            MinAge = 10,
            GenderModifiers = new Dictionary<GenderType, double>
            {
                { GenderType.Female, 1.75 },
                { GenderType.Male, 1.0 },
                { GenderType.NonBinary, 1.4 }
            }
        },

        // === ZWANGSSTÖRUNGEN ===

        ["Zwangsstörung"] = new DiseaseConfig
        {
            Name = "Zwangsstörung",
            StressDebuff = 1.22,
            MoodDebuff = 0.85,
            SocialProximityDebuff = 0.88,
            // Dynamic ranges (rigid patterns, less variance)
            StressDebuffMin = 1.18,
            StressDebuffMax = 1.28,
            MoodDebuffMin = 0.80,
            MoodDebuffMax = 0.90,
            SocialDebuffMin = 0.82,
            SocialDebuffMax = 0.92,
            Volatility = 0.25, // Low volatility - compulsive consistency
            TriggerChance = 10,
            HealingTime = 12,
            MinAge = 8
        },

        // === PERSÖNLICHKEITSSTÖRUNGEN ===

        ["BorderlineStörung"] = new DiseaseConfig
        {
            Name = "Borderline Identitätsstörung",
            StressDebuff = 1.30,
            MoodDebuff = 0.75,
            SocialProximityDebuff = 0.80,
            // Dynamic ranges (extreme volatility)
            StressDebuffMin = 1.0,
            StressDebuffMax = 1.60,
            MoodDebuffMin = 0.50,
            MoodDebuffMax = 0.95,
            SocialDebuffMin = 0.60,
            SocialDebuffMax = 0.98,
            Volatility = 0.9, // Very high - emotional dysregulation
            TriggerChance = 15,
            HealingTime = 20,
            MinAge = 16
        },

        // === Dissoziative IdentitätsstörungEN ===

        ["DissoziativeStörung"] = new DiseaseConfig
        {
            Name = "Dissoziative Identitätsstörung",
            StressDebuff = 1.25,
            MoodDebuff = 0.80,
            SocialProximityDebuff = 0.82,
            // Dynamic ranges (dissociative episodes)
            StressDebuffMin = 1.0,
            StressDebuffMax = 1.50,
            MoodDebuffMin = 0.65,
            MoodDebuffMax = 0.92,
            SocialDebuffMin = 0.68,
            SocialDebuffMax = 0.92,
            Volatility = 0.65, // High - dissociative episodes are unpredictable
            TriggerChance = 16,
            HealingTime = 16,
            MinAge = 5
        }
    };
}