using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class LevelData : MonoBehaviour
{
    public Dictionary<Vector3, BlockData> blockData = new Dictionary<Vector3, BlockData>();
    public Dictionary<SerialiseVector3Int, int> tileData = new Dictionary<SerialiseVector3Int, int>();
    public Tilemap tilemap;
    public List<BlockData> blocks;
    public RuleTile emptyTile;
}
