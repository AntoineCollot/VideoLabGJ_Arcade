using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3;

    // Start is called before the first frame update
    void Start()
    {
        PlatformerManager.Instance.onMiniGameStart.AddListener(OnMiniGameStart);
        if (PlatformerManager.Instance.gameIsPlaying)
            OnMiniGameStart();
    }

    void OnMiniGameStart()
    {
        StartCoroutine(SpawnEnemyLoop());
    }

    IEnumerator SpawnEnemyLoop()
    {
        while(PlatformerManager.Instance.gameIsPlaying)
        {
            yield return new WaitForSeconds(spawnInterval);

            Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform.parent);
        }
    }
}
