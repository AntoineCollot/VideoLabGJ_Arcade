using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerTNT : MonoBehaviour
{
    public GameObject tnt;
    public ParticleSystem tntSFX;
    public Sprite tntTriggerOnSprite;
    bool hasTrigger = false;

    private void Start()
    {
        PlatformerManager.Instance.onMiniGameStart.AddListener(OnMiniGameStart);
    }

    private void OnMiniGameStart()
    {
        if (ArcadeManager.Instance.tntHasExploded)
            SetTriggeredState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlatformerCharacterController charController = collision.gameObject.GetComponent<PlatformerCharacterController>();
        print("Velocity " + charController.GetComponent<Rigidbody2D>().velocity.y);
        if (charController!=null && charController.GetComponent<Rigidbody2D>().velocity.y<0.2f)
        {
            Trigger();
        }
    }

    void Trigger()
    {
        if (ArcadeManager.Instance.tntHasExploded)
            return;

        ArcadeManager.Instance.tntHasExploded = true;

        SetTriggeredState();
        tntSFX.gameObject.SetActive(true);
    }

    void SetTriggeredState()
    {
        GetComponent<SpriteRenderer>().sprite = tntTriggerOnSprite;
        tnt.SetActive(false);
    }
}
