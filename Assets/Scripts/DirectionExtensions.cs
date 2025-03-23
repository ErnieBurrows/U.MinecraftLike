using System.Numerics;
using UnityEngine;

public static class DirectionExtensions
{
    public static Vector3Int GetVector(this Direction direction)
    {
        return direction switch
        {
            Direction.forward => Vector3Int.forward,
            Direction.backward => Vector3Int.back,
            Direction.left =>  Vector3Int.left,
            Direction.right => Vector3Int.right,
            Direction.up => Vector3Int.up,
            Direction.down => Vector3Int.down,
            _ => throw new System.Exception("Invalid input direction")
        };
    }
}
