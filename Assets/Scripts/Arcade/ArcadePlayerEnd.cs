using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadePlayerEnd : MonoBehaviour
{
    public GameObject endCanvas;

    public void GunDamage()
    {
        if(!ArcadeManager.Instance.gameEnded)
            StartCoroutine(End());
    }

    IEnumerator End()
    {
        AudioManager.PlaySound(SFX.ArcadePlayerDeath);
        ArcadeManager.Instance.gameEnded = true;

        endCanvas.SetActive(true);

        yield return null;
    }
}
