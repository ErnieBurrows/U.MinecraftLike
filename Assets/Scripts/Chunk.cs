using System;
using UnityEngine;

public static class Chunk
{
    public static void LoopThroughBlocks(ChunkData chunkData, Action<int, int, int> action)
    {
        for (int i = 0; i < chunkData.blocks.Length; i++)
        {
            var position = GetPositionFromIndex(chunkData, i);
            action(position.x, position.y, position.z);
        }
    }

    /// <summary>
    /// Converts a world position to a local chunk position.
    /// </summary>
    public static Vector3Int GetBlockInChunkCoordinate(ChunkData chunkData, Vector3Int position)
    {
        return new Vector3Int
        {
            x = position.x - chunkData.worldPosition.x,
            y = position.y - chunkData.worldPosition.y,
            z = position.z - chunkData.worldPosition.z
        };
    }

    public static MeshData GetChunkMeshData(ChunkData chunkData)
    {
        MeshData meshData = new MeshData(true);

        LoopThroughBlocks(chunkData, (x, y, z) =>
        {
            meshData = BlockHelper.GetMeshData(chunkData, x, y, z, meshData, chunkData.blocks[GetIndexFromPosition(chunkData, x, y, z)]);
        });

        return meshData;
    }

    public static void SetBlock(ChunkData chunkData, Vector3Int localPosition, BlockType block)
    {
        if (InRange(chunkData, localPosition.x) && InRangeHeight(chunkData, localPosition.y) && InRange(chunkData, localPosition.z))
        {
            int index = GetIndexFromPosition(chunkData, localPosition.x, localPosition.y, localPosition.z);
            chunkData.blocks[index] = block;
        }
        else
        {
            throw new Exception("Need to ask world for appropriate chunk to set block");
        }
    }

    public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z)
    {
        if(InRange(chunkData, x) && InRangeHeight(chunkData, y) && InRange(chunkData, z))
        {
            int index = GetIndexFromPosition(chunkData, x, y, z);
            return chunkData.blocks[index];
        }

        return chunkData.worldReference.GetBlockFromChunkCoordinates(chunkData, chunkData.worldPosition.x + x, chunkData.worldPosition.y + y, chunkData.worldPosition.z + z);
    }

    public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, Vector3Int chunkCoordinates)
    {
        return GetBlockFromChunkCoordinates(chunkData, chunkCoordinates.x, chunkCoordinates.y, chunkCoordinates.z);
    }


    private static int GetIndexFromPosition(ChunkData chunkData, int x, int y, int z)
    {
        return x + chunkData.chunkSize * y + chunkData.chunkSize * chunkData.chunkHeight * z;
    }

    private static Vector3Int GetPositionFromIndex(ChunkData chunkData, int index)
    {
        int x = index % chunkData.chunkSize;
        int y = index / chunkData.chunkSize % chunkData.chunkHeight;
        int z = index / (chunkData.chunkSize * chunkData.chunkHeight);
        return new Vector3Int(x, y, z);
    }

    /// <summary>
    /// Checks if the x or z coordinate is in range of the chunk size within the local chunk coordinates.
    /// </summary>
    private static bool InRange(ChunkData chunkData, int axisCoordinate)
    {
        if(axisCoordinate < 0 || axisCoordinate >= chunkData.chunkSize)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if the y coordinate is in range of the chunk height within the local chunk coordinates.
    /// </summary>
    private static bool InRangeHeight(ChunkData chunkData, int yCoordinate)
    {
        if (yCoordinate < 0 || yCoordinate >= chunkData.chunkHeight)
        {
            return false;
        }

        return true;
    }

    public static Vector3Int ChunkPositionFromBlockCoords(World world, int x, int y, int z)
    {
        Vector3Int position = new Vector3Int
        {
            x = Mathf.FloorToInt(x / (float)world.chunkSize) * world.chunkSize,
            y = Mathf.FloorToInt(y / (float)world.chunkHeight) * world.chunkHeight,
            z = Mathf.FloorToInt(z / (float)world.chunkSize) * world.chunkSize
        };

        return position;
    }
}
