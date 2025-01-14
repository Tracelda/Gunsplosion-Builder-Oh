﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SaveAndLoad : MonoBehaviour
{
    [System.Serializable]
    public class SaveData
    {
        public Dictionary<SerialiseVector3Int, BlockInfo> blockData = new Dictionary<SerialiseVector3Int, BlockInfo>();
        public Dictionary<SerialiseVector3Int, int> tileData = new Dictionary<SerialiseVector3Int, int>();
        //public Tilemap tilemap;
    }

    public string fileName;
    public string levelName;
    public LevelData levelData;

    public const string blockPrefabAddress = "Blocks/Prefab/";
    public const string blockDataAddress = "Blocks/Data/";

    public static SaveAndLoad instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    public void SaveLevel(LevelData data, string fileName)
    {
        SaveData saveData = new SaveData();
        foreach(var blockData in data.blockData)
        {
            saveData.blockData[SerialiseVector3Int.FromVector3(blockData.Key)] = blockData.Value.blockInfo;
        }
        saveData.tileData = data.tileData;

        BinaryFormatter bf = new BinaryFormatter();
        Directory.CreateDirectory(Application.persistentDataPath + "/levels");
        FileStream file = File.Create(Application.persistentDataPath + "/levels/" + fileName + ".dat");
        bf.Serialize(file, saveData);
        file.Position = 0;
        file.Close();
    }

    public void LoadLevel(string fileName)
    {
        levelName = fileName;
        SaveData data = new SaveData();
        if (File.Exists(Application.persistentDataPath + "/levels/" + fileName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/levels/" + fileName + ".dat", FileMode.Open);
            data = (SaveData)bf.Deserialize(file);
            file.Position = 0;
            file.Close();

            levelData.blockData.Clear();

            foreach (BlockInfo block in data.blockData.Values)
            {
                BlockData blockData = Instantiate(Resources.Load(blockDataAddress + block.objectname) as BlockData);

                if (blockData.blockType == BlockData.BlockTypes.Object) {
                    Vector3 newPos = new Vector3(block.position.x, block.position.y, block.position.z);
                    Quaternion newRot = Quaternion.Euler(0, 0, block.rotation.z);
                    GameObject newObject = Instantiate(Resources.Load(blockPrefabAddress + block.objectname) as GameObject, newPos, newRot);
                    blockData.gameObject = newObject;
                    blockData.blockInfo = block;
                }

                if (levelData.blockData.ContainsKey(block.position.ToVector3()))
                    levelData.blockData.Remove(block.position.ToVector3());
                levelData.blockData.Add(block.position.ToVector3(), blockData);
            }

            foreach(var tile in data.tileData)
            {
                Vector3Int position = new Vector3Int((int)tile.Key.x, (int)tile.Key.y, (int)tile.Key.z);
                RuleTile newTile;
                if (tile.Value >= 0) {
                    newTile = levelData.blocks[tile.Value].tile;
                    levelData.tileData.Add(SerialiseVector3Int.FromVector3(position), tile.Value);
                }
                else if (tile.Value == -1){
                    newTile = levelData.emptyTile;
                    levelData.tileData.Add(SerialiseVector3Int.FromVector3(position), -1);
                }
                else {
                    newTile = null;
                    levelData.tileData.Add(SerialiseVector3Int.FromVector3(position), -2);
                }
                levelData.tilemap.SetTile(position, newTile);
            }

        }
    }
}
