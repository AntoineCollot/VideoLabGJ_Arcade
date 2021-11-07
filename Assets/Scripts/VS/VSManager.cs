using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VSManager : MiniGameManager
{
    public static VSManager Instance;
    public bool gameWon = false;

    public UnityEvent onMiniGameWon = new UnityEvent();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MiniGameWin()
    {
        gameWon = true;
        onMiniGameWon.Invoke();
        ArcadeManager.Instance.wheelObtained = true;
    }

    public override void StartMiniGame()
    {
        base.StartMiniGame();

        gameWon = false;
    }

    public override void MiniGameOver()
    {
        base.MiniGameOver();

        gameWon = false;
    }
}
