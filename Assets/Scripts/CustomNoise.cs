using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public static class CustomNoise 
{
    public static float RemapValue(float value, float initialMin, float initialMax, float targetMin, float targetMax)
    {
        return targetMin + (value - initialMin) * (targetMax - targetMin) / (initialMax - initialMin);
    }

    public static float RemapValue(float value, float targetMin, float targetMax)
    {
        return targetMin + (value - 0) * (targetMax - targetMin) / (1 - 0);
    }

    public static int RemapValueToInt(float value, float targetMin, float targetMax)
    {
        return (int)RemapValue(value, targetMin, targetMax);
    }

    public static float Redistribution(float noise, NoiseData settings)
    {
        return Mathf.Pow(noise * settings.redistributionModifier, settings.exponent);
    }
    public static float OctavePerlin(float x, float z, NoiseData settings)
    {
        x *= settings.noiseScale;
        z *= settings.noiseScale;
        x += settings.noiseScale;
        z += settings.noiseScale;

        float total = 0;
        float frequency = 1;
        float amplitude = 1;
        float amplitudeSum = 0;

        for(int i = 0; i < settings.octaves; i++)
        {
            total += Mathf.PerlinNoise((settings.offset.x + settings.worldOffset.x + x) * frequency, 
                    (settings.offset.y + settings.worldOffset.y + z) * frequency) * amplitude;

            amplitudeSum += amplitude;
            amplitude *= settings.persistence;
            frequency *= 2;
        }

        return total / amplitudeSum;
    }
}


