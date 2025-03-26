using UnityEngine;

[CreateAssetMenu(fileName = "NoiseSettings", menuName = "Data/NoiseSettings", order = 1)]
public class NoiseData : ScriptableObject
{
    public float noiseScale;
    public int octaves;
    public Vector2Int offset;
    public Vector2Int worldOffset;
    public float persistence;
    public float redistributionModifier;
    public float exponent;
}
