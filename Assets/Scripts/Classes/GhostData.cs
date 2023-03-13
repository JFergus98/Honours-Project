using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GhostData
{
    // Declare variables
    [field: SerializeField]
    public float timeStamp { get; private set; }
    [field: SerializeField]
    public Vector3 position { get; private set; }
    [field: SerializeField]
    public Quaternion rotation { get; private set; }

    // Constructor for objects of class Ghostdata
    public GhostData(float timeStamp, Vector3 position, Quaternion rotation)
    {
        this.timeStamp = timeStamp;
        this.position = position;
        this.rotation = rotation;
    }
}
