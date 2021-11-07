using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour {

    Transform camT;
    new public string tag;

    public bool freeY;

    // Use this for initialization
    void Awake()
    {
        camT = GameObject.FindGameObjectWithTag(tag).transform;
    }

	// Update is called once per frame
	void LateUpdate () {
        Vector3 target = camT.position;

        if(!freeY)
        {
            target.y = transform.position.y;
        }

        transform.LookAt(target);
	}
}
