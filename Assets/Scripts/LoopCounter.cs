using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class LoopCounter : MonoBehaviour
{
    private static LoopCounter _instance;

    public static LoopCounter Instance {
        get {
            return _instance;
        }
    }
    [SerializeField] public int loopCount { get; private set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // If there is already an instance, then delete self;
        if (_instance != null && _instance != this) {
            Debug.Log("An instance of LoopCounter already exists in the scene.");
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        loopCount = 0;
    }

    public void incrementLoopCount()
    {
        loopCount++;
    }
}