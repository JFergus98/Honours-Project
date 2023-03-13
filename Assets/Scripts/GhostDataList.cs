using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GhostDataList
{
    [field: SerializeField]
    public List<GhostData> ghostData { get; private set; }

    public GhostDataList()
    {
        this.ghostData = new List<GhostData>();
    }
}
