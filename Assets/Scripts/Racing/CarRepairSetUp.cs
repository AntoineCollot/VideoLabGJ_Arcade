using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRepairSetUp : MonoBehaviour
{
    public GameObject brokenCar;
    public GameObject workingCar;

    // Update is called once per frame
    void Update()
    {
        brokenCar.SetActive(!ArcadeManager.Instance.wheelObtained);
        workingCar.SetActive(ArcadeManager.Instance.wheelObtained);
    }
}
