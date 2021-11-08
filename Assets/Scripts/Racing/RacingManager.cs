using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingManager : MiniGameManager
{
    public static RacingManager Instance;

    public float raceTotalTime = 33;
    [HideInInspector] public float currentRaceTime;

    public bool gameWon = false;
    public GameObject textWin;

    public bool RaceIsOn
    {
        get
        {
            return currentRaceTime + 3 < raceTotalTime;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsPlaying)
        {
            currentRaceTime -= Time.deltaTime;
            if(currentRaceTime<0)
            {
                //MiniGameOver();
            }
        }
    }

    public override void StartMiniGame()
    {
        base.StartMiniGame();

        currentRaceTime = raceTotalTime;
        AudioManager.PlaySound(SFX.CountdownRace);
        textWin.SetActive(false);
    }

    public void MiniGameWin()
    {
        gameWon = true;
        ArcadeManager.Instance.keyObtained = true;
        textWin.SetActive(true);
    }
}
