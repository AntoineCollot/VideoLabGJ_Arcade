using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : MonoBehaviour
{
    public GameObject[] barrelObjects;
    public GameObject FX;
    public GameObject SmokeFX;
    public float animLength = 1.5f;

    //Triggered by player's gun send message
    public void GunDamage()
    {
        ArcadeManager.Instance.barrelsExploded = true;
        StartCoroutine(ExplosionAnim());
    }

    IEnumerator ExplosionAnim()
    {
        FX.SetActive(true);
        SmokeFX.SetActive(true);
        AudioManager.PlaySound(SFX.BarrelExplosion);

        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < barrelObjects.Length; i++)
        {
            barrelObjects[i].SetActive(false);
            yield return new WaitForSeconds(animLength/ barrelObjects.Length);
        }
    }
}
