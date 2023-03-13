using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlatformRow
{
    // Declare variables
    [SerializeField]
    private List<GameObject> platforms;
    
    // Get the list of platforms
    public List<GameObject> getPlatforms()
    {
        return platforms;
    }

    // Gets the number of elements in platforms
    public int count()
    {
        return platforms.Count;
    }

    // Gets the element at the specified index
    public GameObject getElementAt(int index)
    {
        return platforms[index];
    }
}