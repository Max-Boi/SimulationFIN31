using System;
using SimulationFIN31.Services;
using Xunit;

namespace SimulationFIN31.Tests.Services;

/// <summary>
/// Unit tests for the InfluenceCalculator service.
/// Tests verify the exponential weighting formula used to modify event probabilities
/// based on normalized state values.
/// </summary>
public sealed class InfluenceCalculatorTests
{
    private readonly InfluenceCalculator _sut;

    public InfluenceCalculatorTests()
    {
        _sut = new InfluenceCalculator();
    }

    #region Positive Exponent Tests

    [Fact]
    public void CalculateInfluence_WithPositiveExponentAndHighValue_ReturnsAmplifiedInfluence()
    {
        // Arrange
        const double normalizedValue = 0.9;
        const double exponent = 1.5;

        // Act
        var result = _sut.CalculateInfluence(normalizedValue, exponent);

        // Assert
        // New Logic: 0.9 * 2 = 1.8. 1.8^1.5 approx 2.41
        Assert.True(result > 1.0, "High normalized value with positive exponent should yield boost (> 1.0)");
    }

    [Fact]
    public void CalculateInfluence_WithPositiveExponentAndLowValue_ReturnsReducedInfluence()
    {
        // Arrange
        const double normalizedValue = 0.2;
        const double exponent = 1.5;

        // Act
        var result = _sut.CalculateInfluence(normalizedValue, exponent);

        // Assert
        // New Logic: 0.2 * 2 = 0.4. 0.4^1.5 approx 0.25
        Assert.True(result < 1.0, "Low value with exponent > 0 should assume penalty (< 1.0)");
        Assert.True(result >= 0.02, "Result should not go below minimum multiplier");
    }

    [Fact]
    public void CalculateInfluence_WithExponentOfOne_ReturnsOriginalValue()
    {
        // Arrange
        const double normalizedValue = 0.5;
        const double exponent = 1.0;

        // Act
        var result = _sut.CalculateInfluence(normalizedValue, exponent);

        // Assert
        // New Logic: 0.5 * 2 = 1.0. 1.0^1.0 = 1.0
        Assert.Equal(1.0, result, precision: 5);
    }

    #endregion

    #region Negative Exponent Tests

    [Fact]
    public void CalculateInfluence_WithNegativeExponentAndHighValue_ReturnsLowInfluence()
    {
        // Arrange
        const double normalizedValue = 0.9;
        const double exponent = -1.5;

        // Act
        var result = _sut.CalculateInfluence(normalizedValue, exponent);

        // Assert
        // New Logic: 0.9 -> inv 0.1 -> 0.2. 0.2^1.5 approx 0.089
        Assert.True(result < 1.0, "High value with negative exponent should yield penalty (< 1.0)");
    }

    [Fact]
    public void CalculateInfluence_WithNegativeExponentAndLowValue_ReturnsHighInfluence()
    {
        // Arrange
        const double normalizedValue = 0.1;
        const double exponent = -1.5;

        // Act
        var result = _sut.CalculateInfluence(normalizedValue, exponent);

        // Assert
        // New Logic: 0.1 -> inv 0.9 -> 1.8. 1.8^1.5 approx 2.41
        Assert.True(result > 1.0, "Low value with negative exponent should yield boost (> 1.0)");
    }

    #endregion

    #region Zero Exponent Tests

    [Fact]
    public void CalculateInfluence_WithZeroExponent_ReturnsOne()
    {
        // Arrange
        const double normalizedValue = 0.7;
        const double exponent = 0.0;

        // Act
        var result = _sut.CalculateInfluence(normalizedValue, exponent);

        // Assert
        Assert.Equal(1.0, result, precision: 5);
    }

    [Fact]
    public void CalculateInfluence_WithNearZeroExponent_ReturnsOne()
    {
        // Arrange
        const double normalizedValue = 0.5;
        const double exponent = 0.0005;

        // Act
        var result = _sut.CalculateInfluence(normalizedValue, exponent);

        // Assert
        Assert.Equal(1.0, result, precision: 5);
    }

    #endregion

    #region Boundary Tests

    [Fact]
    public void CalculateInfluence_WithZeroNormalizedValue_ReturnsMinimumMultiplier()
    {
        // Arrange
        const double normalizedValue = 0.0;
        const double exponent = 2.0;

        // Act
        var result = _sut.CalculateInfluence(normalizedValue, exponent);

        // Assert
        Assert.Equal(0.02, result, precision: 5);
    }

    [Fact]
    public void CalculateInfluence_WithExtremePositiveExponent_IsClamped()
    {
        // Arrange
        const double normalizedValue = 0.99;
        const double exponent = 0.01;

        // Act
        var result = _sut.CalculateInfluence(normalizedValue, exponent);

        // Assert
        Assert.True(result <= 50.0, "Result should be clamped to maximum multiplier");
        Assert.True(result >= 0.02, "Result should be clamped to minimum multiplier");
    }

    [Theory]
    [InlineData(0.01, 1.0)]
    [InlineData(0.5, 1.0)]
    [InlineData(0.99, 1.0)]
    [InlineData(0.3, 2.0)]
    [InlineData(0.7, 0.5)]
    public void CalculateInfluence_WithVariousInputs_ReturnsValueInValidRange(
        double normalizedValue,
        double exponent)
    {
        // Act
        var result = _sut.CalculateInfluence(normalizedValue, exponent);

        // Assert
        Assert.InRange(result, 0.02, 50.0);
    }

    #endregion

    #region Mathematical Correctness Tests

    [Fact]
    public void CalculateInfluence_VerifyFormula_PositiveExponent()
    {
        // Arrange
        const double normalizedValue = 0.8;
        const double exponent = 2.0;
        var expected = Math.Pow(normalizedValue, exponent);

        // Act
        var result = _sut.CalculateInfluence(normalizedValue, exponent);

        // Assert
        Assert.Equal(expected, result, precision: 5);
    }

    [Fact]
    public void CalculateInfluence_VerifyFormula_NegativeExponent()
    {
        // Arrange
        const double normalizedValue = 0.3;
        const double exponent = -2.0;
        var invertedValue = 1.0 - normalizedValue;
        var expected = Math.Pow(invertedValue, Math.Abs(exponent));

        // Act
        var result = _sut.CalculateInfluence(normalizedValue, exponent);

        // Assert
        Assert.Equal(expected, result, precision: 5);
    }

    #endregion
}
