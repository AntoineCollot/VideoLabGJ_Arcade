using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeManager : MonoBehaviour
{
    public static ArcadeManager Instance;

    [Header("Game Progress")]
    public bool tntHasExploded = false;
    public bool wheelObtained = false;
    public bool barrelsExploded = false;
    public bool keyObtained = false;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

#if !UNITY_EDITOR
    tntHasExploded = false;
    wheelObtained = false;
    barrelsExploded = false;
    keyObtained = false;
#endif
    }
}
