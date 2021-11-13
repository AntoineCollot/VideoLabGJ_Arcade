using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBarrelAOEDamage : MonoBehaviour
{
    public float radius = 3;
    public LayerMask targetLayers;
    public GameObject explosionFX;

    public void GunDamage()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        Explosion();
        yield return new WaitForSeconds(0.3f);
        Explosion();
        Destroy(gameObject);
    }

    void Explosion()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, radius, targetLayers);
        foreach (Collider col in targets)
        {
            col.attachedRigidbody.SendMessage("GunDamage");
        }
        Instantiate(explosionFX, transform.position, Quaternion.identity, null);
        AudioManager.PlaySound(SFX.TNTExplosion);
    }
}
