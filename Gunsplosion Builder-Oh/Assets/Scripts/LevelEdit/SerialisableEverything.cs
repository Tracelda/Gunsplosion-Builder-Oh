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