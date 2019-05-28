using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerialiseVector3
{
    public float x, y, z;

    public SerialiseVector3(float x_, float y_, float z_)
    {
        x = x_;
        y = y_;
        z = z_;
    }
}

[System.Serializable]
public class SerialiseVector3Int
{
    public int x, y, z;

    public SerialiseVector3Int(int x_, int y_, int z_)
    {
        x = x_;
        y = y_;
        z = z_;
    }

    public Vector3 ToVector3() {
        Vector3 result = new Vector3(x, y, z);
        return result;
    }

    public static SerialiseVector3Int FromVector3(Vector3 vector) {
        SerialiseVector3Int result = new SerialiseVector3Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y), Mathf.FloorToInt(vector.z));
        return result;
    }
}