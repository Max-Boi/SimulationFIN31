using System;

namespace SimulationFIN31.Services;

/// <summary>
///     1D Perlin noise generator for smooth random fluctuation.
///     Used to create realistic "good days/bad days" patterns in illness debuffs.
/// </summary>
public sealed class PerlinNoise
{
    private readonly int[] _permutation;

    /// <summary>
    ///     Creates a Perlin noise generator with the given seed.
    /// </summary>
    public PerlinNoise(int seed)
    {
        var random = new Random(seed);
        _permutation = new int[512];

        // Generate permutation table
        var p = new int[256];
        for (var i = 0; i < 256; i++) p[i] = i;

        // Fisher-Yates shuffle
        for (var i = 255; i > 0; i--)
        {
            var j = random.Next(i + 1);
            (p[i], p[j]) = (p[j], p[i]);
        }

        // Duplicate for wraparound
        for (var i = 0; i < 512; i++) _permutation[i] = p[i & 255];
    }

    /// <summary>
    ///     Gets a noise value at the given position.
    ///     Returns a value between 0.0 and 1.0.
    /// </summary>
    /// <param name="x">Position along the noise curve</param>
    /// <returns>Noise value normalized to [0, 1]</returns>
    public double Noise1D(double x)
    {
        // Find unit interval containing x
        var xi = (int)Math.Floor(x) & 255;
        var xf = x - Math.Floor(x);

        // Fade curve for smooth interpolation
        var u = Fade(xf);

        // Get gradient values at corners
        var a = _permutation[xi];
        var b = _permutation[xi + 1];

        var gradA = Gradient(a, xf);
        var gradB = Gradient(b, xf - 1);

        // Interpolate and normalize to [0, 1]
        var result = Lerp(gradA, gradB, u);
        return (result + 1.0) / 2.0; // Convert from [-1, 1] to [0, 1]
    }

    /// <summary>
    ///     Gets a noise value influenced by volatility.
    ///     Higher volatility = more frequent and extreme fluctuations.
    /// </summary>
    /// <param name="step">Current simulation step</param>
    /// <param name="volatility">Volatility factor (0.0 = stable, 1.0 = chaotic)</param>
    /// <returns>Noise value normalized to [0, 1]</returns>
    public double GetFluctuationValue(int step, double volatility)
    {
        // Scale the input based on volatility
        // Low volatility = slow, gentle waves. High volatility = rapid, jagged changes.
        var frequency = 0.05 + volatility * 0.3; // Range: 0.05 to 0.35
        var noiseValue = Noise1D(step * frequency);

        // Add octaves for more natural variation at high volatility
        if (volatility > 0.5)
        {
            var octave2 = Noise1D(step * frequency * 2.0) * 0.5;
            noiseValue = (noiseValue + octave2) / 1.5;
        }

        return noiseValue;
    }

    /// <summary>
    ///     Smoothstep fade function for continuous derivatives at integer boundaries.
    ///     f(t) = 6t^5 - 15t^4 + 10t^3
    /// </summary>
    private static double Fade(double t)
    {
        return t * t * t * (t * (t * 6 - 15) + 10);
    }

    /// <summary>
    ///     Linear interpolation between two values.
    /// </summary>
    private static double Lerp(double a, double b, double t)
    {
        return a + t * (b - a);
    }

    /// <summary>
    ///     Pseudo-random gradient based on hash value.
    /// </summary>
    private static double Gradient(int hash, double x)
    {
        // Use the lowest bit to determine direction
        return (hash & 1) == 0 ? x : -x;
    }
}
