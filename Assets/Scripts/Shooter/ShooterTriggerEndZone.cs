using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTriggerEndZone : MonoBehaviour
{
    public AudioSource shooterMusic;
    public LoadPrefabOnMiniGameStart enemyLoader;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("ShooterPlayer"))
            return;
        shooterMusic.Stop();
        if(enemyLoader != null)
            Destroy(enemyLoader.instance);
    }
}
