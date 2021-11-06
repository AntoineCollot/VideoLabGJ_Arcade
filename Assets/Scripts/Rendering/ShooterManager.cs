using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterManager : MiniGameManager
{
    public static ShooterManager Instance;

    public int playerHP = 3;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
#if UNITY_EDITOR
        Invoke("DebugStartGame", 0.1f);
#endif
    }

    void DebugStartGame()
    {
        onGameStart.Invoke();
        gameIsPlaying = true;
    }

    public void PlayerDamaged()
    {
        playerHP--;

        if(playerHP<=0)
        {
            Debug.Log("Game Over Shooter");
        }
    }
}
