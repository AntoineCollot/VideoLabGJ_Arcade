using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeUIClickToPlayText : MonoBehaviour
{
    Graphic graphic;

    // Start is called before the first frame update
    void Awake()
    {
        graphic = GetComponent<Graphic>();
    }

    // Update is called once per frame
    void Update()
    {
        graphic.enabled = ArcadePlayer.Instance.selectedBorne != null;
    }
}
