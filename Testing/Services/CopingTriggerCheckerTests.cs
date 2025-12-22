using System;
using System.Collections.Generic;
using SimulationFIN31.Models;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.structs;
using SimulationFIN31.Services;
using Xunit;

namespace SimulationFIN31.Tests.Services;

/// <summary>
/// Unit tests for the CopingTriggerChecker service.
/// Tests verify the trigger logic for coping mechanisms based on stress, mood, and belonging thresholds.
/// </summary>
public sealed class CopingTriggerCheckerTests
{
    private readonly CopingTriggerChecker _sut;

    public CopingTriggerCheckerTests()
    {
        _sut = new CopingTriggerChecker();
    }

    #region IsTriggered - Stress Threshold Tests

    [Fact]
    public void IsTriggered_WithStressAboveThreshold_ReturnsTrue()
    {
        // Arrange
        var coping = CreateCopingMechanism(stressThreshold: 50);
        var state = CreateSimulationState(currentStress: 60);

        // Act
        var result = _sut.IsTriggered(coping, state);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsTriggered_WithStressEqualToThreshold_ReturnsTrue()
    {
        // Arrange
        var coping = CreateCopingMechanism(stressThreshold: 50);
        var state = CreateSimulationState(currentStress: 50);

        // Act
        var result = _sut.IsTriggered(coping, state);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsTriggered_WithStressBelowThreshold_ReturnsFalse()
    {
        // Arrange
        var coping = CreateCopingMechanism(stressThreshold: 50);
        var state = CreateSimulationState(currentStress: 40);

        // Act
        var result = _sut.IsTriggered(coping, state);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region IsTriggered - Mood Threshold Tests

    [Fact]
    public void IsTriggered_WithMoodBelowThreshold_ReturnsTrue()
    {
        // Arrange
        var coping = CreateCopingMechanism(moodThreshold: -20);
        var state = CreateSimulationState(currentMood: -30);

        // Act
        var result = _sut.IsTriggered(coping, state);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsTriggered_WithMoodEqualToThreshold_ReturnsTrue()
    {
        // Arrange
        var coping = CreateCopingMechanism(moodThreshold: -20);
        var state = CreateSimulationState(currentMood: -20);

        // Act
        var result = _sut.IsTriggered(coping, state);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsTriggered_WithMoodAboveThreshold_ReturnsFalse()
    {
        // Arrange
        var coping = CreateCopingMechanism(moodThreshold: -20);
        var state = CreateSimulationState(currentMood: 10);

        // Act
        var result = _sut.IsTriggered(coping, state);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region IsTriggered - Belonging Threshold Tests

    [Fact]
    public void IsTriggered_WithBelongingBelowThreshold_ReturnsTrue()
    {
        // Arrange
        var coping = CreateCopingMechanism(belongingThreshold: 40);
        var state = CreateSimulationState(socialBelonging: 30);

        // Act
        var result = _sut.IsTriggered(coping, state);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsTriggered_WithBelongingEqualToThreshold_ReturnsTrue()
    {
        // Arrange
        var coping = CreateCopingMechanism(belongingThreshold: 40);
        var state = CreateSimulationState(socialBelonging: 40);

        // Act
        var result = _sut.IsTriggered(coping, state);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsTriggered_WithBelongingAboveThreshold_ReturnsFalse()
    {
        // Arrange
        var coping = CreateCopingMechanism(belongingThreshold: 40);
        var state = CreateSimulationState(socialBelonging: 60);

        // Act
        var result = _sut.IsTriggered(coping, state);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region IsTriggered - Multiple Thresholds (OR Logic) Tests

    [Fact]
    public void IsTriggered_WithMultipleThresholdsAndOneTriggered_ReturnsTrue()
    {
        // Arrange
        var coping = CreateCopingMechanism(stressThreshold: 70, moodThreshold: -30);
        var state = CreateSimulationState(currentStress: 80, currentMood: 10);

        // Act
        var result = _sut.IsTriggered(coping, state);

        // Assert
        Assert.True(result, "Should trigger when stress threshold is met even if mood is fine");
    }

    [Fact]
    public void IsTriggered_WithMultipleThresholdsAllTriggered_ReturnsTrue()
    {
        // Arrange
        var coping = CreateCopingMechanism(stressThreshold: 50, moodThreshold: -20, belongingThreshold: 40);
        var state = CreateSimulationState(currentStress: 70, currentMood: -40, socialBelonging: 20);

        // Act
        var result = _sut.IsTriggered(coping, state);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsTriggered_WithMultipleThresholdsNoneTriggered_ReturnsFalse()
    {
        // Arrange
        var coping = CreateCopingMechanism(stressThreshold: 70, moodThreshold: -30);
        var state = CreateSimulationState(currentStress: 40, currentMood: 20);

        // Act
        var result = _sut.IsTriggered(coping, state);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region IsTriggered - No Trigger Tests

    [Fact]
    public void IsTriggered_WithNoTriggerDefined_ReturnsTrue()
    {
        // Arrange
        var coping = CreateCopingMechanism();
        var state = CreateSimulationState();

        // Act
        var result = _sut.IsTriggered(coping, state);

        // Assert
        Assert.True(result, "Coping with no triggers should always be available");
    }

    #endregion

    #region IsTriggered - Null Parameter Tests

    [Fact]
    public void IsTriggered_WithNullCoping_ThrowsArgumentNullException()
    {
        // Arrange
        var state = CreateSimulationState();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sut.IsTriggered(null!, state));
    }

    [Fact]
    public void IsTriggered_WithNullState_ThrowsArgumentNullException()
    {
        // Arrange
        var coping = CreateCopingMechanism();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sut.IsTriggered(coping, null!));
    }

    #endregion

    #region FilterTriggered Tests

    [Fact]
    public void FilterTriggered_WithMixedTriggerStates_ReturnsOnlyTriggered()
    {
        // Arrange
        var mechanisms = new List<CopingMechanism>
        {
            CreateCopingMechanism("coping1", stressThreshold: 50),
            CreateCopingMechanism("coping2", stressThreshold: 80),
            CreateCopingMechanism("coping3", moodThreshold: -30)
        };
        var state = CreateSimulationState(currentStress: 60, currentMood: 10);

        // Act
        var result = _sut.FilterTriggered(mechanisms, state);

        // Assert
        Assert.Single(result);
        Assert.Equal("coping1", result[0].Id);
    }

    [Fact]
    public void FilterTriggered_ExcludesEventsOutsideAgeRange()
    {
        // Arrange
        var mechanisms = new List<CopingMechanism>
        {
            CreateCopingMechanism("coping_adult", stressThreshold: 50, minAge: 18, maxAge: 100),
            CreateCopingMechanism("coping_child", stressThreshold: 50, minAge: 6, maxAge: 12)
        };
        var state = CreateSimulationState(currentStress: 60, currentAge: 25);

        // Act
        var result = _sut.FilterTriggered(mechanisms, state);

        // Assert
        Assert.Single(result);
        Assert.Equal("coping_adult", result[0].Id);
    }

    [Fact]
    public void FilterTriggered_ExcludesUniqueEventsAlreadyInHistory()
    {
        // Arrange
        var mechanisms = new List<CopingMechanism>
        {
            CreateCopingMechanism("coping_unique", stressThreshold: 50, isUnique: true),
            CreateCopingMechanism("coping_repeatable", stressThreshold: 50, isUnique: false)
        };
        var state = CreateSimulationState(currentStress: 60);
        state.EventHistory.Add("coping_unique");

        // Act
        var result = _sut.FilterTriggered(mechanisms, state);

        // Assert
        Assert.Single(result);
        Assert.Equal("coping_repeatable", result[0].Id);
    }

    [Fact]
    public void FilterTriggered_WithAllTriggered_ReturnsAll()
    {
        // Arrange
        var mechanisms = new List<CopingMechanism>
        {
            CreateCopingMechanism("coping1", stressThreshold: 50),
            CreateCopingMechanism("coping2", stressThreshold: 60),
            CreateCopingMechanism("coping3")
        };
        var state = CreateSimulationState(currentStress: 70);

        // Act
        var result = _sut.FilterTriggered(mechanisms, state);

        // Assert
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void FilterTriggered_WithNoneTriggered_ReturnsEmpty()
    {
        // Arrange
        var mechanisms = new List<CopingMechanism>
        {
            CreateCopingMechanism("coping1", stressThreshold: 80),
            CreateCopingMechanism("coping2", stressThreshold: 90)
        };
        var state = CreateSimulationState(currentStress: 30);

        // Act
        var result = _sut.FilterTriggered(mechanisms, state);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void FilterTriggered_WithEmptyList_ReturnsEmpty()
    {
        // Arrange
        var mechanisms = new List<CopingMechanism>();
        var state = CreateSimulationState();

        // Act
        var result = _sut.FilterTriggered(mechanisms, state);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void FilterTriggered_WithNullMechanisms_ThrowsArgumentNullException()
    {
        // Arrange
        var state = CreateSimulationState();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sut.FilterTriggered(null!, state));
    }

    #endregion

    #region Helper Methods

    private static CopingMechanism CreateCopingMechanism(
        string id = "test_coping",
        double? stressThreshold = null,
        double? moodThreshold = null,
        double? belongingThreshold = null,
        int minAge = 0,
        int maxAge = 100,
        bool isUnique = false)
    {
        return new CopingMechanism
        {
            Id = id,
            Name = $"Test Coping {id}",
            BaseProbability = 0.5,
            MinAge = minAge,
            MaxAge = maxAge,
            IsUnique = isUnique,
            Type = CopingType.Functional,
            Trigger = new CopingTrigger(stressThreshold, moodThreshold, belongingThreshold),
            ShortTermRelief = 0.5,
            LongTermImpactMultiplier = 1.0,
            IsHabitForming = false,
            InfluenceFactors = []
        };
    }

    private static SimulationState CreateSimulationState(
        double currentStress = 30,
        double currentMood = 20,
        double socialBelonging = 60,
        int currentAge = 25)
    {
        return new SimulationState
        {
            CurrentAge = currentAge,
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
            CurrentStress = currentStress,
            CurrentMood = currentMood,
            SocialBelonging = socialBelonging,
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
