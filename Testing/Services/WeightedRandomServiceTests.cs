using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SimulationFIN31.Models;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;
using SimulationFIN31.Services;
using SimulationFIN31.Services.Interfaces;
using Xunit;

namespace SimulationFIN31.Tests.Services;

/// <summary>
/// Unit tests for the WeightedRandomService using Stochastic Universal Sampling.
/// Tests verify selection behavior, edge cases, and statistical properties of SUS.
/// </summary>
public sealed class WeightedRandomServiceTests
{
    private readonly Mock<IEventWeightCalculator> _weightCalculatorMock;
    private readonly Mock<ICopingTriggerChecker> _triggerCheckerMock;

    public WeightedRandomServiceTests()
    {
        _weightCalculatorMock = new Mock<IEventWeightCalculator>();
        _triggerCheckerMock = new Mock<ICopingTriggerChecker>();
    }

    #region Constructor Tests

    [Fact]
    public void Constructor_WithNullWeightCalculator_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new WeightedRandomService(null!, _triggerCheckerMock.Object));
    }

    [Fact]
    public void Constructor_WithNullTriggerChecker_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new WeightedRandomService(_weightCalculatorMock.Object, null!));
    }

    #endregion

    #region SelectGenericEvent Tests

    [Fact]
    public void SelectGenericEvent_WithEligibleEvents_ReturnsEvent()
    {
        // Arrange
        var events = new List<GenericEvent>
        {
            CreateGenericEvent("event1", baseProbability: 0.5),
            CreateGenericEvent("event2", baseProbability: 0.3)
        };
        var state = CreateSimulationState();

        SetupWeightCalculator(events, state);
        var sut = CreateServiceWithSeed(42);

        // Act
        var result = sut.SelectGenericEvent(events, state);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result.Id, new[] { "event1", "event2" });
    }

    [Fact]
    public void SelectGenericEvent_WithNoEligibleEvents_ReturnsNull()
    {
        // Arrange
        var events = new List<GenericEvent>
        {
            CreateGenericEvent("event1", minAge: 30, maxAge: 40)
        };
        var state = CreateSimulationState(currentAge: 25);

        var sut = CreateServiceWithSeed(42);

        // Act
        var result = sut.SelectGenericEvent(events, state);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void SelectGenericEvent_WithEmptyList_ReturnsNull()
    {
        // Arrange
        var events = new List<GenericEvent>();
        var state = CreateSimulationState();
        var sut = CreateServiceWithSeed(42);

        // Act
        var result = sut.SelectGenericEvent(events, state);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void SelectGenericEvent_WithNullEvents_ThrowsArgumentNullException()
    {
        // Arrange
        var state = CreateSimulationState();
        var sut = CreateServiceWithSeed(42);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => sut.SelectGenericEvent(null!, state));
    }

    [Fact]
    public void SelectGenericEvent_ExcludesUniqueEventAlreadyOccurred()
    {
        // Arrange
        var events = new List<GenericEvent>
        {
            CreateGenericEvent("unique_event", isUnique: true),
            CreateGenericEvent("repeatable_event", isUnique: false)
        };
        var state = CreateSimulationState();
        state.EventHistory.Add("unique_event");

        SetupWeightCalculatorForSingleEvent(events[1], state);
        var sut = CreateServiceWithSeed(42);

        // Act
        var result = sut.SelectGenericEvent(events, state);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("repeatable_event", result.Id);
    }

    #endregion

    #region SelectPersonalEvent Tests

    [Fact]
    public void SelectPersonalEvent_WithEligibleEvents_ReturnsEvent()
    {
        // Arrange
        var events = new List<PersonalEvent>
        {
            CreatePersonalEvent("personal1", baseProbability: 0.4),
            CreatePersonalEvent("personal2", baseProbability: 0.6)
        };
        var state = CreateSimulationState();

        SetupWeightCalculator(events, state);
        var sut = CreateServiceWithSeed(42);

        // Act
        var result = sut.SelectPersonalEvent(events, state);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result.Id, new[] { "personal1", "personal2" });
    }

    [Fact]
    public void SelectPersonalEvent_WithNullState_ThrowsArgumentNullException()
    {
        // Arrange
        var events = new List<PersonalEvent> { CreatePersonalEvent("test") };
        var sut = CreateServiceWithSeed(42);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => sut.SelectPersonalEvent(events, null!));
    }

    #endregion

    #region SelectCopingMechanism Tests

    [Fact]
    public void SelectCopingMechanism_WithTriggeredMechanisms_ReturnsMechanism()
    {
        // Arrange
        var mechanisms = new List<CopingMechanism>
        {
            CreateCopingMechanism("coping1", stressThreshold: 50),
            CreateCopingMechanism("coping2", stressThreshold: 60)
        };
        var state = CreateSimulationState(currentStress: 70);

        var triggered = new List<CopingMechanism> { mechanisms[0], mechanisms[1] };
        _triggerCheckerMock
            .Setup(x => x.FilterTriggered(mechanisms, state))
            .Returns(triggered);

        SetupWeightCalculator(triggered, state);
        var sut = CreateServiceWithSeed(42);

        // Act
        var result = sut.SelectCopingMechanism(mechanisms, state);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result.Id, new[] { "coping1", "coping2" });
    }

    [Fact]
    public void SelectCopingMechanism_WithNoTriggeredMechanisms_ReturnsNull()
    {
        // Arrange
        var mechanisms = new List<CopingMechanism>
        {
            CreateCopingMechanism("coping1", stressThreshold: 80)
        };
        var state = CreateSimulationState(currentStress: 30);

        _triggerCheckerMock
            .Setup(x => x.FilterTriggered(mechanisms, state))
            .Returns(new List<CopingMechanism>());

        var sut = CreateServiceWithSeed(42);

        // Act
        var result = sut.SelectCopingMechanism(mechanisms, state);

        // Assert
        Assert.Null(result);
    }

    #endregion

    #region SelectMultiple Tests

    [Fact]
    public void SelectMultiple_WithSufficientEvents_ReturnsRequestedCount()
    {
        // Arrange
        var events = new List<GenericEvent>
        {
            CreateGenericEvent("event1", baseProbability: 0.3),
            CreateGenericEvent("event2", baseProbability: 0.3),
            CreateGenericEvent("event3", baseProbability: 0.2),
            CreateGenericEvent("event4", baseProbability: 0.2)
        };
        var state = CreateSimulationState();

        SetupWeightCalculator(events, state);
        var sut = CreateServiceWithSeed(42);

        // Act
        var result = sut.SelectMultiple(events, state, 3);

        // Assert
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void SelectMultiple_WithFewerEligibleThanRequested_ReturnsAvailable()
    {
        // Arrange
        var events = new List<GenericEvent>
        {
            CreateGenericEvent("event1", baseProbability: 0.5),
            CreateGenericEvent("event2", baseProbability: 0.5)
        };
        var state = CreateSimulationState();

        SetupWeightCalculator(events, state);
        var sut = CreateServiceWithSeed(42);

        // Act
        var result = sut.SelectMultiple(events, state, 5);

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void SelectMultiple_WithZeroCount_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var events = new List<GenericEvent> { CreateGenericEvent("test") };
        var state = CreateSimulationState();
        var sut = CreateServiceWithSeed(42);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => sut.SelectMultiple(events, state, 0));
    }

    [Fact]
    public void SelectMultiple_WithNegativeCount_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var events = new List<GenericEvent> { CreateGenericEvent("test") };
        var state = CreateSimulationState();
        var sut = CreateServiceWithSeed(42);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => sut.SelectMultiple(events, state, -1));
    }

    [Fact]
    public void SelectMultiple_WithNoEligibleEvents_ReturnsEmptyList()
    {
        // Arrange
        var events = new List<GenericEvent>
        {
            CreateGenericEvent("event1", minAge: 50)
        };
        var state = CreateSimulationState(currentAge: 20);

        var sut = CreateServiceWithSeed(42);

        // Act
        var result = sut.SelectMultiple(events, state, 3);

        // Assert
        Assert.Empty(result);
    }

    #endregion

    #region GetWeightedEvents Tests

    [Fact]
    public void GetWeightedEvents_ReturnsWeightedList()
    {
        // Arrange
        var events = new List<GenericEvent>
        {
            CreateGenericEvent("event1", baseProbability: 0.4),
            CreateGenericEvent("event2", baseProbability: 0.6)
        };
        var state = CreateSimulationState();

        var expectedWeighted = new List<WeightedEvent>
        {
            new(events[0], 0.4, 0.4),
            new(events[1], 0.6, 0.6)
        };

        _weightCalculatorMock
            .Setup(x => x.CalculateAllWeights(It.IsAny<IEnumerable<LifeEvent>>(), state))
            .Returns(expectedWeighted);

        var sut = CreateServiceWithSeed(42);

        // Act
        var result = sut.GetWeightedEvents(events, state);

        // Assert
        Assert.Equal(2, result.Count);
    }

    #endregion

    #region SelectEvent (IEventSelector) Tests

    [Fact]
    public void SelectEvent_WithWeightedList_ReturnsEvent()
    {
        // Arrange
        var event1 = CreateGenericEvent("event1");
        var event2 = CreateGenericEvent("event2");

        var weightedEvents = new List<WeightedEvent>
        {
            new(event1, 0.3, 0.3),
            new(event2, 0.7, 0.7)
        };

        var sut = CreateServiceWithSeed(42);

        // Act
        var result = sut.SelectEvent(weightedEvents);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void SelectEvent_WithEmptyList_ReturnsNull()
    {
        // Arrange
        var weightedEvents = new List<WeightedEvent>();
        var sut = CreateServiceWithSeed(42);

        // Act
        var result = sut.SelectEvent(weightedEvents);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void SelectEvent_WithZeroProbabilities_ReturnsNull()
    {
        // Arrange
        var event1 = CreateGenericEvent("event1");
        var weightedEvents = new List<WeightedEvent>
        {
            new(event1, 0.0, 0.0)
        };

        var sut = CreateServiceWithSeed(42);

        // Act
        var result = sut.SelectEvent(weightedEvents);

        // Assert
        Assert.Null(result);
    }

    #endregion

    #region SUS Algorithm Statistical Properties Tests

    [Fact]
    public void SelectMultiple_WithDeterministicSeed_ProducesConsistentResults()
    {
        // Arrange
        var events = new List<GenericEvent>
        {
            CreateGenericEvent("event1", baseProbability: 0.5),
            CreateGenericEvent("event2", baseProbability: 0.3),
            CreateGenericEvent("event3", baseProbability: 0.2)
        };
        var state = CreateSimulationState();

        SetupWeightCalculator(events, state);
        var sut1 = CreateServiceWithSeed(123);
        var sut2 = CreateServiceWithSeed(123);

        // Act
        var result1 = sut1.SelectMultiple(events, state, 2);
        var result2 = sut2.SelectMultiple(events, state, 2);

        // Assert
        Assert.Equal(result1.Count, result2.Count);
        for (var i = 0; i < result1.Count; i++)
        {
            Assert.Equal(result1[i].Id, result2[i].Id);
        }
    }

    [Fact]
    public void SelectMultiple_OverManyIterations_SelectsProportionalToWeights()
    {
        // Arrange
        var events = new List<GenericEvent>
        {
            CreateGenericEvent("high_prob", baseProbability: 0.8),
            CreateGenericEvent("low_prob", baseProbability: 0.2)
        };
        var state = CreateSimulationState();

        SetupWeightCalculator(events, state);

        var highCount = 0;
        var lowCount = 0;
        const int iterations = 1000;

        // Act
        for (var i = 0; i < iterations; i++)
        {
            var sut = CreateServiceWithSeed(i);
            var result = sut.SelectGenericEvent(events, state);

            if (result?.Id == "high_prob") highCount++;
            else if (result?.Id == "low_prob") lowCount++;
        }

        // Assert
        var highRatio = (double)highCount / iterations;
        Assert.True(highRatio > 0.65, $"High probability event should be selected ~80% of the time, was {highRatio:P}");
        Assert.True(highRatio < 0.95, $"High probability event ratio seems too high: {highRatio:P}");
    }

    #endregion

    #region Helper Methods

    private WeightedRandomService CreateServiceWithSeed(int seed)
    {
        return new WeightedRandomService(_weightCalculatorMock.Object, _triggerCheckerMock.Object, seed);
    }

    private void SetupWeightCalculator<TEvent>(List<TEvent> events, SimulationState state)
        where TEvent : LifeEvent
    {
        var weightedEvents = events
            .Select(e => new WeightedEvent(e, e.BaseProbability, e.BaseProbability / events.Sum(x => x.BaseProbability)))
            .ToList();

        _weightCalculatorMock
            .Setup(x => x.CalculateAllWeights(It.IsAny<IEnumerable<LifeEvent>>(), state))
            .Returns(weightedEvents);
    }

    private void SetupWeightCalculatorForSingleEvent(LifeEvent lifeEvent, SimulationState state)
    {
        var weightedEvents = new List<WeightedEvent>
        {
            new(lifeEvent, lifeEvent.BaseProbability, 1.0)
        };

        _weightCalculatorMock
            .Setup(x => x.CalculateAllWeights(It.IsAny<IEnumerable<LifeEvent>>(), state))
            .Returns(weightedEvents);
    }

    private static GenericEvent CreateGenericEvent(
        string id,
        double baseProbability = 0.5,
        int minAge = 0,
        int maxAge = 100,
        bool isUnique = false)
    {
        return new GenericEvent
        {
            Id = id,
            Name = $"Test Event {id}",
            BaseProbability = baseProbability,
            MinAge = minAge,
            MaxAge = maxAge,
            IsUnique = isUnique,
            InfluenceFactors = []
        };
    }

    private static PersonalEvent CreatePersonalEvent(
        string id,
        double baseProbability = 0.5,
        int minAge = 0,
        int maxAge = 100)
    {
        return new PersonalEvent
        {
            Id = id,
            Name = $"Test Personal Event {id}",
            BaseProbability = baseProbability,
            MinAge = minAge,
            MaxAge = maxAge,
            InfluenceFactors = []
        };
    }

    private static CopingMechanism CreateCopingMechanism(
        string id,
        double? stressThreshold = null,
        double baseProbability = 0.5)
    {
        return new CopingMechanism
        {
            Id = id,
            Name = $"Test Coping {id}",
            BaseProbability = baseProbability,
            MinAge = 0,
            MaxAge = 100,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(stressThreshold),
            IsHabitForming = false,
            InfluenceFactors = []
        };
    }

    private static SimulationState CreateSimulationState(
        int currentAge = 25,
        double currentStress = 40)
    {
        return new SimulationState
        {
            CurrentAge = currentAge,
            IncomeLevel = IncomeLevel.Medium,
            ParentsEducationLevel = ParentsEducationLevel.Medium,
            JobStatus = JobStatus.MediumPrestige,
            SocialEnvironmentLevel = 50,
            FamilyCloseness = 60,
            ParentsRelationshipQuality = ParentsRelationshipQuality.Harmonious,
            ParentsWithAddiction = false,
            HasAdhd = false,
            HasAutism = false,
            IntelligenceScore = 100,
            AnxietyLevel = 30,
            SocialEnergyLevel = SocialEnergyLevel.Ambivert,
            CurrentStress = currentStress,
            CurrentMood = 20,
            SocialBelonging = 60,
            ResilienceScore = 50,
            PhysicalHealth = 80,
            Gender = GenderType.Male,
            LifePhase = LifePhase.Adulthood,
            EventHistory = new List<string>(),
            CopingPreferences = new Dictionary<string, double>()
        };
    }

    #endregion
}
