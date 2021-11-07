using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerDoor : MonoBehaviour
{
    public Sprite doorOpen;

    // Update is called once per frame
    void Update()
    {
        if (ArcadeManager.Instance.keyObtained)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = doorOpen;
            enabled = false;
        }
    }
}
