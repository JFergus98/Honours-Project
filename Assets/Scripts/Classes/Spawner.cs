using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]public class Spawner
{
    // Declare variables
    [SerializeField]private GameObject obstaclePrefab;
    [SerializeField]private Transform transform;

    // Gets obstaclePrefab
    public GameObject getObstaclePrefab()
    {
        return obstaclePrefab;
    }

    // Gets transform
    public Transform getTransform()
    {
        return transform;
    }
}
