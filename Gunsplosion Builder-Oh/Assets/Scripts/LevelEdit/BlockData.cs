﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
[System.Serializable]
public class BlockData : ScriptableObject
{
    public GameObject prefab;
    public Tile tile;
    public enum BlockTypes { Tile, Object };
    public BlockTypes blockType;
    public GameObject gameObject;
}