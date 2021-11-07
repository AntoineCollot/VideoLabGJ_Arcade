using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTNTState : MonoBehaviour
{
    public GameObject tntTriggerOn;
    public GameObject tntTriggerOff;
    public GameObject tnt;
    public GameObject glass;
    public GameObject glassBroken;

    // Start is called before the first frame update
    void Start()
    {
        ShooterManager.Instance.onMiniGameStart.AddListener(OnMiniGameStart);
    }

    void OnMiniGameStart()
    {
        if (ArcadeManager.Instance.tntHasExploded)
            SetTriggeredState();
    }

    void SetTriggeredState()
    {
        tntTriggerOn.SetActive(true);
        glassBroken.SetActive(true);
        tntTriggerOff.SetActive(false);
        tnt.SetActive(false);
        glass.SetActive(false);
    }
}
