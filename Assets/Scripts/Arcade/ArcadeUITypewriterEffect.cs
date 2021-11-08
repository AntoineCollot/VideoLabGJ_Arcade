using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArcadeUITypewriterEffect : MonoBehaviour
{
    public float timerPerCharacter = 0.1f;

    private void OnEnable()
    {
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        string textContent = text.text;

        for (int i = 0; i < textContent.Length; i++)
        {
            text.maxVisibleCharacters = i + 1;
            if (textContent.Length > i && textContent[i] != ' ')
            {
                AudioManager.PlaySound(SFX.TypeWritingEffect);

            }
                yield return new WaitForSeconds(timerPerCharacter);
        }
    }
}
