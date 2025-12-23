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
/// Unit tests for the EventWeightCalculator service.
/// Tests verify the weight calculation formula combining base probability with influence factors.
/// </summary>
public sealed class EventWeightCalculatorTests
{
    private readonly Mock<IFactorNormalizer> _factorNormalizerMock;
    private readonly Mock<IInfluenceCalculator> _influenceCalculatorMock;
    private readonly EventWeightCalculator _sut;

    public EventWeightCalculatorTests()
    {
        _factorNormalizerMock = new Mock<IFactorNormalizer>();
        _influenceCalculatorMock = new Mock<IInfluenceCalculator>();
        _sut = new EventWeightCalculator(_factorNormalizerMock.Object, _influenceCalculatorMock.Object);
    }

    #region Constructor Tests

    [Fact]
    public void Constructor_WithNullFactorNormalizer_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new EventWeightCalculator(null!, _influenceCalculatorMock.Object));
    }

    [Fact]
    public void Constructor_WithNullInfluenceCalculator_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new EventWeightCalculator(_factorNormalizerMock.Object, null!));
    }

    #endregion

    #region CalculateWeight Tests

    [Fact]
    public void CalculateWeight_WithNoInfluenceFactors_ReturnsBaseProbability()
    {
        // Arrange
        var lifeEvent = new GenericEvent
        {
            Id = "test_event",
            BaseProbability = 0.5,
            InfluenceFactors = []
        };
        var state = CreateDefaultSimulationState();

        // Act
        var result = _sut.CalculateWeight(lifeEvent, state);

        // Assert
        Assert.Equal(0.5, result, precision: 5);
    }

    [Fact]
    public void CalculateWeight_WithSingleInfluenceFactor_AppliesMultiplier()
    {
        // Arrange
        var lifeEvent = new GenericEvent
        {
            Id = "test_event",
            BaseProbability = 0.4,
            InfluenceFactors = [new InfluenceFactor("AnxietyLevel", 1.5)]
        };
        var state = CreateDefaultSimulationState();

        _factorNormalizerMock
            .Setup(x => x.Normalize(state, "AnxietyLevel"))
            .Returns(0.6);
        _influenceCalculatorMock
            .Setup(x => x.CalculateInfluence(0.6, 1.5))
            .Returns(1.2);

        // Act
        var result = _sut.CalculateWeight(lifeEvent, state);

        // Assert
        var expected = 0.4 * 1.2;
        Assert.Equal(expected, result, precision: 5);
    }

    [Fact]
    public void CalculateWeight_WithMultipleInfluenceFactors_MultipliesAllFactors()
    {
        // Arrange
        var lifeEvent = new GenericEvent
        {
            Id = "test_event",
            BaseProbability = 0.3,
            InfluenceFactors =
            [
                new InfluenceFactor("AnxietyLevel", 1.5),
                new InfluenceFactor("FamilyCloseness", 1.2)
            ]
        };
        var state = CreateDefaultSimulationState();

        _factorNormalizerMock.Setup(x => x.Normalize(state, "AnxietyLevel")).Returns(0.5);
        _factorNormalizerMock.Setup(x => x.Normalize(state, "FamilyCloseness")).Returns(0.8);
        _influenceCalculatorMock.Setup(x => x.CalculateInfluence(0.5, 1.5)).Returns(1.1);
        _influenceCalculatorMock.Setup(x => x.CalculateInfluence(0.8, 1.2)).Returns(1.3);

        // Act
        var result = _sut.CalculateWeight(lifeEvent, state);

        // Assert
        var expected = 0.3 * 1.1 * 1.3;
        Assert.Equal(expected, result, precision: 5);
    }

    [Fact]
    public void CalculateWeight_WithVeryHighResult_ClampsToMaximum()
    {
        // Arrange
        var lifeEvent = new GenericEvent
        {
            Id = "test_event",
            BaseProbability = 0.9,
            InfluenceFactors = [new InfluenceFactor("AnxietyLevel", 2.0)]
        };
        var state = CreateDefaultSimulationState();

        _factorNormalizerMock.Setup(x => x.Normalize(state, "AnxietyLevel")).Returns(0.9);
        _influenceCalculatorMock.Setup(x => x.CalculateInfluence(0.9, 2.0)).Returns(5.0);

        // Act
        var result = _sut.CalculateWeight(lifeEvent, state);

        // Assert
        Assert.Equal(0.99, result, precision: 5);
    }

    [Fact]
    public void CalculateWeight_WithVeryLowResult_ClampsToMinimum()
    {
        // Arrange
        var lifeEvent = new GenericEvent
        {
            Id = "test_event",
            BaseProbability = 0.01,
            InfluenceFactors = [new InfluenceFactor("AnxietyLevel", 2.0)]
        };
        var state = CreateDefaultSimulationState();

        _factorNormalizerMock.Setup(x => x.Normalize(state, "AnxietyLevel")).Returns(0.1);
        _influenceCalculatorMock.Setup(x => x.CalculateInfluence(0.1, 2.0)).Returns(0.01);

        // Act
        var result = _sut.CalculateWeight(lifeEvent, state);

        // Assert - result is clamped to MINIMUM_WHEIGHT (0.01)
        Assert.Equal(0.01, result, precision: 5);
    }

    [Fact]
    public void CalculateWeight_WithNullEvent_ThrowsArgumentNullException()
    {
        // Arrange
        var state = CreateDefaultSimulationState();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sut.CalculateWeight(null!, state));
    }

    [Fact]
    public void CalculateWeight_WithNullState_ThrowsArgumentNullException()
    {
        // Arrange
        var lifeEvent = new GenericEvent { Id = "test" };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sut.CalculateWeight(lifeEvent, null!));
    }

    #endregion

    #region CopingMechanism Habit Boost Tests

    [Fact]
    public void CalculateWeight_WithHabitFormingCopingAndPreviousUsage_AppliesHabitBoost()
    {
        // Arrange
        var coping = new CopingMechanism
        {
            Id = "coping_exercise",
            BaseProbability = 0.4,
            IsHabitForming = true,
            InfluenceFactors = [],
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(stressThreshold: 50)
        };

        var state = CreateDefaultSimulationState();
        state.CopingPreferences["coping_exercise"] = 50.0;

        // Act
        var result = _sut.CalculateWeight(coping, state);

        // Assert
        Assert.True(result > 0.4, "Habit-forming coping with usage history should have boosted weight");
    }

    [Fact]
    public void CalculateWeight_WithNonHabitFormingCoping_NoHabitBoost()
    {
        // Arrange
        var coping = new CopingMechanism
        {
            Id = "coping_distraction",
            BaseProbability = 0.5,
            IsHabitForming = false,
            InfluenceFactors = [],
            Type = CopingType.Neutral,
            Trigger = new CopingTrigger(stressThreshold: 40)
        };

        var state = CreateDefaultSimulationState();
        state.CopingPreferences["coping_distraction"] = 80.0;

        // Act
        var result = _sut.CalculateWeight(coping, state);

        // Assert
        Assert.Equal(0.5, result, precision: 5);
    }

    #endregion

    #region CalculateAllWeights Tests

    [Fact]
    public void CalculateAllWeights_WithMultipleEvents_ReturnsNormalizedProbabilities()
    {
        // Arrange
        var events = new List<LifeEvent>
        {
            new GenericEvent { Id = "event1", BaseProbability = 0.3, InfluenceFactors = [] },
            new GenericEvent { Id = "event2", BaseProbability = 0.5, InfluenceFactors = [] },
            new GenericEvent { Id = "event3", BaseProbability = 0.2, InfluenceFactors = [] }
        };
        var state = CreateDefaultSimulationState();

        // Act
        var result = _sut.CalculateAllWeights(events, state);

        // Assert
        Assert.Equal(3, result.Count);

        var totalProbability = result.Sum(w => w.NormalizedProbability);
        Assert.Equal(1.0, totalProbability, precision: 5);
    }

    [Fact]
    public void CalculateAllWeights_PreservesEventOrder()
    {
        // Arrange
        var events = new List<LifeEvent>
        {
            new GenericEvent { Id = "first", BaseProbability = 0.2, InfluenceFactors = [] },
            new GenericEvent { Id = "second", BaseProbability = 0.3, InfluenceFactors = [] }
        };
        var state = CreateDefaultSimulationState();

        // Act
        var result = _sut.CalculateAllWeights(events, state);

        // Assert
        Assert.Equal("first", result[0].Event.Id);
        Assert.Equal("second", result[1].Event.Id);
    }

    [Fact]
    public void CalculateAllWeights_WithEmptyList_ReturnsEmptyList()
    {
        // Arrange
        var events = new List<LifeEvent>();
        var state = CreateDefaultSimulationState();

        // Act
        var result = _sut.CalculateAllWeights(events, state);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void CalculateAllWeights_WithNullEvents_ThrowsArgumentNullException()
    {
        // Arrange
        var state = CreateDefaultSimulationState();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sut.CalculateAllWeights(null!, state));
    }

    #endregion

    #region Helper Methods

    private static SimulationState CreateDefaultSimulationState()
    {
        return new SimulationState
        {
            CurrentAge = 25,
            IncomeLevel = IncomeLevel.Medium,
            ParentsEducationLevel = ParentsEducationLevel.Medium,
            JobStatus = JobStatus.MediumPrestige,
            SocialEnvironmentLevel = 50,
            FamilyCloseness = 60,
            ParentsRelationshipQuality = 70,
            ParentsWithAddiction = false,
            HasAdhd = false,
            HasAutism = false,
            IntelligenceScore = 100,
            AnxietyLevel = 30,
            SocialEnergyLevel = SocialEnergyLevel.Ambivert,
            CurrentStress = 40,
            CurrentMood = 20,
            SocialBelonging = 60,
            ResilienceScore = 50,
            PhysicalHealth = 80,
            Gender = GenderType.Male,
            LifePhase = LifePhase.Adulthood,
            EventHistory = [],
            CopingPreferences = new Dictionary<string, double>()
        };
    }

    #endregion
}
