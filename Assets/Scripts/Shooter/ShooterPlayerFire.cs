using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPlayerFire : MonoBehaviour
{
    Animator anim;

    public LayerMask obstacleLayers;
    public float reloadTime = 1;
    float lastFireTime = -1;

    [Header("Explosion")]
    public GameObject explosionEffectPrefab;
    public float explosionRadius = 0.3f;
    public LayerMask targetLayers;

    [Header("Weapon Walking Wave")]
    public float amplitude = -50;
    public float frequency;
    RectTransform weaponRectT;
    CharacterController controller;
    float weaponWaveTime;
    float lastCosSign;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        weaponRectT = anim.GetComponent<RectTransform>();
        controller = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShooterManager.Instance.gameIsPlaying)
            return;

        if (Input.GetButtonDown("Action"))
        {
            Fire();
        }

        AnimWeaponWalkingWave();
    }

    void AnimWeaponWalkingWave()
    {
        Vector2 pos = weaponRectT.anchoredPosition;
        weaponWaveTime += frequency * controller.velocity.magnitude;
        float cos = Mathf.Cos(weaponWaveTime);
        if (Mathf.Sign(cos - 0.5f) != lastCosSign)
        {
            AudioManager.PlaySound(SFX.ShooterWalkStep);
        }
        pos.y = cos * amplitude + amplitude;
        weaponRectT.anchoredPosition = pos;
        lastCosSign = Mathf.Sign(cos-0.5f);
    }

    void Fire()
    {
        if (Time.time < lastFireTime + reloadTime)
            return;

        anim.SetTrigger("Fire");
        lastFireTime = Time.time;

        AudioManager.PlaySound(SFX.ShooterFire);

        RaycastHit hit;
        if(Physics.Raycast(new Ray(transform.position, transform.forward),out hit, 100,obstacleLayers))
        {
            StartCoroutine(FireDelayed(hit.point));
        }
    }

    IEnumerator FireDelayed(Vector3 hitPoint)
    {
        yield return new WaitForSeconds(0.2f);

        Instantiate(explosionEffectPrefab, hitPoint, Quaternion.identity, null);
        Collider[] colliders = Physics.OverlapSphere(hitPoint, explosionRadius, targetLayers);
        foreach (Collider col in colliders)
        {
            col.attachedRigidbody?.SendMessage("GunDamage", SendMessageOptions.DontRequireReceiver);
        }
    }
}
