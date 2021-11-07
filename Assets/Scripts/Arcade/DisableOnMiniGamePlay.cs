using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnMiniGamePlay : MonoBehaviour
{
    public MiniGameManager miniGameManager;

    // Start is called before the first frame update
    void Start()
    {
        miniGameManager.onMiniGameStart.AddListener(OnMiniGameStart);
        miniGameManager.onMiniGameOver.AddListener(OnMiniGameOver);
    }

    private void OnMiniGameStart()
    {
        gameObject.SetActive(false);
    }

    private void OnMiniGameOver()
    {
        gameObject.SetActive(true);
    }
}
