using UnityEngine;
using UnityEngine.Experimental.AI;

public class ChunkData 
{
    public BlockType[] blocks;
    public int chunkSize;
    public int chunkHeight;
    public World worldReference;
    public Vector3Int worldPosition;

    public bool modifiedByThePlayer = false;

    public ChunkData(int chunkSize, int chunkHeight, World world, Vector3Int worldPosition)
    {
        this.chunkHeight = chunkHeight;
        this.chunkSize = chunkSize;
        this.worldReference = world;
        this.worldPosition = worldPosition;
        blocks = new BlockType[chunkSize * chunkSize * chunkHeight];
    }
}
