using System;
using SimulationFIN31.Services.Interfaces;

namespace SimulationFIN31.Services;

/// <summary>
/// Calculates the influence of a factor on event probability using exponential weighting.
///
/// The influence formula applies the exponent to the normalized value:
/// - Exponent > 1: Amplifies the influence (high values have disproportionately higher impact)
/// - Exponent = 1: Linear influence (no amplification)
/// - Exponent between 0 and 1: Dampens the influence (compresses differences)
/// - Negative exponents: Inverse relationship (high values reduce probability)
///
/// Mathematical basis: This approach is grounded in the stress-diathesis model,
/// where vulnerability factors can have non-linear effects on outcomes.
/// </summary>
public sealed class InfluenceCalculator : IInfluenceCalculator
{
    /// <summary>
    /// Minimum value to prevent division by zero or extreme amplification.
    /// </summary>
    private const double MINIMUM_NORMALIZED_VALUE = 0.01;

    /// <summary>
    /// Maximum multiplier to allow significant probability amplification
    /// for events strongly correlated with adverse conditions.
    /// Increased from 10.0 to create more realistic outcome differentiation.
    /// </summary>
    private const double MAX_MULTIPLIER = 50.0;

    /// <summary>
    /// Minimum multiplier to allow significant probability reduction
    /// for events that become unlikely under adverse conditions.
    /// Reduced from 0.1 to create more realistic outcome differentiation.
    /// </summary>
    private const double MIN_MULTIPLIER = 0.02;

    /// <summary>
    /// Calculates the influence multiplier for an event based on a normalized state value and exponent.
    ///
    /// The calculation handles three cases:
    /// 1. Positive exponent: normalizedValue^exponent (high values amplified)
    /// 2. Negative exponent: (1 - normalizedValue)^|exponent| (inverse relationship)
    /// 3. Zero exponent: Returns 1.0 (no influence)
    /// </summary>
    /// <param name="normalizedValue">State value normalized to [0.01, 0.99] range.</param>
    /// <param name="exponent">
    /// The influence exponent from InfluenceFactor.
    /// Positive values create direct relationship, negative values create inverse relationship.
    /// </param>
    /// <returns>
    /// A multiplier clamped to [0.1, 10.0] to apply to base probability.
    /// </returns>
    public double CalculateInfluence(double normalizedValue, double exponent)
    {
        if (Math.Abs(exponent) < 0.001)
        {
            return 1.0;
        }

        var clampedValue = Math.Max(normalizedValue, MINIMUM_NORMALIZED_VALUE);

        double influence;

        if (exponent > 0)
        {
            influence = Math.Pow(clampedValue, exponent);
        }
        else
        {
            var invertedValue = 1.0 - clampedValue;
            invertedValue = Math.Max(invertedValue, MINIMUM_NORMALIZED_VALUE);
            influence = Math.Pow(invertedValue, Math.Abs(exponent));
        }

        return Math.Clamp(influence, MIN_MULTIPLIER, MAX_MULTIPLIER);
    }
}