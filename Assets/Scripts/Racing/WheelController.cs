using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    CarController carController;
    public float rotationMultiplier = 100;
    public Transform directionPivot;
    public float directionMultiplier = 20;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponentInParent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-Vector3.up * carController.inputs.y * rotationMultiplier * Time.deltaTime,Space.Self);
        if (directionPivot != null)
        {
            directionPivot.localEulerAngles = new Vector3(0,carController.inputs.x * directionMultiplier, 0);
        }
    }
}
