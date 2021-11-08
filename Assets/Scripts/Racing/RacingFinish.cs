using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingFinish : MonoBehaviour
{
    public Transform key;
    public float rotationSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        key.gameObject.SetActive(!ArcadeManager.Instance.keyObtained && RacingManager.Instance.currentRaceTime>0);
        key.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        RacingManager.Instance.MiniGameWin();
    }
}
