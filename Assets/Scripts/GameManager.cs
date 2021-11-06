using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameManager : MonoBehaviour
{
    [HideInInspector] public bool gameIsPlaying = false;
    public UnityEvent onGameStart = new UnityEvent();
}
