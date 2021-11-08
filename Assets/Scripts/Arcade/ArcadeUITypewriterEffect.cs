using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArcadeUITypewriterEffect : MonoBehaviour
{
    public float delay = 0;
    public float timerPerCharacter = 0.1f;
    public bool playSounds = true;

    private void OnEnable()
    {
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        string textContent = text.text;
        text.maxVisibleCharacters = 0;

        yield return new WaitForSeconds(delay);

        for (int i = 0; i < textContent.Length; i++)
        {
            text.maxVisibleCharacters = i + 1;
            if (playSounds && textContent.Length > i && textContent[i] != ' ')
            {
                AudioManager.PlaySound(SFX.TypeWritingEffect);

            }
                yield return new WaitForSeconds(timerPerCharacter);
        }
    }
}
