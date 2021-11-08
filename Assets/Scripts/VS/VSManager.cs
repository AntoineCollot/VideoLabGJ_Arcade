using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VSManager : MiniGameManager
{
    public static VSManager Instance;
    public bool gameWon = false;

    public UnityEvent onMiniGameWon = new UnityEvent();

    public GameObject playerWinText;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void MiniGameWin()
    {
        gameWon = true;
        onMiniGameWon.Invoke();
        ArcadeManager.Instance.wheelObtained = true;
        playerWinText.SetActive(true);

        AudioManager.PlaySound(SFX.VSPlayerWin);
    }

    public override void StartMiniGame()
    {
        base.StartMiniGame();

        gameWon = false;
        AudioManager.PlaySound(SFX.CountDownFight);
        playerWinText.SetActive(false);
    }

    public override void MiniGameOver()
    {
        base.MiniGameOver();

        gameWon = false;
    }
}
