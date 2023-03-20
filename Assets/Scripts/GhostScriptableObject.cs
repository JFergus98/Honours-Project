using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GhostScriptableObject", menuName = "ScriptableObjects/Ghost")]
public class GhostScriptableObject : ScriptableObject
{
    [field: SerializeField]
    public float recordFrequency { get; private set; }

    [field: SerializeField]
    public List<Ghost> ghosts { get; private set; }
}
