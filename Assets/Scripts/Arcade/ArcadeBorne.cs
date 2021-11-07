using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeBorne : MonoBehaviour
{
    public Transform cameraFocusSpot = null;
    public MiniGameManager miniGameManager;

    // Start is called before the first frame update
    void Start()
    {
        miniGameManager.onMiniGameOver.AddListener(OnMiniGameOver);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayArcadeGame()
    {
        miniGameManager.StartMiniGame();
    }

    public void LeaveArcadeGame()
    {
        miniGameManager.MiniGameOver();
    }

    void OnMiniGameOver()
    {
        ArcadePlayer.Instance.LeaveFocus(this);
    }
}
