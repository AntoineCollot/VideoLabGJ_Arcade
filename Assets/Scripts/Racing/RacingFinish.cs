using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFinish : MonoBehaviour
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
        key.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
