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
            StressDebuff = 1.1,      // Leicht erhöhte Stressreaktivität
            MoodDebuff = 0.85,        // Leichte Stimmungseinbuße
            SocialProximityDebuff = 0.90, // Geringe soziale Rückzugstendenz
            TriggerCountdown = 4,
            HealingTime = 2
        },
        
        // === ANGSTSTÖRUNGEN ===
        
        ["GeneralizedAnxiety"] = new DiseaseConfig
        {
            Name = "Generalized Anxiety Disorder",
            StressDebuff = 1.3,       // Chronisch erhöhte Stressreaktivität
            MoodDebuff = 0.75,        // Moderate Stimmungsbeeinträchtigung
            SocialProximityDebuff = 0.85, // Leichte bis moderate soziale Beeinträchtigung
            TriggerCountdown = 6,     // Entwickelt sich schleichend
            HealingTime = 8           // Chronischer Verlauf, schwer zu behandeln
        },
        
        ["SocialPhobia"] = new DiseaseConfig
        {
            Name = "Social Anxiety Disorder",
            StressDebuff = 1.2,      // Erhöhte Stressreaktivität in sozialen Situationen
            MoodDebuff = 0.80,        // Moderate Stimmungseinbuße
            SocialProximityDebuff = 0.50, // MASSIVER sozialer Rückzug (Hauptsymptom)
            TriggerCountdown = 6,
            HealingTime = 8          // Sehr persistent, schwer zu behandeln
        },
        
        ["PanicDisorder"] = new DiseaseConfig
        {
            Name = "Panic Disorder",
            StressDebuff = 1.45,      // Sehr hohe Stressreaktivität (Angst vor Angst)
            MoodDebuff = 0.85,        
            SocialProximityDebuff = 0.8, // Vermeidungsverhalten beeinträchtigt soziales Leben
            TriggerCountdown = 4,     // Kann schnell entstehen
            HealingTime = 7
        },
        
        // === TRAUMA-BEZOGENE STÖRUNGEN ===
        
        ["PTSD"] = new DiseaseConfig
        {
            Name = "Post-Traumatic Stress Disorder",
            StressDebuff = 1.5,       // Sehr hohe Stressreaktivität (Hyperarousal)
            MoodDebuff = 0.7,        // Starke Stimmungseinbuße (Numbing, Depression)
            SocialProximityDebuff = 0.7, // Deutlicher Rückzug, Vertrauensprobleme
            TriggerCountdown = 3,     // Kann akut nach Trauma auftreten
            HealingTime = 10          // Sehr langwierig, oft chronisch
        },
        
        // === SUCHTERKRANKUNGEN ===
        
        ["Alcoholism"] = new DiseaseConfig
        {
            Name = "Alcohol Use Disorder",
            StressDebuff = 1.1,      // Erhöhte Stressreaktivität (besonders bei Entzug)
            MoodDebuff = 0.65,        // Depression durch Alkohol
            SocialProximityDebuff = 0.60, // Beziehungsprobleme, Isolation
            TriggerCountdown = 12,    // Entwickelt sich langsam über Jahre
            HealingTime = 10          // Hohe Rückfallrate
        },
        
        ["SubstanceAbuse"] = new DiseaseConfig
        {
            Name = "Substance Use Disorder",
            StressDebuff = 1.3,       
            MoodDebuff = 0.60,        // Kann je nach Substanz variieren
            SocialProximityDebuff = 0.55, // Oft stärkere soziale Isolation
            TriggerCountdown = 10,
            HealingTime = 12
        },
        
        // === ESSSTÖRUNGEN ===
        
        ["AnorexiaNervosa"] = new DiseaseConfig
        {
            Name = "Anorexia Nervosa",
            StressDebuff = 1.4,       // Hohe Stressreaktivität (Perfektionismus, Kontrolle)
            MoodDebuff = 0.6,        // Schwere depressive Symptome häufig
            SocialProximityDebuff = 0.65, // Sozialer Rückzug (Scham, Verheimlichung)
            TriggerCountdown = 8,
            HealingTime = 10          // Höchste Mortalität unter psych. Störungen, sehr langwierig
        },
        
        ["BulimiaNervosa"] = new DiseaseConfig
        {
            Name = "Bulimia Nervosa",
            StressDebuff = 1.2,      
            MoodDebuff = 0.8,        // Scham, Depression
            SocialProximityDebuff = 0.70, // Oft besser kaschiert, weniger Rückzug
            TriggerCountdown = 6,
            HealingTime = 10
        },
        
        ["BingeEatingDisorder"] = new DiseaseConfig
        {
            Name = "Binge Eating Disorder",
            StressDebuff = 1.25,      // Stress als Trigger
            MoodDebuff = 0.75,        // Scham, aber oft weniger schwer als andere Essstörungen
            SocialProximityDebuff = 0.80, // Geringere soziale Beeinträchtigung
            TriggerCountdown = 5,
            HealingTime = 8
        },
        
        // === ZWANGSSTÖRUNGEN ===
        
        ["OCD"] = new DiseaseConfig
        {
            Name = "Obsessive-Compulsive Disorder",
            StressDebuff = 1.45,      // Hohe chronische Anspannung
            MoodDebuff = 0.70,        // Frustration, Erschöpfung
            SocialProximityDebuff = 0.75, // Zeitverlust durch Zwänge beeinträchtigt Beziehungen
            TriggerCountdown = 10,    // Oft schleichender Beginn
            HealingTime = 12          // Chronischer Verlauf, aber behandelbar
        },
        
        // === PERSÖNLICHKEITSSTÖRUNGEN ===
        
        ["BorderlinePersonality"] = new DiseaseConfig
        {
            Name = "Borderline Personality Disorder",
            StressDebuff = 1.6,       // Sehr hohe emotionale Dysregulation
            MoodDebuff = 0.50,        // Extreme Stimmungsschwankungen (Durchschnitt niedrig)
            SocialProximityDebuff = 0.60, // Instabile Beziehungen (Nähe/Distanz-Problem)
            TriggerCountdown = 15,    // Entwickelt sich über Adoleszenz
            HealingTime = 20          // Sehr langwierige Therapie (DBT)
        },
        
        ["AvoidantPersonality"] = new DiseaseConfig
        {
            Name = "Avoidant Personality Disorder",
            StressDebuff = 1.3,       
            MoodDebuff = 0.75,        
            SocialProximityDebuff = 0.40, // MASSIVE soziale Vermeidung (Kernsymptom)
            TriggerCountdown = 12,    
            HealingTime = 15
        },
        
        // === DISSOZIATIVE STÖRUNGEN ===
        
        ["DissociativeDisorder"] = new DiseaseConfig
        {
            Name = "Dissociative Disorder",
            StressDebuff = 1.5,       // Hohe Stressreaktivität
            MoodDebuff = 0.60,        // Emotionale Taubheit, Depression
            SocialProximityDebuff = 0.65, // Entfremdung von anderen
            TriggerCountdown = 8,
            HealingTime = 16          // Sehr komplex zu behandeln
        }
    };
}