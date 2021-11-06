using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadePlayer : MonoBehaviour
{
    public ArcadeBorne selectedBorne { get; private set; }

    [Header("Focus")]
    public float focusLerpTime;
    KeyboardControls keyboardControls;
    MouseLook mouseLook;
    Transform camT;
    Vector3 originalCameraLocalPosition;

    public static ArcadePlayer Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        camT = GetComponentInChildren<Camera>().transform;
        keyboardControls = GetComponentInChildren<KeyboardControls>();
        mouseLook = GetComponentInChildren<MouseLook>();
        originalCameraLocalPosition = camT.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Action") && selectedBorne!=null)
        {
            StartCoroutine(FocusOnBorne(selectedBorne));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(LeaveBorneFocus());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ArcadeBorne hitBorne = other.attachedRigidbody?.GetComponent<ArcadeBorne>();
        if (hitBorne != null)
        {
            selectedBorne = hitBorne;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ArcadeBorne hitBorne = other.attachedRigidbody?.GetComponent<ArcadeBorne>();
        if (hitBorne == selectedBorne)
        {
            selectedBorne = null;
        }
    }

    IEnumerator FocusOnBorne(ArcadeBorne borne)
    {
        Vector3 camPos = camT.position;
        Quaternion camRot = camT.rotation;

        mouseLook.enabled = false;
        keyboardControls.enabled = false;

        float t = 0;
        while(t<1)
        {
            t += Time.deltaTime / focusLerpTime;

            camT.position = Curves.QuadEaseInOut(camPos, selectedBorne.cameraFocusSpot.position, Mathf.Clamp01(t));
            camT.rotation = Quaternion.Slerp(camRot, selectedBorne.cameraFocusSpot.rotation, Mathf.Clamp01(t));
            
            yield return null;
        }
    }

    IEnumerator LeaveBorneFocus()
    {
        Vector3 camPos = camT.localPosition;

        mouseLook.enabled = true;
        keyboardControls.enabled = true;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / (focusLerpTime);

            camT.localPosition = Curves.QuadEaseInOut(camPos, originalCameraLocalPosition, Mathf.Clamp01(t));

            yield return null;
        }
    }
}
