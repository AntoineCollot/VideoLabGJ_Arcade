using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadePlayerSprite : MonoBehaviour
{
    new SpriteRenderer renderer;
    public Transform shooterPlayer;

    public Sprite sideSprite;
    public Sprite backSprite;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPlayer = shooterPlayer.position - transform.position;
        toPlayer.y = 0;
        toPlayer.Normalize();
        if (Vector3.Angle(transform.forward, toPlayer)>135)
        {
                renderer.flipX = false;
            renderer.sprite = backSprite;
        }
        else
        {
            renderer.sprite = sideSprite;
            if (Vector3.Cross(transform.forward, toPlayer).y > 0)
                renderer.flipX = true;
            else
                renderer.flipX = false;
        }
    }
}
