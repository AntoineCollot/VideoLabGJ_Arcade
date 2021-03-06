using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameManager : MonoBehaviour
{
    [HideInInspector] public bool gameIsPlaying = false;
    public UnityEvent onMiniGameStart = new UnityEvent();
    public UnityEvent onMiniGameOver = new UnityEvent();

    [Header("Boot")]
    public GameObject[] bootObjects;
    public GameObject[] inGameObjects;

    [Header("Debug")]
    public bool godMode = false;

    new protected AudioSource audio;

    private void Start()
    {
        UpdateObjectsActivation();
        audio = GetComponent<AudioSource>();
    }

    [ContextMenu("StartMiniGame")]
    public virtual void StartMiniGame()
    {
        gameIsPlaying = true;
        onMiniGameStart.Invoke();

        UpdateObjectsActivation();

        if (audio != null)
            audio.Play();
    }

    public virtual void MiniGameOver()
    {
        if (!gameIsPlaying)
            return;

#if UNITY_EDITOR
        if (godMode)
            return;
#endif

        Debug.Log("Mini Game Over");
        gameIsPlaying = false;
        onMiniGameOver.Invoke();

        UpdateObjectsActivation();

        if (audio != null)
            audio.Stop();
    }

    void UpdateObjectsActivation()
    {
        foreach (GameObject go in bootObjects)
            go.SetActive(!gameIsPlaying);
        foreach (GameObject go in inGameObjects)
            go.SetActive(gameIsPlaying);
    }
}
