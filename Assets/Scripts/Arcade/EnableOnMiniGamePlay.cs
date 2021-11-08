using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnMiniGamePlay : MonoBehaviour
{
    public MiniGameManager miniGameManager;

    // Start is called before the first frame update
    void Start()
    {
        miniGameManager.onMiniGameStart.AddListener(OnMiniGameStart);
        miniGameManager.onMiniGameOver.AddListener(OnMiniGameOver);

        if (!miniGameManager.gameIsPlaying)
            gameObject.SetActive(false);
    }

    private void OnMiniGameStart()
    {
        gameObject.SetActive(true);
    }

    private void OnMiniGameOver()
    {
        gameObject.SetActive(false);
    }
}
