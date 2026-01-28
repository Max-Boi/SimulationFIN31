using System;
using SimulationFIN31.Services;
using Xunit;

namespace SimulationFIN31.Tests.Services;

public class PerlinNoiseTests
{
    [Fact]
    public void Noise1D_ReturnsValuesInRange()
    {
        // Arrange
        var noise = new PerlinNoise(12345);
        
        // Act & Assert
        for (double x = 0; x < 100; x += 0.1)
        {
            var value = noise.Noise1D(x);
            Assert.InRange(value, 0.0, 1.0);
        }
    }

    [Fact]
    public void Noise1D_IsDeterministic()
    {
        // Arrange
        var seed = 12345;
        var noise1 = new PerlinNoise(seed);
        var noise2 = new PerlinNoise(seed);
        
        // Act
        var val1 = noise1.Noise1D(10.5);
        var val2 = noise2.Noise1D(10.5);
        
        // Assert
        Assert.Equal(val1, val2);
    }

    [Fact]
    public void Noise1D_DifferentSeedsProduceDifferentResults()
    {
        // Arrange
        var noise1 = new PerlinNoise(12345);
        var noise2 = new PerlinNoise(67890);
        
        // Act
        var val1 = noise1.Noise1D(123.456);
        var val2 = noise2.Noise1D(123.456);
        
        // Assert
        Assert.NotEqual(val1, val2);
    }

    [Fact]
    public void GetFluctuationValue_ReturnsNormalizedValue()
    {
        // Arrange
        var noise = new PerlinNoise(12345);
        
        // Act
        var value = noise.GetFluctuationValue(10, 0.5);
        
        // Assert
        Assert.InRange(value, 0.0, 1.0);
    }
}
