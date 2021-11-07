using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShooterUIHP : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;

    Image image;
    public int id;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShooterManager.Instance.gameIsPlaying)
            return;

        if (id < ShooterManager.Instance.playerHP)
            image.sprite = onSprite;
        else
            image.sprite = offSprite;
    }
}
