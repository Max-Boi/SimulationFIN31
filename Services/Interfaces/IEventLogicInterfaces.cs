using System.Collections.Generic;
using SimulationFIN31.Models;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;

namespace SimulationFIN31.Services.Interfaces;

/// <summary>
///     Normalisiert Avatar-Eigenschaften auf [0,1] für Berechnungen
/// </summary>
public interface IFactorNormalizer
{
    double Normalize(SimulationState profile, string factorName);
}

/// <summary>
///     Berechnet Einfluss eines Faktors basierend auf Exponenten
/// </summary>
public interface IInfluenceCalculator
{
    double CalculateInfluence(double normalizedValue, double exponent);
}

/// <summary>
///     Berechnet Gewichtung eines Events für einen Avatar
/// </summary>
public interface IEventWeightCalculator
{
    double CalculateWeight(LifeEvent lifeEvent, SimulationState profile);

    List<WeightedEvent> CalculateAllWeights(
        IEnumerable<LifeEvent> events,
        SimulationState profile);
}

/// <summary>
///     Wählt Event basierend auf Gewichtungen aus
/// </summary>
public interface IEventSelector
{
    LifeEvent? SelectEvent(List<WeightedEvent> weightedEvents);
}

/// <summary>
///     Wendet Event-Effekte auf Avatar an
/// </summary>
public interface IEventEffectApplier
{
    void ApplyEffects(LifeEvent lifeEvent, SimulationState profile);
    EventOccurrence RecordOccurrence(LifeEvent lifeEvent, SimulationState profile);
}

/// <summary>
///     Prüft ob Coping-Trigger erfüllt sind
/// </summary>
public interface ICopingTriggerChecker
{
    bool IsTriggered(CopingMechanism coping, SimulationState profile);

    List<CopingMechanism> FilterTriggered(
        IEnumerable<CopingMechanism> mechanisms,
        SimulationState profile);
}