using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class LevelData : MonoBehaviour
{
    public Dictionary<SerialiseVector3, BlockData> blockData = new Dictionary<SerialiseVector3, BlockData>();
    public Tilemap tilemap;
}
