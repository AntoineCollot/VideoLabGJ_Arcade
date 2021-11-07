using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeManager : MonoBehaviour
{
    public static ArcadeManager Instance;

    [Header("Game Progress")]
    [HideInInspector] public bool tntHasExploded = false;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
}
