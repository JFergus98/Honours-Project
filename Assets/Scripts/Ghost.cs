using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ghost
{
    [field: SerializeField]
    public List<GhostData> ghostData { get; private set; }

    public Ghost()
    {
        this.ghostData = new List<GhostData>();
    }
}
