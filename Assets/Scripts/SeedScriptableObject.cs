//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeedScriptableObject", menuName = "ScriptableObjects/Seed")]
public class SeedScriptableObject : ScriptableObject
{
    [field: SerializeField]
    public int seed { get; private set; }
    
    public void GenerateSeed()
    {
        seed = Random.Range(int.MinValue, int.MaxValue);
        Debug.Log("Seed = " + seed);
    }
}
