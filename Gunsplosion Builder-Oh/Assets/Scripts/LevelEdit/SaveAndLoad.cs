using System;
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
    public class SaveData : ScriptableObject
    {
        public Dictionary<SerialiseVector3, BlockData> blockData = new Dictionary<SerialiseVector3, BlockData>();
        public Tilemap tilemap;
    }

    public string fileName;
    public Tilemap tilemap;
    public LevelData levelData;

    public static SaveAndLoad instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    public class Serializer
    {
        public static BinaryFormatter bf = new BinaryFormatter();

        public static String SerializeObject(object obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            bf.Serialize(memoryStream, obj);

            bf.Binder = new VersionDeserializationBinder();

            return System.Convert.ToBase64String(memoryStream.ToArray());
        }

        public static object DeserializeObject(string obj)
        {
            MemoryStream memoryStream = new MemoryStream(System.Convert.FromBase64String(obj));

            bf.Binder = new VersionDeserializationBinder();

            return bf.Deserialize(memoryStream);
        }
    }

    public sealed class VersionDeserializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
        {
                Type typeToDeserialize = null;

                assemblyName = Assembly.GetExecutingAssembly().FullName;

                //The following line of code returns the type.
                typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));

                return typeToDeserialize;
            }

            return null;
        }
    }

    //public void SaveLevel(LevelData data, string fileName)
    //{
    //    SaveData saveData = new SaveData();
    //    foreach(var blockData in data.blockData)
    //    {
    //        saveData.blockData[blockData.Key] = blockData.Value;
    //    }
    //    //saveData.tilemap = data.tilemap;

    //    BinaryFormatter bf = new BinaryFormatter();
    //    Directory.CreateDirectory(Application.persistentDataPath + "/levels");
    //    FileStream file = File.Create(Application.persistentDataPath + "/levels/" + fileName + ".dat");
    //    bf.Serialize(file, saveData);
    //    file.Position = 0;
    //    file.Close();
    //}

    public void SaveLevel(LevelData data, string fileName)
    {
        print(Serializer.SerializeObject(data));
    }

    //public void LoadLevel(string fileName)
    //{
    //    LevelData data = new LevelData();
    //    if (File.Exists(Application.persistentDataPath + "/levels/" + fileName + ".dat"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open(Application.persistentDataPath + "/levels/" + fileName + ".dat", FileMode.Open);
    //        data = (LevelData)bf.Deserialize(file);
    //        file.Position = 0;
    //        file.Close();

    //        //tilemap = data.tilemap;

    //        levelData.blockData = data.blockData;

    //        foreach (BlockData block in data.blockData.Values)
    //        {
    //            //Vector3 newPos = new Vector3(block.serialiseTransform.pos_x, block.serialiseTransform.pos_y, block.serialiseTransform.pos_z);
    //            //Quaternion newRot = new Quaternion(block.serialiseTransform.rot_x, block.serialiseTransform.rot_y, block.serialiseTransform.rot_z, 0);
    //            //Instantiate(block.prefab, newPos, newRot);
    //        }
    //    }
    //}

    public void LoadLevel(string fileName)
    {
        string loadJson = PlayerPrefs.GetString(fileName);
        print(loadJson);
        SaveData saveFile = SaveData.CreateInstance<SaveData>();
        JsonUtility.FromJsonOverwrite(loadJson, saveFile);
        print("fuck");
        levelData.tilemap = saveFile.tilemap;
        levelData.blockData = saveFile.blockData;
        print("fuck");
        foreach (BlockData block in levelData.blockData.Values)
        {
            Vector3 spawnPos = new Vector3(block.blockInfo.position.x, block.blockInfo.position.y, block.blockInfo.position.z);
            Instantiate(block.prefab, spawnPos, Quaternion.Euler(block.blockInfo.rotation.x, block.blockInfo.rotation.y, block.blockInfo.rotation.z));
            print("shit");
        }
        print("fuck");
    }

    private void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            LoadLevel(fileName);
        }
    }
}
