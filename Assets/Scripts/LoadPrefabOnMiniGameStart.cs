using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPrefabOnMiniGameStart : MonoBehaviour
{
    public MiniGameManager miniGameManager;

    public GameObject prefab;
    GameObject instance;

    // Start is called before the first frame update
    void Awake()
    {
        miniGameManager.onMiniGameStart.AddListener(OnMiniGameStart);
        if (miniGameManager.gameIsPlaying)
            OnMiniGameStart();
        miniGameManager.onMiniGameOver.AddListener(OnMiniGameOver);
    }

    void OnMiniGameStart()
    {
        instance = Instantiate(prefab, transform);
    }

    void OnMiniGameOver()
    {
        Destroy(instance);
    }
}
