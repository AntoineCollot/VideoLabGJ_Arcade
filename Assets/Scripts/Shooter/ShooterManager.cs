using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterManager : MiniGameManager
{
    public static ShooterManager Instance;

    float lastDamageTime;
    public int playerHP = 3;

    private void Awake()
    {
        Instance = this;
    }

    public override void StartMiniGame()
    {
        base.StartMiniGame();

        playerHP = 3;
    }

    public void PlayerDamaged()
    {
        if (Time.time < lastDamageTime + 1)
            return;

        AudioManager.PlaySound(SFX.Hurt);

        lastDamageTime = Time.time;
        Debug.Log("Player Damaged");
        playerHP--;

        if(playerHP<=0)
        {
            MiniGameOver();
        }
    }
}
