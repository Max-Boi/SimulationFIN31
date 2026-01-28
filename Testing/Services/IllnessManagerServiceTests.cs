using System.Collections.Generic;
using SimulationFIN31.Models;
using SimulationFIN31.Models.Enums;
using SimulationFIN31.Models.EventTypes;
using SimulationFIN31.Models.MentalDiseases;
using SimulationFIN31.Services;
using Xunit;

namespace SimulationFIN31.Tests.Services;

public class IllnessManagerServiceTests
{
    private readonly IllnessManagerService _sut;

    public IllnessManagerServiceTests()
    {
        _sut = new IllnessManagerService();
    }

    [Fact]
    public void ProcessIllnessStep_IncrementsActiveIllnessSteps()
    {
        // Arrange
        var state = new SimulationState();
        var illnessKey = "MildDepression";
        state.CurrentIllnesses[illnessKey] = new DiseaseConfig { Name = "Depressions", HealingTime = 10 };
        state.IllnessProgressionStates[illnessKey] = new IllnessProgressionState { StepsActive = 0, IllnessKey = illnessKey };

        // Act
        _sut.ProcessIllnessStep(state);

        // Assert
        Assert.Equal(1, state.IllnessProgressionStates[illnessKey].StepsActive);
    }

    [Fact]
    public void ProcessIllnessStep_HealsIllness_WhenTimeReached()
    {
        // Arrange
        var state = new SimulationState();
        var illnessKey = "MildDepression";
        state.CurrentIllnesses[illnessKey] = new DiseaseConfig { Name = "Depressions", HealingTime = 10 };
        state.IllnessProgressionStates[illnessKey] = new IllnessProgressionState { StepsActive = 10, IllnessKey = illnessKey };

        // Act
        _sut.ProcessIllnessStep(state);

        // Assert
        Assert.DoesNotContain(illnessKey, state.CurrentIllnesses.Keys);
        Assert.DoesNotContain(illnessKey, state.IllnessProgressionStates.Keys);
    }

    [Fact]
    public void ApplyDebuffs_WithNoIllness_ReturnsOriginalImpacts()
    {
        // Arrange
        var state = new SimulationState();
        var originalEvent = new GenericEvent
        {
            StressImpact = 10,
            MoodImpact = 10,
            SocialBelongingImpact = 10,
            ResilienceImpact = 5,
            HealthImpact = 5
        };

        // Act
        var result = _sut.ApplyDebuffs(state, originalEvent);

        // Assert
        Assert.Equal(originalEvent.StressImpact, result.StressImpact);
        Assert.Equal(originalEvent.MoodImpact, result.MoodImpact);
        Assert.Equal(originalEvent.SocialBelongingImpact, result.SocialBelongingImpact);
    }

    [Fact]
    public void ApplyDebuffs_WithIllness_ModifiesImpacts()
    {
        // Arrange
        var state = new SimulationState();
        var illnessKey = "MildDepression";
        
        // Setup a depression state which should generally increase stress impact and reduce mood gains
        state.CurrentIllnesses[illnessKey] = new DiseaseConfig 
        { 
            Name = "Deep Sadness",
            StressDebuffMin = 1.5,
            StressDebuffMax = 1.5,
            MoodDebuffMin = 0.5,
            MoodDebuffMax = 0.5
        };
        state.IllnessProgressionStates[illnessKey] = new IllnessProgressionState { IllnessKey = illnessKey };

        var originalEvent = new GenericEvent
        {
            StressImpact = 10,
            MoodImpact = 10
        };

        // Act
        var result = _sut.ApplyDebuffs(state, originalEvent);

        // Assert
        // We expect stress impact to change (increase) and mood impact to change (decrease)
        // Since we force fixed min/max, it should be predictable roughly.
        Assert.NotEqual(originalEvent.StressImpact, result.StressImpact);
        Assert.NotEqual(originalEvent.MoodImpact, result.MoodImpact);
    }
}
