using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerManager : MiniGameManager
{
    public static PlatformerManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
