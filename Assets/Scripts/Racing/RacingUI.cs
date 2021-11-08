using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RacingUI : MonoBehaviour
{
    public GameObject raceText;
    public GameObject tooLateText;
    public TextMeshProUGUI timeText;

    // Update is called once per frame
    void Update()
    {
        raceText.SetActive(RacingManager.Instance.currentRaceTime > 0);
        timeText.gameObject.SetActive(RacingManager.Instance.currentRaceTime > 0);
        tooLateText.SetActive(RacingManager.Instance.currentRaceTime < 0 && !RacingManager.Instance.gameWon);
        if(!RacingManager.Instance.gameWon)
            timeText.text = RacingManager.Instance.currentRaceTime.ToString("N2");
    }
}
